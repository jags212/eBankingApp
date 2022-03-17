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
         public static string applyLoan(IWebDriver driver,
            string fullyQualifiedUrlLoan,LoanDetails loanDetails)
        {
            var getHashCode = string.Empty;
            try
            {
                driver.Navigate().GoToUrl(fullyQualifiedUrlLoan);
                driver.FindElement(By.Id("fname")).SendKeys(loanDetails.firstName);
                driver.FindElement(By.Id("lname")).SendKeys(loanDetails.lastName);
                driver.FindElement(By.Id("email")).SendKeys(loanDetails.email);

                SelectElement oSelect = new SelectElement(driver.FindElement(By.Id("loanType")));
                oSelect.SelectByValue(loanDetails.loanType);
                oSelect.SelectByText(loanDetails.loanType);

                driver.FindElement(By.Id("loanDuration")).SendKeys(Convert.ToString(loanDetails.loanDuration));

                driver.FindElement(By.Id("submit")).Click();
                getHashCode = driver.FindElement(By.Id("hashCodeValue")).Text;

            }
            catch (Exception ex)
            {
                getHashCode = ex.Message;
            }
            
            return getHashCode;
        }
    }
}
