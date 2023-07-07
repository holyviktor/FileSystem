using FileSystem.Data;
using FileSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace FileSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly FileSystemDbContext _context;

        public HomeController(FileSystemDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var folders = _context.Folders.ToList();
            return View(folders);
        }

        public async Task<IActionResult> Folder(int id)
        {
            var folder = await _context.Folders.Include(m => m.SubFolders).Include(m => m.ParentFolder)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (folder == null)
            {
                return NotFound();
            }
            return View(folder);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}