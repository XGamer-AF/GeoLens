using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using PagedList; 
namespace Data.ViewModel
{
    public class EventImageAddViewModel
    {

        public int EventImageID { get; set; }
        public EventImage EventImage { get; set; }
        public IEnumerable<EventImageViewModel> EventImageAll { get; set; }

    }
    
}
