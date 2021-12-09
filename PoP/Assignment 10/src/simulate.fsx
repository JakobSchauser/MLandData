type Drone (startx: int, starty : int, destinationx : int, destinationy : int, speed : int) =
    let mutable movedPosition = (startx, starty)
    let direction = atan2 (float (destinationx - startx)) (float (destinationy - starty))

    member this.Speed = speed
    member this.Position = movedPosition
    member this.Destination = (destinationx,destinationy)

    member this.Fly =
        let xx = (cos direction) * (float speed)
        let yy = (sin direction) * (float speed)
        movedPosition <- (int (float (fst movedPosition) + xx), int (float (snd movedPosition) + yy))

    member this.AtDestination =
        match movedPosition with
             | (x,y) -> speed*speed < (destinationx - x) * (destinationx - x) + (destinationy - y) * (destinationy - y)


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
        this.AddDrone (rnd.Next(size), rnd.Next(size), rnd.Next(size), rnd.Next(size), rnd.Next(size)/10)

    
    member this.WillCollide (minutes : int) =
        let mutable allcrashes = []
        for i in 0..(minutes*60) do
            this.FlyDrones
            let crashes = findFallenDrones drones [] |> List.filter (fun drones -> 
                                                               match drones with | (Some d1,Some d2) -> true | _ -> false )
            allcrashes <- allcrashes@crashes

            drones <- List.filter (fun (d1,d2) -> not ((List.contains d1 drones) && (List.contains d2 drones)) )




let space = Airspace ()
space.AddRandomDrone 50
space.AddRandomDrone 50
space.AddRandomDrone 50
space.AddRandomDrone 50
space.AddRandomDrone 50
space.AddRandomDrone 50
space.AddRandomDrone 50

printfn "%A" (space.WillCollide 5)