using ExerciseGB.DTO.Models;
using ExerciseGB.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseGB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseCalculatorController : ControllerBase
    {
        private readonly IPurchaseCalculatorService vatCalculatorService;
        public PurchaseCalculatorController(IPurchaseCalculatorService vatCalculatorService)
        {
            this.vatCalculatorService = vatCalculatorService;
        }

        /// 
        /// <summary>
        /// Gets the purchase
        /// </summary>
        /// <param name="net">Value of net</param>
        /// <param name="gross">Value of gross</param>
        /// <param name="vat">Value of vat</param>
        /// <param name="vatRate">Rate value of Vat</param>
        /// <returns>Returns the Purchase</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(IEnumerable<Purchase>), 200)]
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] double? net, [FromQuery] double? gross, [FromQuery] double? vat, [FromQuery] VatRate? vatRate)
        {
            var purchase = await this.vatCalculatorService.GetPurchaseValue(net, gross, vat, vatRate);

            return this.Ok(purchase);
        }
    };
}