namespace fspoker

module Cards =
    
    //0-51 
    //0 = 2c, 1 = 2d, 2 = 2h, 3 = 2s
    type Card = int
    
    let rank c = c / 4 
    let suit c = c % 4
    let mask c = 1UL <<< (c + 1)

    let toString c = 
            
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
    
        (rankToString (rank c)) + (suitToString (suit c))

    let cardsByString = 
        [|0..51|] 
        |> Array.map (fun c -> toString c, c) 
        |> Map.ofArray
    
    let fromString s = cardsByString.[s]

