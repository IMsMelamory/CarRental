using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalDesktop.ViewModel
{
    public  class BaseTab: BaseViewModel
    {
        public abstract class HeaderTab:BaseTab
        {
            public abstract string Header { get; }
        }
       }
}
