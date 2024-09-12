using FileSort.Core;

namespace FileSort.Domain.Sorting.BigFilesSort.Processors
{
    public interface ISortFileProcessor
    {
        Task Start(SortEngineConfiguration configuration);
    }

    public class SortFileProcessor : ISortFileProcessor
    {
        public async Task Start(SortEngineConfiguration configuration)
        {
            var take = 10; // should be calculated based on size of single file - hardcoded safe value for 1 GB  file
            LoggerWrapper.Debug($"Starting sorting files. {take} files at same time");
            var batchFiles = Directory.GetFiles(configuration.WorkDir, $"{BigFileSortConsts.BatchFileName}*");
            int iteration = 1;
            while (true)
            {
                var tasks = new List<Task>();

                var batches = batchFiles.Skip((iteration - 1) * take).Take(take).ToList();
                if (!batches.Any())
                {
                    break;
                }

                foreach (var file in batches)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        var lines = File.ReadAllLines(file);
                        var sorted = lines.Select(x => FileRecord.FileRecord.Parse(x)).OrderBy(x => x.SortingText)
                            .ThenBy(x => x.Number).Select(x => x.GetFormattedLine()).ToArray();
                        File.WriteAllLines(file, sorted);
                    }));
                }

                iteration++;
                await Task.WhenAll(tasks);
                LoggerWrapper.Debug($"Finished sorting {tasks.Count} files.");
            }

            
        }

    }
}
