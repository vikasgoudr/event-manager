using AutoMapper;
using EventManager.BLL.Contracts;
using EventManager.DAL.Contracts;
using EventManager.DAL.Entities;
using EventManager.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.BLL.Services
{
    
    public class PaymentTierService : IPaymentTierService
    {
        private readonly IPaymentTierRepository _repository;
        private readonly IMapper _mapper;

        public PaymentTierService(IPaymentTierRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

 
        public async Task<IEnumerable<PaymentTierDTO>> GetPaymentTiersByEventId(int eventId)
        {
            var paymentTiers = await _repository.GetPaymentTiersByEventId(eventId);
            return _mapper.Map<IEnumerable<PaymentTierDTO>>(paymentTiers);
        }

 


        public async Task<bool> AddPaymentTier(PaymentTierDTO paymentTierDto)
        {
            var paymentTier = _mapper.Map<PaymentTier>(paymentTierDto);
            return await _repository.AddPaymentTier( paymentTier);
        }


        
        public async Task<bool> UpdatePaymentTier(PaymentTierDTO paymentTierDto)
        {
            var paymentTier = _mapper.Map<PaymentTier>(paymentTierDto);
            return await _repository.UpdatePaymentTier(paymentTier);
        }

        
       

        public async Task<bool> DeletePaymentTiersByEventId(int eventId)
        {
            return await _repository.DeletePaymentTiersByEventId(eventId);
        }
    }
}
