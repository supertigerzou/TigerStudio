using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http.Dispatcher;
using Autofac;
using TigerStudio.Framework.Config;
using TigerStudio.Framework.Helpers;

namespace TigerStudio.Framework.WebApiExtensions
{
    public class ExtendedDefaultAssembliesResolver : DefaultAssembliesResolver
    {
        private readonly ContainerBuilder _builder;

        public ExtendedDefaultAssembliesResolver(ContainerBuilder builder)
        {
            _builder = builder;
        }

        public override ICollection<Assembly> GetAssemblies()
        {
            var settings = PreLoadedAssembliesSettings.GetSection();
            if (null != settings)
            {
                foreach (AssemblyElement element in settings.AssemblyNames)
                {
                    var assemblyFile = (new WebHelper()).MapPath("~/") + @"bin\" + element.AssemblyName;
                    var assemblyName = AssemblyName.GetAssemblyName(assemblyFile);
                    if (!AppDomain.CurrentDomain.GetAssemblies().Any(assembly => AssemblyName.ReferenceMatchesDefinition(assembly.GetName(), assemblyName)))
                    {
                        AppDomain.CurrentDomain.Load(assemblyName);
                    }
                }
            }
            return base.GetAssemblies();
        }
    }
}
