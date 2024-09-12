using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSort.Domain.Sorting
{
    public interface ISortEngine
    {
        Task Start(SortEngineConfiguration configuration);
    }
}
