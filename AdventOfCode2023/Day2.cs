using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Quic;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day2
    {
        public static void IndexValues(string filePath)
        {
            String line;

            StreamReader sr = new StreamReader(filePath);

            line = sr.ReadLine();

            bool redPossible = true;
            bool greenPossible = true;
            bool bluePossible = true;

            int totalIndexs = 0;

            int lineTrue = 0;

            int index;

            //For all Games
            for (int i = 1; i <= 100; i++)
            {
                //games 1-9
                if(i <= 9)
                {
                    line = line.Substring(7);
                }
                //games 10-99
                else if(i >= 10 && i <= 99)
                {
                    line = line.Substring(8);
                }
                else if(i >= 100)
                {
                    line = line.Substring(9);
                }
               
                string[] aGame = line.Split(';');
                 index = i;

                //for each game in the game (one line)
                foreach (string game in aGame)
                {
                    Console.WriteLine(game);
                    string[] aPiece = game.Split(",");

                    //for each piece in a game 
                    foreach (string piece in aPiece)
                    {

                        if (piece.Contains("red"))
                        {
                            int red = 0;
                            for(int j = 0; j < 4; j++)
                            {
                                if(char.IsDigit(piece[j]))
                                {
                                    string holder = piece[j].ToString();
                                    red = int.Parse(red.ToString() + holder);
                                }
                            }

                            //check if red is less than or equal to 12 
                            if (red <= 12)
                            {
                                redPossible = true;
                            }
                            else
                            {
                                redPossible = false;
                                
                            }
                        }
                        if (piece.Contains("green"))
                        {
                            int green = 0;
                            for (int j = 0; j < 4; j++)
                            {
                                if (char.IsDigit(piece[j]))
                                {
                                    string holder = piece[j].ToString();
                                    green = int.Parse(green.ToString() + holder);
                                }
                            }

                            //check if red is less than or equal to 12 
                            if (green <= 13)
                            {
                                greenPossible = true;
                            }
                            else
                            {
                                greenPossible = false;
                                
                            }
                        }
                        if (piece.Contains("blue"))
                        {
                            int blue = 0;
                            for (int j = 0; j < 4; j++)
                            {
                                if (char.IsDigit(piece[j]))
                                {
                                    string holder = piece[j].ToString();
                                    blue = int.Parse(blue.ToString() + holder);
                                }
                            }

                            //check if red is less than or equal to 12 
                            if (blue <= 14)
                            {
                                bluePossible = true;
                            }
                            else
                            {
                                bluePossible = false;
                                
                            }
                        }
                       
                    }
                    if (redPossible && bluePossible && greenPossible)
                    {
                        lineTrue++;
                    }

                }
                if (lineTrue == aGame.Length)
                {
                    totalIndexs += index;
                }
                redPossible = true;
                greenPossible = true;
                bluePossible = true;
                lineTrue = 0;

                line = sr.ReadLine();


            }
            Console.WriteLine(totalIndexs);
        }
    }
}
