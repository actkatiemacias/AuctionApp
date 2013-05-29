using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;


namespace ConsoleApp
{
    /// <summary>
    /// Auction object that can change it's state. State pattern is implemented in this project and Auction is the Context. 
    /// </summary>
    public class Auction
    {
        /// <summary>
        /// Total time the auction has been in the ACTIVE state.
        /// </summary>
        public TimeSpan Elapsed;
        /// <summary>
        /// StartTime is the last time the auction was transistioned to the ACTIVE state.
        /// </summary>
        public DateTime StartTime;
        /// <summary>
        /// EndTime is the last time the auction was transistioned to the INACTIVE state.
        /// </summary>
        public DateTime EndTime;
        /// <summary>
        /// Abstract state object used to identify what state the auction object is in.
        /// </summary>
        public AuctionState State;
        /// <summary>
        /// Items within the auction.
        /// </summary>
        public List<Item> Items;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Auction()
        {
            // Auction is an Inactive state by default.
            State = new AuctionInactiveState(this);
            PopulateItems();
        }

        /// <summary>
        /// Populates the Auction with items from a static xml file, items.xml
        /// </summary>
        /// <remarks>
        /// items.xml should reside in the same folder as the project file.
        /// If the items cannot be loaded from xml, the error message will be displayed and 3 default items will populate the auction.
        /// </remarks>
        private void PopulateItems()
        {
            // Create item objects and add items to the item list.
            try
            {
                string path = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "items.xml");
                var XMLDoc = XDocument.Load(path);
                var entries = from item in XMLDoc.Descendants("Item")
                              select item;
                Items = new List<Item>();
                foreach (var i in entries)
                {
                    Item item = new Item(int.Parse(i.Attribute("Id").Value),
                    i.Element("Name").Value,
                    decimal.Parse(i.Element("Price").Value),
                    decimal.Parse(i.Element("Decrement").Value),
                    TimeSpan.FromSeconds(double.Parse(i.Element("Interval").Value)));
                    Items.Add(item);
                }
            }
            // If XML cannot be loaded populate Auction with three default items and display error message.
            catch (Exception e)
            {
                Console.WriteLine("Items could not be loaded from XML. Default items will be loaded");
                Items = new List<Item>();
                Item item1 = new Item(1, "Hat", 15.00m, .20m, TimeSpan.FromSeconds(10));
                Item item2 = new Item(2, "Shoes", 45.00m, .45m, TimeSpan.FromSeconds(30));
                Item item3 = new Item(3, "Glasses", 100.00m, 1.00m, TimeSpan.FromSeconds(60));
                Items.Add(item1);
                Items.Add(item2);
                Items.Add(item3);
                Console.WriteLine("Exception was caused from: " + e.Message);
            }
        }

        /// <summary>
        /// Method to Start the auction.
        /// </summary>
        public void Start()
        {
            State.Start();
        }

        /// <summary>
        /// Method to Stop the auction.
        /// </summary>
        public void Stop()
        {
            State.Stop();
        }

        /// <summary>
        /// Method to Pause the auction.
        /// </summary>
        public void Pause()
        {
            State.Pause();
        }

        /// <summary>
        /// Method to List the auction items.
        /// </summary>
        public void List()
        {
            State.List();
        }

        /// <summary>
        /// Method to restart the auction.
        /// </summary>
        public void Reset()
        {
            State.Reset();
        }
    }
}
