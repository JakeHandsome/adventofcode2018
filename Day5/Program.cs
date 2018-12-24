using System;
using System.Collections.Generic;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputs = System.IO.File.ReadAllLines(@"..\..\..\input.txt");

            foreach (string input in inputs)
            {
                string part1 = SimplifyString(input);
                Console.WriteLine(String.Format("Part1:{0}", part1.Length));

                int part2 = int.MaxValue;
                for (char alphabet = 'A'; alphabet<='Z';alphabet++)
                {
                    int lengthOneRemoved = SimplifyString(input.Replace(alphabet.ToString(), "").Replace((((char)(alphabet + 0x20)).ToString()), "")).Length;
                    if (part2 > lengthOneRemoved)
                    {
                        part2 = lengthOneRemoved;
                    }
                }
                Console.WriteLine(String.Format("Part2:{0}", part2));
            }
        }
        static string SimplifyString (string polymerString)
        {
            List<int> indexes = new List<int>();
            char[] polymer = polymerString.ToCharArray();
            for (int i = 0; i < polymer.Length-1; i++)
            {
                if ((polymer[i] == (polymer[i+1] + 0x20)) ||
                    (polymer[i] == (polymer[i+1] - 0x20)))
                {
                    indexes.Add(i);
                    i++; //skip the next letter
                }
            }
            if (indexes.Count == 0)
            {
                return polymerString;
            }
            else
            {
                int[] indexArr = indexes.ToArray();
                for (int i = indexArr.Length-1; i > -1; i--)
                {
                    int startindex = indexArr[i];
                    polymerString = polymerString.Remove(startindex, 2);
                }
                int debug = polymerString.Length;
                return (SimplifyString(polymerString));
            }


        }
    }
}
