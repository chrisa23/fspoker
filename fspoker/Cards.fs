namespace fspoker

module Cards =
    
    //1-52
    //1 = 2c, 2 = 2d, 3 = 2h, 4 = 2s
    type Card = int
    
    let rank c = (c - 1) / 4 
    let suit c = (c - 1) % 4
    let mask c = 1UL <<< c

    let rankToString r = 
        match r with
        | 12 -> "A"
        | 11 -> "K"
        | 10 -> "Q"
        | 9 -> "J"
        | 8 -> "T"
        | a when a >= 0 && a < 8 -> (a + 2).ToString()
        | _ -> "?"

    let suitToString s = 
        match s with
        | 0 -> "c"
        | 1 -> "d"
        | 2 -> "h"
        | 3 -> "s"
        | _ -> "?"

    let toString c = (rankToString (rank c)) + (suitToString (suit c))

    let cardsByString = 
        [|1..52|] 
        |> Array.map (fun c -> toString c, c) 
        |> Map.ofArray
    
    let card s = cardsByString.[s]

    let chkCards a b = mask a &&& mask b = 0UL

