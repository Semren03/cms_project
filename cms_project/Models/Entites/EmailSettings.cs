namespace cms_project.Models.Entites
{
    public class EmailSettings
    {
        public int Id { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public string UserName { get;set; }

        public string Password { get; set; }

        public bool EnableSsl { get; set; }

        public string FromEmail { get; set; }

        public string DisplayName { get; set; }


    }
}
