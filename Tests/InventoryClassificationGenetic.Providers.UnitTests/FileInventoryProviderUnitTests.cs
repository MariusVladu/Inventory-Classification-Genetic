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
            return @"Item,Annual Dollar Usage, Number Of Requests Per Year, Unit Price
R12,8910,2700,0.33,A
R13,7520,4700,1.6,A
R16,10304,3200,3.22,B
R20,26600,20000,1.33,C";
        }

        private List<Item> GetCities()
        {
            return new List<Item>
            {
                new Item { Identifier = "R12", AnnualDollarUsage = 8910, NumberOfRequestsPerYear = 2700, UnitPrice = 0.33, Class = ItemClass.A },
                new Item { Identifier = "R13", AnnualDollarUsage = 7520, NumberOfRequestsPerYear = 4700, UnitPrice = 1.6, Class = ItemClass.A },
                new Item { Identifier = "R16", AnnualDollarUsage = 10304, NumberOfRequestsPerYear = 3200, UnitPrice = 3.22, Class = ItemClass.B },
                new Item { Identifier = "R20", AnnualDollarUsage = 26600, NumberOfRequestsPerYear = 20000, UnitPrice = 1.33, Class = ItemClass.C },
            };
        }
    }
}
