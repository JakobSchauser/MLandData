// #load "pieces.fs"
module Chess
// open Pieces
open System
/// The possible colors of chess pieces
type Color = White | Black

/// A superset of positions on a board
type Position = int * int

/// <summary> An abstract chess piece. </summary>
/// <param name = "col"> The color black or white </param>
[<AbstractClass>]
type chessPiece(color : Color) =
  let mutable _position : Position option = None

  /// The type of the chess piece as a string, e.g., "king" or "rook".
  abstract member nameOfType : string

  /// Make a deep copy including a copy of an existing pieces. Must be
  /// implemented in the inheriting class and cast to chessPiece for
  /// this to work as an abstract member, since the abstract class has
  /// no knowledge of any future inheritors
  abstract member copy : unit -> chessPiece

  /// The color either White or Black
  member this.color = color

  /// The position as a Position option, e.g., None, Some (0,0), Some
  /// (3,4).
  member this.position
    with get() = _position
    and set(pos) = _position <- pos

  /// Return the first letter of the piece's type usint capital case
  /// for white pieces and lower case for black pieces. E.g., "K" and
  /// "k" for white and a black king respectively.
  override this.ToString () =
    match color with
      White -> (string this.nameOfType.[0]).ToUpper ()
      | Black -> (string this.nameOfType.[0]).ToLower ()

  /// A maximum list of relative runs, a piece may make regardless of
  /// its position and the other pieces on the board. For example, a
  /// rook can move up, down, left, and right, so its list must
  /// contain 4 runs, and the "up" run must contain 7 positions
  /// [(-1,0); (-2,0)]...[-7,0]]. Runs must be ordered such that the
  /// first in a list is closest to the piece at hand.
  abstract member candiateRelativeMoves : Position list list

/// A chess board.
type board () =
  let _board = Collections.Array2D.create<chessPiece option> 8 8 None

  /// <summary> Wrap a position as option type. </summary>
  /// <param name = "pos"> a position </param>
  /// <returns> Some pos or None if the position is on the board or
  /// not </returns>
  let validPositionWrap (pos : Position) : Position option =
    let (rank, file) = pos // square coordinate
    if rank < 0 || rank > 7 || file < 0 || file > 7 
    then None
    else Some (rank, file)

  /// <summary> Converts relative coordinates to absolute and removes
  /// out of board coordinates. </summary>
  /// <param name = "pos"> an absolute position </param>
  /// <param name = "lst"> a list of relative positions </param>
  /// <returns> A list of absolute and valid positions </returns>
  let relativeToAbsolute (pos : Position) (lst : Position list) : Position list =
    let addPair (a : Position) (b : Position) : Position = 
      (fst a + fst b, snd a + snd b)
    // Add origin and delta positions
    List.map (addPair pos) lst
    // Choose absolute positions that are on the board
    |> List.choose validPositionWrap

  /// <summary> Find the tuple of empty squares and first neighbour if any. </summary>
  /// <param name = "run"> A run of absolute positions </param>
  /// <returns> A pair of a list of empty neighbouring positions and
  /// a possible neighbouring piece, which blocks the run. </returns>
  let getVacantNOccupied (run : Position list) : (Position list * (chessPiece option)) =
    try
      // Find index of first non-vacant square of a run
      let idx = List.findIndex (fun (i, j) -> _board.[i,j].IsSome) run
      let (i,j) = run.[idx]
      let piece = _board.[i, j] // The first non-vacant neighbour
      if idx = 0
      then ([], piece)
      else (run.[..(idx-1)], piece)
    with
      _ -> (run, None) // outside the board

  /// Board is indexed using .[,] notation
  member this.Item
    with get(a : int, b : int) = _board.[a, b]
    and set(a : int, b : int) (p : chessPiece option) =
      if p.IsSome then p.Value.position <- Some (a,b)
      _board.[a, b] <- p

  /// Make a deep copy af a board including all the pieces on it
  member this.copy () =
    let b = board ()
    for i = 0 to 7 do
      for j = 0 to 7 do
        b.[i,j] <- Option.bind (fun (p : chessPiece) -> Some (p.copy ())) this.[i,j]
    b
        
  /// Produce string of board for, e.g., the printfn function.
  override this.ToString() =
    let rec boardStr (i : int) (j : int) : string =
      match (i,j) with 
        (8,0) -> ""
        | _ ->
          let stripOption (p : chessPiece option) : string = 
            match p with
              None -> ""
              | Some p -> p.ToString()
          // print top to bottom row
          let pieceStr = stripOption _board.[7-i,j]
          let lineSep = " " + String.replicate (8*4-1) "-"
          match (i,j) with 
          (0,0) -> 
            let str = sprintf "%s\n| %1s " lineSep pieceStr
            str + boardStr 0 1
          | (i,7) -> 
            let str = sprintf "| %1s |\n%s\n" pieceStr lineSep
            str + boardStr (i+1) 0 
          | (i,j) -> 
            let str = sprintf "| %1s " pieceStr
            str + boardStr i (j+1)
    boardStr 0 0

  /// <summary> Move piece from a source to a target position. Any
  /// piece on the target position is removed. </summary>
  /// <param name = "source"> The source position </param>
  /// <param name = "target"> The target position </param>
  member this.move (source : Position) (target : Position) : unit =
    // Update piece' knowledge about it's position
    Option.iter (fun (p : chessPiece) -> p.position <- None) this.[fst target, snd target]
    Option.iter (fun (p : chessPiece) -> p.position <- Some target) this.[fst source, snd source]
    // Update board's pieces
    this.[fst target, snd target] <- this.[fst source, snd source]
    this.[fst source, snd source] <- None
    
  /// <summary> Find the list of available empty positions for this
  /// piece, and the list of possible own and opponent pieces, which
  /// can be protected or taken. </summary>
  /// <param name = "piece"> A chess piece </param>
  /// <returns> A pair of lists of all available moves and neighbours,
  /// e.g., ([(1,0); (2,0);...], [p1; p2]) </returns>
    member this.availableMoves (piece : chessPiece) : (Position list * chessPiece list)  =
    match piece.position with
      None -> 
        ([],[])
      | Some p ->
        let convertNWrap = 
          (relativeToAbsolute p) >> getVacantNOccupied
        let vacantPieceLists = List.map convertNWrap piece.candiateRelativeMoves
        // Extract and merge lists of vacant squares
        let mutable vacant = List.collect fst vacantPieceLists
        // Extract and merge lists of first obstruction pieces and filter out own pieces
        let opponent = List.choose snd vacantPieceLists

        let mutable threatened = []
        if piece.nameOfType = "king" then
          for i in 0..7 do
            for j in 0..7 do
              match this.[i,j] with
                | Some (opp) when opp.color <> piece.color && opp.nameOfType <> "king" ->
                              threatened <- threatened @ fst (this.availableMoves opp)
                | Some (opp) when opp.color <> piece.color && opp.nameOfType = "king" ->
                              threatened <- threatened @ (List.collect fst (List.map ((relativeToAbsolute (Option.get opp.position)) >> getVacantNOccupied) opp.candiateRelativeMoves))
                | _ -> threatened <- threatened
          let possible = List.filter (fun v -> not (List.exists (fun o -> o = v) threatened)) vacant

          vacant <- possible


        // for all opponents, find their available moves
        // remove from vacant list
        // ?
        (vacant, opponent)

  member this.fromNotationToPos (str : string) : Position =  (int str.[0] - 97, int str.[1] - 49)
    

/// <summary> Checks wheter a character is a lowercase letter </summary>
/// <param name = "chr"> The character </param>
/// <returns> A bool </returns>
let isletter (chr:char) : bool = int chr >= 97 && int chr < 106

/// <summary> Checks wheter a character is a number </summary>
/// <param name = "chr"> The character </param>
/// <returns> A bool </returns>
let isnum (chr:char) : bool = int chr >= 49 && int chr <= 59 
  

/// <summary> Checks wheter a movement string is correctly formatted on the form 
///  "a1 a1" where a is any lowercase letter and 1 is a 1-digit number </summary>
/// <param name = "str"> The string on to check if is formatted correctly </param>
/// <returns> A bool </returns>
let isCorrectlyFormatted (str : string) : bool = 
                   isletter str.[0] && isnum str.[1] && isletter str.[3] && isnum str.[4]


[<AbstractClass >]
type Player () = 
  abstract member nextMove : board -> string

type Human () =
  inherit Player ()

  /// <summary> Finds the next move for the human player</summary>
  /// <param name = "Board"> The board the game is played on </param>
  /// <returns> A string corresponding to a legal move </returns>
  override this.nextMove (Board : board) : string =
    try
      let inp = Console.ReadLine(); 
      if not (isCorrectlyFormatted inp) then (if inp = "quit" then "quit" else "wrong formatting") else
        let from = Board.fromNotationToPos inp.[..1]
        let _to = Board.fromNotationToPos inp.[3..]
        
        match Board.[fst from, snd from] with
          | Some (piece) -> if List.exists (fun mv -> mv = _to) (fst (Board.availableMoves piece)) then inp else "not a legal move"
          | _ -> "no piece selected"
    with
      | _ -> "error"


type Game (player1 : Player, player2 : Player) =
  let mutable gameboard = new board ()

  /// <summary> Recursively takes turns for a list of players. Only advancing when a legal move is made </summary>
  /// <param name = "players"> The list of players in the current game </param>
  /// <param name = "gameboard"> The board the game is played on </param>
  /// <param name = "ind"> The current count number </param>
  /// <returns> An int counting hwo many rounds were played </returns>
  let rec taketurn (players : Player list) (gameboard: board) (ind : int) : int =
    printf "%A" (gameboard.ToString ())
    printf "\n%s player to move:\n" (["White";"Black"].[ind%2])
    let nextmove = players.[ind%2].nextMove gameboard
    match nextmove with
      | "quit" ->  ind
      | "not a legal move" | "error" | "no piece selected" | "wrong formatting" | ""-> 
        printf "%A" (gameboard.ToString ())
        printf "%A: Unable to do move!\n" nextmove
        taketurn players gameboard ind
      | _ -> 
        let a = gameboard.fromNotationToPos nextmove.[..1]
        let b = gameboard.fromNotationToPos nextmove.[3..]
        gameboard.move a b
        printf "%A has been moved" nextmove
        taketurn players gameboard (ind+1)

  /// <summary> Starts allowing the players to take turns </summary>
  /// <returns> An int counting hwo many rounds were played </returns>
  member this.run () : int = taketurn [player1;player2] gameboard 0

  member this.Board = gameboard
// let game = new Game ()

// game.run (new Human (), new Human ())