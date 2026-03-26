using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.DTOs
{
    public class CreateTicketDto
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public string Priority { get; set; }

        public string AssignedTo { get; set; }
    }
}