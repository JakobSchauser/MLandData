// This is the answer to the question (a)
// <summary> Finds the sum up to a number n </summary>
// <param name = "n"> The number to sum to </param>
// <returns> The total sum. 0 if n < 0 <\returns>
let sum (n : int) : int =
    if n > 0 then
        let mutable count : int = 1
        let mutable s : int = 0
        while count <= n  do
            s <- count + s
            count <- count + 1
        s
    else 0


// This is the answer to the question (b)
// This is an implementation of Gauss' summation rule, that for a given n returns the sum from 1 to n:
// <summary> Finds the sum up to a number n using Gauss' summation rule </summary>
// <param name = "n"> The number to sum to </param>
// <returns> The total sum. 0 if n < 0 <\returns>
let simpleSum (n: int) : int = if n > 0 then n*(n+1)/2 else 0





// <summary> Recursively prints rows with correct whitespace </summary>
// <param name = "n"> The row number to start at </param>
// <returns> true when finished <\returns>
let rec printRow (n : int) = 
    if n > 10 then
        true
    else
        printfn "%-4i%-4i%-4i" n (sum n) (simpleSum n)
        printRow(n+1)

// This is the answer to the question (c)

// <summary> Takes an input from the user and tries casting it to an integer </summary>
// <returns> The integer inputted by the user <\returns>
let input () : int =
    printfn "Enter a number to sum to:"
    let x = System.Console.ReadLine()
    x |> int
let x = input()
printfn "%A" (sum x)


// This is the answer to (d)
printfn "n  |sum|simplesum"
printRow(1)


// The maximal number is 2^31-1. This is purely because we have defined the functions as outputting ints, which has a maximal value of 2147483647
// A workaround is defining them to take unsigned ints or doubles which both have higher maximal values. 

// This is also means that the highest n where the calculation works is when the sum is less than 2147483647. A quick calculation shows that this happens at:
// n*(n+1)/2 = 2147483647  <=>   n^2+n = 2147483647*2  => n = (sqrt(8*2147483647+1)-1)/2 = 65535
