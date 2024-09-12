using FileSort.Core;
using FileSort.Domain.Consts;
using FileSort.Domain.FileHandling;
using FileSort.Domain.FileRecord;
using Microsoft.Extensions.DependencyInjection;

LoggerWrapper.Debug("Staring application");
LoggerWrapper.Debug("Configuring DI");

var serviceProvider = new ServiceCollection()
    .AddSingleton<IFileService, FileService>()
    .AddSingleton<IFileFactory, FileFactory>()
    .AddSingleton<IFileRecordFactory, FileRecordFactory>()
    .BuildServiceProvider();

long fileSizeInBytes = 2 * AppConsts.MegaByteInBytes; // 1 MB

var fileService = serviceProvider.GetService<IFileService>();
await fileService.CreateFileWithRandomData(@"F:\\FileSort\\source_2MB.txt", fileSizeInBytes, 1);