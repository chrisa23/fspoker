namespace fspoker.tests

open NUnit.Framework
open FsUnit
open fspoker

[<TestFixture>]
module EquityTests =

    open Cards
    open Holes
    open Boards
    open Ranges
    open Eval
    open Equity

    [<Test;Ignore >]
    let EquityTest1() = 
        
               
        let hls = getHoles [|"AKs";"22";"33";"AQo";|]

        hls |> Array.length |> should equal 4

        let hls2 = getHoles [|"AKs,AQs,AJs";"22,33,44,55,66,77";"33,44,55,66,77,88,99,TT";"AQo";|] 

        let count = 200

        let rl = getXHoles hls2 count

        rl |> Array.length |> should equal 200

        let col = count - Seq.length rl

        let h = 
            [for i in 1..20000 do
                yield pickHoles hls2]

        let eq = getEquities [|"AKs,AQs,AJs";"22,33,44,55,66,77";"33,44,55,66,77,88,99,TT";"AQo";|] 1000 100

        let eq2a = getEquities [|"AAo";"XxXx";|] 1000 1000

      //  let eq1 =  getEquities [|"XxXx";"XxXx";"XxXx";"XxXx";"XxXx";"XxXx";"XxXx";|] 10000 1000
        
        //let eq2 = getEquities [|"ATo";"TTo";|] 10000 100

        //let eq3 = getEquities [|"XxXx";"XxXx";"XxXx";|] 1000 100

        true |> should equal true

        
