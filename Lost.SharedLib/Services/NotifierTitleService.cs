using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lost.SharedLib
{
    public class NotifierTitleService
    {
        // Can be called from anywhere
        public async Task Update(string value)
        {
            if (Notify != null)
            {
                await Notify.Invoke(value);
            }
        }

        public event Func<string, Task> Notify;
    }
}
