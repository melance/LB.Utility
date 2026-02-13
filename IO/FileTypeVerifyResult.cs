using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB.Utility.IO
{
    public class FileTypeVerifyResult
    {
        public String? Name { get; set; }
        public String? Description { get; set; }
        public String? MediaType { get; set; }
        public Boolean IsValid { get; set; }
    }
}
