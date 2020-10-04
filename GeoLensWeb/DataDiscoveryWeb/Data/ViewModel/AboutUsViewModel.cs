using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Data.ViewModel
{
    public class AboutUsViewModel
    {
        public int aboutUsID { get; set; }
        public string aboutUsDescEn { get; set; }
        public string aboutUsDescAr { get; set; }
        public Nullable<int> statusID { get; set; }
    }
}
