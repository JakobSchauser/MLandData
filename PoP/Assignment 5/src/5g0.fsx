let test = [[10;10;10];[9;9;9];[8;8;8]]

let allSame (len : int) (elem : 'a list) : bool = (elem.Length = len)
 
let isTable (llst : 'a list list) : bool =
    match llst with
    | [] -> false
    | _ -> llst.[0].Length > 0 && llst.Length = (List.filter (allSame llst.[0].Length) llst).Length 

let firstColumn (llst : 'a list list) : 'a list =
    match List.contains [] llst with
        | true -> []
        | _ -> List.map (fun (l : 'a list) -> l.[0]) llst

let dropFirstColumn (llst : 'a list list) : 'a list list =
    match List.contains [] llst with
        | true -> [[]]
        | _ -> List.map (fun (l : 'a list) -> l.[1..]) llst

let rec trans (donelist : 'a list list) (whole : 'a list list) : 'a list list =
    let df = dropFirstColumn whole 
    match df with
    | [[]] -> donelist
    | _ -> trans (donelist@([firstColumn whole])) df

let transposeLstLst (llst : 'a list list) : 'a list list =
    match isTable llst with
        | false -> []
        | true -> trans [] llst


printfn "%A" (isTable test)

printfn "%A" (dropFirstColumn test)
 
printfn "%A" (transposeLstLst test)
