module readNWrite 
open System


/// <summary> Reads a file </summary>
/// <param name = filename> The name of the file to read </param>
/// <returns> A string option with the read file or None </returns>
let readFile (filename:string) : string option = 
    try 
        Some (System.IO.File.ReadAllText filename) 
    with 
        | _ -> None


/// <summary> Concatenates a list of files </summary>
/// <param name = filenames> The list of filenames to concatenate </param>
/// <returns> A string option with the concatenated read files or None  </returns>
let cat (filenames:string list) : string option = 
    List.fold (fun acc e -> match (readFile(e), acc) with (Some f, Some a) -> Some (a + f) | _ -> None) (Some "") filenames
        

/// <summary> Reverses and concatenates a list of files </summary>
/// <param name = filenames> The list of filenames to reverse and concatenate </param>
/// <returns> A string option with the reversed and concatenated read files or None  </returns>
let tac (filenames:string list) : string option =
    let func acc e = match (readFile(e), acc) with (Some f, Some a) -> Some (a + (f |> Seq.rev |> System.String.Concat)) | _ -> None
    List.fold func (Some "") (filenames |> List.rev)




