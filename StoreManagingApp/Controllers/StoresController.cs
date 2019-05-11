using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Model;
using StoreManagingApp.DataProcessing;

namespace StoreManagingApp.Controllers
{
    [Route("api/[controller]")]
    public class StoresController : Controller
    {
        #region Private Properties

        private readonly ExcelDataHandler excelDataHandler;

        #endregion

        #region Constructor

        public StoresController()
        {
            excelDataHandler = new ExcelDataHandler(0);
        }

        #endregion

        #region Store API

        /// <summary>
        /// Get list of stores with characters
        /// </summary>
        /// <returns>collection of stores with characters</returns>
        [HttpGet("Stores")]
        public ActionResult<IEnumerable<StoreCharacters>> Stores()
        {
            var result = excelDataHandler.GetStores();
            if (result == null)
            {
                return BadRequest("Error while getting data from excel file");
            }

            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
        [HttpPost("AddStore")]
        public ActionResult<int> AddStore([FromBody] StoreCharacters store)
        {
            var errorMessage = ValidationHelper.ValidateStore(store);
            if (errorMessage.Any()) {
                return BadRequest(errorMessage);
            }
            
            var id = excelDataHandler.AddStore(store);
            if (id == null)
            {
                return BadRequest("Error while adding data to excel file");
            }

            return Ok(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
        [HttpPut("UpdateStore")]
        public ActionResult<int> UpdateStore([FromBody] StoreCharacters store)
        {
            var errorMessage = ValidationHelper.ValidateStore(store);
            if (errorMessage.Any())
            {
                return BadRequest(errorMessage);
            }

            var id = excelDataHandler.UpdateStore(store);
            if (id == null)
            {
                return BadRequest("Error while updating data in excel file");
            }

            return Ok(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteStore/{id}")]
        public ActionResult<int> DeleteStore([FromRoute] int Id)
        {
            var id = excelDataHandler.DeleteStore(Id);
            if (id == null)
            {
                return BadRequest("Error while deleting data from excel file");
            }

            return Ok(id);
        }

        #endregion

        //private static string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        //[HttpGet("[action]")]
        //public async Task<IEnumerable<WeatherForecast>> WeatherForecasts()
        //{
            
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    });
        //}

        //public class WeatherForecast
        //{
        //    public string DateFormatted { get; set; }
        //    public int TemperatureC { get; set; }
        //    public string Summary { get; set; }

        //    public int TemperatureF {
        //        get {
        //            return 32 + (int)(TemperatureC / 0.5556);
        //        }
        //    }
        //}
    }
}
