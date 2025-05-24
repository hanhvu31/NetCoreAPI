using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;
using System.IO;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using System.Linq;
using MvcMovie.Models.Process;
using System.Collections.Generic;
using OfficeOpenXml;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace MvcMovie.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ExcelProcess _excelProcess = new ExcelProcess();

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

         public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension != ".xls" && fileExtension != ".xlsx")
                {
                    ModelState.AddModelError("", "Vui lòng chọn file Excel (.xls hoặc .xlsx)!");
                }
                else
                {
                    try
                    {
                        var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Excels");
                        if (!Directory.Exists(uploadDir))
                        {
                            Directory.CreateDirectory(uploadDir);
                        }

                        var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
                        var filePath = Path.Combine(uploadDir, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        DataTable dt;
                        using (var stream = new FileStream(filePath, FileMode.Open))
                        {
                            dt = _excelProcess.ReadExcelFile(stream);
                        }

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            try
                            {
                                var person = new Person
                                {
                                    PersonId = int.Parse(dt.Rows[i][0].ToString()),
                                    FullName = dt.Rows[i][1].ToString(),
                                    Address = dt.Rows[i][2].ToString(),
                                    Age = int.Parse(dt.Rows[i][3].ToString())
                                };
                                _context.Person.Add(person);
                            }
                            catch
                            {
                                continue; // bỏ qua dòng lỗi
                            }
                        }

                        await _context.SaveChangesAsync();
                        ViewBag.Message = "Tải và nhập dữ liệu thành công!";
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Lỗi khi xử lý file: " + ex.Message);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Vui lòng chọn file để tải lên!");
            }

            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,FullName,Address,Age")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var person = await _context.Person.FindAsync(id);
            if (person == null) return NotFound();

            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonId,FullName,Address,Age")] Person person)
        {
            if (id != person.PersonId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var person = await _context.Person.FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null) return NotFound();

            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null) return Problem("Invalid person ID.");

            var person = await _context.Person.FindAsync(id);
            if (person != null)
            {
                _context.Person.Remove(person);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.PersonId == id);
        }

        public IActionResult Download()
        {
            var fileName = "DanhSachPerson.xlsx";
            using (var excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

                worksheet.Cells["A1"].Value = "PersonID";
                worksheet.Cells["B1"].Value = "FullName";
                worksheet.Cells["C1"].Value = "Address";
                worksheet.Cells["D1"].Value = "Age";

                var personList = _context.Person.ToList();
                worksheet.Cells["A2"].LoadFromCollection(personList, false);

                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
    }
}
