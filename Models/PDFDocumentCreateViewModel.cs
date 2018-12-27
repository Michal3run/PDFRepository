using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
namespace PDFRepositoryProject.Models
{
    public class PDFDocumentCreateViewModel : PDFDocumentBaseViewModel
    {
        [Required]        
        [Display(Name = "PDF File")]        
        public IFormFile File { get; set; }
    }
}
