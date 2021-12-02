using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lost.SharedLib
{
    public class BaseViewModel
    {

        public BaseViewModel()
        {


        }
        public long Id { get; set; }

        public bool IsReadOnly { get; set; }

    }
}
