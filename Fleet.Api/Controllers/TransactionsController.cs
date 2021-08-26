using Fleet.Api.Factories;
using Fleet.Api.Services;
using Fleet.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Fleet.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly ITransportationService _transportationService;

        public TransactionsController(
            ITransactionService transactionService,
            ITransportationService transportationService)
        {
            _transactionService = transactionService;
            _transportationService = transportationService;
        }

        /// <summary>
        /// POST api/transactions
        /// 
        /// Create a new transaction
        /// </summary>
        /// <param name="createTransaction">Create transaction parameters</param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ActionResult> AddTransaction([FromBody] CreateTransactionDto createTransaction)
        {
            try
            {
                var fromTransportation = await _transportationService.GetByIdAsync(createTransaction.FromTransportationId);

                var toTransportation = await _transportationService.GetByIdAsync(createTransaction.ToTransportationId);

                await _transactionService.AddAsync(fromTransportation, toTransportation);

                return Ok("Transaction successfully created");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
