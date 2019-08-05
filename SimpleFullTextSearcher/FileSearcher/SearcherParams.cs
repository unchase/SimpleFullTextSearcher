using System.Text;

namespace SimpleFullTextSearcher.FileSearcher
{
    public sealed class SearcherParams
    {
        #region Constructor
        public SearcherParams(string searchDirectories, bool includeSubDirectoriesChecked, string fileName,
            bool containingChecked, string containingText, Encoding encoding, bool searchInZipArchive, bool searchInImages)
        {
            SearchDir = searchDirectories;
            IncludeSubDirsChecked = includeSubDirectoriesChecked;
            FileName = fileName;
            ContainingChecked = containingChecked;
            ContainingText = containingText;
            Encoding = encoding;
            SearchInZipArchive = searchInZipArchive;
            SearchInImages = searchInImages;
        }
        #endregion

        #region Public Properties
        public string SearchDir { get; }

        public bool IncludeSubDirsChecked { get; }

        public string FileName { get; }

        public bool ContainingChecked { get; }

        public string ContainingText { get; }

        public Encoding Encoding { get; set; }

        public bool SearchInZipArchive { get; set; }

        public bool SearchInImages { get; set; }

        #endregion
    }
}
