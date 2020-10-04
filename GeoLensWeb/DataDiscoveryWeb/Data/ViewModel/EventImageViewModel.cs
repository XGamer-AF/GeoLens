using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Data.ViewModel
{
    public class EventImageViewModel
    {
        public int eventImageID { get; set; }
        public Nullable<int> eventID { get; set; }
        public Nullable<int> statusID { get; set; }
    
        public EventInfoViewModel EventInfo { get; set; }
    }
}
