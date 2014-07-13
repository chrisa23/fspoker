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
        h |> should equal (1, 2)
        h|> toString |> printfn "%A"
        "AdAs" |> hole |> hiRank |> should equal 12
        "AsAd" |> hole |> hiRank |> should equal 12
        "AdKd" |> hole |> hiRank |> should equal 12
        "AdKd" |> hole |> suited |> should equal true
        "AAo" |> holes |> Array.length |> should equal 6