using BiletVarmi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Project1.Model;
using Project1.Model;

namespace TestTriggerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemRunnerController : ControllerBase
    {
        IWebDriver driver;
        private readonly BiletKontrol _kontrol;
        public SystemRunnerController(BiletKontrol kontrol)
        {
            _kontrol = kontrol;
        }


        [HttpPost]
        [Route("calistirTest")]
        public async Task<IActionResult> CalistirTest([FromBody] Data data)
        {
            while (true)
            {
                try
                {
                    driver = new ChromeDriver
                    {
                        Url = "https://ebilet.tcddtasimacilik.gov.tr/view/eybis/tnmGenel/tcddWebContent.jsf"
                    };
                    _kontrol.BiletKontroluBaslat(driver, data.fromTrain, data.toTrain, data.departureDate, data.email, data.startTime, data.endTime);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Api içindeki Error" + ex);
                    return Ok("Eskişehirspor");
                }
            }
            return Ok("Başarıyla Tamamlanıd");
        }

    }
}
//{
//    "fromTrain": "Eskişehir",
//    "toTrain": "Ankara Gar",
//    "departureDate": "24.02.2024",
//    "email": "esesli.fvz@gmail.com",
//    "startTime":"10:00",
//    "endTime":"12:00"
//}