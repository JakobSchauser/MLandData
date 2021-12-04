open System

let readFile (filename:string) : string option = 
    try 
        Some (System.IO.File.ReadAllText filename) 
    with 
        | _ -> None


let cat (filenames:string list) : string option = 
    List.fold (fun acc elem -> match (readFile(elem), acc) with (Some f, Some a) -> Some (a + f) | _ -> None) (Some "") filenames
        


let tac (filenames:string list) : string option =
    let func acc elem = match (readFile(elem), acc) with (Some f, Some a) -> Some (a + (f |> Seq.toList |> List.rev |> List.toArray |> System.String).Replace("n\\","\n")) | _ -> None
    List.fold func (Some "") (filenames |> List.rev)


let countLinks (url:string) : int ->
    let req = WebRequest.Create(Uri(url))
    use resp = req.GetResponse
    use stream = resp.GetResponseStream
    use reader = new IO.StreamReader(stream)
    callback reader url
// printfn "%A" (tac ["test.txt"])

