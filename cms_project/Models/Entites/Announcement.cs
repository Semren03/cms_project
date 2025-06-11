namespace cms_project.Models.Entites
{
    public class Announcement
    {

        public int Id { get; set; }

        public string Title { get; set; }

        
        public string Content { get; set; }

        public DateTime DatePosted { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

    }
}
