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
            int[] raceRecords = race.RaceRecords(input);

            int sumWays = 1;

            for(int i = 0; i < raceRecords.Length; i+=2)
            {
                int time = raceRecords[i];
                int distance = raceRecords[i+1];

                sumWays = sumWays * WaysToBeat(time,distance); 
            }

            Console.WriteLine(sumWays);
        }

        class Race
        {

            public int[] RaceRecords(string raceInput)
            {
                // Split the input by lines
                string[] lines = raceInput.Split('\n', StringSplitOptions.RemoveEmptyEntries);

                // Skip the header lines
                string[] timeValues = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string[] distanceValues = lines[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                // get each pair of values and store them in an array
                int[] races = new int[(timeValues.Length - 1) * 2];
                for (int i = 1; i < timeValues.Length; i++)
                {
                    if (!int.TryParse(timeValues[i], out races[(i - 1) * 2]) || !int.TryParse(distanceValues[i], out races[(i - 1) * 2 + 1]))
                    {
                        return new int[0];
                    }
                }

                return races;
            }

        }

        static int WaysToBeat(int time, int distance)
        {
            int waysToBeat = 0;

            for (int holdTime = 0; holdTime <= time; holdTime++)
            {
                int remainingTime = time - holdTime;
                int currentSpeed = holdTime;
                int currentDistance = currentSpeed * remainingTime;

                if(currentDistance > distance)
                {
                    waysToBeat++;
                }
            }

            return waysToBeat;
        }
    }
}
