Decaying Auction Application
Author: Katie Macias
Date: 5/28/2013
Description: An auction holds items that decay at a given rate. 
The Auction reads in the commands Start, Stop, Pause, Reset and List to invoke auction auctions. To end the program use the command 'exit'.
The xml file items.xml should be in the same folder as the project file.

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



