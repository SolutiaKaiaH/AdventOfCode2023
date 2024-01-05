using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day6
    {
        public static void BoatRace(string filePath)
        {
            string input = File.ReadAllText(filePath);

            Race race = new Race();
            long[] raceRecords = race.RaceRecords(input);

            int sumWays = 1;

            for(int i = 0; i < raceRecords.Length; i+=2)
            {
                long time = raceRecords[i];
                long distance = raceRecords[i+1];

                sumWays = sumWays * WaysToBeat(time,distance); 
            }

            Console.WriteLine(sumWays);
        }

        class Race
        {

            public long[] RaceRecords(string raceInput)
            {
                // Split the input by lines
                string[] lines = raceInput.Split('\n', StringSplitOptions.RemoveEmptyEntries);

                // Skip the header lines
                string[] timeValues = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string[] distanceValues = lines[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                // get each pair of values and store them in an array
                long[] races = new long[(timeValues.Length - 1) * 2];
                //UNCOMMENT FOR PART 1 
                //for (int i = 1; i < timeValues.Length; i++)
                //{
                //    if (!int.TryParse(timeValues[i], out races[(i - 1) * 2]) || !int.TryParse(distanceValues[i], out races[(i - 1) * 2 + 1]))
                //    {
                //        return new int[0];
                //    }
                //}
                //COMMENT OUT FOR PART 2
                races[0] = long.Parse(timeValues[1]);
                races[1] = long.Parse(distanceValues[1]);
                return races;
            }

        }

        static int WaysToBeat(long time, long distance)
        {
            int waysToBeat = 0;

            for (int holdTime = 0; holdTime <= time; holdTime++)
            {
                long remainingTime = time - holdTime;
                long currentSpeed = holdTime;
                long currentDistance = currentSpeed * remainingTime;

                if(currentDistance > distance)
                {
                    waysToBeat++;
                }
            }

            return waysToBeat;
        }
    }
}
