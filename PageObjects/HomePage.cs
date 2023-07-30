using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProjectDemo.PageObjects
{
    public class HomePage
    {
        private IWebDriver driver;
        public HomePage(IWebDriver driver) { 
            this.driver = driver;
        }
        By searchBySalonOrService = By.Id("small-searchtermsm1");
        By searchByLocation = By.Id("small-searchtermsm2");
        By searchButton = By.CssSelector("form[id='small-search-box-form1'] button[type='submit']");

        public void EnterSalonOrService(string salonName)
        {
            driver.FindElement(searchBySalonOrService).SendKeys(salonName);
        }
        public void EnterLocation(string locationName)
        {
            driver.FindElement(searchByLocation).SendKeys(locationName);
        }
        public void ClickOnSearchButton()
        {
            driver.FindElement(searchButton).Click();
        }
        public void SearchSalonWithLocation(string salonName, string locationName)
        {
            EnterSalonOrService(salonName);
            EnterLocation(locationName);
            ClickOnSearchButton();
        }
        public void SearchSalonWithoutLocation(string salonName)
        {
            EnterSalonOrService(salonName);
            ClickOnSearchButton();
        }
        public void SearchSalonWithoutSalonName(string locationName)
        {
            EnterLocation(locationName);
            ClickOnSearchButton();
        }
    }
}
