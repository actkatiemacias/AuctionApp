using System;

namespace ConsoleApp
{
    /// <summary>
    /// PAUSED state representation. In the State Pattern this is a concrete state implementation.
    /// </summary>
    public class AuctionPausedState : AuctionState
    {
        /// <summary>
        /// Constructor that passes the auction object as a parameter.
        /// </summary>
        /// <param name="auction">Auction object to be transitioned to INACTIVE state.</param>
        public AuctionPausedState(Auction auction)
        {
            this.Name = "PAUSED";
            this.Auction = auction;
            Console.WriteLine("Auction is paused.");
        }

        /// <summary>
        /// Constructor that passes a state as a parameter.
        /// </summary>
        /// <param name="state">An auction state object.</param>
        public AuctionPausedState(AuctionState state)
        {
            Name = "PAUSED";
            this.Auction = state.Auction;
            Console.WriteLine("Auction is paused.");
        }

        /// <summary>
        /// Logic for auction when Pause command is executed when it is in the PAUSED state.
        /// </summary>
        public override void Pause()
        {

            Console.Write("Auction is already paused.");
        }

        /// <summary>
        /// Logic for auction when Reset command is executed when it is in the PAUSED state.
        /// </summary>
        public override void Reset()
        {
            this.Auction.Elapsed = TimeSpan.FromSeconds(0);
            // Set auction elapsed time to zero and state to inactive.
            this.Auction.State = new AuctionInactiveState(this);
        }

        /// <summary>
        /// Logic for auction when Stop command is executed when it is in the PAUSED state.
        /// </summary>
        public override void Stop()
        {
            Auction.EndTime = DateTime.Now;
            Auction.Elapsed += TimeSpan.FromSeconds(0);
            this.Auction.State = new AuctionInactiveState(this);
        }

        /// <summary>
        /// Logic for auction when Start command is executed when it is in the PAUSED state.
        /// </summary>
        public override void Start()
        {
            //set new start time for next elapsed time segment
            this.Auction.StartTime = DateTime.Now;
            this.Auction.State = new AuctionActiveState(this);

        }

        /// <summary>
        /// Logic for auction when List command is executed when it is in the PAUSED state.
        /// </summary>
        public override void List()
        {
            foreach (var item in Auction.Items)
            {
                item.List(Auction.Elapsed);
            }
        }
    }
}
