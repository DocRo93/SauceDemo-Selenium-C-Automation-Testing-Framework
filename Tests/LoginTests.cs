//using System.ComponentModel;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using NUnit.Framework;
using SauceDemo.Automation.Core;
using SauceDemo.Automation.Pages;
namespace SauceDemo.Automation.Tests;
[AllureNUnit]
[AllureEpic("SauceDemo Web UI")]
public sealed class LoginTests : BaseTest
{
    [Test]
    [Category("Smoke")]
    [AllureFeature("Authentication")]
    [AllureStory("Valid login")]
    public void ValidUserCanLogin()
    {
        var inventory = new LoginPage(DriverContext.Driver).Open(Settings.BaseUrl).LoginAs(Settings.Username,Settings.Password);
        Assert.That(inventory.IsLoaded(),Is.True,"Inventory page was not displayed after login.");
    }


    [Test]
    [TestCase ("standard_user", "wrong_password")]
    [TestCase("wrong_username", "secret_sauce")]
    public void UserLoginWithInvalidDetails(string username, string password)
    {
        var loginPage = new LoginPage(DriverContext.Driver);
        loginPage.Open(Settings.BaseUrl);
        loginPage.LoginAs(username, password);

        Assert.That(loginPage.GetErrorMessage(), Does.Contain("do not match"));
    }

    

    [Test]
    public void UserLoginWithMaxUsernameLength()
    {
        string longUsername = new string('a', 500);

        var loginPage = new LoginPage(DriverContext.Driver);
        loginPage.Open(Settings.BaseUrl);
        loginPage.LoginAs(longUsername, Settings.Password);

        Assert.That(loginPage.GetErrorMessage(), Does.Contain("do not match"));
    }

}
