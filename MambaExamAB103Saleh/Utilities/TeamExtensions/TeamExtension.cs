namespace MambaExamAB103Saleh.Utilities.TeamExtensions
{
    public static class TeamExtension
    {
        public static bool CheckFileType(this IFormFile file, string type)
        {
            if (!file.ContentType.Contains(type))
            {
                return false;
            }
            return true;
        }

        public static bool CheckFileSize(this IFormFile file, int size)
        {
            if (file.Length >= size * 1024)
            {
                return false;
            }
            return true;
        }

        public static async Task<string> CreateFile(this IFormFile file, string root, string folder)
        {
            string filename = Guid.NewGuid().ToString() + file.FileName;
            string path = Path.Combine(root, folder, filename);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filename;
        }

        public static void DeleteFile(this string filename, string root, string folder)
        {
            string path = Path.Combine(root, folder, filename);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
