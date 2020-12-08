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
    public class FileInventoryProviderUnitTests
    {
        private FileInventoryProvider fileInvenotryProvider;

        [TestMethod]
        public void TestThatCitiesAreReadAsExpectedFromFile()
        {
            var fileName = "inventoryTest.txt";
            var fileContent = GetFileContent();
            File.WriteAllText(fileName, fileContent);
            var expectedCities = GetCities();

            fileInvenotryProvider = new FileInventoryProvider(fileName);

            var comparer = new CompareLogic();
            Assert.IsTrue(comparer.Compare(expectedCities, fileInvenotryProvider.Inventory).AreEqual);
        }

        private string GetFileContent()
        {
            return @"Item,Lead time (weeks),Annual demand (units),Average unit cost ($),Class
1,7,12,86.5,A
2,7,2,134.34,A
3,7,27,7.07,B
4,7,1,34.4,C";
        }

        private List<Item> GetCities()
        {
            return new List<Item>
            {
                new Item { Identifier = "1", LeadTime = 7, AnnualDemand = 12, AverageUnitCost = 86.5, Class = ItemClass.A },
                new Item { Identifier = "2", LeadTime = 7, AnnualDemand = 2, AverageUnitCost = 134.34, Class = ItemClass.A },
                new Item { Identifier = "3", LeadTime = 7, AnnualDemand = 27, AverageUnitCost = 7.07, Class = ItemClass.B },
                new Item { Identifier = "4", LeadTime = 7, AnnualDemand = 1, AverageUnitCost = 34.4, Class = ItemClass.C },
            };
        }
    }
}
