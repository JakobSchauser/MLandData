module Rotate

type Board = char list
type Position = int

let rand = System.Random ()
let len (b:Board) : int = ( b |> List.length |> float |> sqrt |> int )

let create (n:uint32) : Board = 
    Seq.toList ['a'..'y'].[..int (n*n - uint32 1)]

    
let board2Str (b:Board) : string = 
    // b |> List.toArray |> System.String
    let l = len b
    b |> List.mapi(fun i x -> if (i%l = 0) && not (i = 0) then ['\n';x] else [x]) |> List.concat |> List.toArray |> System.String
    

let validRotation (b:Board) (p:Position) : bool =
    if p < 0 then 
        false
    else 
        let l = len b
        match ((int p) %  l -  (l - 1 )) with
            | 0 -> false
            | _ -> not (p >= l * (l - 1))
            
let rotate (b:Board) (p:Position) : Board =
    let l = len b
    match validRotation b p with
        | false -> b
        | true ->
            // TODO: Find a better solution
            b |> List.mapi (fun i x ->
                if i = p + 1 then
                    b.[p]
                else if i = p + l + 1 then 
                    b.[p + 1]
                else if i = p + l then
                     b.[p + l + 1]
                else if i = p then
                    b.[p + l]
                else b.[i]
            )


let rec scramble (b:Board) (m:uint32) : Board = 
    match int m with
    | 0 -> b
    | _ -> 
        let rand_ind = rand.Next ((b |> len) - 1) + (rand.Next ((b |> len) - 1)) * (b |> len) 
        scramble (rotate b rand_ind) (m - uint32 1) 


let solved (b:Board) : bool =
    let trues = create (uint32 (b |> len))
    (b, trues) ||> List.forall2 (=)