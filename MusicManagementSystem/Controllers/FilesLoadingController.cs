using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicManagementSystem.ViewModels.FileLoading;

namespace MusicManagementSystem.Controllers
{
    public class FilesLoadingController : Controller
    {
        private readonly ILogger<FilesLoadingController> _logger;
        public FilesLoadingController(ILogger<FilesLoadingController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UploadMusic(FileUploadViewModel fileUploadViewModel)
        {
            if (ModelState.IsValid)
            {
                foreach (var file in fileUploadViewModel.FormFileCollection)
                {
                    _logger.LogInformation("Uploaded file {filename}", file.FileName);
                    _logger.LogInformation("SIZE {length}", file.Length / 1_048_576);
                    string untrustedFileName = Path.GetFileName(file.FileName);
                    using (var stream = file.OpenReadStream())
                    {
                        var tfile = TagLib.File.Create(new FileAbstraction(untrustedFileName, stream));
                        foreach (var tag in tfile.Tag.GetType().GetProperties().Where(p => p.GetGetMethod() != null))
                        {
                            _logger.LogInformation("Tag:{tagName} -> {tagValue}", tag.Name, tag.GetValue(tfile.Tag));
                        }
                    }
                }
                RedirectToAction("Index");
            }
            return View(viewName: "Index");
        }
    }
    public class FileAbstraction : TagLib.File.IFileAbstraction
    {
        public FileAbstraction(string name, Stream stream)
        {
            Name = name;
            FileStream = stream;
        }
        public Stream FileStream { get; set; }
        public string Name { get; }

        public Stream ReadStream => FileStream;

        public Stream WriteStream => FileStream;


        public void CloseStream(Stream stream)
        {
            stream.Close();
        }
    }
}

