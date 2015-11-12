using System;
using Trackmatic.Rest;
using Trackmatic.Rest.Core.Model;

namespace Trackmatic.GettingStarted.Fixtures
{
    public static class Decos
    {
        public static OLocation MyShoppingMall(string clientId)
        {
            var deco = new OLocation
            {
                Name = "My Shopping Mall",
                Address = "Address of deco",
                Category = new OLocationCategory { Id = "1", Description = "Category" },
                ClientId = clientId,
                Id = $"{clientId}/myshoppingmall/1",
                Shape = EZoneShape.Radius,
                Coords = new SpecializedObservableCollection<OCoord>
                        {
                            new OCoord {Latitude = -26.110514, Longitude = 28.052889, Radius = 100}
                        },
                DefaultStopTime = TimeSpan.FromMinutes(22),
                StructuredAddress = new StructuredAddress
                {
                    StreetNo = "61",

                    Street = "Katherine Street",

                    Suburb = "Sandown",

                    City = "Sandton",

                    Province = "Gauteng"
                }
            };
            return deco;
        }

        public static OLocation SandtonCity(string clientId)
        {
            var deco = new OLocation
                {
                    Name = "Sandton City",
                    Address = "Address of deco",
                    Category = new OLocationCategory {Id = "1", Description = "Category"},
                    ClientId = clientId,
                    Id = $"{clientId}/sandton/1",
                    Reference = "Internal Ref",
                    Shape = EZoneShape.Radius,
                    Coords = new SpecializedObservableCollection<OCoord>
                        {
                            new OCoord {Latitude = -26.110514, Longitude = 28.052889, Radius = 100}
                        },
                    DefaultStopTime = TimeSpan.FromMinutes(22),
                    StructuredAddress = new StructuredAddress
                        {
                            StreetNo = "61",

                            Street = "Katherine Street",

                            Suburb = "Sandown",

                            City = "Sandton",

                            Province = "Gauteng"
                        }
                };
            return deco;
        }

        public static OLocation WitsTheatre(string clientId)
        {
            var deco = new OLocation
                {
                    Name = "Wits Theatre",
                    Address = "Address of deco",
                    Category = new OLocationCategory {Id = "1", Description = "Category"},
                    ClientId = clientId,
                    Id = $"{clientId}/2",
                    Reference = "Internal Ref",
                    Shape = EZoneShape.Radius,
                    Coords = new SpecializedObservableCollection<OCoord>
                        {
                            new OCoord {Latitude = -26.190217, Longitude = 28.030218, Radius = 100}
                        }
                };
            return deco;
        }

        public static OLocation Home(string clientId)
        {
            var deco = new OLocation
            {
                Name = "158 Epsom Terrace",
                Address = "Address of deco",
                ClientId = clientId,
                Id = $"{clientId}/44f44cb9-5fed-4623-862a-e3d998e6160b",
                Reference = "Internal Ref",
                Shape = EZoneShape.Radius,
                Coords = new SpecializedObservableCollection<OCoord>
                        {
                            new OCoord {Latitude = -26.041245, Longitude = 28.020495, Radius = 100}
                        }
            };
            return deco;
        }

        public static OLocation TheCampus(string clientId)
        {
            var deco = new OLocation
                {
                    Name = "The Campus",
                    Address = "Address of deco",
                    Category = new OLocationCategory {Id = "1", Description = "Category"},
                    ClientId = clientId,
                    Id = $"{clientId}/3",
                    Reference = "Internal Ref",
                    Shape = EZoneShape.Radius,
                    Coords = new SpecializedObservableCollection<OCoord>
                        {
                            new OCoord {Latitude = -26.041245, Longitude = 28.020495, Radius = 100}
                        }
                };
            return deco;
        }

        public static OLocation FourwaysMall(string clientId)
        {
            var deco = new OLocation
            {
                Name = "Fourways Mall",
                Address = "Address of deco",
                Category = new OLocationCategory { Id = "1", Description = "Category" },
                ClientId = clientId,
                Id = $"{clientId}/6",
                Reference = "Internal Ref",
                Shape = EZoneShape.Radius,
                Coords = new SpecializedObservableCollection<OCoord>
                        {
                            new OCoord {Latitude = -26.020116, Longitude = 28.007320, Radius = 100}
                        }
            };
            return deco;
        }

        public static OLocation AnotherDeco(string clientId)
        {
            var deco = new OLocation
            {
                Name = "Trackmatic Solutions",
                Address = "Address of deco",
                Category = new OLocationCategory { Id = "1", Description = "Category" },
                ClientId = clientId,
                Id = $"{clientId}/5",
                Reference = "Internal Ref",
                Shape = EZoneShape.Radius,
                Coords = new SpecializedObservableCollection<OCoord>
                        {
                            new OCoord {Latitude = -26.130565, Longitude = 28.086042, Radius = 100}
                        }
            };
            return deco;
        }

        public static OLocation Adhoc(string clientId, string name, string reference)
        {
            var deco = new OLocation
                {
                    Name = name,
                    Address = "Address of deco",
                    Category = new OLocationCategory {Id = "1", Description = "Category"},
                    ClientId = clientId,
                    Id = $"{clientId}/$tmp/" + reference,
                    Reference = reference,
                    Shape = EZoneShape.Radius,
                    Coords = new SpecializedObservableCollection<OCoord>
                        {
                            new OCoord {Latitude = -26.041245, Longitude = 28.020495, Radius = 100}
                        }
                };
            return deco;
        }
    }
}