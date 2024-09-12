

namespace FileSort.Domain.Sorting.BigFilesSort
{
    public interface IBatchFileInfoProvider
    {
        int GetNumberOfBatchFiles(SortEngineConfiguration configuration);
    }

    public class BatchFileInfoProvider : IBatchFileInfoProvider
    {
        
        public int GetNumberOfBatchFiles(SortEngineConfiguration configuration)
        {
            if (File.Exists(configuration.SourceFilePath))
            {
                var fileSize = new FileInfo(configuration.SourceFilePath).Length;
                if (fileSize > configuration.TempFileInMemoryLimit)
                {
                    var result = (int)Math.Ceiling((double)(fileSize) / (double)configuration.TempFileInMemoryLimit);
                    return result;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 1;
            }
        }
    }
}
