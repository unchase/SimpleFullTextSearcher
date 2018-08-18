namespace SimpleFullTextSearcher.FileSearcher.EventArgs
{
    public class ThreadEndedEventArgs
    {
        public ThreadEndedEventArgs(bool success, int count, string errorMsg)
        {
            Success = success;
            Count = count;
            ErrorMsg = errorMsg;
        }

        public bool Success { get; }
        public int Count { get; }

        public string ErrorMsg { get; }
    }
}
