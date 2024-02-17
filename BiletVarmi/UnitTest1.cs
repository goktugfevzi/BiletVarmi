using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;

namespace BiletVarmi
{
    [TestClass]
    public class UnitTest1
    {

        IWebDriver driver;
        [TestMethod]
        public void TestMethod1()
        {
            driver = new ChromeDriver
            {
                Url = "https://ebilet.tcddtasimacilik.gov.tr/view/eybis/tnmGenel/tcddWebContent.jsf"
            };
            driver.FindElement(By.Id("nereden")).SendKeys("Eskişehir");
            driver.FindElement(By.Id("nereye")).SendKeys("Ankara Gar");
            driver.FindElement(By.Id("btnSeferSorgula")).Click();
            Thread.Sleep(3000);

            // Yeni sekmenin açılmasını bekleme

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(d => d.FindElement(By.XPath("//*[@id=\"mainTabView:gidisSeferTablosu_data\"]")));



            // <tr> elementlerini döngüye alma
            IList<IWebElement> satirlar = driver.FindElements(By.CssSelector("#mainTabView\\:gidisSeferTablosu_data > tr"));
            foreach (IWebElement satir in satirlar)
            {
                // Süre kontrolü
                string sureStr = satir.FindElement(By.XPath("td[1]/span")).Text;
                TimeSpan sure = TimeSpan.Parse(sureStr);

                if (sure < TimeSpan.Parse("20:40"))
                {
                    ;
                    // Vagon tipini alma
                    string vagonTipi = satir.FindElement(By.XPath("td[5]/div/label")).Text;

                    if (vagonTipi != "2+2 Pulman (Ekonomi) (2 Engelli koltuğu)" &&
                      vagonTipi != "2+2 Pulman (Ekonomi) (1 Engelli koltuğu)" &&
                      vagonTipi != "2+2 Pulman  (Ekonomi) (0 )")
                    {
                        string message = sureStr + " - " + vagonTipi;
                    }
                }
            }

            //driver.Quit();
        }

       

    }
}

