using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PDFRepositoryProject.Models;

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