open read

[<EntryPoint>]
let main args =
    args |> Array.toList |> cat |> Option.get |> System.Console.Write
    0