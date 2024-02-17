using BiletVarmi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestTriggerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemRunnerController : ControllerBase
    {
        IWebDriver driver;
        private readonly UnitTest1 _test;
        public SystemRunnerController(UnitTest1 test)
        {
            _test = test;
        }


        [HttpGet("calistirTest")]
        public void CalistirTest()
        {
            driver = new ChromeDriver
            {
                Url = "https://ebilet.tcddtasimacilik.gov.tr/view/eybis/tnmGenel/tcddWebContent.jsf"

            };
            _test.TestMethod1(driver,"Eskişehir","Ankara Gar", "24.02.2024", "esesli.fvz@gmail.com");
        }

    }
}
