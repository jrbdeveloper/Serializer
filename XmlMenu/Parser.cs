using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace XmlMenu
{
    public class Parser
    {
        public string Xml { get; set; }

        private Dictionary<string, string> Menus = new Dictionary<string, string> {
            { "SchedAero",@"..\..\Menus\SchedAeroMenu.xml"},
            { "Wyvern",@"..\..\Menus\WyvernMenu.xml"}
        };

        public T Deserialize<T>(string xml) where T : class
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (var reader = new StringReader(xml))
                {
                    return (T)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                return null;
            }            
        }

        public string GetXmlBy(string name)
        {
            var xml = string.Empty;

            try
            {
                if (Menus.ContainsKey(name))
                {
                    xml = File.ReadAllText(Menus[name]);
                }                
            }
            catch (Exception ex)
            {
                throw;
            }            

            return xml;
        }

        public string GetArgument(string[] args, string name)
        {
            var val = string.Empty;
            var label = (name == "path") ? "Active Path: " : "Menu Name: ";
            var arg = string.Empty;

            if (args.Length > 0)
            {
                switch (name)
                {
                    case "menu":
                        arg = args[0];
                        break;

                    case "path":
                        arg = args[1];
                        break;
                }
            }

            if (string.IsNullOrEmpty(arg))
            {
                Console.WriteLine(label);
                val = Console.ReadLine();
            }
            else
            {
                Console.WriteLine(label + arg);
                val = arg;
            }

            return val;
        }

        public void Render(object obj, string path, int indent)
        {
            if (obj == null) return;

            var indentString = new string(' ', indent);
            var type = obj.GetType();
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == "ID")
                {
                    continue;
                }

                var value = property.GetValue(obj, null);
                var elements = value as IList;
                var active = string.Empty;

                if (value.ToString() == path)
                {
                    active = "ACTIVE";
                }

                if (elements != null)
                {
                    foreach (var element in elements)
                    {
                        if (element is Item)
                        {
                            Console.WriteLine("");
                            var i = element as Item;
                            i.DisplayName += ":";
                        }

                        Render(element, path, indent + 2);
                    }
                }
                else
                {
                    if (property.PropertyType.Assembly == type.Assembly)
                    {
                        Render(value, path, 0);                        
                    }
                    else
                    {
                        Console.Write("{0} {1} {2}", indentString, value, active);

                        active = string.Empty;
                    }
                }
            }
        }
    }
}