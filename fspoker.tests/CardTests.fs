namespace fspoker.tests

open NUnit.Framework
open FsUnit
open fspoker

[<TestFixture>]
module CardTests =
    
    open Cards

    [<Test>] 
    let RandomTests() = 
       0 |> toString |> should equal "2c"
       "2c" |> fromString |> rank |> should equal 0
       "2d" |> fromString |> rank |> should equal 0
       "2s" |> fromString |> rank |> should equal 0
       "2h" |> fromString |> rank |> should equal 0
       "Ac" |> fromString |> rank |> should equal 12
       "Td" |> fromString |> rank |> should equal 8
       
     


        