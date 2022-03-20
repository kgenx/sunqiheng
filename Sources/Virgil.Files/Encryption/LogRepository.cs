namespace Virgil.DropBox.Client.Encryption
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;

    public class LogRepository
    {
        private const string StorageFileName = @"D:\ocr-export\storage.txt";

        public void AddOrReplace(LogEntry entry)
        {
            var logEntries = this.Load() ?? new List<LogEntry>();

            var existingEntry = logEntries.FirstOrD