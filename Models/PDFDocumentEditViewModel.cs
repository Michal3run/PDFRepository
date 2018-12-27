using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
namespace PDFRepositoryProject.Models
{
    public class PDFDocumentEditViewModel : PDFDocumentBaseViewModel
    { 
        /// <summary>
        /// Field is not required, no file attached means no update in db for attachment
        /// </summary>
        [Display(Name = "PDF File")]        
        public IFormFile File { get; set; }
    }
}
