using System;

namespace AAUT
{
    class Program
    {
        static void Main(string[] args)
        {
            var car = new Car();
            car.DriveForward();
        }
    }

    class Car
    {
        public Engine Engine { get; private set; }
        public Transmission Transmission { get; private set; }

        public bool Running => Engine.Running;

        public Car()
        {
            Engine = new Engine();
            Transmission = new Transmission();
        }

        public void TurnOn()
        {
            Engine.Start();

            if (!Engine.Running) throw new Exception($"The engine failed to start!");
        }

        public void TurnOff()
        {
            Engine.Stop();

            if (Engine.Running) throw new Exception($"The engine is still running!");
        }

        public void ShiftIntoDrive()
        {
            Transmission.SelectGear(Transmission.Gear.Drive);
        }

        public void ShiftIntoReverse()
        {
            Transmission.SelectGear(Transmission.Gear.Reverse);
        }

        public void DriveForward()
        {
            Transmission.SelectGear(Transmission.Gear.Drive);
            Engine.SetRPM(2500);
        }
    }

    class Engine
    {
        private const int MIN_RPM = 750;
        private const int MAX_RPM = 6500;

        public bool Running { get; private set; }
        public int RPM { get; private set; }
        public int MaxRPM => MAX_RPM;
        public int MinRPM => MIN_RPM;

        public void SetRPM(int desiredRPM)
        {
            if (!Running) throw new InvalidOperationException("The engine is not running.");
            if (desiredRPM < MIN_RPM) throw new ArgumentException("Specified RPM is lower than minimum", nameof(desiredRPM));
            if (desiredRPM > MAX_RPM) throw new ArgumentException("Specified RPM is higher than maximum", nameof(desiredRPM));

            RPM = desiredRPM;
        }

        public void Start()
        {
            if (Running) throw new InvalidOperationException("The engine is already running.");

            Running = true;
        }

        public void Stop()
        {
            if (!Running) throw new InvalidOperationException("The engine is not running.");

            Running = false;
        }
    }

    public class Transmission
    {
        public Gear SelectedGear { get; private set; }

        public void SelectGear(Gear desiredGear)
        {
            SelectedGear = desiredGear;
        }

        public enum Gear
        {
            Park,
            Neutral,
            Drive,
            Reverse
        }
    }
}
