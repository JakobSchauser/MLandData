#load "chess.fs"
#load "pieces.fs"
open Chess
open Pieces

/// Print various information about a piece
let printPiece (board : board) (p : chessPiece) : unit =
  printfn "%A: %A %A" p p.position (board.availableMoves p)

let pieces = [|
  king (White) :> chessPiece;
  rook (White) :> chessPiece;
  king (Black) :> chessPiece |]

let game = new Game (new Human (), new Human ())

game.Board.[0,0] <- Some pieces.[0]
game.Board.[1,1] <- Some pieces.[1]
game.Board.[4,1] <- Some pieces.[2]

game.run ()
