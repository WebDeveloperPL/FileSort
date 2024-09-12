namespace FileSort.Domain.FileRecord
{
    public class FileRecord /*: IComparable<FileRecord>*/
    {
        public string Content { get; set; }
        public int Number { get; set; }
        public string SortingText { get; set; }

        public string GetFormattedLine()
        {
            return String.Format($"{Number}. {Content}");
        }

        //public int CompareTo(FileRecord? obj)
        //{
        //    if (obj == null) return 1;

        //    // Compare by Id first
        //    //int idComparison = Id.CompareTo(other.Id);
        //    //if (idComparison != 0) return idComparison;

        //    // If Ids are the same, compare by Name
        //    var result =  string.Compare(SortingText, obj.SortingText);

        //    if (result == 0)
        //    {
        //        var compareNumberResult = Number.CompareTo(obj.Number);
        //        return compareNumberResult;
        //    }
        //    else
        //    {
        //        return result;
        //    }
        //}

        public static FileRecord Parse(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var splitted = value.Split('.');
                var output =  new FileRecord
                {
                    Number = int.Parse(splitted[0]),
                    Content = splitted[1].Trim(),
                };
                output.SortingText = output.Content.Split(" ").FirstOrDefault();
                return output;
            }

            return null;
        }
    }
}
