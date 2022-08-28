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

        private class CardIds
        {
            public Guid PublicKeyId { get; set; }
            public Guid Id { get; set; }
        }

        public static async Task<PersonalCardLoader> Start(string identity, string type)
        {
            var saga = new PersonalCardLoader(identity, type);

            var services = ServiceLocator.Services;
            var searchResult = await services.Cards.Search(saga.identity, saga.type).ConfigureAwait(false);

            saga.setup = searchResult
                .Select(it => new CardIds {PublicKeyId = it.PublicKey.Id, Id = it.Id})
                .Distinct();

            return saga;
  