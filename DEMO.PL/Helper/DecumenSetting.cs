namespace DEMO.PL.Helper
{
    public static class DecumenSetting
    {
        public static string UploadFile(IFormFile file , string foldername)
        {
            //get pass of folder
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/File",foldername);
            // file name
            var fileName = $"{Guid.NewGuid}-{Path.GetFileName(file.FileName)}";
           // get File path
           var filepath = Path.Combine(folderPath,fileName);
            using var Filestream = new FileStream(filepath, FileMode.Create);
            file.CopyTo(Filestream);
            return fileName;
        }
    }
}
