// #load "vec2dSmall.fsi"
#load "vec2dSmall.fs"
open vec2d

let v = (1.3, -2.5)
printfn "Vector %A: (%f, %f)" v (vec2d.len v) (vec2d.ang v)
let w = (-0.1, 0.5)
printfn "Vector %A: (%f, %f)" w (vec2d.len w) (vec2d.ang w)
let s = vec2d.add v w
printfn "Vector %A: (%f, %f)" s (vec2d.len s) (vec2d.ang s)




(*   HÅNDKØRING --  I am very much in doubt how all the one-liners would be written. 
                        I did not have time to attend the helping sessions, so this my best attempt.
                        (when refering to the other files, I simply refered to the line where it was imported)
                        
    Step | Line | Env. | Bindings and evaluations
    0      -      E0     ()
    1      2      E0     dont know what to write here
    2      3      E0     loads the vector module 
    3      5      E0     v = (1.3, -2.5)
    4      2      E1     len = ((a = 1.3,b = -2.5), sqrt (a*a + b*b),())
    5      2      E1     return 2.817801
    6      2      E2     ang = ((a = 1.3,b = -2.5), (atan2 b a),())
    7      2      E2     return -1.091277
    8      6      E0     output: (1.3, -2.5): (2.817801, -1.091277)
    9      7      E0     w = (-0.1, 0.5)
    10     2      E1     len = ((a = -0.1, b = 0.5), sqrt (a*a + b*b),())
    11     2      E1     return 0.509902
    12     2      E2     ang = ((a = -0.1, b = 0.5), (atan2 b a),())
    13     2      E2     return 1.768192
    14     8      E0     output: Vector (-0.1, 0.5): (0.509902, 1.768192)
    15     2      E0     add =  ((a0 = 1.3,b0 = -2.5, a1 = -0.1, b1 = 0.5), ((a0 + a1),  b0 + b1)),())
    16     2      E0     return (1.2, -2.0)
    17     9      E1     s = (1.2, -2.0)
    18     10     E0     output: Vector (1.2, -2.0): (2.332381, -1.030377)
*)