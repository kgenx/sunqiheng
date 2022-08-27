namespace Virgil.SDK.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Identities;


    public class PersonalCardLoader 
    {
        private readonly string identity;
        private readonly string type;
        private IEnumerable<CardIds> setup;

        private PersonalCardLoader(string identity, string type)
        {
            this.identity = identity;
            this.type = type;
        }

        priv