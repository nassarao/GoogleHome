using GoogleHome.Models;
using SharpCaster.Models;
using SharpCaster.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace GoogleHome.Controllers
{
    public class IftttController : Controller
    {
        // GET: Ifttt
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult TheaterMode()
        {
            Library lib = GetLibrary();
            lib.FindByName("popcorn time").Start();
            lib.FindByName("Audioswitch").Start("/s 2");
            MonitorControll.SetMonitorState(MonitorControll.MonitorState.OFF);
            Console.WriteLine("Started Theater mode");
            return null;
        }

        [HttpGet]
        public ActionResult TVMode()
        {
            Library lib = GetLibrary();
            lib.FindByName("Audioswitch").Start("/s 2");
            MonitorControll.SetMonitorState(MonitorControll.MonitorState.OFF);
            Console.WriteLine("Started TV mode");
            return null;
        }

        [HttpGet]
        public ActionResult SteamTVMode()
        {
            Library lib = GetLibrary();
            lib.FindByName("Steam Big Picture").Start();
            lib.FindByName("Audioswitch").Start("/s 2");
            MonitorControll.SetMonitorState(MonitorControll.MonitorState.OFF);
            Console.WriteLine("Started Steam TV mode");
            return null;
        }

        [HttpGet]
        public ActionResult Monitors(string state)
        {
            switch (state.ToLower().Trim())
            {
                case "on":
                    MonitorControll.SetMonitorState(MonitorControll.MonitorState.ON);
                    break;
                case "off":
                    MonitorControll.SetMonitorState(MonitorControll.MonitorState.OFF);
                    break;
                case "stand by":
                    MonitorControll.SetMonitorState(MonitorControll.MonitorState.STANDBY);
                    break;
                default:
                    break;
            }
            return null;
        }

        [HttpGet]
        public ActionResult Start(string app)
        {
            Library lib = GetLibrary();
            App application = lib.Apps.FirstOrDefault(x => x.Name.ToLower() == app.Trim().ToLower());

            if (application != null)
            {
                application.Start();
                return null;
            }
            Console.WriteLine("Failed to start app: " + app);
            return null;
        }
        [HttpGet]
        public ActionResult Close(string app)
        {
            Library lib = GetLibrary();
            App application = lib.Apps.FirstOrDefault(x => x.Name.ToLower() == app.Trim().ToLower());

            if (application != null)
            {
                application.Close();
                return null;
            }
            Console.WriteLine("Failed to close app: " + app);
            return null;
        }

        [HttpGet]
        public ActionResult LockComputer()
        {
            Process.Start(@"C:\WINDOWS\system32\rundll32.exe", "user32.dll,LockWorkStation");
            return null;
        }

        private Library GetLibrary()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Library));
            using (StreamReader reader = new StreamReader(@"C: \Users\KingZelot\Documents\My Web Sites\GoogleHome\ApplicationsCmd.xml"))
            {
                Library lib = (Library)serializer.Deserialize(reader);
                return lib;
            }
        }


    }
}