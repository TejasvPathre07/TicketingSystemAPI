namespace TicketingSystem.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Type { get; set; } // Bug, Feature, Task

        public string Priority { get; set; } // Low, Medium, High

        public string Status { get; set; } = "Open";    

        public string AssignedTo { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }
    }

    public class TicketHistory
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public DateTime ChangedAt { get; set; } = DateTime.Now;
    }
}