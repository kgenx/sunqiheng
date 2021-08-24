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
        public string PrivateKeyPath { 