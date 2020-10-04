using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using PagedList; 

namespace Data.ViewModel
{
    public class AboutUsAddViewModel
    {

        public int AboutUsID { get; set; }
        public AboutUs AboutUs { get; set; }
        public IPagedList<AboutUsViewModel> AboutUsAll { get; set; }

    }
    
}
