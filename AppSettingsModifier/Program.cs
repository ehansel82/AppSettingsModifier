using System;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace AppSettingsModifier
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DebugArgs(args);
            var options = new Options();

            if (!CommandLine.Parser.Default.ParseArguments(args, options))
            {
                Environment.Exit((int)ExitCode.InvalidArguments);
            }

            ModifyConfig(options);

            Environment.Exit((int)ExitCode.Success);
        }

        private static void ModifyConfig(Options options)
        {
            XDocument xmlDoc = new XDocument();
            try
            {
                xmlDoc = XDocument.Load(options.File);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading file {0} {1}.", options.File, ex.Message);
                Environment.Exit((int)ExitCode.FileLoadException);
            }

            try
            {
                var action = options.Action.ToLower();
                var validActions = new string[] { "add", "modify", "remove" };
                if (validActions.Where( x=> x == action).SingleOrDefault() == null)
                {
                    Console.WriteLine("Invalid action");
                    Environment.Exit((int)ExitCode.InvalidArguments);
                }

                var appSettingsNode = xmlDoc.Descendants("appSettings").Single();
                if (options.Action.ToLower() == "add")
                {
                    appSettingsNode.Add(new XElement("add", new XAttribute("key", options.Key), new XAttribute("value", options.Value)));
                } else if (options.Action.ToLower() == "modify")
                {
                    var modifyNode = appSettingsNode.Descendants("add").Where(x => x.Attribute("key").Value == options.Key).Single();  
                    modifyNode.Attributes("value").First().Value = options.Value;
                } else if (options.Action.ToLower() == "remove")
                {
                    var removeNode = appSettingsNode.Descendants("add").Where(x => x.Attribute("key").Value == options.Key).Single();
                    removeNode.Remove();
                }
                try
                {
                    xmlDoc.Save(options.File);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error saving file {0} {1}.", options.File, ex.Message);
                    Environment.Exit((int)ExitCode.FileSaveException);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error modifying XML {0}.", ex.Message);
                Environment.Exit((int)ExitCode.XmlModifyException);
            }
        }

        private static void DebugArgs(string[] args)
        {
            for (var i = 0; i < args.Length; i++)
            {
                Debug.WriteLine("Arg {0}: {1}", i, args[i]);
            }
        }

        private enum ExitCode : int
        {
            Success = 0,
            InvalidArguments = 1,
            FileLoadException = 2,
            XmlModifyException = 3,
            FileSaveException = 4
        }
    }
}