namespace cms_project.Models.Entites
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Claims> Claims { get; set; }       =new List<Claims>();

        public ICollection<UserAccount>? UserAccounts { get; set; }




    }
}

