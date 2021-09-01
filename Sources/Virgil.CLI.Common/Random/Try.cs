namespace Virgil.CLI.Common.Random
{
    using System.Collections.Generic;
    using System.Linq;

    public class Try
    {
        protected readonly List<string> Errors = new List<string>();

        public void AddError(string error)
        {
            this.Errors.Add(error);
        }

        public bool IsValid()
        {
            return !this.Errors.Any();
        }

        public List<string> GetErrors()
        {
    