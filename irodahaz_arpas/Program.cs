using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace irodahaz_arpas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Iroda> irodak = new List<Iroda>();
            using (StreamReader sr = new StreamReader(@"..\..\..\src\irodahaz.txt", encoding: Encoding.UTF8))
            {
                int emeletIndex = 1;

                while (!sr.EndOfStream)
                {
                    irodak.Add(new Iroda(sr.ReadLine(), emeletIndex));
                    emeletIndex++;
                }
            }

            Console.WriteLine("\n7. feladat:");

            Console.WriteLine($"Emelet | {"Kód",-13} | Beköltözés | Irodalétszámok");

            foreach (var item in irodak)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\n8. feladat:");

            Console.WriteLine($"A(z) {LegtobbDolgozoEmelet(irodak).Emelet} emeleten dolgoznak a legtöbben.");

            Console.WriteLine("\n9. feladat:");

            var f9 = KilencDolgozosIroda(irodak);
            if (f9 != null) Console.WriteLine(f9); else Console.WriteLine("Hiba! Nincs emelet 9 dolgozóval.");

            Console.WriteLine("\n10. feladat:");

            Console.WriteLine($"{OtDolgozonalTobbIroda(irodak)} irodában vannak 5-nél többen.");

            Console.WriteLine("\n11. feladat:");

            using StreamWriter sw = new StreamWriter(@"..\..\..\src\nemdolgoznak.txt", false, encoding: Encoding.UTF8);

            List<string> nemDolgozokLista = NemDolgoznak(irodak);
            foreach (var nemDolgozo in nemDolgozokLista)
            {
                sw.WriteLine(nemDolgozo);
            }

            Console.WriteLine("A fájlbeírás megtörtént.");

            Console.WriteLine("\n12. feladat:");

            Console.WriteLine($"A LOGMEIN kódú cég irodáiban átlagosan {AtlagDolgozoLOGMEIN(irodak)} személy dolgozik.");

            Console.WriteLine("\n13. feladat:");

            AdottEmeletenDolgozokSzama(irodak, sw);
            
            Console.WriteLine("A fájlbeírás megtörtént.");

            Console.WriteLine();

            Console.WriteLine("\n14. feladat:");
            Console.WriteLine($"Az irodai dolgozók összlétszáma {OsszesFoAzIrodaban(irodak)}.");

            Console.WriteLine("\n15. feladat:");

            Console.WriteLine($"Az első irodabérlés éve {ElsoIrodaBerlesEve(irodak)}.");

            Console.WriteLine("\n16. feladat:");
            Console.WriteLine($"{EnnyiEveNemTortentUjIrodaberles(irodak)} éve nem történt új irodabérlés.");

            Console.ReadKey();
        }

        static Iroda LegtobbDolgozoEmelet(List<Iroda> irodak) => irodak.MaxBy(i => i.IrodaLetszamok.Sum());
        static Iroda KilencDolgozosIroda(List<Iroda> irodak) => irodak.SingleOrDefault(i => i.IrodaLetszamok.Any(e => e == 9));
        static int OtDolgozonalTobbIroda(List<Iroda> irodak) => irodak.Sum(i => i.IrodaLetszamok.Count(e => e > 5));
        static List<string> NemDolgoznak(List<Iroda> irodak)
        {
            List<Iroda> emeletenNemDolgoznak = irodak.Where(i => i.IrodaLetszamok.Contains(0)).ToList();
            List<string> returnList = new List<string>();
            foreach (var e in emeletenNemDolgoznak)
            {
                List<int> sorszamok = new List<int>();
                for (int i = 0; i < e.IrodaLetszamok.Count; i++)
                {
                    if (e.IrodaLetszamok[i] == 0)
                    {
                        sorszamok.Add(i + 1);
                    }
                }
                string cegKod = e.Kod;
                string teljesAdat = $"{cegKod} {string.Join(" ", sorszamok)}";
                returnList.Add(teljesAdat);
            }
            return returnList;
        }
        static int AtlagDolgozoLOGMEIN(List<Iroda> irodak) => (int)irodak.First(i => i.Kod == "LOGMEIN").IrodaLetszamok.Average(e => e);
        static void AdottEmeletenDolgozokSzama(List<Iroda> irodak, StreamWriter sw)
        {
            sw.WriteLine("Az emeletenkénti dolgozó emberek száma:");
            for (int i = 0; i < irodak.Count; i++) sw.WriteLine($"{i + 1}. {irodak[i].IrodaLetszamok.Sum()}");
        }
        static int OsszesFoAzIrodaban(List<Iroda> irodak) => irodak.Sum(i => i.IrodaLetszamok.Sum());
        static int ElsoIrodaBerlesEve(List<Iroda> irodak) => irodak.Min(i => i.Kezdet);
        static int EnnyiEveNemTortentUjIrodaberles(List<Iroda> irodak) => -(irodak.Max(i => i.Kezdet) - DateTime.Now.Year);
    }
}