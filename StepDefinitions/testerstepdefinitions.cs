using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowProjectDemo.StepDefinitions
{
    [Binding]
    public sealed class testerstepdefinitions
    {
        private IWebDriver driver;

        public testerstepdefinitions(IWebDriver driver)
        {
            this.driver = driver;
        }

        [Given(@"open the Web browser")]
        public void GivenOpenTheWebBrowser()
        {
            
        }

        [When(@"enter the url")]
        public void WhenEnterTheUrl()
        {
            //driver.Url = "https://www.youtube.com/";
            driver.Url = "https://glamz.com/";
            Thread.Sleep(5000);
        }

        [Then(@"search for the testers")]
        public void ThenSearchForTheTesters()
        {
            //driver.FindElement(By.XPath("//*[@name='earch_query']")).SendKeys("tester");
            //driver.FindElement(By.XPath("//*[@name='earch_query']")).SendKeys(Keys.Enter);
            driver.FindElement(By.Id("small-searchtermsm1")).SendKeys("hi");
            driver.FindElement(By.Id("small-searchtermsm1")).SendKeys(Keys.Enter);
            Thread.Sleep(1000);
        }

        [Then(@"search the (.*)")]
        public void ThenSearchForTheHi(string searchKey)
        {
            driver.FindElement(By.Id("small-searchtermsm1")).SendKeys(searchKey);
            driver.FindElement(By.Id("small-searchtermsm1")).SendKeys(Keys.Enter);
            Thread.Sleep(1000);
        }

        [Then(@"search given <searchKey>")]
        public void ThenSearchGivenSearchKey(Table table)
        {
            var searchTable = table.CreateSet<searchData>();
            foreach (var item in searchTable)
            {
                driver.FindElement(By.Id("small-searchtermsm1")).Clear();
                driver.FindElement(By.Id("small-searchtermsm1")).SendKeys(item.searchKey);
                driver.FindElement(By.Id("small-searchtermsm1")).SendKeys(Keys.Enter);
                Thread.Sleep(1000);
                driver.Navigate().Back();
                Thread.Sleep(1000);
            }
        }

    }
}
public class searchData
{
    public string searchKey { get; set; }
}