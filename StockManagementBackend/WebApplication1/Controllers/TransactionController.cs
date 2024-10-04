using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using shared.dtos;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly ILogger<TransactionController> _logger; 

        public TransactionController(ITransactionService transactionService, ILogger<TransactionController> logger)
        {
            _transactionService = transactionService;
            _logger = logger;
        }

        [HttpGet("ShowAllPaginatedTransaction")]
        public async Task<IActionResult> ShowAllPaginatedTransaction(PaginationSortDto paginationSortDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var response = await _transactionService.ShowTransactionPaginatedDto(paginationSortDto);
            if (!response.Success) return BadRequest();
            return Ok(response.Data);
        }

    }
}
