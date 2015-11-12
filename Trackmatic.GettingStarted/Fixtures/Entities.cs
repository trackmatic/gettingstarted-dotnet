using System;
using Trackmatic.Rest.Routing.Model;

namespace Trackmatic.GettingStarted.Fixtures
{
    public class Entities
    {
        public static Entity Entity(string clientId, string id, string name, Action<Entity> customsize)
        {
            var entity = new Entity
                {
                    Id = $"{clientId}/{id}",
                    Name = name
                };
            customsize(entity);
            return entity;
        }
    }
}
