//using System.ComponentModel;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using NUnit.Framework;
using SauceDemo.Automation.Core;
using SauceDemo.Automation.Pages;
namespace SauceDemo.Automation.Tests;

[AllureNUnit]
[AllureEpic("SauceDemo Web UI")]
public sealed class InventoryTests : BaseTest
{


    [Category("Smoke")]
    [AllureFeature("Inventory")]
    [AllureStory("Sorting Items Alphabetically")]
    [TestCase ("az")]
    [TestCase("za")]
    public void UserSortsInventoryListAlphabetically(string listOrder)
    {
        
        var loginPage = new LoginPage(DriverContext.Driver);
        loginPage.Open(Settings.BaseUrl);
        loginPage.LoginAs(Settings.Username, Settings.Password);
        var inventoryPage = new InventoryPage(DriverContext.Driver);
        inventoryPage.SortBy(listOrder);
        //lohi
        //hilo
        List<string> itemsList = inventoryPage.GetProductNames();
        
        if (listOrder == "az")
        {
            Assert.That(inventoryPage.IsAlphabeticalAZ(itemsList), Is.EqualTo(true));
        } else if (listOrder == "za") {
            Assert.That(inventoryPage.IsAlphabeticalZA(itemsList), Is.EqualTo(true));
        }
        
    }

}