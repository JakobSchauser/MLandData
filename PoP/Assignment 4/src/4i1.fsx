// #load "vec2dSmall.fsi"
#load "vec2dSmall.fs"
open vec2d

let a = (-3.0, -4.0)
let b = (0.0, 0.0)
let c = (1.0, 0.0)
let d = (-30000.0, -40000.0)
let e = (30000.0, 40000.0)
let f = (0.0,-100000.0)



printfn "Black-box testing of len" 
printfn "%5b: v.x < 0 and v.y < 0" (vec2d.len a = 5.0)
printfn "%5b: v.x = 0 and v.y = 0" (vec2d.len b = 0.0)
printfn "%5b: a large vector" (vec2d.len e = 50000.0)
printfn "%5b: a large negative vector" (vec2d.len d = 50000.0)



printfn "\nBlack-box testing of ang" 
printfn "%5b: a with a zero as the y component" (vec2d.ang c = 0.0)
printfn "%5A: the zero vector" (vec2d.ang b |> string = "invalid")
printfn "%5A: a large vector in an odd quadrant" (vec2d.ang f = -System.Math.PI/2.0)
printfn "%5A: also this normal case" (vec2d.ang c = 0.0)




printfn "\nBlack-box testing of add" 
printfn "%5b: two normal vectors" (vec2d.add a c = (-2.0,-4.0))
printfn "%5b: large, opposite vectors" (vec2d.add d e = (0.0,0.0))
printfn "%5A: only given one vector" (vec2d.add f |> string = "invalid")
printfn "%5b: a vector with itself" (vec2d.add f f = (0.0,-200000.0))
printfn "%5b: two zero-vector" (vec2d.add b b = (0.0,0.0))



