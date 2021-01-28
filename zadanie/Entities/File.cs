using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace zadanie.Entities
{
    public class File
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string Name { get; set; }

        public Form Form { get; set; }
        public long? FormId { get; set; }

        public File Parent { get; set; }
        public List<File> Subfiles { get; set; }

        public File()
        {
            Subfiles = new List<File>();
        }
    }
}
