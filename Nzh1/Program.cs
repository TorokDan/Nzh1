using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nzh1
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] forest = GenerateForest();
            Show(forest);
            Console.WriteLine();
            SpawnGhost(forest);
            Show(forest);
            Console.WriteLine();
            Console.WriteLine(NbrOfCandies(forest));
            RemoveCandies(forest);
            Show(forest);
            Console.WriteLine();
            Console.WriteLine(NbrOfCandies(forest));
            Console.WriteLine(DistributedEntries(forest, 'T'));
            char[] elemek = GetAllEntries(forest);
            for (int i = 0; i < elemek.Length; i++)
            {
                Console.Write(elemek[i] + " ");
            }

        }
        static char[,] GenerateForest()
        {
            char[,] forest = new char[20, 20];
            Random rnd = new Random();
            char[] szamok = { '1', '2', '3', '4', '5' };
            for (int i = 0; i < forest.GetLength(0); i++)
            {
                for (int j = 0; j < forest.GetLength(1); j++)
                {
                    int szazalek = rnd.Next(0, 101);
                    if (szazalek <= 35) forest[i, j] = 'T';
                    else if (szazalek <= 60) forest[i, j] = szamok[rnd.Next(0, 5)]; //itt kérdéses ez, mivel nem vagyok benne biztos, hogy az 5 is kell
                    else if (!CheckNeighbours(forest, i, j, 'C') && szazalek <= 60) forest[i, j] = 'C';
                    else if (CheckNeighbours(forest, i, j, 'C') && szazalek <= 40) forest[i, j] = 'C';
                    else forest[i, j] = ' ';
                }
            }
            return forest;
        }
        static bool CheckNeighbours(char[,] forest, int i, int j, char soughValue)
        {
            bool sorElso = i == 0;
            bool sorUtolso = i == forest.GetLength(0) - 1;
            bool oszlopElso = j == 0;
            bool oszlopUtolso = j == forest.GetLength(1) - 1;
            if (!sorElso && !oszlopElso && forest[i - 1, j - 1] == soughValue) return true;
            if (!sorUtolso && !oszlopElso && forest[i + 1, j - 1] == soughValue) return true;
            if (!sorElso && !oszlopUtolso && forest[i - 1, j + 1] == soughValue) return true;
            if (!sorElso && forest[i - 1, j] == soughValue) return true;
            if (!sorUtolso && forest[i + 1, j] == soughValue) return true;
            if (!oszlopElso && forest[i, j - 1] == soughValue) return true;
            if (!oszlopUtolso && forest[i, j + 1] == soughValue) return true;
            return false;
        }
        static void Show(char[,] forest)
        {
            for (int i = 0; i < forest.GetLength(0); i++)
            {
                for (int j = 0; j < forest.GetLength(1); j++)
                {
                    Console.Write(forest[i, j] + " ");
                }
                Console.WriteLine();
            }
            for (int i = 0; i < 39; i++) //itt azért 39, mert h ha 40-ig megy, olyan, mintha 1-el több _ karakter lenne.
            {
                Console.Write('_');
            }
        }
        static int NbrOfEntries(char[,] forest, char target)
        {
            int db = 0;
            for (int i = 0; i < forest.GetLength(0); i++)
            {
                for (int j = 0; j < forest.GetLength(1); j++)
                {
                    if (forest[i, j] == target) db++;
                }
            }
            return db;
        }
        static bool DistributedEntries(char[,] forest, char target)
        {
            for (int i = 0; i < forest.GetLength(0); i++)
            {
                bool vanSorban = false;
                for (int j = 0; j < forest.GetLength(1); j++)
                {
                    if (forest[i, j] == target) vanSorban = true;
                }
                if (!vanSorban) return false;
            }
            return true;
        }
        static void SpawnGhost(char[,] forest)
        {
            Random rnd = new Random();
            for (int i = 0; i < forest.GetLength(0); i++)
            {
                for (int j = 0; j < forest.GetLength(1); j++)
                {
                    if (forest[i, j] == ' ' && rnd.Next(0, 101) <= 15) forest[i, j] = '*';
                }
            }
        }
        static void RemoveCandies(char[,] forest)
        {
            for (int i = 0; i < forest.GetLength(0); i++)
            {
                bool sor = false;
                int szamlalo = 0;
                while (sor == false && szamlalo < forest.GetLength(1))
                {
                    if (forest[i, szamlalo] == '*')
                    {
                        sor = true; 
                        for (int j = 0; j < forest.GetLength(1); j++)
                        {
                            if (forest[i, j] == '1' || forest[i, j] == '2' || forest[i, j] == '3' || forest[i, j] == '4' || forest[i, j] == '5') forest[i, j] = ' ';
                        }
                    }
                    szamlalo++;
                }
            }
            for (int j = 0; j < forest.GetLength(1); j++)
            {
                bool oszlop = false;
                int szamlalo = 0;
                while(oszlop == false && szamlalo < forest.GetLength(0))
                {
                    if ( forest[szamlalo, j] == '*')
                    {
                        oszlop = true;
                        for (int i = 0; i < forest.GetLength(0); i++)
                        {
                            if (forest[i, j] == '1' || forest[i, j] == '2' || forest[i, j] == '3' || forest[i, j] == '4' || forest[i, j] == '5') forest[i, j] = ' ';
                        }
                    }
                    szamlalo++;
                }
            }
        }
        static int NbrOfCandies(char[,] forest)
        {
            int darab = 0;
            for (int i = 0; i < forest.GetLength(0); i++)
            {
                for (int j = 0; j < forest.GetLength(1); j++)
                {
                    if (forest[i, j] == '1' || forest[i, j] == '2' || forest[i, j] == '3' || forest[i, j] == '4' || forest[i, j] == '5') darab++;
                }
            }
            return darab;
        }
        static char[] GetAllEntries(char[,] forest)
        {
            char[] tomb = new char[NbrOfEntries(forest, 'T') + NbrOfEntries(forest, 'C') + NbrOfEntries(forest, '*') + NbrOfCandies(forest)];
            int szamlalo = 0;
            for (int i = 0; i < forest.GetLength(0); i++)
            {
                for (int j = 0; j < forest.GetLength(1); j++)
                {
                    if (forest[i, j] == 'T' || forest[i, j] == 'C' || forest[i, j] == '*' || forest[i, j] == '1' || forest[i, j] == '2' || forest[i, j] == '3' || forest[i, j] == '4' || forest[i, j] == '5') 
                    {
                        tomb[szamlalo] = forest[i, j];
                        szamlalo++;
                    }
                }
            }
            return tomb;
        }
    }
}
