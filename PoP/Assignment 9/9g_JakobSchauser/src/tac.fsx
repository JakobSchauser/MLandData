open readNWrite

[<EntryPoint>]
let main args =
    args |> Array.toList |> tac |> Option.get |> System.Console.Write
    0