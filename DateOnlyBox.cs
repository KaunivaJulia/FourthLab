using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4
{
    public class DateOnlyBox
    {
        [Key]
        public required DateOnly Value { get; set; }
    }
}
