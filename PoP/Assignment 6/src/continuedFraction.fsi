module continuedFraction

/// <summary> Takes a list and returns the decimal number </summary>
/// <param name = "lst"> A list of integers </param> 
/// <returns> Returns a float </summary>
val cfrac2float : int list -> float

/// <summary> Takes a decimal number and returns the list of numbers for the continued fraction </summary>
/// <param name = 'x'> A floating point number </param> 
/// <returns> Returns a list of integers </summary>
val float2cfrac : float -> int list