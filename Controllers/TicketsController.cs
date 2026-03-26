using Microsoft.AspNetCore.Mvc;
using TicketingSystem.Data;
using TicketingSystem.DTOs;
using TicketingSystem.Models;

namespace TicketingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TicketsController(AppDbContext context)
        {
            _context = context;
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create(CreateTicketDto dto)
        {
            var ticket = new Ticket
            {
                Title = dto.Title,
                Description = dto.Description,
                Type = dto.Type,
                Priority = dto.Priority,
                AssignedTo = dto.AssignedTo
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return Ok(ticket);
        }

        // GET
        [HttpGet]
        public IActionResult Get(string status, string type, string assignedTo)
        {
            var query = _context.Tickets.AsQueryable();

            if (!string.IsNullOrEmpty(status))
                query = query.Where(x => x.Status == status);

            if (!string.IsNullOrEmpty(type))
                query = query.Where(x => x.Type == type);

            if (!string.IsNullOrEmpty(assignedTo))
                query = query.Where(x => x.AssignedTo == assignedTo);

            return Ok(query.ToList());
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTicketDto dto)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
                return NotFound();

            ticket.Title = dto.Title ?? ticket.Title;
            ticket.Description = dto.Description ?? ticket.Description;
            ticket.Type = dto.Type ?? ticket.Type;
            ticket.Priority = dto.Priority ?? ticket.Priority;
            ticket.Status = dto.Status ?? ticket.Status;
            ticket.AssignedTo = dto.AssignedTo ?? ticket.AssignedTo;
            ticket.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(ticket);
        }

        // UPDATE STATUS
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
                return NotFound();

            ticket.Status = status;
            ticket.UpdatedAt = DateTime.Now;

            _context.TicketHistories.Add(new TicketHistory
            {
                TicketId = ticket.Id,
                Status = status,
                Notes = "Status updated"
            });

            await _context.SaveChangesAsync();

            return Ok(ticket);
        }
    }
}