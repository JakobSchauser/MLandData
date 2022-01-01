
type Square = {on : string; right_wall : bool; bottom_wall : bool}
type BoardDisplay (rows:int, cols:int) =
   let mutable board = [for i in 1 .. rows*cols -> {"  ";false;false} ]

   // "" = empty
   // "A"-"Z" = robot
   // r = right wall
   // b = bottom wall

   member.Board = board
   member.Set (row:int) (col:int) (cont:string) =
      

   member SetBottomWall : row:int * col:int -> unit
   member SetRightWall : row:int * col:int -> unit
   member.Show =
      let rec makerow (row1:string) (row2:string) (boardrow:list) =
         match boardrow with
            | head::tail -> 
               let mutable newstr = head.on
               if head.right_wall then
                  newstr <- newstr + "|"
               else
                  newstr <- newstr + " "
               if head.bottom_wall then
                  let newstrl2 = "--+"
               else
                  let newstrl2 = "  +"

               makerow (row1 + newstr) (row2 + newstrl2) tail
            | [] -> (row1, row2)

      for row in 0..(rows-2) do
         let rows = makerow "" "" board.[i*cols:(i+1)*cols]
         printfn "%A" fst rows
         printfn "%A" snd rows
      end

   end