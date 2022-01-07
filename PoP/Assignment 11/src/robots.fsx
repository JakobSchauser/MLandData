
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



type Position = (x,y)
type Direction = North | South | East | West

type Action =
   | Stop of Position
   | Continue of Direction * Position
   | Ignore

[<AbstractClass >]
type BoardElement () =
   abstract member RenderOn : BoardDisplay -> unit
   abstract member Interact : Robot -> Direction -> Action
   default __.Interact  _ _ = Ignore
   abstract member GameOver : Robot list -> bool
   default __.GameOver _ = false


let nextPos (pos:Position) (dir : Direction) : Position =
   match (dir, pos) with
      | (North, (r,c)) -> (r-1,c)
      | (South, (r,c)) -> (r+1,c)
      | (East, (r,c)) -> (r,c+1)
      | (West, (r,c)) -> (r,c-1)

type Robot (row:int , col:int, name:string) =
   let mutable position = (row,col)

   inherit BoardElement ()
   
   member this.Position = position
  
   override this.Interact other dir = 
      match nextPos other.Position dir with
         | this.Position -> Stop other.Position
         | _ -> Ignore // Continue (dir, other.Position)
   
   override this.RenderOn display = ...

   member val Name = name

   member robot.Step dir = position <- nextPos position dir


type Goal (r:int, c:int) =
   inherit BoardElement ()
   member this.Position = (r,c)
   override this.GameOver (robots: Robot list) -> List.exist (fun (r : Robot) -> r.Position = this.Position) robots

type BoardFrame (r:int, c:int) =
   let findedge x y =
      match (x%0,y%0,x,y) with 
         | (0,_,_,y) -> Some (0,y)
         | (_,_,r-1,y) -> Some (r-1,y)
         | (_,0,x,_) -> Some (x,0)
         | (_,_,x,c-1) -> Some (x,x-1)
         | _ -> None
   let mutable positions = []
   for x in 0..r do
      for y in 0..r do
         match findedge x y with
            | Some pos -> positions::[pos]
            | None -> 
      end
   end

   inherit BoardElement ()

type HorizontalWall (r:int, c:int, n:int) =
   member.Positions = [for i in 0 .. (abs n) -> (r + i * n / (abs n), c)]  
   inherit BoardElement ()

   let interactPos (otherpos: Position) (dir: Direction) (pos: Position) = 
      match (nextpos otherpos dir, dir) with
         | (pos ,Direction.North) | ((fst pos, snd pos + 1), Direction.South) -> true
         | _ -> false

   override this.Interact (robot:Robot) (dir:Direction) = 
      if List.exist (fun pos -> interactPos robot.Position dir pos) this.Positions then Stop robot.Position else Ignore

type VerticalWall (r:int, c:int, n:int) =
   member.Positions = [for i in 0 .. (abs n) -> (r, c - i * n / (abs n))]
   inherit BoardElement ()
   
   let interactPos (otherpos: Position) (dir: Direction) (pos: Position) = 
      match (nextpos otherpos dir, dir) with
         | (pos,Direction.West) | ((fst pos, snd pos + 1),Direction.East) -> true
         | _ -> false

   override this.Interact (robot:Robot) (dir:Direction) = 
      if List.exist (fun pos -> interactPos robot.Position dir pos) this.Positions then Stop robot.Position else Ignore

type Board =
   let mutable robots = []
   let mutable otherElements = []

   member this.AddRobot (robot:Robot) = robots <- robots::robot

   member this.AddElement (element:BoardElement) = otherElements <- otherElements::element

   member this.Elements () = otherElements@robots

   member this.Robots () = robots

   member rec this.Move (robot: Robot) (dir:Direction) = 
      match List.find (fun elem -> match elem.Interact robot dir with |Stop pos -> true | Ignore -> false) this.Elements with
         | Stop pos -> 
         | _ -> 
            robot.Step dir
            this.Move robot dir 
      


let r = 3
let c = 3

let game = new Board
game.AddElement (new BoardFrame 3 3)

let a = new BoardDisplay (3,4)
a.SetBottomWall (1,1)
a.Set (0,2, "BB")
a.Set (0,1, "CC")
a.Show ()