using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using PagedList; 
namespace Data.ViewModel
{
    public class CountriesAddViewModel
    {

        public int countryID { get; set; }
        public Countries Countries { get; set; }
        public IPagedList<CountriesViewModel> CountriesAll { get; set; }

    }
    
}
