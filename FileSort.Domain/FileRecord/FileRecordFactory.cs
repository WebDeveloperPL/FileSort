using System.Text;

namespace FileSort.Domain.FileRecord
{
    public interface IFileRecordFactory
    {
        List<FileRecord> Create(int number);
    }

    public class FileRecordFactory : IFileRecordFactory
    {
        private Random _random;
        public FileRecordFactory()
        {
            _random = new Random(DateTime.Now.Millisecond);
        }

        public List<FileRecord> Create(int number)
        {
            var output = new List<FileRecord>();
            for (int i = 0; i < number; i++)
            {
                output.Add(new FileRecord
                    {
                        Content = GetRandomContent(),
                        Number = GetRandomNumber(),
                    });
            }

            return output;
        }

        public int GetRandomNumber()
        {
            return _random.Next(1, int.MaxValue);
        }

        public string GetRandomContent()
        {
            var numberOfWords = _random.Next(2, 50);
            List<string> words = new List<string>();
            for (int i = 0; i < numberOfWords; i++)
            {
                var numberOfLetters = _random.Next(3, 5);
                words.Add(GetRandomWord(numberOfLetters));
            }

            return string.Join(' ', words).Trim();
        }

        public string GetRandomWord(int letters)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < letters; i++)
            {
                sb.Append(GetRandomLetter());
            }

            return sb.ToString();
        }

        public char GetRandomLetter()
        {
            var randValue = _random.Next(0, 26);
            var letter = Convert.ToChar(randValue + 65);
            return letter;
        }
    }
}
