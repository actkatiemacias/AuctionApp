using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    /// <summary>
    /// Decaying Auction Application
    /// Author: Katie Macias
    /// Date: 5/28/2013
    /// Description: An auction holds items that decay at a given rate. 
    /// The Auction reads in the commands Start, Stop, Pause, Reset and List from the console to invoke auction auctions. 
    /// To end the program use the command 'exit'.
    /// The xml file items.xml should be in the same folder as the project file.
    /// The program uses the State pattern implementation.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            List<Item> items = new List<Item>();
            Auction auction = new Auction();
            String command;
            command = Console.ReadLine();
            while (command.ToUpper() != "EXIT")
            {

                // Check for input.
                if (command.Length > 0)
                {
                    // Convert to upper case so any case works.
                    command = command.ToUpper();

                    switch (command)
                    {
                        case "START": auction.Start();
                            break;
                        case "STOP": auction.Stop();
                            break;
                        case "PAUSE": auction.Pause();
                            break;
                        case "LIST": auction.List();
                            break;
                        case "RESET": auction.Reset();
                            break;
                        default: Console.WriteLine("Unknown command.");
                            break;
                    }
                }
                command = Console.ReadLine();
            }
        }
    }
}
