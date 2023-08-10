using EventManager.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.DAL.Contracts
{
    /// <summary>
    /// This interface is used to perform database operations on Ticket Table
    /// </summary>
    public interface ITicketRepository
    {
        /// <summary>
        /// Creates a ticket based on provided ticket information
        /// </summary>
        /// <param name="ticket">Ticket Model</param>
        /// <returns>Returns Ticket Id</returns>
        public Task<int> CreateTicket(Ticket ticket);
        /// <summary>
        /// Method used to cancel ticket
        /// </summary>
        /// <param name="transactionId">Transaction Id</param>
        /// <returns>Returns boolean indicating ticket is cancelled or not</returns>
        public Task<bool> CancelTicket(string transactionId);
    }
}
