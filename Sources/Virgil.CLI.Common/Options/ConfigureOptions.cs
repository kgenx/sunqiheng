namespace Virgil.CLI.Common.Options
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using CommandLine;
    using CommandLine.Text;

    [Verb("config", HelpText = "Configures Virgil Sync to use specific Virgil card and dropbox account.")]
    public class ConfigureOptions
    {
        [Option('v', "virgil-card", Required = true, HelpText = "Path to the virgil card file created from CLI.")]
        public string VirgilCardPath { get; set; }

        [Option('k', "private-key", Required = true, HelpText = "Path to the private key of the specified virgil card.")]
        public string PrivateKeyPath { get; set; }
        
        [Option('s', "source-dir", Required = true, HelpText = "Path to the directory you want to sync with DropBox.")]
        public string SourceDirectory { get; set; }
        
        public string GetUsage()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            var help = new HelpText
            {
                Heading = new HeadingInfo("Virgl Sync CLI", version),
                Copyright = new CopyrightInfo("Virgil Security", 2016),
                AdditionalNewLineAfterOption = true,
                AddDashesToOption = true
            };
            
            return help;