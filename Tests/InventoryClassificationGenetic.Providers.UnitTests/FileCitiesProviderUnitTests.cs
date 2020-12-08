using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using InventoryClassificationGenetic.Domain;

namespace InventoryClassificationGenetic.Providers.UnitTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class FileCitiesProviderUnitTests
    {
        private FileCitiesProvider fileCitiesProvider;

        [TestMethod]
        public void TestThatCitiesAreReadAsExpectedFromFile()
        {
            var fileName = "citiesTest.txt";
            var fileContent = GetFileContent();
            File.WriteAllText(fileName, fileContent);
            var expectedCities = GetCities();

            fileCitiesProvider = new FileCitiesProvider(fileName);

            var comparer = new CompareLogic();
            Assert.IsTrue(comparer.Compare(expectedCities, fileCitiesProvider.Cities).AreEqual);
        }

        private string GetFileContent()
        {
            return @" -0.307996E-04 -0.186857E-04
  -14.8058     -0.373991E-04
  -45.6755      -9.74380    
  -30.6703      -2.02336    
   12.5209      -13.3649    
  -46.3966      -9.17876    
  -34.7018       13.4900    
  -6.15704       2.20589    
  -8.02244      -1.35290    
  -27.9627      -17.0577    
   9.95119      -6.27887  ";
        }

        private List<City> GetCities()
        {
            return new List<City>
            {
                new City { X = -3.07996E-05, Y = -1.86857E-05},
                new City { X = -14.8058, Y = -3.73991E-05},
                new City { X = -45.6755, Y = -9.74380},
                new City { X = -30.6703, Y = -2.02336},
                new City { X = 12.5209 , Y = -13.3649},
                new City { X = -46.3966, Y = -9.17876},
                new City { X = -34.7018, Y = 13.4900},
                new City { X = -6.15704, Y = 2.20589 },
                new City { X = -8.02244, Y = -1.35290},
                new City { X = -27.9627, Y = -17.0577},
                new City { X = 9.95119, Y = -6.27887},
            };
        }
    }
}
