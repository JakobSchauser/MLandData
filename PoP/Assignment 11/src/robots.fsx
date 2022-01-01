 type BoardDisplay (rows:int, cols:int) =
    let mutable board = [for i in 1 .. rows*cols -> "  "]

    // "  " = empty
    // "AA"-"ZZ" = robot
    // b = bottom wall
    // r = right wall
    member.Board = board
    member.Set (row:int) (col:int) (cont:string) =
        

    member SetBottomWall : row:int * col:int -> unit
    member SetRightWall : row:int * col:int -> unit
    member.Show =

    end