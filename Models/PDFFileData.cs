using System.ComponentModel.DataAnnotations.Schema;

namespace PDFRepositoryProject.Models
{
    public class PDFFileData
    {
        [ForeignKey("DbPDFDocument")]
        public int Id { get; set; }

        public byte[] Content { get; set; }  
        
        public string ExtractedText { get; set; }

        public virtual DbPDFDocument DbPDFDocument { get; set; }
    }
}
