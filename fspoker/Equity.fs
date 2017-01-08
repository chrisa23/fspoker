namespace fspoker

module Equity =

    open System
    open Cards
    open Holes
    open Boards
    open Eval
    open Ranges

    //let private r = new Random((int)DateTime.Now.Ticks);
    let rboard (ri: int -> int) = Boards.allBoards.[ri bd5Lgth]

    let getEmptyResult n = [for i in 1..n -> 0.]
   
    let sumArrays a b = Array.map2 (+) a b

    let getWins vals =
        let max = Array.max vals
        let wins = Array.map (fun x -> if x = max then 1. else 0.) vals
        let winCt = Array.fold (+) 0. wins
        Array.map (fun x -> x/winCt) wins

    let evalSub (hls:Hole[]) b =
        let ev h = eval b h.Cards
        Array.map ev hls |> getWins

    let evalHandsSub (hls:Hole[]) bd = 
        Array.map (evalSub hls) bd
        |> Array.fold sumArrays (Array.create hls.Length 0.)

    let rndBoard msk (ri: int -> int) =
        let rec tryPick m c =
            let bd = rboard ri
            if m &&& (bd |> snd) = 0UL then bd |> fst
            elif (c-1) > 0 then tryPick msk (c-1) 
            else raise BadRange
        tryPick msk 1000 

    let getRndBoards n msk (ri: int -> int) = [|for p in 1..n -> rndBoard msk ri|] 

    let runBoards n (ri: int -> int) (msk,holes)  = evalHandsSub holes (getRndBoards n msk ri)

    let evalHands2 (ri: int -> int) x mask (ranges:Hole[]) = 
        //let mask = ranges |> Array.fold (fun x y -> x &&& y.Mask) 0UL
        let r =
            (mask, ranges)
            |> (runBoards x ri)//Can be parralel, but board choose function messes up with .Net Array.Parallel, but not with Flying Frog dll.
            //|> Array.fold sumArrays (Array.create ranges.Length 0.)
        let t = float (Array.sum r)
        Array.map (fun x -> x/t) r

    let evalHands (ranges:Range[]) t x (ri: int -> int) = 
        let r =
            getXHoles ranges t ri
            |> Array.map (runBoards x ri)//Can be parralel, but board choose function messes up with .Net Array.Parallel, but not with Flying Frog dll.
            |> Array.fold sumArrays (Array.create ranges.Length 0.)
        let t = float (Array.sum r)
        Array.map (fun x -> x/t) r

    let getEquities (holes:string[]) (nholes:int) (nboards:int) = evalHands (getHoles holes) nholes nboards

