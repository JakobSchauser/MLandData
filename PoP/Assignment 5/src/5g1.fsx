/// <summary> Takes a 2-dimensional array and returns its transpose </summary>
/// <param name = a> The Array2d to transpose </param>
/// <returns> The transposed Array2d </returns>
let transposeArr (a : 'a [,]) : 'a [,] = Array2D.init (Array2D.length2 a) (Array2D.length1 a) (fun i j -> a.[j,i])

let test = array2D [[3;3;3];[2;2;2];[1;1;1]]

printfn "%A" (transposeArr test)


// (c)
// The advantages of transposeArr is, that the code is short and therefore easily read.
// We don't really see the advantages of doing it as we did in 5g0.fsx, but in other usecases,
// the more elaborate, handwritten implementation might be advantageous.
// Also arrays does not allow for raggedness, which might be needed for a given problem.


// (d) 
// It is obvious from the ease transposeArr was written with compared to transposeLstLst that arrays has some clear advantages.
// The mutable elements and fixed sizes of the arrays makes them obvious for the for loops of the imperative paradigm.  
// For a functional paradigm, lists are better built for recursion as elements are immutable and pattern matching because of the built-in f# functionality.
