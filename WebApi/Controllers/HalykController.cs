using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Currency.Query;
using Application.Currency.Query.GetCurrency;

namespace HalykTz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HalykController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HalykController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("currency/save/{date}")]
        public async Task<IActionResult> SaveCurrencyRates(string date)
        {
            var query = new SaveCurrencyQueryCommand { Date = date };
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpGet("currency")]
        public async Task<IActionResult> GetCurrencyData([FromQuery] DateTime? date, string? code)
        {
            var query = new GetCurrencyQueryCommand { Date = date, Code = code };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}