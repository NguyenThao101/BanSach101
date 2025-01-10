namespace BanSach.Components.Model
{
    public class Personal_access_token
    {
        public int PersonalId { get; set; }
        public string? TokenableType { get; set; }
        public int TokenableId { get; set; }
        public string? Name { get; set; }
        public string? Token { get; set; }
        public string? Abilities { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? LastUser { get; set; } 
        public DateTime? Expires { get; set; }
    }
}
