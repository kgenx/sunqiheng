
﻿namespace Virgil.Sync.CLI
{
    using CommandLine;
    using Virgil.CLI.Common.Handlers;
    using Virgil.CLI.Common.Options;

    class Program
    {
        public static int Main(string[] args)
        {
            var parserResult = Parser.Default.ParseArguments<ConfigureOptions, StartOptions>(args);

            var configHandler = new ConfigHandler();
            var startHandler = new StartHandler();

            return parserResult.MapResult(
                (ConfigureOptions options) => configHandler.Handle(options),
                (StartOptions options) => startHandler.Handle(options),
                errs => 1);
        }
    }
}