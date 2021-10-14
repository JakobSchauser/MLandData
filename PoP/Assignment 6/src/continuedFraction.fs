module continuedFraction

let eps = 0.0000001

/// <summary> Takes a list and returns the decimal number </summary>
/// <param name = "lst"> A list of integers </param> 
/// <returns> Returns a float </summary>
let rec cfrac2float (lst : int list): float = 
    match List.length lst with
        | 1 -> 1.0/float(lst.[0])
        | _ -> float(lst.[0]) + 1.0/(cfrac2float lst.[1..])

/// <summary> Takes a decimal number and returns the list of numbers for the continued fraction </summary>
/// <param name = 'x'> A floating point number </param> 
/// <returns> Returns a list of integers </summary>
let rec float2cfrac (x : float) : int list =
    let q = floor (x + eps)
    let r = x - q
    match (r < eps) with
        | false -> int(q) :: (float2cfrac (1.0/r))
        | true -> [int(q)]