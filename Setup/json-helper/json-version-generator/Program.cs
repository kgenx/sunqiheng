namespace json_version_generator
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.Text.RegularExpressions;

    [DataContract]
    public class UpdatesInfo
    {
        [DataMember(Name = "version")]
        public string Version { get; set; }

        [DataMember(Name = "download_url")]
        public string DownloadUrl { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "setup_url")]
        public string SetupUrl { get; set; }
        
    }

    class Program
    {
        static void Main(string[] args)
        {
            var installerFolder = args[0];
            var outputPath = args[1];

            var installer = Directory.EnumerateFiles(installerFolder)
                .First(it => it.EndsW