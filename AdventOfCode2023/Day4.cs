using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day4
    {
        public static void CardsSums(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);

            string Card = sr.ReadLine();

            int totalPoints = 0;

            int index = Card.IndexOf(":");
            string fixedCard = Card.Substring(index+1);

            int index2 = fixedCard.IndexOf("|");
            string winningNums = fixedCard.Substring(0, index2).Trim();
            string myNums = fixedCard.Substring(index2+1).Trim();

            //convert strings to arrays for comparison
            string[] winningArray = winningNums.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int[] winningNumsArray = Array.ConvertAll(winningArray, int.Parse);
            string[] myArray = myNums.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int[] myNumsArray = Array.ConvertAll(myArray, int.Parse);

            int cardWins = 0;

            //Loop through my number and compare each to an array of the winning ones
            //keep a total of the matches, then calculate how many points that card has 
            while (!sr.EndOfStream)
            {
                for (int i = 0; i < myNumsArray.Length; i++)
                {

                    for (int j = 0; j < winningNumsArray.Length; j++)
                    {

                        if (myNumsArray[i] == winningNumsArray[j])
                        {
                            cardWins++;
                        }
                    }

                }
                if (cardWins == 0)
                {
                    totalPoints += 0;
                }
                else
                {
                    totalPoints += (int)Math.Pow(2, cardWins - 1);
                }

                Console.WriteLine(totalPoints);

                Card = sr.ReadLine();
                index = Card.IndexOf(":");
                fixedCard = Card.Substring(index + 1);

                index2 = fixedCard.IndexOf("|");
                winningNums = fixedCard.Substring(0, index2).Trim();
                myNums = fixedCard.Substring(index2 + 1).Trim();

                //convert strings to arrays for comparison
                winningArray = winningNums.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                winningNumsArray = Array.ConvertAll(winningArray, int.Parse);
                myArray = myNums.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                myNumsArray = Array.ConvertAll(myArray, int.Parse);

                cardWins = 0;
            }
            Console.WriteLine(totalPoints);
        }

     }
}
