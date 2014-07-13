namespace fspoker

module Holes = 

    open Cards

    type Hole = Card * Card

    let hi (c1, c2) = if (c1 |> rank) > (c2 |> rank) then c1 else c2
    //lo is in this form to force return of second item if equal ranks
    let lo (c1, c2) = if (c1 |> rank) > (c2 |> rank) then c2 else c1

    //order by rank then suit
    let create c1 c2 = 
        let r1, r2 = (c1 |> rank), (c2 |> rank)
        let s1, s2 = (c1 |> suit), (c2 |> suit)
        match r1 < r2, r1 = r2, s1 < s2 with
        | true, _, _ -> c2, c1
        | _, true, false -> c2, c1
        | _ -> c1, c2

    let hiRank = hi >> rank
    let loRank = lo >> rank

    let suited (c1, c2) = suit c1 = suit c2
    let paired (c1, c2) = rank c1 = rank c2

    let index169 h = 
        let v = if suited h
                then (h |> loRank) * 13 + (h |> hiRank)
                else (h |> hiRank) * 13 + (h |> loRank)
        168 - v

    let toString h = (h |> fst |> toString) + (h |> snd |> toString)
    let private toAltString h = (h |> snd |> Cards.toString) + (h |> fst |> Cards.toString)

    let shortString h = 
        string (h |> hiRank |> rankToString) + 
        string (h |> loRank |> rankToString) + 
        if suited h then "s" else "o"
       
    let chkMask a b = a &&& b = 0UL

    let mask (c1,c2) = mask c1 ||| mask c2

    let equals h1 h2 = mask h1 = mask h2
   
    let allHoles = 
        [|for i in [1..51] do
            for j in [i+1..52] ->
               create i j|]

    //String -> Hole
    let private holesByString = 
        let first = allHoles |> Array.map (fun x -> toString x, x) 
        let sec = allHoles |> Array.map (fun x -> toAltString x, x)
        Array.append first sec        
        |> Map.ofArray

    let hole s = holesByString.[s]

    //ShortString -> Hole[]
    let private hole169Map = 
        allHoles
        |> Seq.groupBy (fun h -> h |> shortString)
        |> Seq.map (fun (a,b) -> a, b |> Seq.toArray)
        |> Map.ofSeq

    let holes s = hole169Map.[s] 

    //ShortString -> Index169
    let private hole169IndexMap = 
        allHoles
        |> Seq.groupBy (fun h -> h |> shortString)
        |> Seq.map (fun (a,b) -> a,  b |> Seq.nth 0 |> index169)
        |> Map.ofSeq

    let index s = hole169IndexMap.[s] 
