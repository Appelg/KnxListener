using Knx.Bus.Common;
using Knx.Bus.Common.Configuration;
using Knx.Bus.Common.GroupValues;
using Knx.Falcon.Sdk;

namespace KnxListener
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var serverIp = args[0]; // 192.168.42.232
            ushort port = 0x0e57;

            Console.WriteLine($"Using serverip: {serverIp} : {port}");
            using var bus = new Bus(new KnxIpTunnelingConnectorParameters(serverIp, 0x0e57, false));
            bus.Connect();
            bus.GroupValueReceived += args =>
            {
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: GroupValueReceived: " + args.IndividualAddress + " Value: " + args.Value + " Address:" + args.Address);
            };
            bus.GroupValueReadReceived += args =>
            {
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: GroupValueReadReceived: " + args.IndividualAddress + " Value: " + args.Value + " Address:" + args.Address);
            };
            bus.StateChanged += args =>
            {
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: StateChanged: " + args);
            }; ;

            if (args.Length > 2)
            {
                var groupAddress = args[1];
                var writeVal = int.Parse(args[2]);
                Console.WriteLine($"Writing value {writeVal} to group address {groupAddress}");
                bus.WriteValue(new GroupAddress("0/0/1"), new GroupValue(new TwoBit(writeVal)));
            }

            while (true)
            {
                await Task.Delay(5000);
            }
        }

    }
}
