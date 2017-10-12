using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace ConsoleApp1
{
    class Program
    {
        public static Func<IWebDriver, bool> WaitingToDisplay(By by)
        {
            Func<IWebDriver, bool> wait = a =>
            {
                try
                {
                    return a.FindElement(by).Displayed;
                }
                catch (NoSuchElementException e)
                {
                    return false;
                }
            };

            return wait;

        }
        static void Main(string[] args)
        {
            using (IWebDriver webdriver = new ChromeDriver())
            {
                try

                {
                    WebDriverWait wait = new WebDriverWait(webdriver, TimeSpan.FromSeconds(5));
                    webdriver.Navigate().GoToUrl("https://www.schwab.com/public/schwab/nn/login/login.html&lang=en");
                    var loginIFrame = webdriver.FindElement(By.Id("loginIframe"));
                    webdriver.SwitchTo().Frame(loginIFrame);

                    webdriver.FindElement(By.Id("LoginId")).SendKeys("puzhadfdnila");
                    webdriver.FindElement(By.Id("Password")).SendKeys("dsfdsfds");
                    webdriver.FindElement(By.Id("LoginSubmitBtn")).SendKeys(Keys.Enter);
                    
                    wait.Until(WaitingToDisplay(By.LinkText("Trade")));

                    webdriver.FindElement(By.LinkText("Trade")).Click();

                    wait.Until(WaitingToDisplay(By.Id("txtSym_0")));

                    webdriver.FindElement(By.Id("txtSym_0")).SendKeys("AMD");
                    webdriver.FindElement(By.Id("ddlAction_0")).SendKeys("Buy");
                    webdriver.FindElement(By.Id("txtQty_0")).SendKeys("30");
                    webdriver.FindElement(By.Id("ddlType_0")).SendKeys("Market Order");
                   
                    //webdriver.FindElement(By.Id("ddlTiming_0")).SendKeys("Day Only");
                    webdriver.FindElement(By.Id("btnReview")).SendKeys(Keys.Enter);

                    wait.Until(WaitingToDisplay(By.CssSelector("li.logout button")));

                    webdriver.FindElement(By.CssSelector("li.logout button")).SendKeys(Keys.Enter);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    webdriver.Close();
                    webdriver.Quit();
                }
                
            }
        }
    }
}
