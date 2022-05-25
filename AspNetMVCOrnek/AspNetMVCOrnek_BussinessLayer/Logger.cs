using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetMVCOrnek_BussinessLayer
{
    public static class Logger //new lenmesin diye
    {
        public static void LogMessage(string message) {
            try
            {
                // Bir tane txt dosyası oluşsun onun içerisine mesajlar yazılsın.
                string dosyaAdi = "AspnetMVCOrnekLogs_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";

                string dosyaYolu = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dosyaAdi);

                StreamWriter yazici = new StreamWriter(dosyaYolu, append: true);
                yazici.Flush();
                yazici.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} {message}");
                yazici.Close();
            }
            catch (Exception ex)
            {

                
            }
        }
    }
}
