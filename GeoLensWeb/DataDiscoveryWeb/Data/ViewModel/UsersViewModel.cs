using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Data.ViewModel
{
    public class UsersViewModel
    {
        public int userID { get; set; }
        public string userNameEn { get; set; }
        public string userNameAR { get; set; }
        public string userTel { get; set; }
        public string userEmail { get; set; }
        public Nullable<int> userTypeID { get; set; }
        public Nullable<int> statusID { get; set; }
    
        public IEnumerable<EventInfoViewModel> EventInfo { get; set; }
        public UserTypeViewModel UserType { get; set; }
    }
}
