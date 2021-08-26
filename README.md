# Fleet web API
Workshop; A simple business case scenario (Fleet) used as an example to demonstrate an Asp.net core web API.
### Requirements
1. As a user I want the ability create containers and load them to my fleet of Ships and Trucks
2. As a user I want the load of my ships to be persisted so that state is persisted between service restarts (the data store can be as simple or complex as you want)
3. As a user I want to restrict the load per ship to a max of 4 containers because ship 1 and 2 have a max capacity of 4
4. As a user I want to register an unlimitted number of ships and make load transfers between them, so that I can manage my whole fleet
5. As a user I want to specify individual capacities per ship, so that I can handle ships with diferent capacities
6. As a user I want to also manage trucks, so that I can use the program to manage load of my trucks as well
7. As a user I want the offloading from trucks to be restricted to the last load, so that it is not possible to unload unreachable goods
