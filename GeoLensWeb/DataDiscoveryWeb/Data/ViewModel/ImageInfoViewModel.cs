using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Data.ViewModel
{
    public class ImageInfoViewModel
    {

        public string name { get; set; }
        public string  folder { get; set; }
        public byte[] data { get; set; }
    }
    
}
