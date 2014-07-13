namespace fspoker

module Eval =

    open System.IO
    
    let private dataLocation = "D:\HandRanks.dat"

    let hr = 
        [|  use br = new BinaryReader(File.OpenRead(dataLocation))
            for i in 0..32487833 -> br.ReadInt32() |]

    let eval7 a b c d e f g = hr.[hr.[hr.[hr.[hr.[hr.[hr.[53 + a] + b] + c] + d] + e] + f] + g]
    let eval6 a b c d e f  = hr.[hr.[hr.[hr.[hr.[hr.[53 + a] + b] + c] + d] + e] + f] 
    let eval5 a b c d e  = hr.[hr.[hr.[hr.[hr.[53 + a] + b] + c] + d] + e] 

    let handType x = x >>> 12

    let evalFlop b1 b2 b3 (h1, h2) = eval5 b1 b2 b3
    let evalTurn b1 b2 b3 b4 (h1, h2) = eval6 b1 b2 b3 b4
    let evalRiver b1 b2 b3 b4 b5 (h1, h2) = eval7 b1 b2 b3 b4 b5

    let getHoleEval (h1,h2) = eval7 h1 h2
    let getHoleEval6 (h1,h2) = eval6 h1 h2
    let getHoleEval5 (h1,h2) = eval5 h1 h2

        
    type HandType =
        | HighCard = 9
        | OnePair = 8
        | TwoPair = 7
        | ThreeOfAKind = 6
        | Straight = 5 
        | Flush = 4
        | FullHouse = 3
        | FourOfAKind = 2 
        | StraightFlush = 1
    