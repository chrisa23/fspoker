namespace fspoker

module Equity =

    open System
    open Cards
    open Holes
    open Boards
    open Eval
    open Ranges

    let private r = new Random((int)DateTime.Now.Ticks);
    let rboard() = Boards.allBoards.[r.Next(bd5Lgth)]

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

    let evalHandsSub (hls:Hole[],bd) = 
        Array.map (evalSub hls) bd
        |> Array.fold sumArrays (Array.create hls.Length 0.)

    let rndBoard msk =
        let rec tryPick m c =
            let bd = rboard()
            if m &&& (bd |> snd) = 0UL then bd |> fst
            elif (c-1) > 0 then tryPick msk (c-1) 
            else raise BadRange
        tryPick msk 1000 

    let getRndBoards n msk = [|for p in 1..n -> rndBoard msk|] 

    let runBoards n (msk,holes) = evalHandsSub (holes, getRndBoards n msk)

    let evalHands (ranges:Range[]) t x = 
        let r =
            getXHoles ranges t
            |> Array.map (runBoards x)//Can be parralel, but board choose function messes up with .Net Array.Parallel, but not with Flying Frog dll.
            |> Array.fold sumArrays (Array.create ranges.Length 0.)
        let t = float (Array.sum r)
        Array.map (fun x -> x/t) r

    let getEquities (holes:string[]) (nholes:int) (nboards:int) = evalHands (getHoles holes) nholes nboards

