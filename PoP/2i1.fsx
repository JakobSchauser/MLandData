let sliceAndPrint (str : string) =
    let hw : string = "hello world"
    let ind : int = hw.IndexOf(str)
    do printfn "%A" hw.[ind..(ind + String.length str - 1)]


sliceAndPrint "hello"
sliceAndPrint "world"
