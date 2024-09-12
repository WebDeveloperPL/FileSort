using FileSort.Core;
using FileSort.Domain.Sorting.BigFilesSort.Processors;

namespace FileSort.Domain.Sorting.BigFilesSort
{
    public class BigFilesSortingEngine : ISortEngine
    {
        private readonly IBatchFileInfoProvider _batchFileInfoProvider;
        private readonly ICreatingBatchFileProcessor _creatingBatchFileProcessor;
        private readonly IDataMergeProcessor _dataMergeProcessor;
        private readonly ISortFileProcessor _sortFileProcessor;

        public BigFilesSortingEngine(IBatchFileInfoProvider batchFileInfoProvider, ICreatingBatchFileProcessor creatingBatchFileProcessor, IDataMergeProcessor dataMergeProcessor, ISortFileProcessor sortFileProcessor)
        {
            _batchFileInfoProvider = batchFileInfoProvider;
            _creatingBatchFileProcessor = creatingBatchFileProcessor;
            _dataMergeProcessor = dataMergeProcessor;
            _sortFileProcessor = sortFileProcessor;
        }

        public async Task Start(SortEngineConfiguration configuration)
        {
            var numberOfBatchFiles = _batchFileInfoProvider.GetNumberOfBatchFiles(configuration);
            await _creatingBatchFileProcessor.Start(configuration, numberOfBatchFiles, 30000);
            DisplaySourceAndBatchComparision(configuration);
            await _sortFileProcessor.Start(configuration);
            await _dataMergeProcessor.Start(configuration);
        }

        public void DisplaySourceAndBatchComparision(SortEngineConfiguration configuration)
        {
            var batchFiles = Directory.GetFiles(configuration.WorkDir, $"{BigFileSortConsts.BatchFileName}*");
            var size = batchFiles.Select(x => new FileInfo(x).Length).Sum();
            var sourceSize = new FileInfo(configuration.SourceFilePath).Length;
            if (size == sourceSize)
            {
                LoggerWrapper.Success($"Size of batch files is correct. {size / 1024} kilobytes");
            }
            else
            {
                LoggerWrapper.Error($"Size of batch files is incorrect: {size / 1024} KB but should be {sourceSize / 1024} KB");
            }
            
        }
    }
}
