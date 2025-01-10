using System;
namespace BanSach.Components.Model
{
    public class QNA
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Email { get; set; }
        public string AnswererEmail { get; set; }
    }
}
