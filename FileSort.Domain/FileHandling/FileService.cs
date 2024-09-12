
using FileSort.Domain.FileRecord;

namespace FileSort.Domain.FileHandling
{
    public interface IFileService
    {
        Task CreateFileWithRandomData(string fileName, long fileSizeInBytes, int batchSize = 1000);
    }

    public class FileService : IFileService
    {
        private readonly IFileFactory _fileFactory;
        private readonly IFileRecordFactory _fileRecordFactory;

        public FileService(IFileFactory fileFactory, IFileRecordFactory fileRecordFactory)
        {
            _fileFactory = fileFactory;
            _fileRecordFactory = fileRecordFactory;
        }

        public async Task CreateFileWithRandomData(string fileName, long fileSizeInBytes, int batchSize = 1000)
        {
            
            long currentFileSize = 0;

            while (currentFileSize <= fileSizeInBytes)
            {
                var data = _fileRecordFactory.Create(batchSize);
                await _fileFactory.WriteAsync(fileName, data.Select(x=>x.GetFormattedLine()).ToList());
                currentFileSize = GetFileSize(fileName);
            }
        }

        public long GetFileSize(string path)
        {
            if (System.IO.File.Exists(path))
            {
                var size = new FileInfo(path).Length;
                return size;
            }

            return 0;
        }
    }
}
