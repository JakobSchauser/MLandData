module Rotate

type Board = char list
type Position = int

let rand = new System.Random()


let create (n:uint64) : Board = 
    Seq.toList "abcdefghijklmnopqustuvwxyz".[..int (n*n)]

let board2Str (b:Board) : string = 
    System.String (List.toArray b)

let len (b:Board) : int = ( b |> List.length |> float |> sqrt |> int ))

let validRotation (b:Board) (p:Position) : bool =
    let l = len b
    match ((int p) %  l -  (l - 1 ) with
        | 0 -> false
        | _ -> if p >= l * (l - 1) then false else true
        
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


let rec scramble (b:Board) (m:uint64) : Board = 
    match int m with
    | 0 -> b
    | _ -> 
        let rand_ind = rand.Next(0, (b |> List.length ) - 1 ) + rand.Next(0, (b |> List.length ) -1 ) * (b |> List.length) 

        scramble (rotate b rand_ind) (m - uint64 1) 


let solved (b:Board) : bool =
    (b, (b |> List.length |> uint64 |> create)) ||> List.forall2 (=)
