using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Threading;
using System.IO;
using System.Net;

namespace WCTests.Tests
{
    class test1
    {
        public String get_json()
        {
            String result = "";
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("D:\\json.txt"))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                    result += line;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return result;
        }
        FirefoxDriver webDriver;
        [SetUp]
        public void InitializeBrowser()
        {
            webDriver = new FirefoxDriver();
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
        [Test]
        public void login()
        {
            WebRequest request = WebRequest.Create("http://webconfigurator-api.azurewebsites.net/WebConfiguratorApiService.svc/part/List");
            request.Method = "POST";
            string data = "[{\"Operation\": \"Modify\", \"ObjectMap\": {\"PurMfgPM\": \"P\", \"Group\": \"BIURKA\", \"Description\": \"TRANSPORT t6001\", \"PartType\": \"ELEMENT\", \"PlanOrders\": true, \"MasterSched\": true, \"ProdLine\": \"T210\", \"UoM\": \"SZ\", \"StatusSt\": \"A\"}, \"Name\": \"sasza2000\"}]";
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            webDriver.Navigate().GoToUrl("https://webconfigurator-demo.azurewebsites.net/");
            Thread.Sleep(3000);
            IWebElement login_field = webDriver.FindElement(By.CssSelector("body > div.container > div > ui-view > form > div:nth-child(2) > input"));
            IWebElement password_field = webDriver.FindElement(By.CssSelector("body > div.container > div > ui-view > form > div:nth-child(3) > input"));
            IWebElement submit_button = webDriver.FindElement(By.CssSelector("body > div.container > div > ui-view > form > button"));
            login_field.SendKeys("admin");
            password_field.SendKeys("password");
            submit_button.Submit();
            //string script = "document.querySelector(\"nav.flexstyle-app-bar-menu\").style.display ='block';";
            //webDriver.ExecuteScript(script);
            //IWebElement logout_button = webDriver.FindElement(By.CssSelector("a[href*='/Account/Logout']"));
            //logout_button.Click();
            Thread.Sleep(3000);
            IWebElement param_ik = webDriver.FindElement(By.CssSelector("#sidebarMenu > div > ul > li:nth-child(1) > a"));
            param_ik.Click();
            IWebElement kartoteka_grup_ik = webDriver.FindElement(By.CssSelector("#sidebarMenu > div > ul > li.active > ul > li:nth-child(1) > a"));
            kartoteka_grup_ik.Click();
            Thread.Sleep(3000);

            IWebElement dodaj_grupe = webDriver.FindElement(By.CssSelector("body > div.body > div.main-view > div.container-fluid > ui-view > default-list-view > view-header > div > div > div > div > ng-transclude > div:nth-child(1) > button:nth-child(4)"));
            dodaj_grupe.Click();
            Thread.Sleep(3000);
            IWebElement nazwa_grupy = webDriver.FindElement(By.CssSelector("body > div.body > div.main-view > div.container-fluid > ui-view > ng-form > div > form-group:nth-child(1) > div > div > ng-transclude > input"));
            nazwa_grupy.SendKeys("test_group");
            Thread.Sleep(3000);
            IWebElement opis_grupy = webDriver.FindElement(By.CssSelector("body > div.body > div.main-view > div.container-fluid > ui-view > ng-form > div > form-group:nth-child(2) > div > div > ng-transclude > textarea"));
            opis_grupy.SendKeys("test_group");
            IWebElement zapisz_grupe = webDriver.FindElement(By.CssSelector("body > div.body > div.main-view > div.container-fluid > ui-view > view-header > div > div > div > div > ng-transclude > div > button.btn.btn-success.ng-binding.ng-isolate-scope"));
            zapisz_grupe.Click();
            Thread.Sleep(3000);
            IWebElement szukaj_grupe = webDriver.FindElement(By.CssSelector("#def_filter > label > input"));
            szukaj_grupe.SendKeys("test_group");
            Thread.Sleep(3000);
            IWebElement wybrana_grupa = webDriver.FindElement(By.CssSelector("#def > tbody > tr > td:nth-child(2)"));
            wybrana_grupa.Click();
            Thread.Sleep(3000);
            IWebElement usun = webDriver.FindElement(By.CssSelector("body > div.body > div.main-view > div.container-fluid > ui-view > default-list-view > view-header > div > div > div > div > ng-transclude > div:nth-child(1) > button:nth-child(3)"));
            usun.Click();
            Thread.Sleep(3000);
            IWebElement usun_podtwierdzenie = webDriver.FindElement(By.CssSelector("body > div.modal.fade.ng-scope.ng-isolate-scope.in > div > div > confirmation-modal-component > div.modal-footer > button.btn.btn-success.ng-scope"));
            usun_podtwierdzenie.Click();
        }

        [TearDown]
        public void CloseBrowser()
        {
            //string script = "document.querySelector(\"body > div.body > div.main-view > div.top_nav.clearfix > div > nav > ul > li:nth-child(1)\").classList.add(\"open\")";
            //webDriver.ExecuteScript(script);
            //Thread.Sleep(3000);
            //IWebElement logout = webDriver.FindElement(By.CssSelector("ul.dropdown-menu:nth-child(2) > li:nth-child(2)"));
            //logout.Click();
            //Thread.Sleep(3000);
            //webDriver.Close();
        }

    }
}
