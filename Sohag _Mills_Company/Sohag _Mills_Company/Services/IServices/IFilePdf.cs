using Sohag__Mills_Company.Models.Entity;

namespace Sohag__Mills_Company.Services.IServices
{
    public interface IFilePdf
    {
        public string UploadImage(IFormFile image);
        public string UploadFile(IFormFile image, string existingFileName = null);
        public IEnumerable<FilePdf> GetAllFiles_Base_type(int type_pdf);

    }
}
