using BoDi;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using SpecFlowProjectDemo.Utils;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports;

namespace SpecFlowProjectDemo.Features.Hooks
{
    [Binding]
    public sealed class Hooks: ExtentReport
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        // private ScenarioContext _scenarioContext;

        // public Hooks(ScenarioContext scenarioContext)
        // {
        //      _scenarioContext = scenarioContext;
        // }
        private readonly IObjectContainer _container;
        public Hooks(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            ExtentReportInit();
        }

        [AfterTestRun] 
        public static void AfterTestRun()
        {
            ExtentReportDearDown();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            _feature = _extentReports.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario("@tester1")]
        public void BeforeScenarioWithTag()
        {
            Console.WriteLine("Tester Tag is getting Executed");
        }

        [BeforeScenario(Order = 1)]
        public void FirstBeforeScenario(ScenarioContext scenarioContext)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            _container.RegisterInstanceAs<IWebDriver>(driver);

            _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var driver = _container.Resolve<IWebDriver>();
            if(driver != null)
            {
                driver.Quit();
            }
            
        }

        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            string stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            string stepName = scenarioContext.StepContext.StepInfo.Text;

            if(scenarioContext.TestError == null)
            {
                if(stepType == "Given")
                {
                    _scenario.CreateNode<Given>(stepName);
                }
                if (stepType == "When")
                {
                    _scenario.CreateNode<When>(stepName);
                }
                if (stepType == "And")
                {
                    _scenario.CreateNode<And>(stepName);
                }
                if (stepType == "Then")
                {
                    _scenario.CreateNode<Then>(stepName);
                }
            }

            if (scenarioContext.TestError != null)
            {
                var driver = _container.Resolve<IWebDriver>();
                addScreenshot(driver, scenarioContext);

                if (stepType == "Given")
                {
                    _scenario.CreateNode<Given>(stepName).Fail(scenarioContext.TestError.Message, 
                        MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build());
                }
                if (stepType == "When")
                {
                    _scenario.CreateNode<When>(stepName).Fail(scenarioContext.TestError.Message,
                        MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build());
                }
                if (stepType == "And")
                {
                    _scenario.CreateNode<And>(stepName).Fail(scenarioContext.TestError.Message,
                        MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build());
                }
                if (stepType == "Then")
                {
                    _scenario.CreateNode<Then>(stepName).Fail(scenarioContext.TestError.Message,
                        MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build());
                }
            }
        }
    }
}