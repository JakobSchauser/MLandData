module Simulation

type Drone (start : int*int, destination : int*int, speed : int) =
    let mutable movedPosition = start
    let mutable direction = atan2 (float (snd destination - snd start)) (float (fst destination - fst start))

    member this.Speed = speed
    member this.Id = sprintf "%i%i%i%i%i" (fst start) (snd start) (fst destination) (snd destination) speed
    member this.Position = movedPosition
    member this.Destination = destination

    /// <summary> Calculates and executes one time step of flight </summary>
    /// <returns> Nothing </returns>
    member this.Fly () =
        let xx = (cos direction) * (float this.Speed)
        let yy = (sin direction) * (float this.Speed)
        movedPosition <- (int (float (fst this.Position) + xx), int (float (snd this.Position) + yy))
        direction <- atan2 (float (snd this.Destination - snd this.Position)) (float (fst this.Destination - fst this.Position))

    /// <summary> Checks whether the drone is at its destination </summary>
    /// <returns> True or false </returns>
    member this.AtDestination  =        // To negate the fact that the drone flies with a constant speed in a grid,
        match movedPosition with        //   this checks whether the drone would overshoot the target in the following frame. 
             | (x,y) -> speed*speed > (fst destination - x) * (fst destination - x) + (snd destination - y) * (snd destination - y)


/// <summary> Calculates the distance between two points </summary>
/// <param name = (x0,y0))> The first point </param>
/// <param name = (x0,y0))> The second point </param>
/// <returns> The distance as a float </returns>
let dist ((x0, y0) : int*int) ((x1, y1) : int*int) = sqrt (float ((x0 - x1) * (x0 - x1) + (y0 - y1) * (y0 - y1)))

type Airspace ()=
    let size = 50*100
    let mutable drones = []

    /// <summary> Recursive function that finds all drones that collide </summary>
    /// <param name = drones> The list of drones to consider </param>
    /// <param name = fallendrones> The collided drones found so far </param>
    /// <returns> A list of collided drones as Some(drone1,drone2) or None </returns>
    let rec findFallenDrones (drones) (fallendrones) =
        match drones with
            | head::tail -> 
                let newfallendrones = tail |> List.map (fun (d : Drone) -> 
                                                    match (d.AtDestination, head.AtDestination) with
                                                        | (false,false) -> if dist head.Position d.Position < 500.0 then Some (d,head) else None
                                                        | _ -> None)
                findFallenDrones tail (fallendrones@newfallendrones)
            | [] -> fallendrones

    member this.Size = size    
    member this.Drones = drones

    /// <summary> Finds the distance between two drones </summary>
    /// <param name = drone1> The first drone </param>
    /// <param name = drone2> The second drone </param>
    /// <returns> The distance between the drones as a float </returns>
    member this.DroneDist (drone1 : Drone) (drone2 : Drone) = 
        dist drone1.Position drone2.Position

    /// <summary> Lets all the drones in the airspace "fly" </summary>
    /// <returns> Nothing </returns>
    member this.FlyDrones () =
        this.Drones |> List.iter (fun (d : Drone) -> if not d.AtDestination then d.Fly ())

    /// <summary> Adds a new drone to the airspace </summary>
    /// <param name = startx> The starting x-position </param>
    /// <param name = starty> The starting y-position </param>
    /// <param name = destinationx> The destinations x-position </param>
    /// <param name = destinationy> The destinations y-position </param>
    /// <param name = speed> The speed of the drone </param>
    /// <returns> Notihng  </returns>
    member this.AddDrone (start : int*int) (destination : int*int) (speed : int) = 
        let newdrone = new Drone (start, destination, speed)
        drones <- newdrone::drones

    /// <summary> Adds a new drone with a random starting position and destination </summary>
    /// <returns> Nothing  </returns>
    member this.AddRandomDrone () =
        let rnd = System.Random ()
        this.AddDrone (rnd.Next(this.Size), rnd.Next(this.Size)) (rnd.Next(this.Size), rnd.Next(this.Size)) 10

    /// <summary> Adds N new drones with a random starting positions and destinations </summary>
    /// <param name = number> The number of drones to add </param>
    /// <returns> Nothing  </returns>
    member this.AddRandomDrones (number : int) =
        for i in 1..number do
            this.AddRandomDrone ()

    /// <summary> Gets the drone ids in the airspace </summary>
    /// <returns> A list of drone ids </returns>
    member this.GetDrones =  List.map (fun (drone : Drone) -> drone.Id) this.Drones 

    /// <summary> Gets the drone ids in the airspace </summary>
    /// <returns> A list of drone ids </returns>
    member this.GetDronePositions =  List.map (fun (drone : Drone) -> drone.Position) this.Drones 

    /// <summary> Finds all the collisions that happen within the next N minutes </summary>
    /// <param name = minuts> The number of minutes to simulate </param>
    /// <returns> The list of collisions that happened </returns>
    member this.WillCollide (minutes : int) =
        let mutable allcrashes = []
        for i in 0..(minutes*60 - 1) do
            this.FlyDrones ()

            let crashes = findFallenDrones drones [] |> List.filter (fun drones -> 
                                                               match drones with | Some (d1, d2) -> true | _ -> false )

            allcrashes <- allcrashes@crashes

            let toremove = List.map (fun dronepair -> match dronepair with | Some (a,_) | Some (_, a) -> a) crashes

            drones <- List.filter (fun drone -> not (List.contains drone toremove)) this.Drones
        allcrashes




