#load "simulate.fs"
open Simulation

let space = Airspace ()

space.AddRandomDrones 20 5000
printfn "%A" (List.length space.Drones)

printfn "%A" (space.WillCollide 5)

printfn "%A" (List.length space.Drones)
