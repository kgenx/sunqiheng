namespace Virgil.DropBox.Client.Encryption
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public static class StreamHelpers
    {
        public static Task WriteAsync(this Stream srteam, byte[] bytes)
        {
            return srteam.WriteAsync(bytes, 0, bytes.Length);
        }

        public static async Task<byte[]> TryReadExactly(this Stream stream, int count, byte[] buffer)
        {
            int offset = 0;
            while (offset < count)
            {
                int read 