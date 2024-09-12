
using System.Diagnostics;
using FileSort.Core;
using FileSort.Domain.Sorting;
using FileSort.Domain.Sorting.BigFilesSort;
using FileSort.Domain.Sorting.BigFilesSort.Processors;
using Microsoft.Extensions.DependencyInjection;

LoggerWrapper.Debug("Staring application");
LoggerWrapper.Debug("Configuring DI");

var serviceProvider = new ServiceCollection()
    .AddSingleton<ISortEngine, BigFilesSortingEngine>()
    .AddSingleton<IBatchFileInfoProvider, BatchFileInfoProvider>()
    .AddSingleton<ICreatingBatchFileProcessor, CreatingBatchFileProcessor>()
    .AddSingleton<IDataMergeProcessor, DataMergeProcessor>()
    .AddSingleton<ISortFileProcessor, SortFileProcessor>()
    .AddSingleton<ICleaningProcessor, CleaningProcessor>()
    .BuildServiceProvider();



//var config = new SortEngineConfiguration
//{
//    ResultFilePath = @"F:\\FileSort\\output_1MB.txt",
//    SourceFilePath = @"F:\\FileSort\\source_1MB.txt",
//    WorkDir = @"F:\\FileSort",
//    TempFileInMemoryLimit = 100 * 1024 // 100 KB
//};


var config = new SortEngineConfiguration
{
    ResultFilePath = @"F:\\FileSort\\output_100GB.txt",
    SourceFilePath = @"F:\\FileSort\\source_100GB.txt",
    WorkDir = @"F:\\FileSort",
    TempFileInMemoryLimit = 110 * 1024 * 1024 // 100 MB
};

try
{
    var watch = new Stopwatch();
    watch.Start();
    var sortEngine = serviceProvider.GetService<ISortEngine>();
    await sortEngine.Start(config);
    watch.Stop();
    LoggerWrapper.Success($"Finished in: {watch.Elapsed}");
    await serviceProvider.GetService<ICleaningProcessor>().Start(config);
}
catch (Exception e)
{
    LoggerWrapper.Error($"Something went wrong: {e.Message}");
}

Console.ReadKey();