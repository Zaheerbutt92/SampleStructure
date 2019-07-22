using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Bootstraper
{
    public static class UnityIOC
    {
        private static IUnityContainer container;
        private static readonly object syncRoot = new object();

        public static IUnityContainer Instance
        {
            get
            {
                if (container == null)
                {
                    lock (syncRoot)
                    {
                        if (container == null)
                        {
                            container = new UnityContainer();
                        }
                    }
                }
                return container;
            }
        }
    }
}
