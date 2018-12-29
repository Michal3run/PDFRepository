using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PDFRepositoryProject.Data;
using PDFRepositoryProject.Models;
using System.Web;
using PDFRepositoryProject.Utilities;
using PDFRepositoryProject.Managers;
using System.Text;

namespace PDFRepositoryProject.Controllers
{
    public class PDFRepositoryController : Controller
    {
        private readonly PDFContext _context;

        public PDFRepositoryController(PDFContext context)
        {
            _context = context;
        }

        // GET: PDFRepository
        public IActionResult Index(string nameFilter, string contentFilter)
        {
            nameFilter = nameFilter?.ToLower();

            ViewData["nameFilter"] = nameFilter;
            ViewData["contentFilter"] = contentFilter;

            var documents = string.IsNullOrEmpty(contentFilter) 
                ? _context.Documents.AsEnumerable() //not to include data, that won't be used
                : _context.Documents.Include(d => d.Data);

            if (!string.IsNullOrEmpty(nameFilter))
            {
                documents = documents.Where(s => s.FileName.ToLower().Contains(nameFilter));
            }

            if (!string.IsNullOrEmpty(contentFilter))
            {
                documents = documents.Where(s => !string.IsNullOrEmpty(s.Data.ExtractedText) && s.Data.ExtractedText.Contains(contentFilter));
            }

            return View(documents.ToList());
        }

        // GET: PDFRepository/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PDFRepository/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FileName,Description,Category,File")] PDFDocumentCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            PDFDocumentValidator.ValidatePDFAttachment(viewModel.File, ModelState);

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var fileData = FileHelpers.ReadBytes(viewModel.File);
            var PDFDocument = PDFDocumentViewModelToModelHelper.ViewModelToModel(viewModel, fileData);

            _context.Add(PDFDocument);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DownloadPDF(int? id)
        {
            var document = GetDocument(id);
            if (document == null) return NotFound();

            return File(document.Data.Content, "application/pdf", $"{document.FileName}.pdf");
        }

        public IActionResult ShowPDF(int? id)
        {
            var document = GetDocument(id);
            if (document == null) return NotFound();

            return File(document.Data.Content, "application/pdf");
        }

        private DbPDFDocument GetDocument(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var document = _context.Documents.Where(m => m.Id == id).Include(m => m.Data).SingleOrDefault();
            return document;
        }

        // GET: PDFRepository/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Documents.SingleOrDefaultAsync(m => m.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            var viewModelDocument = PDFDocumentViewModelToModelHelper.ModelToViewModel<PDFDocumentEditViewModel>(document);
            return View(viewModelDocument);
        }

        // POST: PDFRepository/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FileName,Description,Category,UploadDateTime,File")] PDFDocumentEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            
            byte[] newAttachment = null;

            if (viewModel.File != null) //new file attached
            {
                PDFDocumentValidator.ValidatePDFAttachment(viewModel.File, ModelState);
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                newAttachment = FileHelpers.ReadBytes(viewModel.File);
            }

            var sourceDocument = GetDocument(id);
            if (sourceDocument == null)
            {
                return NotFound();
            }

            //updates sourceDocument with values from viewModel, replaces Data if newAttachment is not null
            sourceDocument = PDFDocumentViewModelToModelHelper.ViewModelToModel(viewModel, newAttachment, sourceDocument);

            try
            {
                _context.Update(sourceDocument);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PDFDocumentExists(viewModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public JsonResult Download(DbPDFDocument document)
        {
            return new JsonResult(document.Id);
        }

        // GET: PDFRepository/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Documents
                .SingleOrDefaultAsync(m => m.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // POST: PDFRepository/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var document = await _context.Documents.SingleOrDefaultAsync(m => m.Id == id);
            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PDFDocumentExists(int id)
        {
            return _context.Documents.Any(e => e.Id == id);
        }
    }
}
