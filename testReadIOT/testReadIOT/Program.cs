using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Azure.Devices;
using Newtonsoft.Json;

namespace testReadIOT
{
    class Program
    {
        static RegistryManager registryManager;
        private static void Main()
        {
            AddTagsAndQuery().Wait();
            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }

        public static async Task AddTagsAndQuery()
        {
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            var twin = await registryManager.GetTwinAsync("simulator_bus_4");
            var list = new List<Vehicle>();

            var patch =
                @"{
            tags: {
                location: {
                    region: 'US',
                    plant: 'Redmond43'
                }
            }
        }";
            await registryManager.UpdateTwinAsync(twin.DeviceId, patch, twin.ETag);

            var query = registryManager.CreateQuery(
              "SELECT * FROM devices", 100);


            var vehicles = await query.GetNextAsTwinAsync();

            foreach (var vehical in vehicles)
            {
                Vehicle user = JsonConvert.DeserializeObject<Vehicle>(vehical.Properties.Reported.ToString());
                list.Add(user);
            }

            foreach (var vehical2 in list)
            {
                Console.WriteLine("Cars latitude: " + vehical2.latitude);
                Console.WriteLine("Cars longitude: " + vehical2.longitude);

                Console.WriteLine("Cars vin: " + vehical2.vin);
                Console.WriteLine("Cars speed: " + vehical2.speed);
                Console.WriteLine("Cars engine Rpm: " + vehical2.engineRpm);
                Console.WriteLine("Cars engine Run Time: " + vehical2.engineRunTime);
                Console.WriteLine("Cars fuel Level: " + vehical2.fuelLevel);
                Console.WriteLine("Cars ambient Air Temperature: " + vehical2.ambientAirTemperature);
                Console.WriteLine("Cars distance With Malfunction Light: " + vehical2.distanceWithMalfunctionLight);
            }
        }
    }
}