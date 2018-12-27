using System;
using System.ComponentModel.DataAnnotations;

namespace PDFRepositoryProject.Models
{
    public class DbPDFDocument
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "File name")]
        public string FileName { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }
                
        [Display(Name = "Upload Time")]
        [DisplayFormat(DataFormatString = "{0:F}")]
        public DateTime UploadDateTime { get; set; }
        
        public virtual PDFFileData Data { get; set; }
        
    }
}
