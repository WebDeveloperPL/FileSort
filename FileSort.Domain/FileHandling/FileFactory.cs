
namespace FileSort.Domain.FileHandling
{
    public interface IFileFactory
    {
        Task WriteAsync(string path, List<string> data);
    }

    public class FileFactory : IFileFactory
    {
        public async Task WriteAsync(string path, List<string> data)
        {
            if (!System.IO.File.Exists(path))
            {
                using (var stream = System.IO.File.Create(path))
                {

                }
            }

            await System.IO.File.AppendAllLinesAsync(path, data.ToArray());
        }
    }
}
