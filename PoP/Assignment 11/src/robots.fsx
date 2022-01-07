
type Square =
   val mutable on: string
   val mutable right_wall: bool
   val mutable bottom_wall: bool
   new(_on : string, right : bool, bottom : bool) = {on = _on; right_wall = right; bottom_wall = bottom}

type BoardDisplay (rows:int, cols:int) =
   let size = (rows + 1)* (cols + 1)
   let mutable board = [for i in 0 .. size -> new Square ("  ",false,false)]

   // "" = empty
   // "A"-"Z" = robot
   // r = right wall
   // b = bottom wall

   member this.Board = board

   member this.Reset () = board <- [for i in 0 .. size -> new Square ("  ",false,false)]

   member this.Set (row:int,col:int) (cont:string) =
      board.[row + col*cols].on <- cont

   member this.SetBottomWall (row:int, col:int) = 
      board.[row + col*cols].bottom_wall <- true

   member this.SetRightWall (row:int, col:int) = 
      board.[row + col*cols].right_wall <- true


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

      for row in 0..(rows-1) do
         let rr = board.[row*(cols + 1) .. (row + 1)*(cols + 1)]
         let (first,second) = makerow "" "" rr
         if row <> 0 then printfn "%A" first.[1..]
         
         printfn "%A" second.[1..]



type Position = int*int
type Direction = North | South | East | West

type Action =
   | Stop of Position
   | Continue of Direction * Position
   | Ignore


let nextPos (pos:Position) (dir : Direction) : Position =
   match (dir, pos) with
      | (North, (r,c)) -> (r-1,c)
      | (South, (r,c)) -> (r+1,c)
      | (East, (r,c)) -> (r,c+1)
      | (West, (r,c)) -> (r,c-1)

[<AbstractClass >]
type BoardElement () =
   abstract member RenderOn : BoardDisplay -> unit
   abstract member Interact : Robot -> Direction -> Action
   default __.Interact  _ _ = Ignore
   abstract member GameOver : Robot list -> bool
   default __.GameOver _ = false

and Robot (row:int , col:int, name:string) =
   inherit BoardElement ()

   let mutable position = (row,col)


   member this.Position = position

   override this.Interact other dir = 
      match nextPos other.Position dir with
         | this.Position -> Stop other.Position
         | _ -> Ignore // Continue (dir, other.Position)

   override this.RenderOn display = display.Set (r,c) (name + name)

   member val Name = name

   member robot.Step dir = position <- nextPos position dir

type Goal (r:int, c:int) =
   inherit BoardElement ()
   member this.Position = (r,c)
   override this.GameOver robots = List.exist (fun (r : Robot) -> r.Position = this.Position) robots
   override this.RenderOn display = display.Set (r,c) "GG"

type BoardFrame (r:int, c:int) =


   let findBorder () = 
      let mutable positions = []

      for x in 0..r do
         positions <- positions::[(-1, x)]
         positions <- positions::[(c, x)]

      for y in 0..(c-2) do
         positions <- positions::[(y+1, -1)]
         positions <- positions::[(y+1, r)]

      positions

   let mutable positions = findBorder ()
   inherit BoardElement ()

   member this.Positions = positions

   override this.RenderOn display = this.Positions |> List.iter (fun pos -> display.SetBottomWall pos)


   inherit BoardElement ()

type HorizontalWall (r:int, c:int, n:int) =
   member this.Positions = [for i in 0 .. (abs n) -> (r + i * n / (abs n), c)]  
   inherit BoardElement ()

   let interactPos (otherpos: Position) (dir: Direction) (pos: Position) = 
      let south = (fst pos, snd pos + 1)
      match (nextpos otherpos dir, dir) with
         | (pos,Direction.North) | (south, Direction.South) -> true
         | _ -> false

   override this.Interact (robot:Robot) (dir:Direction) = 
      if List.exist (fun pos -> interactPos robot.Position dir pos) this.Positions then Stop robot.Position else Ignore
   
   override this.RenderOn display = this.Positions |> list.Iter (fun pos -> display.SetBottomWall pos)

type VerticalWall (r:int, c:int, n:int) =
   member this.Positions = [for i in 0 .. (abs n) -> (r, c - i * n / (abs n))]
   inherit BoardElement ()
   
   let interactPos (otherpos: Position) (dir: Direction) (pos: Position) = 
      let east = (fst pos, snd pos + 1)
      match (nextpos otherpos dir, dir) with
         | (pos,Direction.West) | (east,Direction.East) -> true
         | _ -> false

   override this.Interact (robot:Robot) (dir:Direction) = 
      if List.exist (fun pos -> interactPos robot.Position dir pos) this.Positions then Stop robot.Position else Ignore

   override this.RenderOn display = this.Positions |> list.Iter (fun pos -> display.SetRightWall pos)


type Board (r:int, c:int) =
   let mutable display = new BoardDisplay (r,c) 
   let mutable robots = []
   let mutable otherElements = []

   member this.AddRobot (robot:Robot) = robots <- robots::robot

   member this.AddElement (element:BoardElement) = otherElements <- otherElements::element

   member this.Elements = otherElements@robots

   member this.Robots = robots

   let rec movement (robot: Robot) (dir:Direction) = 
      if List.forall (fun elem -> match elem.Interact robot dir with |Stop pos -> false | Ignore -> true) this.Elements then
            robot.Step dir
            this.Move robot dir
         
            

   member this.Move (robot: Robot) (dir:Direction) = movement robot dir
      
   
   member this.Render () =
      display.Reset ()
      this.Elements |> List.iter (fun elem -> elem.RenderOn display)
      display.show ()
      

let r = 3
let c = 3

let game = new Board (r,c)

game.AddElement (new BoardFrame (r,c))

game.render ()