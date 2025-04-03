namespace Company.S03.PL.Helpers
{
    public class DocumentSetting
    {
        public static string UploadFile(IFormFile file , string folderName)
        {
            //1. Get Folder Location
            //var folderPath = Directory.GetCurrentDirectory() + "\\wwwroot\\Files\\" + folderName;

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", folderName);

            
            //Get File Name and Make it Unique
            var fileName = $"{Guid.NewGuid()}{file.FileName}";

            //File Path
            var filePath = Path.Combine(folderPath, fileName);

            using var fileStram = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStram);

            return fileName;
        }

        public static void DeleteFile(string fileName,String folderName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", folderName, fileName);

            if(File.Exists(filePath))
                File.Delete(filePath);

        }
    }
}
