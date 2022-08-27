
﻿namespace Virgil.SDK.Domain
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Identities;
    using Virgil.Crypto;


    /// <summary>
    /// Domain entity that represents a list of recipients Virgil Cards.
    /// </summary>
    /// <seealso cref="RecipientCard" />
    public class Cards : IReadOnlyCollection<RecipientCard>
    {
        private readonly List<RecipientCard> cards;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cards"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public Cards(IEnumerable<RecipientCard> collection)
        {