namespace FileSort.Domain.Sorting
{
    public class SortEngineConfiguration
    {
        public string SourceFilePath { get; set; }
        public string ResultFilePath { get; set; }
        public string WorkDir { get; set; }
        public int TempFileInMemoryLimit { get; set; }
    }
}
