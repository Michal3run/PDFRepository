using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
namespace PDFRepositoryProject.Models
{
    public class PDFDocumentBaseViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "File name")]
        [StringLength(255, MinimumLength = 1)]
        public string FileName { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Description { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Category { get; set; }

        [Display(Name = "Upload Time")]
        [DisplayFormat(DataFormatString = "{0:F}")]
        public DateTime UploadDateTime { get; set; }
    }
}
