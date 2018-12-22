using System;
using System.Collections.Generic;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = System.IO.File.ReadAllLines(@"..\..\..\input.txt");
            #region part1
            int sum = 0;

            foreach (string number in input)
            {
                sum += int.Parse(number);
            }
            Console.WriteLine(String.Format("Part1:{0}",sum));
            #endregion

            #region part2
            int rollingsum = 0;
            SortedSet<int> frequencies = new SortedSet<int>();
            bool duplicatefound = false;
            while (!duplicatefound)
            {
                foreach (string number in input)
                {
                    rollingsum += int.Parse(number);
                    if (!frequencies.Contains(rollingsum))
                    {
                        frequencies.Add(rollingsum);
                    }
                    else
                    {
                        duplicatefound = true;
                        break;
                    }
                }
            }
            Console.WriteLine(String.Format("Part2:{0}", rollingsum));
            #endregion
        }
    }
}
