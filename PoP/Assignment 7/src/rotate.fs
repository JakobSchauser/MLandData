module Rotate

type Board = char list
type Position = int

let create (n:uint64) : Board = 
    Seq.toList "abcdefghijklmnopqustuvwxyz".[..int (n*n)]


let board2Str (b:Board) : string = 
    System.String (List.toArray b)

let print a = printfn "%A" a

print (board2Str (create (uint64 5)))



let len (b:Board) : int = ( b |> List.length |> float |> sqrt |> int ))

let validRotation (b:Board) (p:Position) : bool =
    let l = len b
    match ((int p) %  l -  (l - 1 ) with
        | 0 -> false
        | _ -> if p >= l * (l - 1) then false else true
        
let bo = (create (uint64 3))
print (validRotation bo 0)
print (validRotation bo 2)
print (validRotation bo 6)

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


print (board2Str (rotate bo 0)) 

let rec until f p = 
    let a = f()
    if p a then a else until f p


let rec scramble (b:Board) (m:uint64) : Board = 
    match int m with
    | 0 -> b
    | _ -> 
        // TODO: Only make legal moves
        let rand = fun () -> (new System.Random()).Next(0, b |> List.length)
        
        let valid = until rand (validRotation b)
        scramble (rotate b valid) (m - uint64 1) 


print (scramble bo (uint64 4))

let solved (b:Board) : bool =
    (b, (b |> List.length |> uint64 |> create)) ||> List.forall2 (=)

print (solved (scramble bo (uint64 4)))