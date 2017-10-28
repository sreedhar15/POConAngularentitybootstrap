using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRPBookLibrary
{
    public class GlobalConfiguration
    {
        public static string WebAPIRoot
        {
            get
            {
                return (string) System.Configuration.ConfigurationSettings.AppSettings["WebAPIRoot"];
            }
        }
    }
}
