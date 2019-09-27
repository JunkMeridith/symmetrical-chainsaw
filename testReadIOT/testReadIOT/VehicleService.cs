using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Azure.Devices;
using Newtonsoft.Json;
namespace testReadIOT
{
    public class VehicleService
    {
        static RegistryManager registryManager;


        public VehicleService()
        {
        }

        static Random randomNumberGenerator = new Random();


        public async Task StartAsync()
        {
            (await getVehiclesAsync()).Select(v => Task.Run(() => simulation_loop(v))).ToList();
        }

        private async Task simulation_loop(Vehicle vehicle)
        {
            var list = await getVehiclesAsync();
            var newVehicle = list.Where(x => x.vin == vehicle.vin).FirstOrDefault();
            vehicle = newVehicle;
        }


        public event EventHandler<Vehicle> VehicleUpdated;

        public async Task<IEnumerable<Vehicle>> getVehiclesAsync()
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
            return list;

        }

        public async Task<FleetStatus> HealthSummary()
        {
            var list = await getVehiclesAsync();
            return new FleetStatus
            {
                ActiveBuses = list.Count(),
                TotalIncidents = list.Count(x => x.speed > 60),
                MaintenancesRequest = 51
            };
        }

    }
}
