namespace cms_project.Models.Entites
{
    public class Claims
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}
