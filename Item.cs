using System;

namespace ConsoleApp
{
    /// <summary>
    /// Item objects that decay over time given a decay rate (decimal/seconds).
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Item id.
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Item name.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Item price.
        /// </summary>
        public decimal Price { get; private set; }
        /// <summary>
        /// Amount to decrement the item price by each interval.
        /// </summary>
        public decimal Decrement { get; private set; }
        /// <summary>
        /// The time, in seconds, that must elapse to decrement an item once.
        /// </summary>
        public TimeSpan Interval { get; private set; }

        /// <summary>
        /// Constructor for the item.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="decrement"></param>
        /// <param name="interval"></param>
        public Item(int id, string name, decimal price, decimal decrement, TimeSpan interval)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.Decrement = decrement;
            this.Interval = interval;
        }

        /// <summary>
        /// Prints to the console the item's current price and time until next drop in price.
        /// </summary>
        /// <param name="elapsed">The total time the Auction has been in the ACTIVE state, a timespan.</param>
        public void List(TimeSpan elapsed)
        {
            double decrements = NumOfDecrements(elapsed.TotalSeconds);
            double nextDrop= NextDrop(elapsed.TotalSeconds);
            decimal decrementAmount = DecrementAmount(decrements);
            decimal currentPrice = CurrentPrice(decrementAmount);
            Console.WriteLine(Name + ": " + currentPrice + ", next drop: " + nextDrop + " seconds");
        }

        /// <summary>
        /// Determines the number of times the item will decrement.
        /// </summary>
        /// <param name="elapsedSeconds">The number of seconds the Auction has been in the ACTIVE state, a double.</param>
        /// <returns>The number of times the item will drop in price, a double.</returns>
        public double NumOfDecrements(double elapsedSeconds)
        {
            return Math.Floor(elapsedSeconds / Interval.TotalSeconds);
        }

        /// <summary>
        /// Determines the seconds until the next drop in price.
        /// </summary>
        /// <param name="elapsedSeconds">The number of seconds the Auction has been in the ACTIVE state, a double.</param>
        /// <returns>The seconds until the next drop in price, a double.</returns>
        public double NextDrop(double elapsedSeconds)
        {
            // Check that current price is not zeroed out.
            if (NumOfDecrements(elapsedSeconds) < (double)(Price / Decrement))
            {
                return Math.Round(Interval.TotalSeconds - (elapsedSeconds - NumOfDecrements(elapsedSeconds) * Interval.TotalSeconds));
            }
            else
            {
                return 0.0;
            }
        }

        /// <summary>
        /// Determines the total amount the item will drop in price from the base price.
        /// </summary>
        /// <param name="decrements">Times the item will drop in price, a double.</param>
        /// <returns>The amount to drop the item price from the base price, a decimal. </returns>
        public decimal DecrementAmount(double decrements)
        {
            return (decimal)decrements * Decrement;
        }

        /// <summary>
        /// Determines the current price of an item.
        /// </summary>
        /// <param name="decrementAmount">The amount to reduce the base price by, a decimal.</param>
        /// <returns>The current price of an item, a decimal. </returns>
        public decimal CurrentPrice(decimal decrementAmount)
        {
            return Math.Max(Price - decrementAmount, 0.00m);
        }
    }
}
