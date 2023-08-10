using AutoMapper;
using EventManager.BLL.Contracts;
using EventManager.DAL.Contracts;
using EventManager.DAL.Entities;
using EventManager.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EventManager.BLL.Services;

/// <summary>
/// The TicketService class provides business logic for managing event tickets.
/// </summary>
public class TicketService : ITicketService
{
    private readonly IMapper _mapper;
    private readonly ITicketRepository _ticketRepository;
    public TicketService(ITicketRepository ticketRepository, IMapper mapper)
    {
        _ticketRepository = ticketRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new event ticket.
    /// </summary>
    /// <param name="ticket">The TicketDTO object containing the ticket information.</param>
    /// <returns>The ID of the created ticket.</returns>
    public async Task<int> CreateTicket(TicketDTO ticket)
    {
        return await _ticketRepository.CreateTicket(_mapper.Map<Ticket>(ticket));
    } 

    public async Task<bool> CancelTicket(string transactionId)
    {
        return await _ticketRepository.CancelTicket(transactionId);
    }
}
