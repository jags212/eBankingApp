using eBankingApp;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;
using eBankingApp.Models;
using eBankingApp.Services;

namespace eBankingTests
{
    [TestClass]
    public class IntegrationTestFixtures
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
        }

        [TestCleanup]
        public void OnTestCleanup()
        {
            _systemUnderTest?.Dispose();
        }

        private CustomWebApplicationFactory<Startup> _systemUnderTest;
        public CustomWebApplicationFactory<Startup> SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest = new CustomWebApplicationFactory<Startup>();
                }

                return _systemUnderTest;
            }
        }

        private LoanService _instance;
        private LoanService GetInstanceOfAnotherUsefulService(IServiceProvider arg)
        {
            if (_instance == null)
            {
                _instance = new LoanService();
            }

            return _instance;
        }

        [TestMethod]
        public void CallHomePage_UsingSeleniumAndWebApplicationFactory()
        {
            var expectedText = "Fill Loan Details";

            var url = "home/index";
            var fullyQualifiedUrl = 
                SystemUnderTest.GetServerAddressForRelativeUrl(url);

            var driverOptions = new ChromeOptions();

            driverOptions.AddArgument("headless");

            string driverPath = Environment.CurrentDirectory;
            using var driver = new ChromeDriver(driverPath.Split("bin")[0].ToString(),driverOptions);

            // act
            Console.WriteLine($"Navigating to '{fullyQualifiedUrl}...'");
            driver.Navigate().GoToUrl(fullyQualifiedUrl);

            // assert
            AssertDivExistsAndContainsText(expectedText, driver, "headerValue");            
        }

        private void InitializeWithTypeReplacements()
        {
            _systemUnderTest = new CustomWebApplicationFactory<Startup>(addDevelopmentConfigurations: builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<ILoanService, LoanService>();
                    AssertTypeIsRegistered<ILoanService>(services);

                    services.RemoveAll<ILoanService>();
                    services.RemoveAll<LoanService>();

                    AssertTypeIsNotRegistered<ILoanService>(services);

                    services.AddSingleton<ILoanService, LoanService>(GetInstanceOfAnotherUsefulService);
                });
            });

            Console.WriteLine($"InitializeWithTypeReplacements(): TestServer is null = {(_systemUnderTest.TestServer == null)}");
        }

        private static void AssertTypeIsRegistered<T>(IServiceCollection services)
        {
            if (services is ServiceCollection asServiceCollection)
            {
                var match = (from temp in asServiceCollection
                             where temp.ServiceType == typeof(T)
                             select temp).FirstOrDefault();

                Assert.IsNotNull(match, "Type should be registered.");
            }
            else
            {
                Assert.Fail("Problem checking type registration");
            }
        }

        private static void AssertTypeIsNotRegistered<T>(IServiceCollection services)
        {
            if (services is ServiceCollection asServiceCollection)
            {
                var match = (from temp in asServiceCollection
                             where temp.ServiceType == typeof(T)
                             select temp).FirstOrDefault();

                Assert.IsNull(match, "Type should not be registered.");
            }
            else
            {
                Assert.Fail("Problem checking type registration");
            }
        }

        [TestMethod]
        public void testEmptyLoanDetails()
        {
            InitializeWithTypeReplacements();

            var driverOptions = new ChromeOptions();

            driverOptions.AddArgument("headless");

            string driverPath = Environment.CurrentDirectory;
            using var driver = new ChromeDriver(driverPath.Split("bin")[0].ToString(), driverOptions);

            LoanDetails loanDetails = new LoanDetails()
            {
                firstName ="",
                lastName="",
                email ="",
                loanType="",
                loanDuration = new Random().Next(100)
            };

            // Insert Logic to Assert the expected and Actual HashCode Value

            Assert.AreEqual(0, 1); // Modify
        }

        [TestMethod]
        public void testCorrectLoanDetails()
        {
            InitializeWithTypeReplacements();

            var driverOptions = new ChromeOptions();

            driverOptions.AddArgument("headless");

            string driverPath = Environment.CurrentDirectory;
            using var driver = new ChromeDriver(driverPath.Split("bin")[0].ToString(), driverOptions);

            LoanDetails loanDetails = new LoanDetails()
            {
                firstName = "Hacker",
                lastName = "Rank",
                email = "h@r.com",
                loanType = "Commercial",
                loanDuration = new Random().Next(7) + 1
            };

            // Insert Logic to Assert the expected and Actual HashCode Value

            Assert.AreEqual(0, 1); // Modify
        }

        [TestMethod]
        public void testIncorrectDetails()
        {
            InitializeWithTypeReplacements();

            var driverOptions = new ChromeOptions();

            driverOptions.AddArgument("headless");

            string driverPath = Environment.CurrentDirectory;
            using var driver = new ChromeDriver(driverPath.Split("bin")[0].ToString(), driverOptions);

            LoanDetails loanDetails = new LoanDetails()
            {
                firstName = "Hacker",
                lastName = "Rank",
                email = "hacker@r",
                loanType = "Test",
                loanDuration = new Random().Next(100) + 1
            };

           // Insert Logic to Assert the expected and Actual HashCode Value

            Assert.AreEqual(0, 1); // Modify
        }

        private static void AssertDivExistsAndContainsText(string expectedText, ChromeDriver driver, string id)
        {
            var element = driver.FindElement(By.Id(id));

            Assert.IsNotNull(element, $"element '{id}' should not be null");
            Assert.IsTrue(element.Displayed, $"element '{id}' should be displayed");
            Assert.IsTrue(element.Enabled, $"element '{id}' should be enabled");

            Assert.IsTrue(element.Text.Contains(expectedText), 
                $"element '{id}' should contain expected text. Actual: '{element.Text}'");
        }        
    }
}
