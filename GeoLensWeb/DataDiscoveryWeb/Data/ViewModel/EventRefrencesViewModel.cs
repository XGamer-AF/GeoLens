using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Data.ViewModel
{
    public class EventRefrencesViewModel
    {
        public int eventRefrencesID { get; set; }
        public Nullable<int> orderID { get; set; }
        public string eventURL { get; set; }
        public Nullable<int> eventID { get; set; }
        public Nullable<int> statusID { get; set; }
    
        public EventInfoViewModel EventInfo { get; set; }
    }
}
