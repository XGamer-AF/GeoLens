using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Data.ViewModel
{
    public class EventInfoViewModel
    {
        public int eventID { get; set; }
        public string eventNameEn { get; set; }
        public string eventNameAr { get; set; }
        public string eventDescEn { get; set; }
        public string eventDescAr { get; set; }
        public Nullable<double> latitude { get; set; }
        public Nullable<double> longitude { get; set; }
        public Nullable<int> eventTypeID { get; set; }
        public Nullable<int> countryID { get; set; }
        public Nullable<System.DateTime> eventDate { get; set; }
        public Nullable<int> userID { get; set; }
        public Nullable<int> statusID { get; set; }
        public string eventImage { get; set; }
        public Nullable<System.DateTime> dateAdd { get; set; }



        public CountriesViewModel Countries { get; set; }
        public EventTypesViewModel EventTypes { get; set; }
        public UsersViewModel Users { get; set; }

     
    }
}
