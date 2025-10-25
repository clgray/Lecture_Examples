using System;
using System.IO;
using System.IO.Compression;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace Implementation.Decorator
{
    public interface IBike
    {
        string GetDetails();
        double GetPrice();
    }

    public class AluminiumBike : IBike
    {
        public double GetPrice()
        {
            return 100;
        }

        public string GetDetails()
        {
            return "Aluminium Bike";
        }
    }

    public class CarbonBike : IBike
    {
        public double GetPrice()
        {
            return 1000;
        }

        public string GetDetails()
        {
            return "Carbon";
        }
    }


    public abstract class BikeAccessories : IBike
    {
        private readonly IBike _bike;

        public BikeAccessories(IBike bike)
        {
            _bike = bike;
        }

        public virtual double GetPrice()
        {
            return _bike.GetPrice();
        }

        public virtual string GetDetails()
        {
            return _bike.GetDetails();
        }
    }

    public class SecurityPackage : BikeAccessories
    {
        public SecurityPackage(IBike bike) : base(bike)
        {

        }

        public override string GetDetails()
        {
            return base.GetDetails() + " + Security Package";
        }

        public override double GetPrice()
        {
            return base.GetPrice() + 1;
        }

    }

    public class SportPackage : BikeAccessories
    {
        public SportPackage(IBike bike) : base(bike)
        {

        }

        public override string GetDetails()
        {
            return base.GetDetails() + " + Sport Package";
        }

        public override double GetPrice()
        {
            return base.GetPrice() + 10;
        }
    }

    public class BikeShop
    {
        public static void UpgradeBike()
        {
            var basicBike = new AluminiumBike();
            IBike upgraded = new SecurityPackage(basicBike);
            upgraded = new SportPackage(upgraded);
            upgraded = new SportPackage(upgraded);
            upgraded = new SportPackage(upgraded);
            upgraded = new SportPackage(upgraded);
            upgraded = new SportPackage(upgraded);
            upgraded = new SecurityPackage(upgraded);


            Console.WriteLine($"Bike: '{upgraded.GetDetails()}' Cost: {upgraded.GetPrice()}");

        }
        public static void StreamTest()
        {
	        var stream = new MemoryStream();
	        var gzipStream = new GZipStream(stream, CompressionLevel.Fastest);
	        var buffered = new BufferedStream(gzipStream);
	        var cryto = new CryptoStream(buffered,
		        new FromBase64Transform(FromBase64TransformMode.DoNotIgnoreWhiteSpaces), CryptoStreamMode.Write);
            //var net = new NetworkStream()
        }
    }
}