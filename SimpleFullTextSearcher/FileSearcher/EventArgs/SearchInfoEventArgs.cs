using System.IO;

namespace SimpleFullTextSearcher.FileSearcher.EventArgs
{
    public class SearchInfoEventArgs
    {
        public SearchInfoEventArgs(FileSystemInfo info, int count)
        {
            Info = info;
            Count = count;
        }

        public FileSystemInfo Info { get; }
        public int Count { get; }
    }
}
