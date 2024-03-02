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
    public class BiletKontrol
    {
        public void BiletKontroluBaslat(IWebDriver driver, string fromTrain, string toTrain, string departureDate, string email, string startTime, string endTime)
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

                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                    wait.Until(d => d.FindElement(By.XPath("//*[@id=\"mainTabView:gidisSeferTablosu_data\"]")));
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ilk sayfa açlışındaki Try Test içindeki" + ex);
                    break;
                }
            }

            while (true)
            {
                try
                {
                    IList<IWebElement> satirlar = driver.FindElements(By.CssSelector("#mainTabView\\:gidisSeferTablosu_data > tr"));
                    foreach (IWebElement satir in satirlar)
                    {
                        string sureStr = satir.FindElement(By.XPath("td[1]/span")).Text;
                        TimeSpan sure = TimeSpan.Parse(sureStr);

                        if (sure > TimeSpan.Parse(startTime) && sure < TimeSpan.Parse(endTime))
                        {
                            string vagonTipi = satir.FindElement(By.XPath("td[5]/div/label")).Text;

                            if (vagonTipi != "2+2 Pulman (Ekonomi) (2 Engelli koltuğu)" &&
                                vagonTipi != "2+2 Pulman (Ekonomi) (1 Engelli koltuğu)" &&
                                vagonTipi != "2+2 Pulman (Ekonomi) (0 )")
                            {
                                string message = sureStr + " - " + vagonTipi + " " + "https://ebilet.tcddtasimacilik.gov.tr/view/eybis/tnmGenel/tcddWebContent.jsf";
                                EmailSend.emailSend(message, email);
                            }
                        }
                    }


                    Thread.Sleep(TimeSpan.FromMinutes(1));
                    driver.FindElement(By.Id("mainTabView:btnOncekiGunGidis")).Click();
                    Thread.Sleep(3000);
                    driver.FindElement(By.Id("mainTabView:btnSonrakiGunGidis")).Click();
                    Thread.Sleep(3000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ana Try Test içindeki" + ex);
                    break;
                }
            }
        }
    }
}