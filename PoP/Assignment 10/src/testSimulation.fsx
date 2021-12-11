#load "simulate.fs"
open Simulation


let space = Airspace (50*100)



//test
let drone = Drone (0, 0, 1000, 1000, 10) 
let droneAtDestination = Drone (0, 0, 0, 0, 10) 
let droneZeroSpeed = Drone (0, 0, 1000, 1000, 0) 

// Fly
let posbefore = drone.Position
drone.Fly
printfn "%b" (posbefore <> drone.Position)

let zeroposbefore = droneZeroSpeed.Position
droneZeroSpeed.Fly
printfn "%b" (zeroposbefore = droneZeroSpeed.Position)

// AtDestination
printfn "%b" (droneAtDestination.AtDestination)

printfn "%b" (not drone.AtDestination)


// AddDrone
printfn "%b" (List.length space.Drones = 0)
space.AddDrone (0, 0, 1000, 1000, 10) 
printfn "%b" (List.length space.Drones = 1)

// AddRandomDrone
let mutable dronesbefore = space.GetDrones
space.AddRandomDrone
let mutable dronesafter = space.GetDrones
printfn "%b" (List.length dronesbefore < List.length dronesafter)


// AddRandomDrones
dronesbefore <- space.GetDrones
space.AddRandomDrones 20
dronesafter <- space.GetDrones
printfn "%b" (List.length dronesbefore + 20 = List.length dronesafter)


// DroneDist
printfn "%b" (space.DroneDist droneAtDestination droneZeroSpeed = 0.0)
printfn "%b" (space.DroneDist droneAtDestination drone > 0.0)

// WillCollide
let mutable emptyspace = Airspace (50*100)
let collided = emptyspace.WillCollide 10
printfn "%b" (List.length collided = 0)

emptyspace.AddRandomDrones 50
let collidednow = emptyspace.WillCollide 10
printfn "%b" (List.length collidednow > 0)

// FlyDrones
emptyspace <- Airspace (50*100)
emptyspace.AddRandomDrones 50

let droneposbefore = emptyspace.GetDronePositions
emptyspace.FlyDrones
emptyspace.FlyDrones
emptyspace.FlyDrones

let allchanged = List.exists2 (fun ((a,b) :int*int) ((c,d) :int*int) -> a = c && b = d) droneposbefore emptyspace.GetDronePositions
printfn "%b" (not allchanged)
