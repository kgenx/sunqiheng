namespace Virgil.SDK.Domain
{
    using System.Text;

    internal static class BytesExtensions
    {
        /// <summary>
        /// Gets the byte representation of string in specified encoding.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="encoding">The encoding. Optional. UTF8 is used by default</param>
        /// <returns>Byte array</returns>