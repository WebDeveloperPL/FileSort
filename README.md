FileSort.GneratorApp - application used for generating new source file. You need to provide location of file + size of expected file in MegaBytes.

FileSort.ConsoleApp - application which do sorting of file. You need to provide correct configuration object in Program.cs

var config = new SortEngineConfiguration
{
    ResultFilePath = @"F:\\FileSort\\output_100GB.txt",
    SourceFilePath = @"F:\\FileSort\\source_100GB.txt",
    WorkDir = @"F:\\FileSort",
    TempFileInMemoryLimit = 110 * 1024 * 1024 // 100 MB
};

You need to defines source and output files. Working directory is a directory where TMP files are created. 
TempFileInMemoryLimit - size of single file used to split bigger files - by default keep it as 110 MB. Configuration is adjusted to get result < 60 seconds for 1 GB file. 

For file like 100 GB it should also work correctly. Sorting of single batch file is done 10 files in parrallel - this value is hardcoded. It makes application secure, to not overload memory. This value could be adjusted and calculated based on file size, but I decided to not spend more time on this issue. 
