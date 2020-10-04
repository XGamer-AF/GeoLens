using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Data.ViewModel
{
    public class CountriesViewModel
    {
        public int countryID { get; set; }
        public string countryNameEn { get; set; }
        public string countryNameAr { get; set; }
        public Nullable<int> orderID { get; set; }
        public Nullable<int> statusID { get; set; }
    
        public IEnumerable<EventInfoViewModel> EventInfo { get; set; }
    }
}
