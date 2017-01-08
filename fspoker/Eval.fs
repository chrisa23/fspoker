namespace fspoker

module Eval =

    open System.IO
    
    //let private dataLocation = "X:\HandRanks.dat"

    let mutable hr = [|0|]
    
    let load location = 
        hr <- [| use br = new BinaryReader(File.OpenRead(location))
                 for i in 0..32487833 -> br.ReadInt32() |]

    let eval7 a b c d e f g = hr.[hr.[hr.[hr.[hr.[hr.[hr.[53 + a] + b] + c] + d] + e] + f] + g]
    let eval6 a b c d e f  =hr.[hr.[hr.[hr.[hr.[hr.[hr.[53 + a] + b] + c] + d] + e] + f]]
    let eval5 a b c d e  = hr.[hr.[hr.[hr.[hr.[hr.[53 + a] + b] + c] + d] + e]]

    
    let handType x = x >>> 12

    let evalFlop b1 b2 b3 (h1, h2) = eval5 b1 b2 b3
    let evalTurn b1 b2 b3 b4 (h1, h2) = eval6 b1 b2 b3 b4
    let evalRiver b1 b2 b3 b4 b5 (h1, h2) = eval7 b1 b2 b3 b4 b5
    
    let eval (b1, b2, b3, b4, b5) (h1, h2) = eval7 b1 b2 b3 b4 b5 h1 h2

    let getHoleEval (h1,h2) = eval7 h1 h2
    let getHoleEval6 (h1,h2) = eval6 h1 h2
    let getHoleEval5 (h1,h2) = eval5 h1 h2

        
    type HandType =
        | HighCard = 1
        | OnePair = 2
        | TwoPair = 3
        | ThreeOfAKind = 4
        | Straight = 5
        | Flush = 6
        | FullHouse = 7
        | FourOfAKind = 8
        | StraightFlush = 9
    