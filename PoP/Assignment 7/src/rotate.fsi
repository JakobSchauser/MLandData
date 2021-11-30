type Board = char list
type Position = int

val create : uint32 -> Board
val board2Str : Board -> string
val validRotation : Board -> Position -> bool
val rotate : Board -> Position -> Board
val scramble : Board -> uint32 -> Board
val solved : Board -> bool