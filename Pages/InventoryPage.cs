using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
namespace SauceDemo.Automation.Pages;
public sealed class InventoryPage : BasePage
{
    [FindsBy(How = How.CssSelector, Using = ".title")] private IWebElement? Title { get; set; }
    
    //Sorting elements
    [FindsBy(How = How.CssSelector, Using = ".product_sort_container")] private IWebElement? SortDropdown { get; set; }
    [FindsBy(How = How.CssSelector, Using = ".inventory_item_name")] private IList<IWebElement>? ProductNames { get; set; }
    [FindsBy(How = How.CssSelector, Using = ".inventory_item_price")] private IList<IWebElement>? ProductPrices { get; set; }

    public InventoryPage(IWebDriver driver) : base(driver) => PageFactory.InitElements(driver, this);
    public bool IsLoaded() { Wait.UrlContains("inventory.html"); return Title!.Displayed && Title.Text == "Products"; }

    public void SortBy (string value)
    {
        var select = new SelectElement(SortDropdown);
        select.SelectByValue(value);
    }

    public List<string> GetProductNames() => ProductNames!.Select(e => e.Text).ToList();

    public List<decimal> GetProductPrices() =>
        ProductPrices!.Select(e => decimal.Parse(e.Text.Replace("$", ""))).ToList();



    public bool IsAlphabeticalAZ(List<string> itemsList)
    {
        List<string> sortedCopy = new List<string>(itemsList);
        sortedCopy.Sort();
        return itemsList.SequenceEqual(sortedCopy);

    }

    public bool IsAlphabeticalZA(List<string> itemsList)
    {
        List<string> sortedCopy = new List<string>(itemsList);
        sortedCopy.Sort();
        sortedCopy.Reverse();
        return itemsList.SequenceEqual(sortedCopy);

    }


}
