using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using YoutifyLib;

namespace YoufityWinForms
{
    public class ServiceRole
    {
        public ServiceHandler Service { get; private set; }
        public bool Source = false;
        public bool Target = false;

        public ServiceRole(ServiceHandler serviceHandler) => Service = serviceHandler;
    }
}
