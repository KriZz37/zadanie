using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace zadanie.ViewModels
{
    public class MoveFolderViewModel
    {
        public long FolderId { get; set; }
        public long NewParentId { get; set; }
    }
}
