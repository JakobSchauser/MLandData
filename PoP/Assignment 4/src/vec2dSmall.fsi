// A 2 dimensional  vector  library.
// Vectors  are  represented  as  pairs of  floats
module vec2d

// <summary> Finds the angle of a vector </summary>
// <param name = "(a,b)"> The x- and y-components of the vector </param>
// <returns> The angle of vector a <\returns>
val ang : float * float -> float


// <summary> Finds the length of a vector </summary>
// <param name = "(a,b)"> The x- and y-components of the vector </param>
// <returns> The length of vector a <\returns>
val len : float * float -> float


// <summary> Adds two vectors </summary>
// <param name = "(a0,b0)"> The x- and y-components of the first vector </param>
// <param name = "(a1,b1)"> The x- and y-components of the second vector </param>
// <returns> The sum of the two vectors <\returns>
val add : float * float  -> float * float  -> float * float

