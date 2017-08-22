using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.IO;
using System.Net;
using System.Data.SqlClient;
namespace WCTests.Tests
{

    class user_test
    {
        FirefoxDriver webDriver;
        public bool WaitUntilElementIsPresent(String selector)
        {
            WebDriverWait wait = new WebDriverWait(this.webDriver, TimeSpan.FromSeconds(10));
            IWebElement myDynamicElement = wait.Until<IWebElement>((d) =>
            {
                return d.FindElement(By.CssSelector(selector));
            });
            return false;
        }

        [SetUp]
        public void InitializeBrowser()
        {
            webDriver = new FirefoxDriver();
            //webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }
        [Test]
        public void login()
        {

            SqlConnection connection = new System.Data.SqlClient.SqlConnection("Server= localhost; Database= webconfigurator;Integrated Security = SSPI; ");
            connection.Open();
            String sql = "DELETE FROM dbo.UserAccountUserGroup WHERE UserID != 1";
            SqlCommand command = new SqlCommand(sql, connection);
            command.ExecuteNonQuery();
            command.Dispose();
            sql = "DELETE FROM dbo.UserAccount WHERE Id != 1";
            command = new SqlCommand(sql, connection);
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();

            webDriver.Navigate().GoToUrl("http://localhost:65274/");
            WaitUntilElementIsPresent("body > div.container > div > ui-view > form > div:nth-child(2) > input");
            IWebElement login1_field = webDriver.FindElement(By.CssSelector("body > div.container > div > ui-view > form > div:nth-child(2) > input"));
            IWebElement password1_field = webDriver.FindElement(By.CssSelector("body > div.container > div > ui-view > form > div:nth-child(3) > input"));
            IWebElement submit1_button = webDriver.FindElement(By.CssSelector("body > div.container > div > ui-view > form > button"));
            login1_field.SendKeys("admin");
            password1_field.SendKeys("password");
            submit1_button.Submit();
            WaitUntilElementIsPresent("#sidebarMenu > div > ul > li:nth-child(6)");
            IWebElement administracja = webDriver.FindElement(By.CssSelector("#sidebarMenu > div > ul > li:nth-child(6)"));
            administracja.Click();
            IWebElement uzytkownicy = webDriver.FindElement(By.CssSelector("#sidebarMenu > div > ul > li.active > ul > li:nth-child(1)"));
            uzytkownicy.Click();

            Dictionary<String, Boolean> data = new Dictionary<String, Boolean>();
            data["im"] = true;
            data["your"] = false;
            data["daddy"] = true;
            foreach (KeyValuePair<String, Boolean> entry in data)
            {
                //IWebElement = webDriver.FindElement(By.CssSelector(""));
                WaitUntilElementIsPresent("body > div.body > div.main-view > div.container-fluid > ui-view > default-list-view > view-header > div > div > div > div > ng-transclude > div:nth-child(1) > button:nth-child(4)");

                IWebElement dodaj = webDriver.FindElement(By.CssSelector("body > div.body > div.main-view > div.container-fluid > ui-view > default-list-view > view-header > div > div > div > div > ng-transclude > div:nth-child(1) > button:nth-child(4)"));
                dodaj.Click();

                WaitUntilElementIsPresent("body > div.body > div.main-view > div.container-fluid > ui-view > ng-form > div > div > form-group:nth-child(1) > div > div > ng-transclude > input");

                IWebElement login_field = webDriver.FindElement(By.CssSelector("body > div.body > div.main-view > div.container-fluid > ui-view > ng-form > div > div > form-group:nth-child(1) > div > div > ng-transclude > input"));
                login_field.SendKeys(entry.Key);
                IWebElement email_field = webDriver.FindElement(By.CssSelector("body > div.body > div.main-view > div.container-fluid > ui-view > ng-form > div > div > form-group:nth-child(2) > div > div > ng-transclude > input"));
                email_field.SendKeys(entry.Key + "@gmail.com");
                IWebElement role_field = webDriver.FindElement(By.CssSelector("body > div.body > div.main-view > div.container-fluid > ui-view > ng-form > div > div > form-group:nth-child(3) > div > div > ng-transclude > div > div:nth-child(1) > input"));
                role_field.Click();
                IWebElement admin_role = webDriver.FindElement(By.CssSelector(".ui-select-choices-row-inner > span:nth-child(1)"));
                admin_role.Click();
                if (entry.Value == true)
                {
                    IWebElement ik = webDriver.FindElement(By.CssSelector(".iCheck-helper"));
                    ik.Click();
                }
                IWebElement zapisz = webDriver.FindElement(By.CssSelector("body > div.body > div.main-view > div.container-fluid > ui-view > view-header > div > div > div > div > ng-transclude > div > button.btn.btn-success.ng-binding.ng-isolate-scope"));
                zapisz.Click();
            }
            WaitUntilElementIsPresent("body > div.body > div.main-view > div.top_nav.clearfix > div > nav > ul > li:nth-child(1)");

            string script1 = "document.querySelector(\"body > div.body > div.main-view > div.top_nav.clearfix > div > nav > ul > li:nth-child(1)\").classList.add(\"open\")";
            webDriver.ExecuteScript(script1);
            WaitUntilElementIsPresent("ul.dropdown-menu:nth-child(2) > li:nth-child(2)");
            IWebElement logout1 = webDriver.FindElement(By.CssSelector("ul.dropdown-menu:nth-child(2) > li:nth-child(2)"));
            logout1.Click();

            foreach (KeyValuePair<String, Boolean> entry in data)
            {
                if (entry.Value == true)
                {
                    webDriver.Navigate().GoToUrl("http://localhost:65274/");
                    WaitUntilElementIsPresent("body > div.container > div > ui-view > form > div:nth-child(2) > input");
                    IWebElement login_field = webDriver.FindElement(By.CssSelector("body > div.container > div > ui-view > form > div:nth-child(2) > input"));
                    IWebElement password_field = webDriver.FindElement(By.CssSelector("body > div.container > div > ui-view > form > div:nth-child(3) > input"));
                    IWebElement submit_button = webDriver.FindElement(By.CssSelector("body > div.container > div > ui-view > form > button"));
                    login_field.SendKeys(entry.Key);
                    password_field.SendKeys("password");
                    submit_button.Submit();
                    WaitUntilElementIsPresent("body > div.container > div > ui-view > form > div:nth-child(3) > input");
                    IWebElement current_pass = webDriver.FindElement(By.CssSelector("body > div.container > div > ui-view > form > div:nth-child(3) > input"));
                    IWebElement new_pass = webDriver.FindElement(By.CssSelector("body > div.container > div > ui-view > form > div:nth-child(4) > input"));
                    IWebElement new_pass2 = webDriver.FindElement(By.CssSelector("body > div.container > div > ui-view > form > div:nth-child(5) > input"));
                    IWebElement button = webDriver.FindElement(By.CssSelector("body > div.container > div > ui-view > form > button"));

                    current_pass.SendKeys("password");
                    new_pass.SendKeys("password");
                    new_pass2.SendKeys("password");
                    button.Click();

                    WaitUntilElementIsPresent("body > div.container > div > ui-view > form > div:nth-child(3) > input");
                    password_field = webDriver.FindElement(By.CssSelector("body > div.container > div > ui-view > form > div:nth-child(3) > input"));
                    password_field.SendKeys("password");
                    submit_button = webDriver.FindElement(By.CssSelector("body > div.container > div > ui-view > form > button"));
                    submit_button.Submit();

                    WaitUntilElementIsPresent("body > div.body > div.main-view > div.top_nav.clearfix > div > nav > ul > li:nth-child(1)");
                    string script = "document.querySelector(\"body > div.body > div.main-view > div.top_nav.clearfix > div > nav > ul > li:nth-child(1)\").classList.add(\"open\")";
                    webDriver.ExecuteScript(script);
                    WaitUntilElementIsPresent("ul.dropdown-menu:nth-child(2) > li:nth-child(2)");
                    IWebElement logout = webDriver.FindElement(By.CssSelector("ul.dropdown-menu:nth-child(2) > li:nth-child(2)"));
                    logout.Click();
                }
                else
                {
                    WaitUntilElementIsPresent("body > div.container > div > ui-view > form > div:nth-child(2) > input");
                    IWebElement login_field = webDriver.FindElement(By.CssSelector("body > div.container > div > ui-view > form > div:nth-child(2) > input"));
                    IWebElement password_field = webDriver.FindElement(By.CssSelector("body > div.container > div > ui-view > form > div:nth-child(3) > input"));
                    IWebElement submit_button = webDriver.FindElement(By.CssSelector("body > div.container > div > ui-view > form > button"));
                    login_field.SendKeys(entry.Key);
                    password_field.SendKeys("password");
                    submit_button.Submit();
                    WaitUntilElementIsPresent("body > div.container > div > ui-view > form > div.validation-message.ng-binding.ng-scope");
                    IWebElement validation = webDriver.FindElement(By.CssSelector("body > div.container > div > ui-view > form > div.validation-message.ng-binding.ng-scope"));
                    if (!(validation.Text == "Użytkownik jest nieaktywny."))
                    {
                        throw new Exception("Active user can't log himself in");
                    }
                }

            }
        }

        [TearDown]
        public void CloseBrowser()
        {
            webDriver.Close();
        }
    }
}
