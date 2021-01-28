using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zadanie.ViewModels
{
    public class FileViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public long? FormId { get; set; }
        public bool IsLast { get; set; }
    }
}
