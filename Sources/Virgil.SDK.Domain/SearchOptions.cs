namespace Virgil.SDK.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Identities;
    

    public class SearchOptions
    {
        public SearchOptions(string identityValue)
        {
            this.IdentityValue = identityValue;
        }

        internal string IdentityValue { get; private set; }
        internal bool? IncludeUnconfirmed { get; private set; }
        internal string IdentityType { get; private set; }

        public SearchOptions WithIdentityType(string identityType)
        {
        