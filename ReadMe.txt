Decaying Auction Application
Author: Katie Macias
Date: 5/28/2013
Description: An auction holds items that decay at a given rate. 
The Auction reads in the commands Start, Stop, Pause, Reset and List to invoke auction auctions. To end the program use the command 'exit'.
The xml file items.xml should be in the same folder as the project file.

The Problem:
Problem: You have a list of auction items that are going to decay in price automatically rather than increase given user demand. 
·        Criteria:
o   You have the following 3 items: 
§  { Id = 1, Name = "Hat", Price = 15.00m, Decrement = 0.20m, Interval = TimeSpan.FromSeconds(10) }
§  { Id = 2, Name = "Shoes", Price = 45.00m, Decrement = 0.45m, Interval = TimeSpan.FromSeconds(30) }
§  { Id = 3, Name = "Glasses", Price = 100.00m, Decrement = 1.00m, Interval = TimeSpan.FromSeconds(60) }
o   Each of these items start at their specified price, and at their given intervals, subtract the decrement amount from their price. Upon reaching 0.00, they no longer decrement.
·        Requirements:

o   Build a simple application, that allows for several command inputs:

§  "start" - This starts the auction. All items begin their decrement cycles.

§  "pause" - This pauses the auction.

§  "reset" - This resets the prices and intervals.

§  "list" - This shows each item at their current price and where in the interval they are. For example, after "start"ing the auction and after 13 seconds, "list"ing would show: 

·        Hat: 14.80, next drop: 7 seconds       

·        Shoes: 45.00, next drop: 27 seconds

·        Glasses: 100.00, next drop: 57 seconds

 

Implementation Considerations:
1. Architecture options: Finite State Machine vs State Pattern
The State Pattern was chosen over a Finite State Machine implementation to allow for scalability - easy addition of new states and commands.

2. Storage options: Static items, XML/other text file, Database 
The XML document was chosen as the preferred storage because is is lightweight enough for the problem but allows for more items to be added if necessary.
Larger scale implementations may require a database backend, but for the purpose of this exercise storage from a single xml file was deemed large enough. 

3. State Assumptions: The auction can be in three states Active, Inactive and Paused. 
Below are the states and the assumptions for each command when the auction is in the given state.

Active:
	Start -> invalid command,the auction is already in active state
	Stop -> transistions the auction to inactive state, cannot be active again until reset,start commands are executued
	Pause -> transistions the auction to the paused state, can continue decaying with start command
	Reset -> transitions the auction to the inactive state and sets elapsed time to 0, can be activated with start command
	List -> lists the items price and next drop given the total amount of time that has elapsed for the auction and the time the list command was executed

Inactive:
	Start -> can only transition to active via start command if auction elapsed time is equal to 0. This is an indicator the auction has been reset
	Stop -> invalid command, the auction is already in inactive state
	Pause -> invalid command, the auction is not active
	Reset -> sets the auction's elapsed time to 0
	List -> lists the items price and next drop given the total amount of time that has elapsed for the auction 

Paused:
	Start -> transitions auction back to active state
	Stop -> transitions auction to inactive state
	Pause -> invalid command, the auction is already in paused state
	Reset ->  transitions the auction to the inactive state and sets elapsed time to 0, can be activated with start command
	List -> lists the items price and next drop given the total amount of time that has elapsed for the auction 

4. Other Assumptions:
	When an item is fully decayed the time to next decrement is 0 seconds because there will be no more decrements
	Commands are NOT case sensitive
	When the  XML file cannot be loaded 3 default items will be loaded and the error message will be displayed



