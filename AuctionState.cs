using System;

namespace ConsoleApp
{
    /// <summary>
    /// Abstract state representation. In the State Pattern this is the abstract state. 
    /// Defines the commands (methods) that must be implemented for each state.
    /// </summary>
    public abstract class AuctionState
    {
        public Auction Auction { get; set; }
        public abstract void Start();
        public abstract void Stop();
        public abstract void Reset();
        public abstract void Pause();
        public abstract void List();
        public string Name { get; set; }
    }
}
