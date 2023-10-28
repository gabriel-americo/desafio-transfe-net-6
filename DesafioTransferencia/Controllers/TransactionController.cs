using DesafioTransferencia.Models;
using DesafioTransferencia.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DesafioTransferencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionModel transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Retorna detalhes dos erros de validação ao cliente
            }

            await _transactionRepository.CreateTransaction(transaction);
            return CreatedAtAction(nameof(GetTransactionById), new { transactionId = transaction.Id }, transaction);
        }

        [HttpGet("{transactionId}")]
        public async Task<ActionResult<TransactionModel>> GetTransactionById(int transactionId)
        {
            var transaction = await _transactionRepository.GetTransactionById(transactionId);

            if (transaction == null)
            {
                return NotFound("Transação não encontrada.");
            }

            return Ok(transaction);
        }
    }
}
