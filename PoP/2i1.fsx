// Finds the substring in "hello world" and prints the sliced substring... for some reason..
let sliceAndPrint (str : string) =
    let hw : string = "hello world"
    let ind : int = hw.IndexOf(str)
    do printfn "%A" hw.[ind..(ind + String.length str - 1)]


sliceAndPrint "hello"
sliceAndPrint "world"

// I just did this for fun. The actual question simply asked for the following which will not output anything, as no variables are saved and no functions are called except the slicing:

"hello world".[..4] 
"hello world".[6..] 
