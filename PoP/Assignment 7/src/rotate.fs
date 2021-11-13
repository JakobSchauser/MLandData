module Rotate

type Board = char list
type Position = int

let all = "abcdefghijklmnopqustuvwxyz"

let create (n:uint) : Board = 
    Seq.toList all.[..int (n*n)]


let board2Str (b:Board) : string = 
    System.String (List.toArray b)

let print a = printfn "%A" a

print (board2Str (create (uint 5)))

let validRotation (b:Board) (p:Position) : bool = 
    match int (((int p) % ( b |> List.length |> float |> sqrt |> int )) - (b |> List.length |> float |> sqrt - 1)) with
        | 0 -> false
        | _ -> false //if p - sqrt (List.length b) * (sqrt (List.length b) - 1) > 0 then false else true

let bo = (create (uint 3))
print validRotation bo (Position 1)
// print validRotation bo 2
// print validRotation bo 6
// val rotate : (b:Board) -> (p:Position) -> Board
// val scramble : (b:Board) -> (m:uint) -> Board
// val solved : (b:Board) -> bool
