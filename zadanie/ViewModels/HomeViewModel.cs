using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace zadanie.ViewModels
{
    public class HomeViewModel
    {
        public SeededFilesViewModel SeededFiles { get; set; }
        public NewFolderViewModel NewFolder { get; set; }
        public NewFormViewModel NewForm { get; set; }
        public MoveFolderViewModel MoveFolder { get; set; }
        public ChangeFileNameViewModel ChangeName { get; set; }
        public long FileIdToDelete { get; set; }
    }
}
