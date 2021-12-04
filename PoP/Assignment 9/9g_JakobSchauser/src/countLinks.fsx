/// <summary> Counts the number of links in a website </summary>
/// <param name = url> The url to request to get the website </param>
/// <returns> The number of links or -1 if the website did not respond  </returns>
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
        let count = (System.Text.RegularExpressions.Regex.Matches(all, "<a")).Count
        printfn "%A" count
        count
    with
        | _ -> -1
    

// printfn "%A" (countLinks "http://transcendentinnerglowingstars.neverssl.com/online")
