#load "rotate.fs"
open Rotate
let rec playmove (b: Board) (total: int) = 
    if solved b then printfn "\n\nYou solved the board in %A moves!" total else
        printfn "You have made %A moves and the board looks as follows:\n%s\nWhat move do you want to make?" (total) (board2Str b)
        let move = System.Console.ReadLine(); 
        if validRotation b (int move) then playmove (rotate b (int move)) (total + 1) else printfn "That is not a valid move - try something else!"; playmove b total
printfn "What size board should we use?"
let size = System.Console.ReadLine(); 
let n_mvs = playmove (scramble (create (uint32 size)) ((uint32 size)+ uint32 69)) 0