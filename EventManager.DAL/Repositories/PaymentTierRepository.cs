using EventManager.DAL.Context;
using EventManager.DAL.Contracts;
using EventManager.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventManager.DAL.Repositories
{
    public class PaymentTierRepository : IPaymentTierRepository
    {

        private readonly EventManagerDbContext _context;


        public PaymentTierRepository(EventManagerDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddPaymentTier(PaymentTier paymentTier)
        {

            _context.PaymentTiers.Add(paymentTier);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePaymentTiersByEventId(int eventId)
        {
            var paymentTiers = await _context.PaymentTiers
                .Where(pt => pt.EventId == eventId)
                .ToListAsync();

            _context.PaymentTiers.RemoveRange(paymentTiers);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<PaymentTier>> GetPaymentTiersByEventId(int eventId)
        {
            return await _context.PaymentTiers
              .Where(pt => pt.EventId == eventId)
              .ToListAsync();
        }

        public async Task<bool> UpdatePaymentTier(PaymentTier updatedPaymentTier)
        {
            var paymentTier = await _context.PaymentTiers.Where(x => !x.IsDeleted && x.Id == updatedPaymentTier.Id).FirstOrDefaultAsync();
            if(paymentTier == null)
            {
                throw new Exception("Invalid Id");
            }
            paymentTier.Name = updatedPaymentTier.Name;
            paymentTier.AvailableTickets = updatedPaymentTier.AvailableTickets;
            paymentTier.Amount = updatedPaymentTier.Amount;

            _context.PaymentTiers.Update(paymentTier);
            return await _context.SaveChangesAsync() > 0;
        }


    }
}


