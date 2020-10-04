using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using PagedList; 
namespace Data.ViewModel
{
    public class EventTypesAddViewModel
    {
        public int EventTypeID { get; set; }
        public EventTypes EventTypes { get; set; }
        public IPagedList<EventTypesViewModel> EventTypesAll { get; set; }

    }
    
}
