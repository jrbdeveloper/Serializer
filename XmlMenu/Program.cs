using System;
using System.Collections;

namespace XmlMenu
{
    class Program
    {
        private const string ERROR_MESSAGE = "Menu Not Found.";

        static void Main(string[] args)
        {
            var parser = new Parser();
            var id = parser.GetArgument(args, "menu");
            var path = parser.GetArgument(args, "path");
            var xml = parser.GetXmlBy(id);
            var menu = parser.Deserialize<Menu>(xml);

            if (menu != null)
            {
                if (menu.ID == id)
                {
                    parser.Render(menu, path, 0);
                    Console.ReadKey();
                }
                else
                {
                    ShowError();
                }
            }
            else
            {
                ShowError();
            }          
        }

        private static void ShowError()
        {
            Console.WriteLine(ERROR_MESSAGE);
            Console.ReadKey();
        }
    }
}