using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using Newtonsoft.Json;
using Trackmatic.GettingStarted.Fixtures;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Core.Model;
using Trackmatic.Rest.Routing.Model;
using Trackmatic.Rest.Routing.Model.Descriptors;
using Trackmatic.Rest.Routing.Requests;
using Trackmatic.Rest.Utility.Requests;

namespace Trackmatic.GettingStarted
{
    public class Program
    {
        private static Api _api;

        private static string _clientId;

        private static void Main(string[] args)
        {
            //Specify this setting in the app.config file, under the appSetting key "api/clientid"
            _clientId = ConfigurationManager.AppSettings["api/clientid"];

            //Specify these setting in the app.config file, under the appSetting key "api/address" & "api/username"
            _api = new Api(ConfigurationManager.AppSettings["api/address"], _clientId, ConfigurationManager.AppSettings["api/username"]);

            //Specify this setting in the app.config file, under the appSetting key "api/password"
            _api.Authenticate(ConfigurationManager.AppSettings["api/password"]);

            // Typical routing integration workflow

            // The fastest way to creata a route is by using a template
            // In this scenario we are create a template using the API, however this can also be done through the front end
            // Typically you would have a template per branch/depot in a milk run scenario
            var templateId = "[tempalteid]";

            // Create a list of DECO's to be uploaded
            var decos = new List<OLocation>
            {
                Decos.SandtonCity(_clientId),
                //Decos.TheCampus(_clientId),
                //Decos.FourwaysMall(_clientId),
                //Decos.Adhoc(_clientId, "My New Adhoc Deco", "MNAD")
            };

            // Create a list of actions to be uploaded
            var actions = new[]
            {
                Actions.Delivery(_clientId, decos[0], "4ACTION0_1"),
                Actions.Delivery(_clientId, decos[0], "4ACTION0_2"),
                Actions.Delivery(_clientId, decos[0], "4ACTION0_3"),
                Actions.Delivery(_clientId, decos[0], "4ACTION0_4"),
                Actions.Delivery(_clientId, decos[0], "4ACTION0_5"),
                Actions.Delivery(_clientId, decos[0], "4ACTION0_6"),
                Actions.Delivery(_clientId, decos[0], "4ACTION0_7"),
                Actions.Delivery(_clientId, decos[0], "4ACTION0_8"),
                Actions.Delivery(_clientId, decos[0], "4ACTION0_9")
            };

            // Create a list of entities to be uploaded
            var entities = new[]
            {
                Entities.Entity(_clientId, "entity/demo/1", "Entity 1", e =>
                {
                    // Requirements describe what the driver will be requried to capture when completing an entity visit
                    // This is only applicable if infinity devices are being used
                    e.Requirements = new ObservableCollection<EntityRequirement>()
                    {
                        new RequireRating
                        {
                            Ratings = new List<RatingDescription>
                            {
                                new RatingDescription {Name = "Service", Type = "Simple"}
                            }
                        }
                    };

                }),

                Entities.Entity(_clientId, "entity/demo/2", "Entity 2", e =>
                {
                    e.Requirements = new ObservableCollection<EntityRequirement>
                    {
                        new RequireActionDebrief(),
                        new RequireSignature()
                    };

                }),

                Entities.Entity(_clientId, "entity/demo/3", "Entity 3", e =>
                {
                    e.Requirements = new ObservableCollection<EntityRequirement>
                    {
                        new RequireActionDebrief(),
                        new RequireSignature()
                    };
                }),

                Entities.Entity(_clientId, "entity/demo/4", "Entity 4", e =>
                {
                    e.Requirements = new ObservableCollection<EntityRequirement>
                    {
                        new RequireActionDebrief(),
                        new RequireSignature()
                    };
                }),

                Entities.Entity(_clientId, "entity/demo/5", "Entity 5", e =>
                {
                    e.Requirements = new ObservableCollection<EntityRequirement>
                    {
                        new RequireActionDebrief(),
                        new RequireSignature()
                    };
                })
            };

            // Create the relationships between the various components
            var relationships = new[]
            {
                Relationship.Link(actions[0]).To(entities[0]).At(decos[0]).OnRun(0).Customise(x => x.Mst = TimeSpan.FromMinutes(8)),
                Relationship.Link(actions[1]).To(entities[0]).At(decos[0]).OnRun(0).Customise(x => x.Mst = TimeSpan.FromMinutes(8)),
                Relationship.Link(actions[2]).To(entities[1]).At(decos[0]).OnRun(0).Customise(x => x.Mst = TimeSpan.FromMinutes(8)),
                Relationship.Link(actions[3]).To(entities[1]).At(decos[0]).OnRun(0).Customise(x => x.Mst = TimeSpan.FromMinutes(8)),
                Relationship.Link(actions[4]).To(entities[2]).At(decos[0]).OnRun(0).Customise(x => x.Mst = TimeSpan.FromMinutes(8)),
                Relationship.Link(actions[5]).To(entities[2]).At(decos[0]).OnRun(0).Customise(x => x.Mst = TimeSpan.FromMinutes(8)),
                Relationship.Link(actions[6]).To(entities[3]).At(decos[0]).OnRun(0).Customise(x => x.Mst = TimeSpan.FromMinutes(8)),
                Relationship.Link(actions[7]).To(entities[3]).At(decos[0]).OnRun(0).Customise(x => x.Mst = TimeSpan.FromMinutes(8)),
                Relationship.Link(actions[8]).To(entities[4]).At(decos[0]).OnRun(0).Customise(x => x.Mst = TimeSpan.FromMinutes(8))
            };

            // Descriptors allow you to customise how the Trackmatic API processes the data
            // Each field has an associated descriptor which allows you to set the authority and default value
            // The authority determines which system owns the information. If the authority is set to Trackmatic
            // and the component exists in trackmatic, the field will not be overwritten.
            // If the authority is set to Lob the field will be overwritten regardless of whether or not it exists in trackmatic
            // A descriptor can have other configuration values depending in the field type
            // The LocationStructuredAddressDescriptor for instance has multiple options to allow you to control geocoding rules.
            // The example below tells trackmatic to overwrite values with what is sent from line of business, sets the geocoding proximity
            // to 50km, set the result radius to 500m (when there is a match it will set the deco shape to a radius with the supplied radius value)

            var descriptors = new List<FieldDescriptorBase>
            {
                new LocationCoordsDescriptor
                {
                    Authority = EAuthority.Trackmatic
                },

                new EntityRequirementsDescriptor
                {
                    Authority = EAuthority.Lob
                },

                new RelationshipMstDescriptor
                {
                    Authority = EAuthority.Lob
                }
            };

            // Upload various components and create route
            // Note: in order to just upload DECO's entities and actions just ignore the Route field
            var request = new UploadModel
            {
                Actions = actions.ToList(),
                Decos = decos.ToList(),
                Entities = entities.ToList(),
                Relationships = relationships.ToList(),
                Route = new RouteModel
                {
                    StartDate = DateTime.UtcNow,
                    Id = $"{_clientId}/{Guid.NewGuid()}",
                    TemplateId = templateId,
                    Reference = "SHEP1",
                    Registration = "TESTING",
                    Name = "My New Route Name",
                    Options = new RouteOptions
                    {
                        AutomatedAdjustment = new AutomatedAdjustmentOptions
                        {
                            Enabled = true,

                            Threshold = TimeSpan.FromMinutes(15)
                        },
                        Lock = new LockOptions
                        {
                            All = true,
                            Start = true,
                            End = true
                        },
                        MaxSpeed = 100,
                        Strategy = ERouteStrategy.Shortest,
                        Temperature = 100
                    },
                    DueTimeAdjustments = new List<DueTimeAdjustmentModel>
                    {
                        new DueTimeAdjustmentModel
                        {
                            Adjustment = TimeSpan.FromMinutes(30),

                            Position = new End(),

                            Type = EDueTimeAdjustmentType.Layover
                        }
                    },
                    Schedule = false

                },
                Descriptors = descriptors
            };

            var json = request.ToJson(true);

            var start = DateTime.UtcNow;

            Console.WriteLine(start);

            var response = _api.ExecuteRequest(new Upload(_api.Context, request)).Data;

            Console.WriteLine(DateTime.UtcNow.Subtract(start));

            Console.ReadLine();

            //// Optmise can be called on the route many times
            //// This is only really required if you add/remove DECOS and you require the order to the DECO's to be optmised again
            //response = Optimise(response.Instance);

            //// Set lock all to true to prevent re-ordering of the decos
            //// and allow users to change the order manually
            //response.Instance.Route.LockAll = true;

            //var secondStop = response.Instance.Route.RouteDecos[2];

            //// Move 2nd stop to the last stop
            //response.Instance.Route.MoveDown(secondStop);

            //secondStop = response.Instance.Route.RouteDecos[2];

            //// Move 2nd stop to 1st position
            //response.Instance.Route.MoveUp(secondStop);

            //// The save method will perform an optmisation on the route automatically
            //Save(response.Instance);
        }

        private static RouteInstanceWithActionsAndGeometry 
            Optimise(RouteInstance instance)
        {
            var request = new OptimiseRoute(_api.Context, instance);
            var response = _api.ExecuteRequest(request);
            return response.Data;
        }
        
        #region Some other examples

        private static void GeocodeExample()
        {
            var request = new Geocode(_api.Context, "61 Katherine str, Sandton");
            var result = _api.ExecuteRequest(request).Data[0];
            var output = string.Format("{0} {1}, {2}, {3}, {4}, {5}, {6}",
                                       result.Number,
                                       result.Line1,
                                       result.Suburb,
                                       result.City,
                                       result.PostalCode,
                                       result.Latitude,
                                       result.Longitude);
            Console.WriteLine(output);
        }

        #endregion
    }
}
