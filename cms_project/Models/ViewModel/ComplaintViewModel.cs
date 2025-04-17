using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace cms_project.Models.ViewModel
{
    public class ComplaintViewModel
    {

        public ComplaintType ComplaintType { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
  
        public List<IFormFile> Attachments { get; set; }


        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }

}