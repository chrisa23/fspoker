namespace fspoker.tests

open NUnit.Framework
open FsUnit
open fspoker

[<TestFixture>]
module CardTests =
    
    open Cards
    
    [<Test>] 
    let RandomTests() = 
       1 |> toString |> should equal "2c"
       "2c" |> fromString |> rank |> should equal 0
       "2d" |> fromString |> rank |> should equal 0
       "2s" |> fromString |> rank |> should equal 0
       "2h" |> fromString |> rank |> should equal 0
       "Ac" |> fromString |> rank |> should equal 12
       "Td" |> fromString |> rank |> should equal 8
       

[<TestFixture>]
module EvalTests =
    
    open Cards
    open Eval   

    //run manually since opening Eval is time consuming
    [<Test; Ignore>] 
    let HandTypeTests() = 
        let h = eval7 1 5 9 13 17 19 20 
        let ht = handType h
        printf "%A %A\n" h ht
        ht |> should equal 9
        let h = eval7 1 2 3 4 16 18 19 
        let ht = handType h
        printf "%A %A\n" h ht
        ht |> should equal 8
        let h = eval5 1 2 3 4 16 
        let ht = handType h
        printf "%A %A\n" h ht
        ht |> should equal 8
       
        