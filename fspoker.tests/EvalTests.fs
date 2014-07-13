namespace fspoker.tests

open NUnit.Framework
open FsUnit
open fspoker

[<TestFixture>]
module EvalTests =
    
    open Cards
    open Eval   

    //run manually since opening Eval is time consuming
    [<Test; Ignore>] 
    let HandTypeTests() = 

        let h = eval7 1 5 9 13 17 19 20 
        let ht = handType h
        ht |> should equal 9
        
        let h = eval7 1 2 3 4 16 18 19 
        let ht = handType h
        ht |> should equal 8
        
        let h = eval5 1 2 3 4 16 
        let ht = handType h
        ht |> should equal 8
       