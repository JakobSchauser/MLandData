module System.IO

let txt = File.OpenText "test.txt"
let prnt = txt.Read

printfn "%A" prnt