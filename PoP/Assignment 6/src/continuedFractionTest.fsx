#load "continuedFraction.fs"
open continuedFraction

// Blackbox test:
printfn "Black-box testing of cfrac2float" 
printfn "%5b: The empty list" (cfrac2float [] = 0.0)
printfn "%5b: [3;4;12;4]" (System.Math.Round(cfrac2float [3;4;12;4],3) = System.Math.Round (3.245,3))
printfn "%5b: Simply [0]" (cfrac2float [0] = 0.0)
printfn "%5b: [4;2] = [4;1;1]" (cfrac2float [4;1;1] = cfrac2float [4;2])

printfn "\nBlack-box testing of float2cfrac" 
printfn "%5b: 0.0" (float2cfrac 0.0 = [0])
printfn "%5b: The example 3.245" (float2cfrac 3.245 = [3; 4; 12; 4])
printfn "%5b: A negative number" (float2cfrac -1.0 = [-1])



// Whitebox
// +-----------------+-------+-----------+-------+-----------------+--------------------------+
// | Unit            | Branx | Condition | Input | Expected Output | Comment                  |
// +-----------------+-------+-----------+-------+-----------------+--------------------------+
// | cfrac2float     | 1     | None      |       |                 | No branches no problems  |
// +-----------------+-------+-----------+-------+-----------------+--------------------------+
// | List.length lst | 1a    | 0         | []    | 0.0             |                          |
// +-----------------+-------+-----------+-------+-----------------+--------------------------+
// |                 | 1b    | 1         | [2]   | 2.0             |                          |
// +-----------------+-------+-----------+-------+-----------------+--------------------------+
// |                 | 1c    | else      | [4;2] | 4.5             | + recursive call         |
// +-----------------+-------+-----------+-------+-----------------+--------------------------+
// | float2cfrac     | 2     |           |       |                 |                          |
// +-----------------+-------+-----------+-------+-----------------+--------------------------+
// | r < eps         | 2a    | false     | 3.0   | [3]             |                          |
// +-----------------+-------+-----------+-------+-----------------+--------------------------+
// |                 | 2b    | true      | 3.245 | [3; 4; 12; 4]   |    + recursive calls     |
// +-----------------+-------+-----------+-------+-----------------+--------------------------+
printfn "\nWhite-box tests: "
printfn "1a : %A" (cfrac2float [])
printfn "1b : %A" (cfrac2float [2])
printfn "1c : %A" (cfrac2float [4;2])

// I am not sure how to test this one, the function calls will both do the recursive branch and the break-condition. This tests both:
printfn "2a : %A" (float2cfrac 3.0)
printfn "2a : %A" (float2cfrac 3.245)



