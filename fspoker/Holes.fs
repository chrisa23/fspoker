namespace fspoker

module Holes = 
    open System
    open Cards
    
    let private r = new Random((int)DateTime.Now.Ticks);
    
    exception CantFindHole
    
    [<CustomComparison; CustomEquality>]
    type Hole = 
      { 
        Cards: int * int
        String: string
        Index169: int 
        Mask: uint64 
      }
        override x.Equals(yobj) =
            match yobj with
            | :? Hole as y -> x.Mask = y.Mask
            | _ -> false
        override x.GetHashCode() = hash x.Mask
        interface System.IComparable with
          member x.CompareTo yobj =
              match yobj with
              | :? Hole as y -> compare x.Mask y.Mask
              | _ -> invalidArg "yobj" "cannot compare values of different types"

    let hi (c1, c2) = if (c1 |> rank) > (c2 |> rank) then c1 else c2
    //lo is in this form to force return of second item if equal ranks
    let lo (c1, c2) = if (c1 |> rank) > (c2 |> rank) then c2 else c1

    //order by rank then suit
    let sort c1 c2 = 
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
   
   
    let create c1 c2 =
        let cards = sort c1 c2
        {Cards = cards; Index169 = index169 cards; Mask = mask cards; String = toString cards}

    let cards h = h.Cards

    let allHoles = 
        [|for i in [1..51] do
            for j in [i+1..52] ->
               create i j|]

    let holeLength = allHoles.Length

    //String -> Hole
    let private holesByString = 
        let first = allHoles |> Array.map (fun x -> toString x.Cards, x) 
        let sec = allHoles |> Array.map (fun x -> toAltString x.Cards, x)
        Array.append first sec        
        |> Map.ofArray

    let hole s = holesByString.[s]

    //ShortString -> Hole[]
    let private hole169Map = 
        allHoles
        |> Seq.groupBy (fun h -> h.Cards |> shortString)
        |> Seq.map (fun (a,b) -> a, b |> Seq.toArray)
        |> Map.ofSeq

    let holes s = hole169Map.[s] 

    //ShortString -> Index169
    let hole169IndexMap = 
        allHoles
        |> Seq.groupBy (fun h -> h.Cards |> shortString)
        |> Seq.map (fun (a,b) -> a,  b |> Seq.nth 0 |> (fun x -> x.Cards) |> index169)
        |> Map.ofSeq

    let index s = hole169IndexMap.[s] 

    let rhole() = allHoles.[r.Next(holeLength)]

    let rndHole msk =
        let rec tryPick m c =
            let hl = rhole()
            if m &&& hl.Mask = 0UL then hl
            elif (c-1) > 0 then tryPick msk (c-1) 
            else raise CantFindHole
        tryPick msk 1000 