using System.ComponentModel.DataAnnotations;

namespace ITHTD.Models;

public enum TicketStatus { Open, InProgress, Resolved, Closed }
public enum TicketPriority { Low, Medium, High, Critical }

public class Ticket
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; } = "";

    [Required, StringLength(1000)]
    public string Description { get; set; } = "";

    public TicketStatus Status { get; set; } = TicketStatus.Open;
    public TicketPriority Priority { get; set; } = TicketPriority.Medium;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Requester name is required.")]
    public string RequesterName { get; set; } = "";

    public int? AssignedAgentId { get; set; }
    public Agent? AssignedAgent { get; set; }
}