using System.Collections.Concurrent;
using System.Text;
using FileSort.Core;

namespace FileSort.Domain.Sorting.BigFilesSort.Processors
{
    public interface ICreatingBatchFileProcessor
    {
        Task Start(SortEngineConfiguration configuration, int numberOfFiles, int readLinesBuffer);
    }

    public class CreatingBatchFileProcessor : ICreatingBatchFileProcessor
    {
        public async Task Start(SortEngineConfiguration configuration, int numberOfFiles, int readLinesBuffer)
        {
            LoggerWrapper.Debug("Creating batch files.");
            var batchFileNumber = 1;
            long currentSizeInBytes = 0;
            int enqueueCount = 0;

            List<Task> tasks = new List<Task>();
            using (var reader = new StreamReader(configuration.SourceFilePath, Encoding.UTF8, true, 10 * 1024 * 1024))
            {
                var limitPerFile = readLinesBuffer / numberOfFiles; // keep similar number of rows per file
                var buffer = new ConcurrentQueue<string>();
                while (true)
                {
                    var line = await reader.ReadLineAsync();
                    if (line != null)
                    {
                        buffer.Enqueue(line);
                        enqueueCount++;

                        if (buffer.Count % readLinesBuffer == 0)
                        {
                            for (int i = 1; i <= numberOfFiles; i++)
                            {
                                var fileName = Path.Combine(configuration.WorkDir, $"{BigFileSortConsts.BatchFileName}_{i}.txt");
                                tasks.Add(Task.Run(() => WriteLinesToFile(buffer, fileName, limitPerFile)));
                            }
                            await Task.WhenAll(tasks);
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= numberOfFiles; i++)
                        {
                            var fileName = Path.Combine(configuration.WorkDir, $"{BigFileSortConsts.BatchFileName}_{i}.txt");
                            tasks.Add(Task.Run(() => WriteLinesToFile(buffer, fileName, limitPerFile)));
                        }
                        await Task.WhenAll(tasks);

                        break;
                    }
                }
            }
            LoggerWrapper.Debug("Batch files created");
        }

        public void WriteLinesToFile(ConcurrentQueue<string> queue, string outputFile, int limit)
        {

            var processed = 0;
            using (var fs = new FileStream(outputFile, FileMode.Append, FileAccess.Write, FileShare.None, 5 * 1024 * 1024))
            using (var writer = new StreamWriter(fs))
            {
                string data;
                while (processed <= limit)
                {
                    if (queue.TryDequeue(out data))
                    {
                        writer.WriteLine(data);
                        processed++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}
