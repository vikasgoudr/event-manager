using EventManager.DAL.Context;
using EventManager.DAL.Contracts;
using EventManager.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.DAL.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly EventManagerDbContext _context;

        public TicketRepository(EventManagerDbContext context)
        {
            _context = context;
        }
        public async Task<int> CreateTicket(Ticket ticket)
        {
            var res = await _context.Ticket.AddAsync(ticket);
           try
            {
                await _context.Ticket.AddAsync(ticket); 
                await _context.SaveChangesAsync();
                await _context.Ticket.Entry(ticket).ReloadAsync();
                return ticket.Id;
            }
            catch
            {
                return 0;
            }
        }
        public async Task<bool> CancelTicket(string transactionId)
        {
            try
            {
                var ticket = await _context.Ticket.FirstOrDefaultAsync(x => x.TransactionId == transactionId);
                if (ticket != null)
                {
                    ticket.IsDeleted = true; 
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false; // Ticket not found
                }
            }
            catch
            {
                return false; // Error occurred
            }
        }


    }
}
