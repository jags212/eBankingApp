## Environment:
- .NET version: 5
- Selenium WebDriver: 4.78


## Requirements:
In this challenge, you are going to use selenium Chrome web driver, which uses HtmlUnit headless browser. So you neither need to setup the browsers like Firefox, Chrome nor a web driver executables like FirefoxDriver, ChromeDriver.
You are given a dummy EBanking website which a has online loan application facility. You have to test this loan application page by filling the form. You are provided the data to filled for loan application.

There is a class `EBankingApp` which has a single method:
 
`String applyLoan(IWebDriver driver,
            string fullyQualifiedUrlLoan)`:
 - browse the `loanPageUrl` and fill all the fields using loanDetails model object.
 - its source code structure is like `website/loanPage.html`.
 - upon form submission, server redirects you to another page containing a secret code in the body tag.
 - source code structure of redirecting page is like `website/successPage.html`
 - return that secret code.
 
where the `loanDetails` is the model class which has all the form data to be filled.

There are tests for testing correctness of each methods. So you can make use of these tests while debugging/checking your implementation.
The test's setup method bootstraps an embedded jetty server and deploys small web app which displays randomly generated website. 
The example website is given in the `website` folder where you can view the structure of search page and result page but data displayed are random and will change on every refresh.

The provided vulnerabilities page will look like: 

![web page](loanPage.png)

Your task is to complete the implementation of `EBankingApp` so that the unit tests pass while running the tests.

## Commands
- run: 
```bash
dotnet build
```
- install: 
```bash
dotnet install
```
- test: 
```bash
dotnet test
```
