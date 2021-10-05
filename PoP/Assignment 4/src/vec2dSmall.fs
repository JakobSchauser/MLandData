module vec2d

// <summary> Finds the length of a vector </summary>
// <param name = "(a,b)"> The x- and y-components of the vector </param>
// <returns> The length of vector a <\returns>
let len ((a,b) : (float*float)) = sqrt (a*a + b*b)

// <summary> Finds the angle of a vector </summary>
// <param name = "(a,b)"> The x- and y-components of the vector </param>
// <returns> The angle of vector a <\returns>
let ang ((a,b) : (float*float)) : float = (atan2 b a) |> float

// <summary> Adds two vectors </summary>
// <param name = "(a0,b0)"> The x- and y-components of the first vector </param>
// <param name = "(a1,b1)"> The x- and y-components of the second vector </param>
// <returns> The sum of the two vectors <\returns>
let add ((a0,b0) : (float*float)) ((a1,b1) : (float*float)) : (float*float) = ((a0 + a1),(b0 + b1))


