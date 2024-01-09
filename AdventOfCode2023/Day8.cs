using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day8
    {
        public static void LeftRight(string filePath)
        {
            string[] input = File.ReadAllLines(filePath);

            string firstLine = input[0]; //Get the LRLR Directions

            string[] Directions = input.Skip(2).ToArray();

            string directionLine = Directions[0].ToString();

            string insideParentheses = "";
            string LeftD = "";
            string RightD = "";

            int steps = 0;
            string toFind = "";
            int i = 0;

            //get AAA starting line
            foreach (string line in Directions)
            {
                string firstThreeCharacters = line.Substring(0, Math.Min(3, line.Length));
                if (firstThreeCharacters.Equals("AAA"))
                {
                    insideParentheses = line.Substring(7, 8);
                    LeftD = insideParentheses.Substring(0, 3);
                    RightD = insideParentheses.Substring(5, 3);
                    break;
                }
            }

            if (firstLine[i] == 'L')
            {
                toFind = LeftD;
            }
            else
            {
                toFind = RightD;
            }
            i++;
            steps++;


            while (toFind != "ZZZ")
            {
                                               
                foreach (string line in Directions)
                {
                    string firstThreeCharacters = line.Substring(0, Math.Min(3, line.Length));
                    if (firstThreeCharacters.Equals(toFind))
                    {
                        insideParentheses = line.Substring(7, 8);
                        LeftD = insideParentheses.Substring(0, 3);
                        RightD = insideParentheses.Substring(5, 3);
                        break;
                    }
                }
                if (firstLine[i] == 'L')
                {
                    toFind = LeftD;
                }
                else
                {
                    toFind = RightD;
                }

                i++;

                if (i == firstLine.Length)
                {
                    i = 0;
                }
                steps++;
               
            }
            Console.WriteLine(steps);
        }
    }
}
