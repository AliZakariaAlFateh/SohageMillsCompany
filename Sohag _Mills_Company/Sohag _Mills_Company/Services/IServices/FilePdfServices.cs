using Microsoft.AspNetCore;
using Sohag__Mills_Company.Models.DataContext;
using Sohag__Mills_Company.Models.Entity;

namespace Sohag__Mills_Company.Services.IServices
{
    public class FilePdfServices :Repository<FilePdf>, IFilePdf
    {
        private readonly ECcontext db;
        private readonly IWebHostEnvironment webHost;

        public FilePdfServices(ECcontext _db, IWebHostEnvironment _webHost) : base(_db, _webHost)
        {
            this.db = _db;
            this.webHost = _webHost;
        }
        public  IEnumerable<FilePdf> GetAllFiles_Base_type(int type_pdf)
        {
            return db.FilePdfs.Where(typepdf=>typepdf.type_pdf== type_pdf).ToList() ?? Enumerable.Empty<FilePdf>();
        }

        public string UploadFile(IFormFile image, string existingFileName = null)
        {
            string ImageName = null;
            string filePath = null;
            if (image != null)
            {
                string uploadsFolder = Path.Combine(webHost.WebRootPath, "Images");
                if (existingFileName != null)
                {
                    ImageName = image.FileName;
                     filePath = Path.Combine(uploadsFolder, existingFileName);
                    // If the file already exists, delete it before replacing
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
                else
                {
                    ImageName = image.FileName;

                }
                filePath = Path.Combine(uploadsFolder, ImageName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
                // Copy the new file}
            }
                return ImageName;
        }



        public string UploadImage(IFormFile image) //method to upload image in my site
        {
            string uploadsFolder = Path.Combine(webHost.WebRootPath, "Images");
            string ImageName = Guid.NewGuid().ToString() + "_" + image.FileName;
            string filePath = Path.Combine(uploadsFolder, ImageName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }
            return ImageName;
        }

    }
}
