using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day5
    {

        public static void Fertalizer(string filePath) 
        {
            //get the top line of seeds and split them into an array 
            StreamReader sr = new StreamReader(filePath);
            var mainSeeds = sr.ReadLine()!.Substring(6);
            var seeds = mainSeeds.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(long.Parse)
                .ToArray();

            sr.ReadLine(); //empty line

            //Read each seed map 
            for (int i = 0; i < 7; i++)
            {
                sr.ReadLine(); //header 

                var seedRanges = new List<SeedRange>();
                string? line = sr.ReadLine();

                //read all the lines in a seed group
                while (!string.IsNullOrEmpty(line))
                {
                    var parts = line.Split(' ').Select(long.Parse).ToArray();
                    seedRanges.Add(new SeedRange(parts[0], parts[1], parts[2])); //build a SeedRange with SeedRange class
                    line = sr.ReadLine();
                }
                //build an array that contains x amount of arrays of the seedGroups 
                var seedRangeGroup = new SeedRangeGroup(seedRanges.ToArray());

                //Check if the seed is in Range, get the range and put it in seeds
                for(int j = 0; j < seeds.Length; j++)
                {
                    //replace the seed just used with the new destination int, this goes to the next map and tumbles down
                    seeds[j] = seedRangeGroup.Map(seeds[j]);
                }

                Console.WriteLine(seeds.Min());

            }

        }

        class SeedRange
        {
            private readonly long destinationStart;
            private readonly long sourceStart;
            private readonly long rangeLength;

            public SeedRange(long destinationStart, long sourceStart, long rangeLength)
            {
                this.destinationStart = destinationStart;
                this.sourceStart = sourceStart;
                this.rangeLength = rangeLength;
            }
            //check if the seed is in the source range 
            public bool IsInSourceRange(long value)
            {
                return value >= sourceStart && value < (sourceStart + rangeLength);
            }

            //if it is in source range, then return the seed's destination long
            public long MapSource(long value)
            {
                return destinationStart + (value - sourceStart);
            }
        }

        class SeedRangeGroup
        {
            private readonly SeedRange[] seedRanges;
            public SeedRangeGroup(SeedRange[] seedRanges)
            {
                this.seedRanges = seedRanges;
            }

            public long Map(long value)
            {
                foreach(var range in seedRanges)
                {
                    //check if seed is in the SOURCE range
                    if (range.IsInSourceRange(value))
                    {
                        return range.MapSource(value);
                    }
                }

                return value;
            }
        }
    }
}
