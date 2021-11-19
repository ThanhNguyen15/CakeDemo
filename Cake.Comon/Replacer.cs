using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.Diagnostics;
using System;
using System.Xml;

namespace Cake.Comon
{
    public static class Replacer
    {
        [CakeMethodAlias]
        public static void ReplaceAppSetting(this ICakeContext context, string filename, string key, string newValue)
        {
            var xml = new XmlDocument();
            xml.Load(filename);

            var node = xml.SelectSingleNode($"/configuration/appSettings/add[@key='{key}']");
            if (node == null)
                throw new InvalidOperationException($"ApplicationSetting key ({key}) does not exist.");

            var valueAttribute = node.Attributes["value"];
            if(valueAttribute == null)
            {
                valueAttribute = xml.CreateAttribute("value");
                node.Attributes.SetNamedItem(valueAttribute);
            }
            valueAttribute.Value = newValue;
            xml.Save(filename);

            context?.Log.Write(Verbosity.Normal, LogLevel.Debug, $"Replacing {filename} appSetting key = '{key}'");
        }
    }
}
