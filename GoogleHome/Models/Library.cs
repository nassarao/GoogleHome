using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace GoogleHome.Models
{

    public class Library
    {
        [XmlArray("Apps")]
        public List<App> Apps { get; set; }

        public Library()
        {

        }

        public App FindByName(string name)
        {
           return Apps.FirstOrDefault(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
        }

    }
    public class App
    {
        public string Name { get; set; }
        public string Cmd { get; set; }
        public string Parameters { get; set; }
        public string ProcessName { get; set; }

        public void Start()
        {
            if (String.IsNullOrEmpty(Parameters))
            {
                Process.Start(Cmd);
                Console.WriteLine("Started App- " + this.ToString());
            }
            else
            {
                Process.Start(Cmd, Parameters);
                Console.WriteLine("Started App- " + this.ToString());

            }
        }

        public void Start(string parameters)
        {
            Process.Start(Cmd, parameters);
            Console.WriteLine(String.Format("Started App- {0} {1}", this.ToString(),parameters));
        }

        public void Close()
        {
            Process.Start("taskkill", "/IM " + ProcessName);
            Console.WriteLine(String.Format("Closed app: {0} Process:{1}",Name, ProcessName));
        }

       
        public override string ToString()
        {
            return String.Format("Name: {0} Cmd: {1} Parameters: {2}", Name, Cmd, Parameters);
        }

    }


  
}