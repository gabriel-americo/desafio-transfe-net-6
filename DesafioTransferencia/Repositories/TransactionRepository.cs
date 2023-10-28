using DesafioTransferencia.Data;
using DesafioTransferencia.Models;
using DesafioTransferencia.Repositories.Interfaces;
using DesafioTransferencia.Services;
using Microsoft.EntityFrameworkCore;

namespace DesafioTransferencia.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TransactionModel> GetTransactionById(int transactionId)
        {
            return await _context.Transaction.FindAsync(transactionId);
        }

        public async Task<IEnumerable<TransactionModel>> GetTransactionsByUserId(Guid userId)
        {
            return await _context.Transaction
             .Where(t => t.PayerId == userId || t.PayeeId == userId)
             .ToListAsync();
        }

        public async Task<IEnumerable<TransactionModel>> GetAllTransactions()
        {
            return await _context.Transaction.ToListAsync();
        }

        public async Task<TransactionModel> CreateTransaction(TransactionModel transaction)
        {
            var entry = _context.Transaction.Add(transaction);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task UpdateTransactionAsync(TransactionModel transaction)
        {
            _context.Entry(transaction).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
