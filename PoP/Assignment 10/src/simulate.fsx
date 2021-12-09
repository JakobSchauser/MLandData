type Drone (startx: int, starty : int, destinationx : int, destinationy : int, speed : int) =
    let mutable movedPosition = (startx, starty)
    let mutable direction = atan2 (float (destinationy - starty)) (float (destinationx - startx))

    member this.Speed = speed
    member this.Position = movedPosition
    member this.Destination = (destinationx,destinationy)

    member this.Fly =

        let xx = (cos direction) * (float this.Speed)
        let yy = (sin direction) * (float this.Speed)
        movedPosition <- (int (float (fst this.Position) + xx), int (float (snd this.Position) + yy))
        direction <- atan2 (float (snd this.Destination - snd this.Position)) (float (fst this.Destination - fst this.Position))

    member this.AtDestination =
        match movedPosition with
             | (x,y) -> speed*speed > (destinationx - x) * (destinationx - x) + (destinationy - y) * (destinationy - y)


let dist ((x0, y0) : int*int) ((x1, y1) : int*int) = sqrt (float ((x0 - x1) * (x0 - x1) + (y0 - y1) * (y0 - y1)))

type Airspace (a)=
    let mutable drones = []

    let rec findFallenDrones (drones) (fallendrones) =
        match drones with
            | head::tail -> 
                let newfallendrones = tail |> List.map (fun (d : Drone) -> 
                                                    match (d.AtDestination,head.AtDestination) with
                                                        | (false,false) -> if dist head.Position d.Position < 500.0 then Some (d,head) else None
                                                        | _ -> None
                                                        )
                findFallenDrones tail (fallendrones@newfallendrones)
            | [] -> fallendrones

    member this.Drones = drones

    member this.DroneDist (drone1 : Drone) (drone2 : Drone) = 
        dist drone1.Position drone2.Position

    member this.FlyDrones =
        this.Drones |> List.iter (fun (d : Drone) -> if not d.AtDestination then d.Fly)

    member this.AddDrone (startx: int, starty : int, destinationx : int, destinationy : int, speed : int) = 
        let newdrone = Drone (startx, starty, destinationx, destinationy, speed)
        drones <- newdrone::drones

    member this.AddRandomDrone (size : int) =
        let rnd = System.Random ()
        this.AddDrone (rnd.Next(size), rnd.Next(size), rnd.Next(size), rnd.Next(size), 10)

    member this.AddRandomDrones (number : int) (size : int) =
        for i in 1..number do
            this.AddRandomDrone size

    member this.WillCollide (minutes : int) =
        let mutable allcrashes = []
        for i in 0..(minutes*60) do
            this.FlyDrones
            let crashes = findFallenDrones drones [] |> List.filter (fun drones -> 
                                                               match drones with | Some (d1, d2) -> true | _ -> false )
            allcrashes <- allcrashes@crashes

            let toremove = List.map (fun dronepair -> match dronepair with | Some (a,_) | Some (_, a) -> a) crashes
            // allcrashes <- allcrashes@toremove
            drones <- List.filter (fun drone -> not (List.contains drone toremove)) this.Drones
        allcrashes




let space = Airspace ()

space.AddRandomDrones 20 5000
printfn "%A" (List.length space.Drones)

printfn "%A" (space.WillCollide 5)

printfn "%A" (List.length space.Drones)
