using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace zadanie.ViewModels
{
    public class EditFormViewModel
    {
        public long FormId { get; set; }
        public string FileName { get; set; }

        [DisplayName("Tytuł")]
        public string Title { get; set; }
        [DisplayName("Treść")]
        public string Text { get; set; }
    }
}
