using System;
using System.Text;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace SZGYA_Szamitogepek_20231124
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Szamitogep> szamitogepek = new List<Szamitogep>();

            StreamReader sr = new StreamReader("../../../src/szamitogepek.txt");
            _ = sr.ReadLine();
            while (!sr.EndOfStream)
            {
                szamitogepek.Add(new Szamitogep(sr.ReadLine()));
            }
            sr.Close();

            foreach (var sz in szamitogepek) Console.WriteLine(sz);

            Console.WriteLine("\n7.feladat");
            List<Szamitogep> win20col = Windows20Col(szamitogepek);
            if (win20col.Count > 0) Console.WriteLine($"{win20col.Count} db ilyen gép van");
            else Console.WriteLine("[HIBA] nincs ilyen");

            Console.WriteLine("\n8.feladat");
            List<Szamitogep> leglasabbak = LeglassabbSzamitogepek(szamitogepek);
            foreach (var sz in leglasabbak) Console.WriteLine(sz);
            Console.WriteLine($"{leglasabbak.Count} db; {leglasabbak.First().CPUSebesseg} Ghz a leglassabb sebesség");

            Console.WriteLine($"\n9.feladat: {AtlagMemoriaMeret(szamitogepek)} GB az átlag memóriaméret.");

            Console.WriteLine("\n10.feladat");
            List<string> gyartok = DDR4GepekGyartoi(szamitogepek);
            if (gyartok.Count == 0) Console.WriteLine("[HIBA] nincs ilyen");
            else Console.WriteLine($"gyártók: {String.Join(", ",gyartok)}");

            Console.WriteLine($"\n11.feladat: {String.Join(", ", Intel20ColAlatti(szamitogepek).Select(sz => sz.Id))}");

            StreamWriter sw = new StreamWriter("../../../src/szamitogepekCm.txt", false, Encoding.UTF8);
            foreach (var sz in szamitogepek)
            {
                sw.WriteLine($"{sz.Gyarto} {sz.Modell}|{sz.KijelzoMeretCm}");
            }
            sw.Close();

        }

        static List<Szamitogep> Windows20Col(List<Szamitogep> szamitogepek)
        {
            return szamitogepek.Where(sz => sz.OS.Contains("Windows") && sz.KijelzoMeret > 20f).ToList();
        }

        static List<Szamitogep> LeglassabbSzamitogepek(List<Szamitogep> szamitogepek)
        {
            return szamitogepek.Where(sz => sz.CPUSebesseg == szamitogepek.Min(szz => szz.CPUSebesseg)).ToList();
        }

        static double AtlagMemoriaMeret(List<Szamitogep> szamitogepek)
        {
            return szamitogepek.Average(sz => sz.RAM);
        }

        static List<string> DDR4GepekGyartoi(List<Szamitogep> szamitogepek)
        {
            return szamitogepek
                .Where(sz => sz.RAMTipus == "DDR4")
                .Select(sz => sz.Gyarto)
                .Distinct()
                .OrderBy(sz => sz)
                .ToList();
        }

        static List<Szamitogep> Intel20ColAlatti(List<Szamitogep> szamitogepek)
        {
            return szamitogepek.Where(sz => sz.CPU.Contains("Intel") && sz.KijelzoMeret < 20f).ToList();
        }

    }
}
