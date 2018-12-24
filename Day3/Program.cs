using System;
using System.Text.RegularExpressions;

namespace Day3
{
    class Program
    {
        static int[,] Graph = new int[2000, 2000];
        static void Main(string[] args)
        {
            string[] inputs = System.IO.File.ReadAllLines(@"..\..\..\input.txt");
            #region part1
            Regex inputParsing = new Regex(@"#(\d*) @ (\d*),(\d*): (\d*)x(\d*)");
            foreach (string input in inputs)
            {
                int ID;
                int x;
                int y;
                int w;
                int l;
                MatchCollection matches = inputParsing.Matches(input);
                foreach (Match m in matches)
                {
                    GroupCollection g = m.Groups;
                    ID = int.Parse(g[1].Value);
                    x = int.Parse(g[2].Value);
                    y = int.Parse(g[3].Value);
                    w = int.Parse(g[4].Value);
                    l = int.Parse(g[5].Value);
                    AddToArray(x, y, w, l);
                }
            }
            int area = 0;
            for (int i = 0; i<2000;i++)
            {
                for (int j = 0; j<2000; j++)
                {
                    if (Graph[i,j] > 1)
                    {
                        area++;
                    }
                }
            }
            Console.WriteLine(String.Format("Part1:{0}", area));
            #endregion

            #region part2
            int FinalID = 0;
            foreach (string input in inputs)
            {
                int ID;
                int x;
                int y;
                int w;
                int l;
                MatchCollection matches = inputParsing.Matches(input);
                foreach (Match m in matches)
                {
                    GroupCollection g = m.Groups;
                    ID = int.Parse(g[1].Value);
                    x = int.Parse(g[2].Value);
                    y = int.Parse(g[3].Value);
                    w = int.Parse(g[4].Value);
                    l = int.Parse(g[5].Value);
                    if (DoesIDOverlap(x, y, w, l))
                    {
                        FinalID = ID;
                    }
                }
                if (FinalID != 0)
                {
                    Console.WriteLine(string.Format("Part2:{0}", FinalID));
                    break;
                }
            }
            #endregion
        }
        static void AddToArray(int x, int y, int w, int l)
        {
            for (int i = x; i<x+w;i++)
            {
                for (int j=y;j<y+l;j++)
                {
                    Graph[i,j]++;
                }
            }
        }
        static bool DoesIDOverlap(int x, int y, int w, int l)
        {
            for (int i = x; i < x + w; i++)
            {
                for (int j = y; j < y + l; j++)
                {
                    if (Graph[i, j] != 1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
