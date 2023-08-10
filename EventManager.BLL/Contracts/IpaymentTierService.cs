using EventManager.Models.DTO;
using EventManager.Util.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.BLL.Contracts
{
    /// <summary>
    /// The IPaymentTierService interface defines methods for managing payment tiers for events.
    /// </summary>
    public interface IPaymentTierService
    {
        /// <summary>
        /// Retrieves a list of payment tiers for a specific event by event ID.
        /// </summary>
        /// <param name="eventId">The ID of the event for which the payment tiers are to be retrieved.</param>
        /// <returns>An IEnumerable of PaymentTierDTO objects representing the payment tiers.</returns>
        Task<IEnumerable<PaymentTierDTO>> GetPaymentTiersByEventId(int eventId);

        /// <summary>
        /// Adds a new payment tier for an event.
        /// </summary>
        /// <param name="paymentTierDto">The PaymentTierDTO object containing the payment tier information.</param>
        /// <returns>True if the payment tier is added successfully, otherwise false.</returns>
        Task<bool> AddPaymentTier(PaymentTierDTO paymentTierDto);

        /// <summary>
        /// Updates an existing payment tier for an event.
        /// </summary>
        /// <param name="paymentTierDto">The PaymentTierDTO object containing the updated payment tier information.</param>
        /// <returns>True if the payment tier is updated successfully, otherwise false.</returns>
        Task<bool> UpdatePaymentTier(PaymentTierDTO paymentTierDto);

        /// <summary>
        /// Deletes all payment tiers for a specific event by event ID.
        /// </summary>
        /// <param name="eventId">The ID of the event for which the payment tiers are to be deleted.</param>
        /// <returns>True if the payment tiers are deleted successfully, otherwise false.</returns>
        Task<bool> DeletePaymentTiersByEventId(int eventId);
    }
}
