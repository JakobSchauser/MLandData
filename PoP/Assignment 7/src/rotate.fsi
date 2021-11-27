type Board = char list
type Position = int

val create : (n:uint32) -> Board
val board2Str : (b:Board) -> string
val validRotation : (b:Board) -> (p:Position) -> bool
val rotate : (b:Board) -> (p:Position) -> Board
val scramble : (b:Board) -> (m:uint32) -> Board
val solved : (b:Board) -> bool



