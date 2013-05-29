using System;

namespace ConsoleApp
{
    /// <summary>
    /// ACTIVE state representation. In the State Pattern this is a concrete state implementation.
    /// </summary>
    class AuctionActiveState: AuctionState
    {
        /// <summary>
        /// Constructor that passes the auction object as a parameter.
        /// </summary>
        /// <param name="auction">Auction object to be transitioned to ACTIVE state.</param>
        public AuctionActiveState(Auction auction)
        {
            Name = "ACTIVE";
            this.Auction = auction;            
            Console.WriteLine("Auction is active.");   
        }

        /// <summary>
        /// Constructor that passes a state as a parameter
        /// </summary>
        /// <param name="state">An auction state object.</param>
        public AuctionActiveState(AuctionState state)
        {
            Name = "ACTIVE";
            this.Auction = state.Auction;
            Console.WriteLine("Auction is active.");
        }

        /// <summary>
        /// Logic for auction when Start command is executed when it is in the ACTIVE state.
        /// </summary>
        public override void Start()
        {
            //Do nothing, auction is already active
            Console.WriteLine("Auction is already started.");
        }

        /// <summary>
        /// Logic for auction when Stop command is executed when it is in the ACTIVE state.
        /// </summary>
        public override void Stop()
        {
            //transistion auction to inactive state
            this.Auction.State = new AuctionInactiveState(this);
            //calculate the elapsed time
            this.Auction.Elapsed += (DateTime.Now - this.Auction.StartTime);
        }

        /// <summary>
        /// Logic for auction when Pause command is executed when it is in the ACTIVE state.
        /// </summary>
        public override void Pause()
        {
            //transition auction to paused state
            this.Auction.State = new AuctionPausedState(this);
            //calculate the elapsed time
            this.Auction.Elapsed += (DateTime.Now - this.Auction.StartTime);
        }

        /// <summary>
        /// Logic for auction when Reset command is executed when it is in the ACTIVE state.
        /// </summary>
        public override void Reset()
        {
            //set auction elapsed time to 0 and 
            this.Auction.Elapsed = TimeSpan.FromSeconds(0);
            //transition auction to inactive state
            this.Auction.State = new AuctionInactiveState(this);
        }

        /// <summary>
        /// Logic for auction when List command is executed when it is in the ACTIVE state.
        /// </summary>
        public override void List()
        {
            Auction.Elapsed += DateTime.Now - Auction.StartTime;
            foreach (var item in Auction.Items)
            {
                item.List(Auction.Elapsed);
            }
            Auction.StartTime = DateTime.Now;
        }

    }
}
