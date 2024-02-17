using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Security.Authentication;
using System.Threading;

namespace BiletVarmi
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1(IWebDriver driver, string fromTrain,string toTrain,string departureDate, string email)
        {
            while (true)
            {
                try
                {
                    driver.FindElement(By.Id("nereden")).SendKeys(fromTrain);
                    driver.FindElement(By.Id("nereye")).SendKeys(toTrain);
                    driver.FindElement(By.Id("trCalGid_input")).Clear();
                    driver.FindElement(By.Id("trCalGid_input")).SendKeys(departureDate);
                    driver.FindElement(By.Id("btnSeferSorgula")).Click();

                    Thread.Sleep(3000);

                    while (true)
                    {
                        try
                        {

                            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                            wait.Until(d => d.FindElement(By.XPath("//*[@id=\"mainTabView:gidisSeferTablosu_data\"]")));
                            break;
                        }
                        catch (Exception e)
                        {
                            driver.FindElement(By.Id("nereden")).SendKeys("Eskişehir");
                            driver.FindElement(By.Id("nereye")).SendKeys("Ankara Gar");
                            driver.FindElement(By.Id("trCalGid_input")).Clear();
                            driver.FindElement(By.Id("trCalGid_input")).SendKeys("24.02.2024");
                            driver.FindElement(By.Id("btnSeferSorgula")).Click();

                        }
                    }

                    // <tr> elementlerini döngüye alma
                    IList<IWebElement> satirlar = driver.FindElements(By.CssSelector("#mainTabView\\:gidisSeferTablosu_data > tr"));
                    foreach (IWebElement satir in satirlar)
                    {
                        // Süre kontrolü
                        string sureStr = satir.FindElement(By.XPath("td[1]/span")).Text;
                        TimeSpan sure = TimeSpan.Parse(sureStr);

                        if (sure < TimeSpan.Parse("23:40"))
                        {
                            // Vagon tipini alma
                            string vagonTipi = satir.FindElement(By.XPath("td[5]/div/label")).Text;

                            if (vagonTipi != "2+2 Pulman (Ekonomi) (2 Engelli koltuğu)" &&
                                vagonTipi != "2+2 Pulman (Ekonomi) (1 Engelli koltuğu)" &&
                                vagonTipi != "2+2 Pulman (Ekonomi) (0 )")
                            {
                                string message = sureStr + " - " + vagonTipi + " " + "https://ebilet.tcddtasimacilik.gov.tr/view/eybis/tnmGenel/tcddWebContent.jsf";
                                EmailSend.email_send(message, email);
                            }
                        }
                    }

                    Thread.Sleep(TimeSpan.FromMinutes(3));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hata: " + ex.Message);
                }
            }
        }
    }
}