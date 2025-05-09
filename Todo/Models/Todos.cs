using System.ComponentModel.DataAnnotations;

namespace Todo.Models
{
    public class Todos
    {
        public int Id { get; set; }
        [Required]
        public string Msg { get; set; } = string.Empty;
        public bool IsComplete { get; set; } = false;

    }
}