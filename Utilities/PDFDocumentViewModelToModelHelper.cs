using PDFRepositoryProject.Models;
using PDFRepositoryProject.PDFProcessing;
using System;

namespace PDFRepositoryProject.Managers
{
    public class PDFDocumentViewModelToModelHelper
    {
        public static T ModelToViewModel<T>(DbPDFDocument dbModel) where T : PDFDocumentBaseViewModel, new()
        {
            var result = new T
            {
                Id = dbModel.Id,
                FileName = dbModel.FileName,
                Description = dbModel.Description,
                Category = dbModel.Category,
                UploadDateTime = dbModel.UploadDateTime,
                //File  file null - unchanged
            };

            return result;
        }

        public static DbPDFDocument ViewModelToModel(PDFDocumentBaseViewModel viewModel, byte[] data, DbPDFDocument sourceDocument = null)
        {
            var result = sourceDocument ?? new DbPDFDocument();

            result.FileName = viewModel.FileName;
            result.Description = viewModel.Description;
            result.Category = viewModel.Category;
            result.UploadDateTime = data == null ? sourceDocument.UploadDateTime : DateTime.Now; //update UploadDateTime if new attachment
            result.Data = GetData(sourceDocument, data);          

            return result;
        }

        private static PDFFileData GetData(DbPDFDocument sourceDocument, byte[] data)
        {
            if (data == null)
            {
                return sourceDocument.Data;
            }

            var fileData = sourceDocument.Data ?? new PDFFileData();
            fileData.Content = data;
            fileData.ExtractedText = PDFParser.ExtractTextFromPdf(data);
            return fileData;
        }
    }
}
