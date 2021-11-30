#load "rotate.fs"
open Rotate

let bo2 = create (uint32 2)
let bo3 = create (uint32 3)

// Blackbox

//Create
printfn "Black-box testing of create" 
printfn "%5b: 0 should be empty" (create (uint32 0) = [])
// printfn "%5b: -1 hey wait! thats illegal" (create -1 = "type error")
printfn "%5b: 30 very large, should just make largest" (create (uint32 30) = create (uint32 5))
printfn "%5b: 3 one that works" (create (uint32 3) = ['a';'b';'c';'d';'e';'f';'g';'h';'i'])


// board2Str
printfn "Black-box testing of board2Str" 
printfn "%5b: Is this printing newlined?" ( (board2Str bo2) = "ab\ncd")
printfn "%5b: Empty board?" ( (Board []) = "")


// validRotation
printfn "Black-box testing of validRotation" 
printfn "%5b: -1 not legal" (validRotation bo3 -1 = false)
printfn "%5b: 1 trivially true" (validRotation bo3 1 = true)
printfn "%5b: 3 illegal to the right" (validRotation bo3 3 = false)
printfn "%5b: 8 illegal to the bo3ttom" (validRotation bo3 8 = false)
printfn "%5b: 69 very large" (validRotation bo3 69 = false)

// rotate
printfn "Black-box testing of rotate" 
printfn "%5b: legal 2x2" (rotate bo2 1 = ['c';'a';'d';'b'])
printfn "%5b: illegal 2x2" (rotate bo2 2 = bo2)
printfn "%5b: illegal 3x3" (rotate bo3 3 = bo3)
printfn "%5b: cyclic" ((rotate (rotate (rotate (rotate bo2 1) 1) 1) 1) = bo2)
    


// scramble
printfn "Black-box testing of scramble" 
printfn "%5b: check if unchanged 2x2" ((scramble bo2 (uint32 0) = bo2) = true)
printfn "%5b: check if changed 2x2" ((scramble bo2 (uint32 20) = bo2) = false)
printfn "%5b: check if unchanged 3x3" ((scramble bo3 (uint32 0) = bo3) = true)
printfn "%5b: check if unchanged 3x3" ((scramble bo3 (uint32 20) = bo3) = false)


// solved 
printfn "Black-box testing of solved" 
printfn "%5b: solved 2x2" (solved ['a';'b';'c';'d'] = true)
printfn "%5b: unsolved 2x2" (solved ['b';'d';'s';'m'] = false)
printfn "%5b: solved 3x3" (solved ['a';'b';'c';'d';'e';'f';'g';'h';'i'] = true)
printfn "%5b: unsolved 3x3" (solved  ['a';'u';'b';'e';'r';'g';'i';'n';'e']  = false)
