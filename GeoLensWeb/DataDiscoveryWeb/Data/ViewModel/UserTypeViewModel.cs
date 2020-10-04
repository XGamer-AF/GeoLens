using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Data.ViewModel
{
    public class UserTypeViewModel
    {
        public int userTypeID { get; set; }
        public string userTypeDescEn { get; set; }
        public string userTypeDescAr { get; set; }
        public Nullable<int> statusID { get; set; }
    
        public IEnumerable<UsersViewModel> Users { get; set; }
    }
}
