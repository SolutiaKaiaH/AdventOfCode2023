using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2023
{
    internal class Day3
    {
        public static void PartSums(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);

            string topLine = sr.ReadLine();
            string focusLine = sr.ReadLine();
            string bottomLine = sr.ReadLine();

            string topLineSub;
            string bottomLineSub;
            string focusLineSub;

            double totalNumber = 0;

            char[] symbolsToCheck = { '!', '*', '@', '#', '%', '^', '&', '(', ')', '-', '_', '+', '=', '?', '/', '$' };

            string number = "";

            int firstIndex = 0;

            while (bottomLine != null)
            {
                while (focusLine.Any(char.IsDigit))
                {
                    for (int k = 0; k < focusLine.Length; k++)
                    {
                        if (char.IsDigit(focusLine[k]))
                        {
                            if (firstIndex == 0)
                            {
                                firstIndex = k;
                            }

                            string hold = focusLine[k].ToString();
                            number = number + hold;
                            if (!char.IsDigit(focusLine[k + 1]))
                            {
                                k = focusLine.Length;
                            }
                        }
                    }

                    if (firstIndex > 0)
                    {
                        firstIndex -= 1;
                    }
                    else
                    {
                        firstIndex = 0;
                    }

                    topLineSub = topLine.Substring(firstIndex, number.Length + 2);
                    bottomLineSub = bottomLine.Substring(firstIndex, number.Length + 2);
                    focusLineSub = focusLine.Substring(firstIndex, number.Length + 2);

                    if (symbolsToCheck.Any(symbol => topLineSub.Contains(symbol)))
                    {
                        totalNumber += int.Parse(number);
                    }
                    else if (symbolsToCheck.Any(symbol => bottomLineSub.Contains(symbol)))
                    {
                        totalNumber += int.Parse(number);
                    }
                    else if (symbolsToCheck.Any(symbol => focusLineSub.Contains(symbol)))
                    {
                        totalNumber += int.Parse(number);
                    }

                    //make new substring to replace
                    string replace = "";
                    for (int j = 0; j < number.Length; j++)
                    {
                        replace = replace + ".";
                    }

                    focusLine = focusLine.Substring(0, firstIndex + 1) + replace + focusLine.Substring(firstIndex + number.Length + 1);
                    number = "";
                    firstIndex = 0;
                }

                number = "";
                topLineSub = "";
                bottomLineSub = "";
                focusLineSub = "";
                topLine = focusLine;
                focusLine = bottomLine;
                bottomLine = sr.ReadLine();
            }

            Console.WriteLine(totalNumber);
        }
    }
}
