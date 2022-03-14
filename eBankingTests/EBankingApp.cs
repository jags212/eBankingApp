using eBankingApp.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBankingTests
{
    public  class EBankingApp
    {
        public static int applyLoan(IWebDriver driver,
            string fullyQualifiedUrlLoan, string fullyQualifiedUrlSuccessLoan, LoanDetails loanDetails)
        {
            driver.Navigate().GoToUrl(fullyQualifiedUrlLoan);
            
            driver.FindElement(By.Id("fname")).SendKeys(loanDetails.firstName);
            driver.FindElement(By.Id("lname")).SendKeys(loanDetails.lastName);
            driver.FindElement(By.Id("email")).SendKeys(loanDetails.email);
            driver.FindElement(By.Id("loanType")).SendKeys(loanDetails.loanType);
            driver.FindElement(By.Id("loanDuration")).SendKeys(Convert.ToString(loanDetails.loanDuration));

            driver.Navigate().GoToUrl(fullyQualifiedUrlSuccessLoan);

            var getHashCode = Convert.ToInt32(driver.FindElement(By.Id("hashCodeValue")).Text);
            return getHashCode;
        }
    }
}
