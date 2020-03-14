using Assemble.me.Library.Parts;
using Assemble.me.Library.Parts.PackageAccumulator;
using Assemble.me.Library.Parts.PackageChassis;
using Assemble.me.Library.Parts.PackageEngine;
using Assemble.me.Library.Parts.PackageExtra;
using Assemble.me.Library.Parts.PackageInterrior;
using Assemble.me.Library.Parts.PackageRims;
using Assemble.me.Library.Parts.PackageSuspension;
using Assemble.me.Library.Parts.PackageTires;
using Assemble.me.Library.Parts.PackageTransmission;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Configuration;

namespace Assemble.me.Library
{
    public class ApplicationSettings
    {
        #region MySqlConnection
        /// <summary>
        /// MySQL connections with provided credentials for database communication.
        /// </summary>
        /// <returns>MySQL connection instance.</returns>
        public static MySqlConnection GetConnection()
        {

            return new MySqlConnection("Server=studmysql01.fhict.local;Uid=dbi334307;Database=dbi334307;Pwd=assembleMe;");
        }
        #endregion


        /// <summary>
        /// Gets a static, readonly, list containing all car parts.
        /// </summary>
        /// <returns>List of car parts.</returns>
        public static List<CarPart> GetAllParts()
        {
            return allParts;
        }

        /// <summary>
        /// Method that gets a single part by its id.
        /// </summary>
        /// <param name="id"> Represents the id of the part that needs to be found</param>
        /// <returns> Returns a Car part object</returns>
        public static CarPart GetPartById(int id)
        {
            foreach (CarPart part in allParts)
            {
                if (part.GetId() == id)
                {
                    return part;
                }
            }
            return null;
        }

        public static readonly string[] mainItems = { "Chassis", "Engine", "Accumulator", "Suspension", "Tires", "Rims", "Transmission", "Interior", "Extras" };

        /// <summary>
        /// Creates a List containing all car parts. This list facilitates the communication between all classes.
        /// </summary>
        private static readonly List<CarPart> allParts = new List<CarPart>()
        {
            new Cabrio(),
            new Jeep(),
            new SportCar(),
            new Sedan(),
            new Minivan(),
            new DieselEngine(),
            new PetrolEngine(),
            new ElectricEngine(),
            new AirSuspension(),
            new GasSuspension(),
            new HydraulicSuspension(),
            new Tires(Inches.Eighteen),
            new Tires(Inches.Fourteen),
            new Tires(Inches.Sixteen),
            new Rims(new Tires(Inches.Eighteen)),
            new Rims(new Tires(Inches.Fourteen)),
            new Rims(new Tires(Inches.Sixteen)),
            new ManualTransmission(),
            new AutomaticTransmission(),
            new Accumulator(AccumulatorSize.Small),
            new Accumulator(AccumulatorSize.MegaPower),
            new Accumulator(AccumulatorSize.Average),
            new TextileInterior(),
            new LeatherInterior(),
            new _4x4(),
            new AC(),
            new ButtonStarter(),
            new CruiseControl(),
            new Parktronic(),
            new AudioSystem(),
            new GPS()
        };

    }
}