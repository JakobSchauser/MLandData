// Q: What sound does a cumulative bee make? 
// A: Sum sum sum
let sum (n : int) : int =
    if n > 0 then
        let mutable count : int = 1
        let mutable s : int = 0
        while count <= n  do
            s <- count + s
            count <- count + 1
        s
    else 0

// Gauss was quite a lot smarter than any of us will ever be. 
// He said that the following is equal to the "sum" function above and we will trust that blindly!
let simplesum (n: int) : int = if n > 0 then n*(n+1)/2 else 0


// Take an input and tries casting it to an integer, which is then returned
let input () : int =
    printfn "Enter a number to sum to:"
    let x = System.Console.ReadLine()
    x |> int


// We had not seen that there was a built-in padding in F#, so this was our solution
let addwhite = fun i -> (i |> string) + String.replicate (4 - String.length (i |> string)) " " 


// Recursive row-printing with correct whitespace
let rec printrow (n : int) = 
    if n > 10 then
        true
    else
        let str : string = addwhite n + addwhite (sum n) + addwhite (simplesum n)
        printfn "%s" str 
        printrow(n+1)
    




// To see if we understand the input system, outcomment the following two lines:
// let x = input()
// printfn "%A" (sum x)


// To see our solution to the "print in a table"-problem, outcomment the following two lines:
// printfn "n  |sum|simplesum"
// printrow(1)


// The maximal number is 2^31-1 as we are using integers. 
// To test this, just outcomment the following two lines 
// let max : int = 2147483647
// printfn "%A %A" max (max+1)