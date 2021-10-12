let epsilon = 0.00001

let rec cfrac2float (lst : int list): float = 
    match List.length lst with
        | 1 -> 1.0/(lst.[0] |> float)
        | _ -> (lst.[0] |> float) + 1.0/(cfrac2float lst.[1..])


printfn "%A" (cfrac2float [3;4;12;4])

let rec float2cfrac (x : float) : int list =
    let q = floor (x + epsilon)
    let r = x - q
    match (r < epsilon) with
        | false -> int(q) :: (float2cfrac (1.0/r))
        | true -> [int(q)]


printfn "%A" (float2cfrac 3.2450000)



