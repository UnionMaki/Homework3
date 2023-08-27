using System;
using System.Linq;

namespace BDCrypt
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Crypt crypter = new Crypt();
                string inputline = Console.ReadLine();
                crypter.EnCrypt(ref inputline);
                Field _Data = new Field { CryptedWord = $"{inputline}" };
                db.Data.Add(_Data);
                db.SaveChanges();
                var list = db.Data.ToList();
                Console.WriteLine("crypted list:");
                foreach (Field u in list)
                {
                    Console.WriteLine($"{u.Id}.{u.CryptedWord}");
                }
                int id = int.Parse( Console.ReadLine() );
                Field el = db.Data.FirstOrDefault(x => x.Id == id);
                string outputline = el.CryptedWord;
                crypter.DeCrypt(ref outputline);
                Console.WriteLine($"{outputline}");
            }
        }
        public class Crypt
        {
            private string Al = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЬЫЭЮЯабвгдеёжзийклмнопрстуфхцчшщъьыэюя";
            private string res;

            public void EnCrypt(ref string inputline)
            {
                res = string.Empty;
                foreach (char ch in inputline)
                {
                    int i = 0;
                    while (ch != Al[i])
                    {
                        i++;
                    }
                    if (i == 0)
                        res += "Ю";
                    else
                        if (i == 1)
                        res += "Я";
                    else
                            if (i == 33)
                        res += "ю";
                    else
                                if (i == 34)
                        res += "я";
                    else
                        res += Al[i - 2];
                }
                inputline = res;

            }
            public void DeCrypt(ref string inputline)
            {
                res = string.Empty;
                foreach (char ch in inputline)
                {
                    int i = 0;
                    while (ch != Al[i])
                    {
                        i++;
                    }
                    if (i == 31)
                        res += "А";
                    else
                        if (i == 32)
                        res += "Б";
                    else
                            if (i == 64)
                        res += "а";
                    else
                                if (i == 65)
                        res += "б";
                    else
                        res += Al[i + 2];
                }
                inputline = res;

            }
        }
    }
}