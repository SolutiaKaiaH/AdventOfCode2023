using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2023
{
    internal class Day7
    {
        public static void CamelCards(string filePath)
        {
            string[] input = File.ReadAllLines(filePath);

            List<Hand> hands = input.Select(ParseHand).ToList();

            // Separate hands into different lists based on their ranks
            List<Hand>[] handsByRank = new List<Hand>[8];
            for (int i = 0; i < handsByRank.Length; i++)
            {
                handsByRank[i] = new List<Hand>();
            }

            foreach (var hand in hands)
            {
                handsByRank[hand.Rank].Add(hand);
            }

            int totalWinnings = 0;
            int rank = 5;

            // Sort hands within each rank based on tie-breaking rules
            foreach (var rankHands in handsByRank)
            {
                if (rankHands.Count > 1)
                {
                    rankHands.Sort(new HandComparer());
                }

                foreach (var hand in rankHands)
                {
                    totalWinnings += hand.Bid * rank;
                    rank--;
                }
            }

            Console.WriteLine($"Total Winnings: {totalWinnings}");

            static Hand ParseHand(string input)
            {
                string[] parts = input.Split(' ');
                string cards = parts[0];
                int bid = int.Parse(parts[1]);
                return new Hand(cards, bid);
            }
        }

        class Hand : IComparable<Hand>
        {
            public string OriginalCards { get; }
            public string Cards { get; set; }
            public int Bid { get; }
            public int Rank { get; }

            public Hand(string cards, int bid)
            {
                OriginalCards = cards;
                Cards = cards;
                Bid = bid;
                Rank = GetHandType();  // Set Rank during construction
            }

            public int CompareTo(Hand other)
            {
                // Compare ranks in descending order
                return other.Rank.CompareTo(Rank);
            }

            public int GetHandType()
            {
                int rank = 7;
                var charCounts = Cards.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
                if (Cards.Contains('J'))
                {
                    char maxCountKey = charCounts.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;  // Find the key with the maximum count
                    Cards = Cards.Replace('J', maxCountKey);  // Replace 'J' with the key with the maximum count
                    charCounts = Cards.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
                    Cards = OriginalCards;
                }
               


                if (charCounts.Any(kv => kv.Value >= 5))
                {
                    rank = 1; // Five of a kind
                }
                else if (charCounts.Any(kv => kv.Value == 4))
                {
                    rank = 2; // Four of a kind
                }
                else if (charCounts.Any(kv => kv.Value == 3))
                {
                    int pairCount = charCounts.Count(kv => kv.Value == 2);
                    rank = pairCount == 1 ? 3 : 4; // Full house or Three of a kind
                }
                else if (charCounts.Any(kv => kv.Value == 2))
                {
                    int pairCount = charCounts.Count(kv => kv.Value == 2);

                    if (pairCount == 2)
                    {
                        rank = 5; // Two pair
                    }
                    else if (pairCount == 1)
                    {
                        rank = 6; // One pair
                    }
                }
                //High card 
                else
                {
                    rank = 7; 
                }
                return rank;
            }
        }

        class HandComparer : IComparer<Hand>
        {
            private static string CardStrengthOrder = "AKQT98765432J";

            public int Compare(Hand x, Hand y)
            {
                // Compare individual cards based on tie-breaking rules
                for (int i = 0; i < x.Cards.Length; i++)
                {
                    int xCardStrength = CardStrengthOrder.IndexOf(x.Cards[i]);
                    int yCardStrength = CardStrengthOrder.IndexOf(y.Cards[i]);

                    if (xCardStrength != yCardStrength)
                    {
                        // Cards have different strengths, return the comparison result
                        return xCardStrength.CompareTo(yCardStrength);
                    }
                }

                // All cards are equal, return 0
                return 0;
            }
        }
    }
}
