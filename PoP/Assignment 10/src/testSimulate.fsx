#load "simulate.fs"
open Simulation


let space = new Airspace ()



//test
let drone = new Drone ((0, 0), (1000, 1000), 10) 
let droneAtDestination = Drone ((0, 0), (0, 0), 10) 
let droneZeroSpeed = Drone ((0, 0), (1000, 1000), 0) 

// Fly
printfn "\nBlackbox test of Fly:"

let posbefore = drone.Position
drone.Fly ()
printfn "%5b: Is position different after a flight?" (posbefore <> drone.Position)

let zeroposbefore = droneZeroSpeed.Position
droneZeroSpeed.Fly ()
printfn "%5b: Is position the same if the drone has speed 0?" (zeroposbefore = droneZeroSpeed.Position)

// AtDestination
printfn "\nBlackbox test of AtDestination:"

printfn "%5b: Is the drone that is created at its destination showing correctly?" (droneAtDestination.AtDestination)

printfn "%5b: Is this other random drone at its destination?" (not drone.AtDestination)


// AddDrone
printfn "\nBlackbox test of AddDrone, AddRandomDrone and AddRandomDrones:"
printfn "%5b: Is an empty Airspace droneless" (List.length space.Drones = 0)
space.AddDrone (0, 0) (1000, 1000) 10
printfn "%5b: Does it have exactly one drone after using AddDrone" (List.length space.Drones = 1)

let mutable dronesbefore = space.GetDrones
space.AddRandomDrone ()
let mutable dronesafter = space.GetDrones
printfn "%5b: Does adding a random drone increase the numer of drones?" (List.length dronesbefore < List.length dronesafter)
dronesbefore <- space.GetDrones
space.AddRandomDrones 20
dronesafter <- space.GetDrones
printfn "%5b: If you add 20 random drones, does the count increase by 20?" (List.length dronesbefore + 20 = List.length dronesafter)

// DroneDist
printfn "\nBlackbox test of DroneDist:"
printfn "%5b: Are two drones on top of each other at distance 0.0?" (space.DroneDist droneAtDestination droneZeroSpeed = 0.0)

printfn "%5b: Is it larger if they are away from each other?" (space.DroneDist droneAtDestination drone > 0.0)

printfn "%5b: Does distances commute?" (space.DroneDist droneAtDestination drone = space.DroneDist drone droneAtDestination)

printfn "%5b: Does Is the distance between a drone and itself 0?" (space.DroneDist drone drone = 0.0)

printfn "\nBlackbox test of FlyDrones:"
let mutable emptyspace = new Airspace ()
emptyspace <- new Airspace ()
emptyspace.AddRandomDrones 50

let droneposbefore = emptyspace.GetDronePositions
emptyspace.FlyDrones ()

let allchanged = List.exists2 (fun ((a,b) :int*int) ((c,d) :int*int) -> a = c && b = d) droneposbefore emptyspace.GetDronePositions
printfn "%5b: After using FlyDrones 1 time, all drones should have moved." (not allchanged)

printfn "As this is a method that takes no arguments, I am having a lot of trouble coming up with outher tests :("

// WillCollide
emptyspace <- new Airspace ()
printfn "\nBlackbox test of WillCollide:"
let collided = emptyspace.WillCollide 10
printfn "%5b: There should be no collision for an empty airspace" (List.length collided = 0)

emptyspace.AddRandomDrones 50
let collidednow = emptyspace.WillCollide 10
printfn "%5b: But there should be some for a filled one." (List.length collidednow > 0)

emptyspace <- new Airspace ()
emptyspace.AddDrone (0,0) (1000,1000) 10
emptyspace.AddDrone (1000,1000) (0,0) 10
let collisionCourse = emptyspace.WillCollide 10
printfn "%5b: Is there a collison for two drones on a collision course?" (List.length collisionCourse = 1)

emptyspace <- new Airspace ()

emptyspace.AddDrone (0,0) (30,30) 3
emptyspace.AddDrone (1000,1000) (970,970) 3


let StopCollisionCourse = emptyspace.WillCollide 10

printfn "%5b: Is there no collision if they should stop before they meet?" (List.length StopCollisionCourse = 0)




// FlyDrones
