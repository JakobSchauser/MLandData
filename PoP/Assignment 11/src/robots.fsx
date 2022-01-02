
type Square =
   val mutable on: string
   val mutable right_wall: bool
   val mutable bottom_wall: bool
   new(_on : string, right : bool, bottom : bool) = {on = _on; right_wall = right; bottom_wall = bottom}

type BoardDisplay (rows:int, cols:int) =
   let size = rows*cols
   let mutable board = [for i in 0 .. size -> new Square ("  ",false,false)]

   // "" = empty
   // "A"-"Z" = robot
   // r = right wall
   // b = bottom wall

   member this.Board = board

   member this.Set (row:int,col:int,cont:string) =
      board.[row+col*cols].on <- cont

   member this.SetBottomWall (row:int, col:int) = 
      board.[row+col*cols].bottom_wall <- true

   member this.SetRightWall (row:int, col:int) = 
      board.[row+col*cols].right_wall <- true


   member this.Show () =
      let rec makerow (row1:string) (row2:string) (boardrow: Square list) =
         match boardrow with
            | head::tail -> 
               let mutable newstr = head.on
               let mutable newstrl2 = ""

               if head.right_wall then
                  newstr <- newstr + "|"
               else
                  newstr <- newstr + " "
               if head.bottom_wall then
                  newstrl2 <- "--+"
               else
                  newstrl2 <- "  +"
               

               makerow (row1 + newstr) (row2 + newstrl2) tail
            | [] -> (row1.[0..row1.Length - 2] + "|", row2.[0..row2.Length - 2] + "+")

      let mutable top = "+"
      for i in 1..cols do
         top <- top + "--+"

      printfn "%A" top
      for row in 0..(rows-1) do
         let rr = board.[row*cols..((row + 1)*cols-1)]
         let (first,second) = makerow "" "" rr
         printfn "%A" ("|" + first)
         if not (row = (rows-1)) then 
            printfn "%A" ("+" + second)

      printfn "%A" top



let a = new BoardDisplay (3,4)
a.SetBottomWall (1,1)
a.Set (0,2, "BB")
a.Set (0,1, "CC")
a.Show ()