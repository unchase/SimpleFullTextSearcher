using System.Text;
using iTextSharp.xmp.impl;

namespace SimpleFullTextSearcher.FileSearcher
{
    public class SearcherParams
    {
        #region Constructor
        public SearcherParams(string searchDirectories, bool includeSubDirectoriesChecked, string fileName,
            bool containingChecked, string containingText, Encoding encoding)
        {
            SearchDir = searchDirectories;
            IncludeSubDirsChecked = includeSubDirectoriesChecked;
            FileName = fileName;
            ContainingChecked = containingChecked;
            ContainingText = containingText;
            Encoding = encoding;
        }
        #endregion

        #region Public Properties
        public string SearchDir { get; }

        public bool IncludeSubDirsChecked { get; }

        public string FileName { get; }

        public bool ContainingChecked { get; }

        public string ContainingText { get; }

        public Encoding Encoding { get; set; }

        #endregion
    }
}
