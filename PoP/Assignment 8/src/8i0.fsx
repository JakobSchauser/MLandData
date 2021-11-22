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


let C = ImgUtil.mk 110 110

let circ_fig = Circle ((50,50), 45, (ImgUtil.red))
let sqr_fig = Rectangle ((40,40), (90,110), ImgUtil.blue)

let mix_fig = Mix (circ_fig, sqr_fig)

for y in 0..110 do  
    for x in 0..110 do  
        let cc = colorAt (x, y) mix_fig
        match cc with
            | Some coltodraw -> ImgUtil.setPixel coltodraw (x, y) C
            | None -> ImgUtil.setPixel ImgUtil.green (x, y) C


do ImgUtil.toPngFile "test.png" C