namespace FileSort.Domain.Sorting.BigFilesSort
{
    public class DataReader
    {
        private StreamReader _reader;
        private FileRecord.FileRecord _currentItem { get; set; }

        public FileRecord.FileRecord CurrentItem
        {
            get
            {
                return _currentItem;
            }
        }

        public DataReader(string path)
        {
            _reader = new StreamReader(path);
            var line = _reader.ReadLine();
            if (line != null)
            {
                _currentItem = FileRecord.FileRecord.Parse(line);
            }
            else
            {
                _currentItem = null;
            }
        }

        public async Task Next()
        {
            var line = _reader.ReadLine();
            if (line != null)
            {
                _currentItem = FileRecord.FileRecord.Parse(line);
            }
            else
            {
                _currentItem = null;
                line = null;
            }
        }


        public async Task DisposeAsync()
        {
            try
            {
                _reader.Close();
                _reader.Dispose();
            }
            catch (Exception e)
            {

            }
            
        }
    }
}
