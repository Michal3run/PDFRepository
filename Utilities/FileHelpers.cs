using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace PDFRepositoryProject.Utilities
{
    public class FileHelpers
    {
        public static byte[] ReadBytes(IFormFile formFile)
        {
            byte[] result;

            try
            {
                using (var ms = new MemoryStream())
                {
                    formFile.CopyTo(ms);
                    result = ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}