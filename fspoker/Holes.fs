namespace fspoker

module Holes = 

    open Cards

    type Hole = Card * Card

    let hi (c1, c2) = if (c1 |> rank) > (c2 |> rank) then c1 else c2
    //lo is in this form to force return of second item if equal ranks
    let lo (c1, c2) = if (c1 |> rank) > (c2 |> rank) then c2 else c1

    let hiRank = hi >> rank
    let loRank = lo >> rank

    let suited (c1, c2) = suit c1 = suit c2
    let paired (c1, c2) = rank c1 = rank c2

    let index169 h = 
        let v = if suited h
                then (h |> loRank) * 13 + (h |> hiRank)
                else (h |> hiRank) * 13 + (h |> loRank)
        168 - v

    let toString h = (h |> hi |> toString) + (h |> lo |> toString)

    let shortString h = 
        string (h |> hiRank) + 
        string (h |> loRank) + 
        if suited h then "s" else "o"
       
    let chkMask a b = a &&& b = 0UL

    let mask (c1,c2) = mask c1 ||| mask c2

    let equals h1 h2 = mask h1 = mask h2
   
    let allHoles = 
        [|for i in [1..51] do
            for j in [i+1..52] ->
               i, j|]

    //String -> Hole
    let private holesByString = 
        allHoles 
        |> Array.map (fun x -> toString x, x) 
        |> Map.ofArray

    let fromString s = holesByString.[s]

    //ShortString -> Hole[]
    let private hole169Map = 
        allHoles
        |> Seq.groupBy (fun h -> h |> shortString)
        |> Seq.map (fun (a,b) -> a, b |> Seq.toArray)
        |> Map.ofSeq

    let fromShortString s = hole169Map.[s] 

    //ShortString -> Index169
    let private hole169IndexMap = 
        allHoles
        |> Seq.groupBy (fun h -> h |> shortString)
        |> Seq.map (fun (a,b) -> a,  b |> Seq.nth 0 |> index169)
        |> Map.ofSeq

    let indexFromShortString s = hole169IndexMap.[s] 
