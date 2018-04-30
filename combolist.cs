using System;
using System.IO;
using System.Reflection;

namespace ComboMaker
{
    internal class Program
    {
        private static string runDirectory;

        public static void Main(string[] args)
        {
            Program.runDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Console.WriteLine("Combolist Olusturucu");
            Program.CheckFile();
            Console.Write("Devam etmek icin bir tusa basiniz . . .");
            Console.ReadKey(true);
        }

        public static void CheckFile() //dosyalarin varligi kontrolu
        {
            Console.WriteLine("Kullanici listesi dosyasinin adini girin");
            string users = Console.ReadLine();
            if (!File.Exists(Program.runDirectory + "/" + users + ".txt")) //userlist kontrol
            {
                Console.WriteLine(Program.runDirectory + users + ".txt");
                Console.WriteLine("Hatali kullanici listesi, tekrar deneyin");
                Program.CheckFile();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Sifre listesi dosyasinin adini girin"); //passlist kontrol
                string str = Console.ReadLine();
                if (!File.Exists(Program.runDirectory + "/" + str + ".txt"))
                {
                    Console.WriteLine("Sifre listesi bulunamadi, tekrar deneyin");
                    Program.CheckFile();
                }
                else
                {
                    foreach (string pass in File.ReadAllLines(Program.runDirectory + "/" + str + ".txt")) //combolist olusturulmasi
                        Program.ComboRunning(users, pass);
                    Console.WriteLine("user:pass combolist olusturuldu.");
                }
            }
        }

        public static void ComboRunning(string users, string pass)
        {
            using (StreamWriter streamWriter = File.AppendText(Program.runDirectory + "/" + "combolist.txt")) //combolist.txt  dosya yolu
            {
                using (StreamReader streamReader = new StreamReader(Program.runDirectory + "/" + users + ".txt"))
                {
                    string newValue;
                    while ((newValue = streamReader.ReadLine()) != null)
                    {
                        try
                        {
                            streamWriter.WriteLine(newValue + ":" + pass.Replace("%USER%", newValue)); //%user% replace edilmesi
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
            }
        }
    }
}