using EventManager.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.BLL.Contracts
{
    /// <summary>
    /// The ITicketService interface defines methods for managing tickets.
    /// </summary>
    public interface ITicketService
    {
        /// <summary>
        /// Creates a new ticket.
        /// </summary>
        /// <param name="ticket">The TicketDTO object containing the ticket information.</param>
        /// <returns>The ID of the created ticket.</returns>
        public Task<int> CreateTicket(TicketDTO ticket);

        /// <summary>
        /// Cancels a ticket by transaction ID.
        /// </summary>
        /// <param name="transactionId">The transaction ID of the ticket to be cancelled.</param>
        /// <returns>True if the ticket is cancelled successfully, otherwise false.</returns>
        public Task<bool> CancelTicket(string transactionId);
    }
}
