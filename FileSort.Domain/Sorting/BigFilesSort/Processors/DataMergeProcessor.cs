using System.Collections.Concurrent;
using System.Diagnostics;
using FileSort.Core;

namespace FileSort.Domain.Sorting.BigFilesSort.Processors
{
    public interface IDataMergeProcessor
    {
        Task Start(SortEngineConfiguration configuration);
    }

    public class DataMergeProcessor : IDataMergeProcessor
    {
        private static ConcurrentQueue<string> Queue = new ConcurrentQueue<string>();


        public async Task Start(SortEngineConfiguration configuration)
        {
            LoggerWrapper.Debug("Started merge process");

            var readers = new List<DataReader>();

            using (var s = File.Create(configuration.ResultFilePath))
            {

            }

            var batchFiles = Directory.GetFiles(configuration.WorkDir, $"{BigFileSortConsts.BatchFileName}*");
            foreach (var batch in batchFiles)
            {
                readers.Add(new DataReader(batch));
            }

            DataReader minReader = null;
            while (true)
            {
                bool end = false;
                while (end == false)
                {
                    var w = new Stopwatch();
                    w.Start();
                    minReader = readers.Where(x => x.CurrentItem != null).OrderBy(x => x.CurrentItem.SortingText).ThenBy(x => x.CurrentItem.Number).FirstOrDefault();
                    w.Stop();

                    if (minReader?.CurrentItem != null)
                    {
                        Queue.Enqueue(minReader.CurrentItem.GetFormattedLine());
                        await minReader.Next();
                        if (Queue.Count > 10000)
                        {
                            end = true;
                        }
                    }
                    else
                    {
                        end = true;
                    }

                }

                await WriteToOutput(configuration.ResultFilePath);
                if (minReader?.CurrentItem == null)
                {
                    break;
                }
            }

            readers.ForEach(x=>x.DisposeAsync());
            LoggerWrapper.Debug("Finished merge process");
        }

        public async Task WriteToOutput(string path)
        {
            string data;
            using (var fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.None, 10 * 1024 * 1024))
            using (var writer = new StreamWriter(fs))
            {
                while (true)
                {
                    if (Queue.TryDequeue(out data))
                    {
                        await writer.WriteLineAsync(data);
                    }

                    if (Queue.IsEmpty)
                    {
                        break;
                    }
                }
            }

        }
    }

}
