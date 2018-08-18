using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using SimpleFullTextSearcher.FileSearcher;
using SimpleFullTextSearcher.FileSearcher.EventArgs;
using SimpleFullTextSearcher.FileSearcher.Helpers;

namespace SimpleFullTextSearcher
{
    
    public partial class FormMain : Form
    {
        #region Settings
        /// <summary>
        /// Класс для хранения критериев поиска
        /// </summary>
        [DataContract]
        public class Settings
        {
            #region Properties

            /// <summary>
            /// Начальная директория поиска
            /// </summary>
            [DataMember]
            public string InitialDirectory { get; set; }
            /// <summary>
            /// Шаблон имени файла
            /// </summary>
            [DataMember]
            public string FileNamePattern { get; set; }
            /// <summary>
            /// Искомый текст
            /// </summary>
            [DataMember]
            public string SearchText { get; set; }

            #endregion

            #region Constructors

            public Settings()
            {
                InitialDirectory = "";
                FileNamePattern = "";
                SearchText = "";
            }

            public Settings(string initialDirectory, string fileNamePattern, string searchText)
            {
                InitialDirectory = initialDirectory;
                FileNamePattern = fileNamePattern;
                SearchText = searchText;
            }

            #endregion

            #region SearchCriteria Save/Load
            public static void SaveSearchCriteriaToJson(Settings contract)
            {
                try
                {
                    File.WriteAllText(SearchCriteriaFilePath, new JavaScriptSerializer().Serialize(contract));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Не удалось сохранить критерии поиска в файл '" + SearchCriteriaFilePath + "'.\nПроизошла ошибка: " + ex.Message,
                        "Сохранение критериев поиска", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            public static Settings LoadSearchCriteriaFromJson()
            {
                try
                {
                    if(File.Exists(SearchCriteriaFilePath))
                        return (Settings)new JavaScriptSerializer().Deserialize(File.ReadAllText(SearchCriteriaFilePath), typeof(Settings));
                    else
                        return default(Settings);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Не удалось загрузить критерии поиска из файла '" + SearchCriteriaFilePath + "'.\nПроизошла ошибка: " +
                        ex.Message, "Загрузка критериев поиска", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return default(Settings);
                }
            }
            #endregion
        }
        #endregion

        #region Variables
        /// <summary>
        /// Полный путь к json-файлу, содержащему сохраненные критерии поиска
        /// </summary>
        public static readonly string SearchCriteriaFilePath = Path.Combine(Environment.CurrentDirectory, "settings.json");

        public Settings CurrentSettings { get; set; }

        private Stopwatch _stopWatch;

        private bool _formClosing = false;

        private bool _searchOnPause = false;

        #endregion

        #region Synchronizing Delegates

        private delegate void FoundInfoSyncHandler(FoundInfoEventArgs e);

        private FoundInfoSyncHandler _foundInfo;

        private delegate void SearchInfoSyncHandler(SearchInfoEventArgs e);

        private SearchInfoSyncHandler _searchInfo;

        private delegate void ThreadEndedSyncHandler(ThreadEndedEventArgs e);

        private ThreadEndedSyncHandler _threadEnded;

        #endregion

        public FormMain()
        {
            InitializeComponent();

            // загружаем параметры поиска из файла
            CurrentSettings = Settings.LoadSearchCriteriaFromJson() ?? new Settings();
            sfsInitialDirectoryTextBox.Text = CurrentSettings.InitialDirectory;
            sfsFileNamePatternTextBox.Text = CurrentSettings.FileNamePattern;
            sfsSearchTextTextBox.Text = CurrentSettings.SearchText;
        }

        #region FormMain Events

        private void FormMain_Load(object sender, EventArgs e)
        {
            _stopWatch = new Stopwatch();

            // подписываемся на собственные события
            _foundInfo += this_FoundInfo;
            _searchInfo += this_SearchInfo;
            _threadEnded += this_ThreadEnded;

            // подписываемся на события Searcher'а
            Searcher.FoundInfo += Searcher_FoundInfo;
            Searcher.SearchInfo += Searcher_SearchInfo;
            Searcher.ThreadEnded += Searcher_ThreadEnded;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // делаем так, чтобы события из Searcher игнорировались
            _formClosing = true;

            // если поиск стоит на поузе, то снимаем паузу
            if (_searchOnPause)
            {
                _searchOnPause = false;
                Searcher.Pause();
            }

            // остановка потока поиска, если он запущен
            Searcher.Stop();

            // сохраняем критерии поиска перед выходом
            Settings.SaveSearchCriteriaToJson(new Settings(sfsInitialDirectoryTextBox.Text,
                sfsFileNamePatternTextBox.Text, sfsSearchTextTextBox.Text));
        }

        #endregion

        #region Searcher and own events

        private void Searcher_FoundInfo(FoundInfoEventArgs e)
        {
            if (!_formClosing)
            {
                Invoke(_foundInfo, e);
            }
        }

        private void this_FoundInfo(FoundInfoEventArgs e)
        {
            sfsToolStripStatusLabel.Text = @"Найдено сопадение в файле: " + e.Info.FullName;

            var initialDirectorySplit = e.Info.FullName.Split('\\');
            AddFileInfoIntoTreeView(initialDirectorySplit.ToList(), sfsSearchResultsTreeView.Nodes, e.Info.FullName);
        }

        private void Searcher_SearchInfo(SearchInfoEventArgs e)
        {
            if (!_formClosing)
            {
                Invoke(_searchInfo, e);
            }
        }

        private void this_SearchInfo(SearchInfoEventArgs e)
        {
            sfsToolStripStatusLabel.Text = @"Просмотрено файлов - " + e.Count + @". Проверяется в: " + e.Info.FullName;
        }

        private void Searcher_ThreadEnded(ThreadEndedEventArgs e)
        {
            if (!_formClosing)
            {
                Invoke(_threadEnded, e);
            }
        }

        private void this_ThreadEnded(ThreadEndedEventArgs e)
        {
            // делаем активными отключенные Controls
            EnableControls();

            // выводим затраченное время поиска
            if (_stopWatch.IsRunning)
            {
                _stopWatch.Stop();

                sfsToolStripStatusLabel.Text = @"Всего найдено совпадений - " + e.Count + @". Затраченное время: " + MillisecondsToTimeStringConverter(_stopWatch.ElapsedMilliseconds);

                _stopWatch.Reset();
            }

            // показать текст ошибки, если необходимо
            if (!e.Success)
            {
                MessageBox.Show(@"Во время поиска произошла ошибка: " + e.ErrorMsg, @"Ошибка поиска",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion

        #region Controls events

        private void sfsInitialDirectorySelectButton_Click(object sender, EventArgs e)
        {
            var dlg = new FolderBrowserDialog
            {
                SelectedPath = sfsInitialDirectoryTextBox.Text,
                Description = @"Выберите начальную директорию поиска"
            };

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                sfsInitialDirectoryTextBox.Text = dlg.SelectedPath;
            }
        }

        private void sfsSearchStopButton_Click(object sender, EventArgs e)
        {
            Searcher.Stop();
        }

        
        private void sfsSearchStartButton_Click(object sender, EventArgs e)
        {
            if (CheckInputFields())
            {
                sfsSearchResultsTreeView.Nodes.Clear();

                var pars = new SearcherParams(sfsInitialDirectoryTextBox.Text.Trim(), true, sfsFileNamePatternTextBox.Text,
                    true, sfsSearchTextTextBox.Text.Trim(), Encoding.ASCII);

                if (Searcher.Start(pars))
                {
                    _stopWatch.Restart();
                    DisableControls();
                }
                else
                {
                    MessageBox.Show(@"Поиск уже запущен. Остановите поиск или дождитесь окончания процесса.",
                        @"Запуск поиска", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void sfsSearchPauseButton_Click(object sender, EventArgs e)
        {
            _searchOnPause = !_searchOnPause;
            if (_searchOnPause)
            {
                sfsSearchPauseButton.Text = @"Продолжить";
                _stopWatch.Stop();
                sfsSearchStopButton.Enabled = false;
            }
            else
            {
                sfsSearchPauseButton.Text = @"Пауза";
                _stopWatch.Start();
                sfsSearchStopButton.Enabled = true; 
            }
            Searcher.Pause();
        }

        private void sfsInitialDirectoryClearButton_Click(object sender, EventArgs e)
        {
            sfsInitialDirectoryTextBox.Text = "";
        }

        private void sfsFileNamePatternClearButton_Click(object sender, EventArgs e)
        {
            sfsFileNamePatternTextBox.Text = "";
        }

        private void sfsSearchTextClearButton_Click(object sender, EventArgs e)
        {
            sfsSearchTextTextBox.Text = "";
        }

        private void sfsAboutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Программа предназначена для полнотекстового поиска в файлах с задаанными критериями поиска.\nАвтор: unchase (https://github.com/unchase), август 2018 г.",
                "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Private methods
        private bool CheckInputFields()
        {
            if (string.IsNullOrEmpty(sfsInitialDirectoryTextBox.Text) ||
                !(new DirectoryInfo(sfsInitialDirectoryTextBox.Text).Exists))
            {
                MessageBox.Show(
                    "Начальная директория поиска не выбрана или не существует!\nВыберите ее и запустите поиск снова.",
                    "Провека входных данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(sfsFileNamePatternTextBox.Text))
            {
                MessageBox.Show(
                    @"Шаблон имени файла не должен быть пуст.",
                    @"Провека входных данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(sfsSearchTextTextBox.Text))
            {
                MessageBox.Show(
                    @"Искомый текст не должен быть пуст.",
                    @"Провека входных данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void AddFileInfoIntoTreeView(List<string> initialDirectoryPartialPaths, TreeNodeCollection treeNodeCollection, string fileFullName)
        {
            if (!initialDirectoryPartialPaths.Any()) return;

            if (!treeNodeCollection.ContainsKey(initialDirectoryPartialPaths[0]))
            {
                if (initialDirectoryPartialPaths.Count > 1)
                    treeNodeCollection.Add(initialDirectoryPartialPaths[0], initialDirectoryPartialPaths[0], 0, 0);
                else
                {
                    var fileIcon = IconHelper.GetIcon(fileFullName);
                    //var fileIcon = Icon.ExtractAssociatedIcon(fileFullName);
                    var imageIndex = sfsTreeViewImageList.Images.IndexOfKey(new FileInfo(fileFullName).Extension);
                    if (imageIndex < 0)
                    {
                        sfsTreeViewImageList.Images.Add(new FileInfo(fileFullName).Extension, fileIcon);
                        treeNodeCollection.Add(initialDirectoryPartialPaths[0], initialDirectoryPartialPaths[0],
                            sfsTreeViewImageList.Images.Count - 1, sfsTreeViewImageList.Images.Count - 1);
                    }
                    else
                    {
                        treeNodeCollection.Add(initialDirectoryPartialPaths[0], initialDirectoryPartialPaths[0], imageIndex, imageIndex);
                    }
                }
                sfsSearchResultsTreeView.ExpandAll();
                sfsSearchResultsTreeView.Refresh();
            }

            var index = treeNodeCollection.IndexOfKey(initialDirectoryPartialPaths[0]);
            if (index >= 0)
                AddFileInfoIntoTreeView(initialDirectoryPartialPaths.Skip(1).ToList(),
                    treeNodeCollection[index].Nodes, fileFullName);
        }

        private void EnableControls()
        {
            sfsInitialDirectorySelectButton.Enabled = true;
            sfsInitialDirectoryClearButton.Enabled = true;
            sfsFileNamePatternClearButton.Enabled = true;
            sfsSearchTextClearButton.Enabled = true;
            sfsSearchStartButton.Enabled = true;
            sfsFileNamePatternTextBox.ReadOnly = false;
            sfsSearchTextTextBox.ReadOnly = false;
            sfsSearchPauseButton.Enabled = false;
            sfsSearchStopButton.Enabled = false;
        }

        private void DisableControls()
        {
            sfsInitialDirectorySelectButton.Enabled = false;
            sfsInitialDirectoryClearButton.Enabled = false;
            sfsFileNamePatternClearButton.Enabled = false;
            sfsSearchTextClearButton.Enabled = false;
            sfsSearchStartButton.Enabled = false;
            sfsFileNamePatternTextBox.ReadOnly = true;
            sfsSearchTextTextBox.ReadOnly = true;
            sfsSearchPauseButton.Enabled = true;
            sfsSearchStopButton.Enabled = true;
        }

        private string MillisecondsToTimeStringConverter(long milliseconds)
        {
            var ts = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(milliseconds));
            return ts.Days + " д, " + ts.Hours + " ч, " + ts.Minutes + " м, " + ts.Seconds + " с, " + ts.Milliseconds +
                   " мс";
        }

        #endregion
    }
}
