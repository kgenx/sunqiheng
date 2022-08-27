namespace Virgil.SDK.Domain
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;
    using Identities;
    using Models;


    public class Identity
    {
        protected Identity()
        {
        }

        public Identity(IdentityModel identityModel)
        {
            this.Id = identityModel.Id;
            this.Value = identityModel.Value;
            this.Type = identityModel.Type.ToIdentityType();
            this.CreatedAt = identityModel.CreatedAt;
        }

        public Guid Id { get; protected set; }

        public string Value { get; protected s