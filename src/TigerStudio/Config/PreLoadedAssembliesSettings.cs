using System.Configuration;

namespace TigerStudio.Framework.Config
{
    public class PreLoadedAssembliesSettings : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public AssemblyElementCollection AssemblyNames
        {
            get { return (AssemblyElementCollection)this[""]; }
        }

        public static PreLoadedAssembliesSettings GetSection()
        {
            return ConfigurationManager.GetSection("preLoadedAssemblies") as PreLoadedAssembliesSettings;
        }
    }

    public class AssemblyElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new AssemblyElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            var serviceTypeElement = (AssemblyElement)element;
            return serviceTypeElement.AssemblyName;
        }
    }

    public class AssemblyElement : ConfigurationElement
    {
        [ConfigurationProperty("assemblyName", IsRequired = true)]
        public string AssemblyName
        {
            get { return (string)this["assemblyName"]; }
            set { this["assemblyName"] = value; }
        }
    }
}
