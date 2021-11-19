#load "rotate.fs"
open Rotate

let bo2 = create (uint64 2)
let bo3 = create (uint64 3)



// Blackbox
printfn "Black-box testing of create" 
printfn "%5b: 0 should be empty" (create (uint64 0) = [])
// printfn "%5b: -1 hey wait! thats illegal" (create -1 = "type error")
printfn "%5b: 420 very large" (create (uint64 30) = create (uint64 5))
printfn "%5b: 3 one that works" (create (uint64 3) = ['a';'b';'c';'d';'e';'f';'g';'h';'i'])


// val board2Str : (b:Board) -> string
// // no idea
printfn "%5b: Is this printing newlined?" ( (board2Str bo2) = "ab\ncd")

// val validRotation : (b:Board) -> (p:Position) -> bool
printfn "Black-box testing of validRotation" 
printfn "%5b: -1 not legal" (validRotation bo3 -1 = false)
printfn "%5b: 0 trivially true" (validRotation bo3 0 = true)
printfn "%5b: 2 illegal to the right" (validRotation bo3 2 = false)
printfn "%5b: 7 illegal to the bo3ttom" (validRotation bo3 7 = false)
printfn "%5b: 69 very large" (validRotation bo3 69 = false)

// val rotate : (b:Board) -> (p:Position) -> Board
printfn "Black-box testing of rotate" 
printfn "%5b: legal" (rotate bo2 0 = ['c';'a';'d';'b'])
printfn "%5b: illegal" (rotate bo2 1 = bo2)
printfn "%5b: cyclic" (rotate bo2 4 = bo2)



// val scramble : (b:Board) -> (m:uint) -> Board
printfn "Black-box testing of scramble" 
printfn "%5b: check if unchanged" ((scramble bo2 (uint64 0) = bo2) = true)
printfn "%5b: chech if changed" ((scramble bo2 (uint64 20) = bo2) = false)
printfn "%5A: HERE" (scramble  (create (uint64 3)) (uint64 4))


// val solved : (b:Board) -> bool
printfn "Black-box testing of solved" 
printfn "%5b: solved" (solved ['a';'b';'c';'d'] = true)
printfn "%5b: unsolved" (solved ['b';'d';'s';'m'] = false)
