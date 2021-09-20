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
// He said that the following is equal to the "sum" function above and I will trust that blindly!
let simplesum (n: int) : int = if n > 0 then n*(n+1)/2 else 0


let input () : int =
    printfn "Enter a number to sum to:"
    let x = System.Console.ReadLine()
    x |> int


let addwhite (str : string) : string = str + String.replicate (4 - String.length str) " " 


let rec printrow (n : int) = 
    if n > 10 then
        true
    else
        let nstr = n |> string
        let sstring = sum(n) |> string
        let ssstring = simplesum(n) |> string
        let str : string = (addwhite nstr + addwhite sstring + addwhite ssstring)
        printfn "%s" str 
        printrow(n+1)
    



// let x = input()

printfn "n  |sum|simplesum"
printrow(1)



