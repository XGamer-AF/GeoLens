using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using PagedList; 
namespace Data.ViewModel
{
    public class EventRefrencesAddViewModel
    {
        public int eventID { get; set; }
        public int EventRefrencesID { get; set; }
        public EventRefrences EventRefrences { get; set; }
        public IPagedList<EventRefrencesViewModel> EventRefrencesAll { get; set; }

    }
    
}
