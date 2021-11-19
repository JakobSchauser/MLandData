#load "rotate.fs"
open Rotate


let rec playmove (b: Board) (total: int) : int= 
    match solved b with
    | true -> total
    | false ->
        printfn "The board looks as follows:\n%s" (board2Str b)
        printfn "What move do you want to make?"
        let move = System.Console.ReadLine();
        if validRotation bo3 -1 then
            playmove (rotate b (int move)) (total + 1)
        else
            printfn "That is not a valid move - try something else!"
            playmove b total




printfn "What size board should we use?"
let size = System.Console.ReadLine(); 
let startgameboard = create (uint64 size)

printfn "What difficulty do you want to play at?"
let diff = System.Console.ReadLine(); 
let gameboard = scramble startgameboard (uint64 diff)

printfn "Your target is:\n"
printfn "%s" (board2Str startgameboard)


let n_mvs = playmove gameboard 0

printfn "You solved the board in %A moves!" n_mvs



