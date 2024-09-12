using FileSort.Core;

namespace FileSort.Domain.Sorting.BigFilesSort.Processors
{
    public interface ICleaningProcessor
    {
        Task Start(SortEngineConfiguration configuration);
    }

    public class CleaningProcessor : ICleaningProcessor
    {
        public async Task Start(SortEngineConfiguration configuration)
        {
            LoggerWrapper.Debug($"Removing temporary files");
            var batchFiles = Directory.GetFiles(configuration.WorkDir, $"{BigFileSortConsts.BatchFileName}*");
            var tasks = new List<Task>();
            foreach (var file in batchFiles)
            {
                tasks.Add(Task.Run(() =>
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception e)
                    {
                        LoggerWrapper.Error($"Failed to remove file: {file}");
                    }
                }));
            }
            await Task.WhenAll(tasks);
            LoggerWrapper.Debug($"Temporary files removed.");
        }

    }
}
