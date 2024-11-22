using System.ComponentModel.DataAnnotations;

namespace TodoServerApp.Data
{
    public class Priority
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int Weight { get; set; }
    }
}
