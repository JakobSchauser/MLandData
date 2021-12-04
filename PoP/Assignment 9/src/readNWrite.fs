module read 
open System


let readFile (filename:string) : string option = 
    try 
        Some (System.IO.File.ReadAllText filename) 
    with 
        | _ -> None


let cat (filenames:string list) : string option = 
    List.fold (fun acc e -> match (readFile(e), acc) with (Some f, Some a) -> Some (a + f) | _ -> None) (Some "") filenames
        


let tac (filenames:string list) : string option =
    let func acc e = match (readFile(e), acc) with (Some f, Some a) -> Some (a + (f |> Seq.rev |> System.String.Concat)) | _ -> None
    List.fold func (Some "") (filenames |> List.rev)


let countLinks (url:string) : int =
    let url2Stream url =
        let uri = System.Uri url
        let request = System.Net.WebRequest.Create uri
        let response = request.GetResponse ()
        response.GetResponseStream ()

    let readUrl url =
        let stream = url2Stream url
        let reader = new System.IO.StreamReader(stream)
        reader.ReadToEnd ()
    
    try 
        let all = readUrl url
        (System.Text.RegularExpressions.Regex.Matches(all, "<a")).Count
    with
        | _ -> -1
    

// printfn "%A" (countLinks "http://transcendentinnerglowingstars.neverssl.com/online")



