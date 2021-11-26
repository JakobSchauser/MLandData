type point = int * int // a point (x, y) in the plane
type color = ImgUtil.color

type figure =
    | Circle of point * int * color
    // defined by center , radius , and color
    | Rectangle of point * point * color
    // defined by corners top -left , bottom -right , and color
    | Mix of figure * figure
    // combine figures with mixed color at overlap

// finds color of figure at point
let rec colorAt (x,y) figure =
    match figure with
        | Circle ((cx,cy), r, col) ->
            if (x-cx)*(x-cx)+(y-cy)*(y-cy) <= r*r
            // uses Pythagoras 'equation to determine
            // distance to center
            then Some col else None
        | Rectangle ((x0,y0), (x1,y1), col) ->
            if x0 <=x && x <= x1 && y0 <= y && y <= y1
            // within corners
            then Some col else None
        | Mix (f1, f2) ->
            match (colorAt (x,y) f1, colorAt (x,y) f2) with
                | (None , c) -> c // no overlap
                | (c, None) -> c // no overlap
                | (Some c1, Some c2) ->
                    let (a1 ,r1 ,g1 ,b1) = ImgUtil.fromColor c1
                    let (a2 ,r2 ,g2 ,b2) = ImgUtil.fromColor c2
                    in Some(ImgUtil.fromArgb ((a1+a2)/2, (r1+r2)/2, (g1+g2)/2, (b1+b2)/2)) // average



/// WRITE HERE
/// <summary> Makes a piture from a figure  </summary>
/// <param name = "filnavn"> The name of the resulting picture </param>
/// <param name = "figur"> The figure to draw </param>
/// <param name = "b"> The width of the picture </param>
/// <param name = "h"> The height of the picture </param>
/// <returns> nothing, I think <\returns>
let makePicture (filnavn:string) (figur:figure) (b:int) (h:int) : unit = 
    let C = ImgUtil.mk b h
    for y in 0..h do  
        for x in 0..b do  
            let cc = colorAt (x, y) figur
            match cc with
                | Some coltodraw -> ImgUtil.setPixel coltodraw (x, y) C
                | None -> ImgUtil.setPixel (ImgUtil.fromArgb (255,128,128,128)) (x, y) C

    do ImgUtil.toPngFile filnavn C

/// WRITE HERE
/// <summary> Checks whether a figure is legal </summary>
/// <param name = "f"> The figure to check </param>
/// <returns> true or false depending on the validity of the figure <\returns>
let rec checkFigure (f:figure) : bool =
    match f with
        | Circle ((cx,cy), r, col) -> 
            r > 0
        | Rectangle ((x0,y0), (x1,y1), col) -> 
            x0 <= x1 && y0 <= y1
        | Mix (f1, f2) -> 
            (checkFigure f1) && (checkFigure f2)

/// WRITE HERE
/// <summary> Moves a figure </summary>
/// <param name = "f"> The figure to move </param>
/// <param name = "(x,y)"> The vector to move the figure by </param>
/// <returns> The moved figure <\returns>
let rec move (f : figure) (x : int,y : int) : figure = 
    match f with
        | Circle ((cx,cy), r, col) -> 
            Circle ((cx + x,cy + y), r, col)
        | Rectangle ((x0,y0), (x1,y1), col) -> 
            Rectangle ((x0 + x, y0 + y), (x1 + x, y1 + y), col) 
        | Mix (f1, f2) -> 
            Mix (move f1 (x,y), move f2 (x,y))

/// WRITE HERE
/// <summary> Finds the smallest of two numbers </summary>
/// <param name = "x"> The first number </param>
/// <param name = "y"> The second number </param>
/// <returns> The smaller number between x and y <\returns>
let min x y =
    if x < y then x 
    else y

/// WRITE HERE
/// <summary> Finds the largest of two numbers </summary>
/// <param name = "x"> The first number </param>
/// <param name = "y"> The second number </param>
/// <returns> The larger number between x and y <\returns>
let max x y = 
    if x > y then x
    else y


/// WRITE HERE
/// <summary> Finds the smallest bounding box needed to fit a figure </summary>
/// <param name = "f"> The figure to bound </param>
/// <returns> The upper left and lower right corners of the bounding box <\returns>
let rec boundingBox  (f:figure) : (point * point) = 
    match f with
        | Circle ((cx,cy), r, col) -> 
            ((cx-r,cy-r), (cx+r,cy+r))
        | Rectangle ((x0,y0), (x1,y1), col) -> 
            ((x0,y0), (x1,y1))
        | Mix (f1, f2) -> 
            match (boundingBox f1,boundingBox f2) with 
                | (((x0, y0),(x1, y1)), ((x2, y2),(x3, y3))) ->
                    (((min x0 x2), (min y0 y2)), ((max x1 x3), (max y1 y3)))

            

let circ_fig = Circle ((50,50), 45, (ImgUtil.red))
let sqr_fig = Rectangle ((40,40), (90,110), ImgUtil.blue)

let figTest = Mix (circ_fig, sqr_fig)

makePicture "figTest.png" figTest 100 150