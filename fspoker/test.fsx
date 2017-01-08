open System
#load "Cards.fs"
open fspoker
open Cards
#load "Holes.fs"
open Holes

#load "Boards.fs"
open Boards

#load "Eval.fs"
open Eval

#load "Ranges.fs"
open Ranges

#load "Equity.fs"
open Equity

let r = new Random((int)DateTime.Now.Ticks)
let ri i = r.Next(i)

let hls = getHoles [|"AKs";"22";"33";"AQo";|]


let hls2 = getHoles [|"AKs,AQs,AJs";"22,33,44,55,66,77";"33,44,55,66,77,88,99,TT";"AQo";|] 

let count = 200

let rl = getXHoles hls2 count ri

let col = count - Seq.length rl

#time
let h = 
    [for i in 1..20000 do
        yield pickHoles hls2 ri]



#time
let eq = getEquities [|"AKs,AQs,AJs";"22,33,44,55,66,77";"33,44,55,66,77,88,99,TT";"AQo";|] 1000 100 ri
#time

#time
let eq2a = getEquities [|"AAo";"XxXx";|] 1000 1000 ri
#time


#time
let eq1 =  getEquities [|"XxXx";"XxXx";"XxXx";"XxXx";"XxXx";"XxXx";"XxXx";|] 10000 1000 ri
#time


#time
let eq2 = getEquities [|"ATo";"TTo";|] 10000 100 ri
#time

#time
let eq3 = getEquities [|"XxXx";"XxXx";"XxXx";|] 1000 100 ri
#time
//
//hole169Map
//|> Seq.iter (fun  a -> 
//    let eq = getEquities [|a.Key;"XxXx";|] 1000 10
//    printfn "%A [%A]" a.Key eq)
//
//let runAA = getEquities [|"AK";"XxXx";|]
//
//let sd =
//    [for n in 1..2 do
//        for m in 1..2 -> 
//            n,m,[for o in 1..10 -> 
//                    let x = pown 10 n
//                    let y = pown 10 m
//                    (runAA (100*x) y).[0]]
//                |> List.toSeq
//                |> Statistics.standard_deviation]

//val runAA : (int -> int -> float list)
//val sd : (int * int * float) list =
//  [(1, 1, 0.005379838907); (1, 2, 0.002523929091); (1, 3, 0.002920240314);
//   (2, 1, 0.001432430336); (2, 2, 0.0005958425897); (2, 3, 0.0004535747411);
//   (3, 1, 0.0003045001551); (3, 2, 0.0001777773767); (3, 3, 0.0002357491853)]


