using System;
using Trackmatic.Rest.Core.Model;
using Action = Trackmatic.Rest.Routing.Model.Action;

namespace Trackmatic.GettingStarted.Fixtures
{
    public static class Actions
    {
        public static Action Collection(string clientId,
            OLocation location, string reference)
        {
            var action = new Action
                             {
                                 Id = $"{clientId}/{reference}",
                                 Reference = reference,
                                 ClientId = clientId,
                                 ExpectedDelivery = DateTime.UtcNow,
                                 Instructions = "Action instructions",
                                 Pallets = 1,
                                 Weight = 100,
                                 VolumetricMass = 3,
                                 Value = 400,
                                 Name = reference,
                                 AmountEx = 10,
                                 AmountIncl = 11,
                                 ActionTypeId = $"{clientId}/collection",
                                 ActionTypeName = "Collection"
                             };
            return action;
        }

        public static Action Delivery(string clientId,
            OLocation location, string reference)
        {
            var action = new Action
                             {
                                 Id = $"{clientId}/{reference}",
                                 Reference = reference,
                                 ClientId = clientId,
                                 ExpectedDelivery = DateTime.UtcNow,
                                 Instructions = "Action instructions",
                                 Pallets = 1,
                                 Weight = 120,
                                 VolumetricMass = 3,
                                 Value = 400,
                                 Name = reference,
                                 AmountEx = 12,
                                 AmountIncl = 13,
                                 ActionTypeId = $"{clientId}/delivery",
                                 ActionTypeName = "Delivery"
                             };
            return action;
        }

    }
}