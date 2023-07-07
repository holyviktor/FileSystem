using FileSystem.Data;
using FileSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FileSystem.Controllers
{
    public class StructureController : Controller
    {
        private readonly ILogger<StructureController> _logger;
        private readonly FileSystemDbContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public StructureController(ILogger<StructureController> logger, FileSystemDbContext context, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Input()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> InputFile()
        {
            string viewMsg;
            var formFile = Request.Form.Files[0];
            if (formFile == null)
            {
                viewMsg = "Файл не вибрано";
            }
            else if (formFile.FileName.Split('.').Last() != "json")
            {
                viewMsg = "Файл повинен бути типу json";
            }
            else
            {
                List<Folder>? valuesFolder;
                try
                {
                    var result = new StringBuilder();
                    using (var reader = new StreamReader(formFile.OpenReadStream()))
                    {
                        while (reader.Peek() >= 0)
                            result.AppendLine(await reader.ReadLineAsync());
                    }
                    string stringJson = result.ToString();
                    valuesFolder = JsonConvert.DeserializeObject<List<Folder>>(stringJson);

                }
                catch(Exception)
                {
                    viewMsg = "Не вдалося декодувати json";
                    ViewBag.Message = viewMsg;
                    return View("Input");
                }
                if (valuesFolder == null)
                {
                    viewMsg = "Файл не містить папок";                 
                }
                else
                {
                    _context.Folders.RemoveRange(_context.Folders.ToList());
                    foreach (var value in valuesFolder)
                    {
                        value.Id = null;
                    }
                    foreach (var value in valuesFolder)
                    {
                        _context.Add(value);
                    }
                    _context.SaveChanges();
                    viewMsg = "Успішно імпоровано";
                }
            }
            Console.WriteLine(viewMsg);
            ViewBag.Message = viewMsg;
            return View("Input");
        }

        public ActionResult Output()
        {
            return View();
        }


        public IActionResult OutputFile()
        {
            var folders = _context.Folders.ToList();
            var utf8Bytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(folders, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            });

            string fileName = "output.json";

            return File(utf8Bytes, "application/force-download", fileName);
        }

    }
}
