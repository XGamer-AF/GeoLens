using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Data.ViewModel
{
    public class EventTypesViewModel
    {
        public int eventTypeID { get; set; }
        public string eventTypeNameEn { get; set; }
        public string eventTypeNameAr { get; set; }
        public Nullable<int> statusID { get; set; }
    
        public IEnumerable<EventInfoViewModel> EventInfo { get; set; }
    }
}
