using System.ComponentModel.DataAnnotations.Schema;

namespace cms_project.Models.Entites
{
    public class Notification
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserAccount UserAccount { get; set; }

       
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
