using CommandLine;
using CommandLine.Text;

namespace AppSettingsModifier
{
    internal class Options
    {
        /// <summary>
        /// Example "-a add" or "--action=add"
        /// </summary>
        [Option('a', "action", Required = true,
          HelpText = "Action to perform on the new AppSettings value (Add).")]
        public string Action { get; set; }

        /// <summary>
        /// Example "-k TestKey" or "--key=TestKey"
        /// </summary>
        [Option('k', "key", Required = true,
          HelpText = "Key to add to the AppSettings node.")]
        public string Key { get; set; }

        /// <summary>
        /// Example "-v TestValue" or "--value=TestValue"
        /// </summary>
        [Option('v', "value", Required = true,
        HelpText = "Value to add to the AppSettings node.")]
        public string Value { get; set; }

        /// <summary>
        /// Example "-f "c:\project\web.config" or "--value="c:\project\web.config""
        /// </summary>
        [Option('f', "file", Required = true,
        HelpText = "File that will have AppSettings node modified.")]
        public string File { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}