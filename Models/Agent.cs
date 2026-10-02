using System.ComponentModel.DataAnnotations;

namespace ITHTD.Models;

public class Agent
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Agent name is required.")]
    public string Name { get; set; } = "";

    public List<Ticket> Tickets { get; set; } = new();
}