using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day4P2
    {

        record Card(int[] WinningNumbers, int[] MyNumbers);

        public void CopiesSum(string filePath)
        {
            var input = File.ReadAllLines(filePath);
            // each 'slot' has a 1 in it right now
            int[] cardCount = Enumerable.Repeat(1, input.Length).ToArray();

            // loop over each card
            for (int cardId = 0; cardId < input.Length; cardId++)
            {
                string? line = input[cardId];
                var card = ParseLine(line);

                //number of winning numbers
                var matchCount = card.WinningNumbers.Intersect(card.MyNumbers).Count();

                // for the number of wins, update any cards with extras.
                for (int i = 0; i < matchCount; i++)
                {
                    cardCount[cardId + 1 + i] += cardCount[cardId];
                }
            }

            Console.WriteLine(cardCount.Sum());
        }

        Card ParseLine(string line)
        {
            var parts = line.Split(':');
            var numbers = parts[1].Split('|');
            var winningNumbers = numbers[0].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                 .Select(int.Parse)
                 .ToArray();
            var myNumbers = numbers[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                 .Select(int.Parse)
                 .ToArray();

            return new Card(winningNumbers, myNumbers);
        } 
    }
}
