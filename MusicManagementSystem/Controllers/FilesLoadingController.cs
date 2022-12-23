using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicManagementSystem.Data;
using MusicManagementSystem.Services.CloudStorage;
using MusicManagementSystem.ViewModels.FileLoading;
using System.Collections;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace MusicManagementSystem.Controllers
{
    public class FilesLoadingController : Controller
    {
        private readonly ILogger<FilesLoadingController> _logger;
        private readonly MusicManagemetSystemDbContext _context;
        private readonly ICloudStorage _cloudStorage;

        public FilesLoadingController(ILogger<FilesLoadingController> logger,
            MusicManagemetSystemDbContext context,
            ICloudStorage cloudStorage)
        {
            _logger = logger;
            _context = context;
            _cloudStorage = cloudStorage;
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
                        var tags = tfile.Tag
                            .GetType()
                            .GetProperties()
                            .Where(p => p.GetSetMethod() != null);

                        foreach (var tag in tags)
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

    public static class FileLoadingUtils
    {
        // this function was used for generating json samples from mp3 files
        public static void DumpTagLibTagProperties(string filename, TagLib.Tag tag)
        {
            string jsonStr = JsonSerializer.Serialize(tag, new JsonSerializerOptions
            {
                IgnoreReadOnlyFields = true,
                IgnoreReadOnlyProperties = true,
                NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals,
                WriteIndented = true,
            });

            File.WriteAllText(filename + ".json", Regex.Unescape(jsonStr), Encoding.UTF8);
        }
    }
}

