namespace fspoker.tests

open NUnit.Framework
open FsUnit
open fspoker

[<TestFixture>]
module HoleTests =
    
    open Holes
    
    [<Test>] 
    let RandomTests() = 
        let h = Holes.create 1 2
        h.Cards |> should equal (1, 2)
        h.Cards |> toString |> printfn "%A"
        "AdAs" |> hole |> cards |> hiRank |> should equal 12
        "AsAd" |> hole |> cards |> hiRank |> should equal 12
        "AdKd" |> hole |> cards |> hiRank |> should equal 12
        "AdKd" |> hole |> cards |> suited |> should equal true
        "AAo" |> holes |> Array.length |> should equal 6
    
    [<Test>] 
    let RandomHole1() = 
        let h = Holes.rndHole 0UL
        h