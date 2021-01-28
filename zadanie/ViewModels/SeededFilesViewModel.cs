using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zadanie.ViewModels
{
    public class SeededFilesViewModel
    {
        public IEnumerable<FileViewModel> Files { get; set; }
        public long? Seed { get; set; }
    }
}
