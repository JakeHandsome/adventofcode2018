using System;
using System.Collections.Generic;

namespace Day2
{
    class Program
    {
        static int diffIndex = 0;
        static void Main(string[] args)
        {
            string[] input = System.IO.File.ReadAllLines(@"..\..\..\input.txt");

            #region part1
            int num2 = 0;
            int num3 = 0;
            foreach (string ID in input)
            {
                SortedDictionary<char, int> D = new SortedDictionary<char, int>();
                char[] letters = ID.ToCharArray();
                foreach (char letter in letters)
                {
                    if (D.ContainsKey(letter))
                    {
                        D[letter]++;
                    }
                    else
                    {
                        D.Add(letter, 1);
                    }
                }
                bool found2 = false;
                bool found3 = false;
                foreach (KeyValuePair<char, int> kvp in D)
                {
                    if (kvp.Value == 2 && !found2)
                    {
                        num2++;
                        found2 = true;
                    }
                    if (kvp.Value == 3 && !found3)
                    {
                        num3++;
                        found3 = true;
                    }
                }
            }
            int answer = num2 * num3;
            Console.WriteLine("Part1:{0}", answer);

            #endregion
            
            #region part2
            string result = null;
            
            for (int i = 0; i < input.Length -1; i++)
            {
                if (result != null)
                {
                    break;
                }
                
                for (int j = i+1; j<input.Length; j++)
                {
                    if (OffByOne(input[i], input[j]))
                    {
                        result = input[i].Remove(diffIndex, 1);
                        break;
                    }
                }

            }
            Console.WriteLine("Part2:{0}", result);
            #endregion
            
        }
        #region helper functions
        static bool OffByOne(string a, string b)
        {
            int numdiff = 0;
            char[] str1 = a.ToCharArray();
            char[] str2 = b.ToCharArray();
            for (int i = 0; i < str1.Length; i++)
            {

                if (str1[i] != str2[i])
                {
                    numdiff++;
                    diffIndex = i;
                }
                if (numdiff > 1)
                {
                    break;
                }

            }
            return (numdiff == 1);
        }
        #endregion
    }
}
