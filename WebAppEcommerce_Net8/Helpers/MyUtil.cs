using System.Reflection.Metadata;
using System.Text;

namespace WebAppEcommerce_Net8.Helpers
{
    public class MyUtil
    {
        public static string UploadHinh(IFormFile Hinh, string folder)
        {
            try
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", folder, Hinh.FileName);
                using (var myfile = new FileStream(fullPath, FileMode.CreateNew))
                {
                    Hinh.CopyTo(myfile);

                }
                return Hinh.FileName;
            } catch(Exception ex)
            {
                return String.Empty;
            }
        }
        public static string GenerateRanDomKey(int lenght = 5)
        {
            var patten = @"qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM!";
            var sb = new StringBuilder();
            var rd = new Random();
            for (int i = 0; i < lenght; i++)
            {
                sb.Append(patten[rd.Next(0, patten.Length)]);
            }
            return sb.ToString();
        }
    }
}
