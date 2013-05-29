using System;

namespace ConsoleApp
{
    /// <summary>
    /// INACTIVE state representation. In the State Pattern this is a concrete state implementation.
    /// </summary>
    public class AuctionInactiveState : AuctionState
    {
        /// <summary>
        /// Constructor that passes the auction object as a parameter.
        /// </summary>
        /// <param name="auction">Auction object to be transitioned to INACTIVE state.</param>
        public AuctionInactiveState(Auction auction)
        {
            this.Name = "INACTIVE";
            this.Auction = auction;
            Console.WriteLine("Auction is inactive.");
        }

        /// <summary>
        /// Constructor that passes a state as a parameter.
        /// </summary>
        /// <param name="state">An auction state object.</param>
        public AuctionInactiveState(AuctionState state)
        {
            this.Name = "INACTIVE";
            this.Auction = state.Auction;
            Console.WriteLine("Auction is inactive.");
        }
        /// <summary>
        /// Logic for auction when Start command is executed when it is in the INACTIVE state.
        /// </summary>
        public override void Start()
        {
            // Only start if elapsed time == 0, indicator how we know auction has been reset.
            if (this.Auction.Elapsed == TimeSpan.FromSeconds(0))
            {
                this.Auction.StartTime = DateTime.Now;
                this.Auction.State = new AuctionActiveState(this);
            }
            else
            {
                Console.WriteLine("Please use the RESET command before attempting to restart auction.");
            }
        }

        /// <summary>
        /// Logic for auction when Stop command is executed when it is in the INACTIVE state.
        /// </summary>
        public override void Stop()
        {
            // Cannot stop twice, auction already inactive.
            Console.WriteLine("The auction is already inactive.");
        }

        /// <summary>
        /// Logic for auction when RESET command is executed when it is in the INACTIVE state.
        /// </summary>
        public override void Reset()
        {
            // If auction elapsed time is 0 do nothing, auction already reset.
            // Else reset auction elapsed time to 0 and remain in inactive state until start is called.
            this.Auction.Elapsed = TimeSpan.FromSeconds(0);
            Console.WriteLine("Auction is reset. Use START command to begin auction.");
        }

        /// <summary>
        /// Logic for auction when Pause command is executed when it is in the INACTIVE state.
        /// </summary>
        public override void Pause()
        {
            // Cannot transition from inactive to paused state.
            Console.WriteLine("Auction is not active. Cannot PAUSE auction.");
        }

        /// <summary>
        /// Logic for auction when List command is executed when it is in the INACTIVE state.
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
