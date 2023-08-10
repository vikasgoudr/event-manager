using EventManager.DAL.Entities;
using EventManager.Util.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.DAL.Contracts
{
    /// <summary>
    /// This interface is used to perform database operations on Payment Table
    /// </summary>
    public interface IPaymentTierRepository
    {
        /// <summary>
        /// Method used to GetPaymentTiers for particular event
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <returns>Returns list is payment tiers for particular event</returns>
        Task<IEnumerable<PaymentTier>> GetPaymentTiersByEventId(int eventId);
        /// <summary>
        /// Method used to add payment tier for particular event
        /// </summary>
        /// <param name="paymentTier">Payment tier</param>
        /// <returns>Returns boolean indicating payment tier created successfully or not</returns>
        Task<bool> AddPaymentTier(PaymentTier paymentTier);
        /// <summary>
        /// Method used to update payment tier for particular event
        /// </summary>
        /// <param name="updatedPaymentTier">Payment tier</param>
        /// <returns>Returns boolean indicating payment tier updated successfully or not</returns>
        Task<bool> UpdatePaymentTier(PaymentTier updatedPaymentTier);
        /// <summary>
        /// Method used to delete payment tier for particular event
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <returns>Returns boolean indicating payment tier deleted successfully or not</returns>
        Task<bool> DeletePaymentTiersByEventId(int eventId);
    }
}

