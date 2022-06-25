namespace Virgil.FolderLink.Local
{
    using System.Collections.Generic;

    public class ByPathComparer : IEqualityComparer<LocalFile>
    {
        public bool Equals(LocalFile x, LocalFile y)
        {
            return string.Equals(x.RelativePath, y.RelativePath);
        }

        public int GetHashCode(LocalFile obj)
    