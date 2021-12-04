open read

[<EntryPoint>]
let main args =
    args |> fun a -> a.[0] |> System.Console.Write
    0