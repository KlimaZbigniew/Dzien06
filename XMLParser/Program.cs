using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = @"c:\tmp\persons.xml";
            string content = File.ReadAllText(filename);

            XmlDocument document = new XmlDocument();
            document.LoadXml(content);

            XmlElement persons = document["persons"];

            Console.WriteLine($"Ilość elementów {persons.ChildNodes.Count}");

            Console.WriteLine($"+{"----------",-10}+{"----------",-10}+{"----------",-10}+");
            Console.WriteLine($"|{"Imię",-10}|{"Nazwisko",-10}|{"Telefon",-10}|");
            Console.WriteLine($"+{"----------",-10}+{"----------",-10}+{"----------",-10}+");
            foreach (XmlElement item in persons.ChildNodes)
            {
                string lname =  item.GetAttribute("lname");
                string name = item.GetAttribute("name");
                string phone = item.GetAttribute("phone");
                Console.WriteLine($"|{name, -10}|{lname,-10}|{phone,-10}|");
            }
            Console.WriteLine($"+{"----------",-10}+{"----------",-10}+{"----------",-10}+");

            Console.ReadKey();

        }
    }
}
