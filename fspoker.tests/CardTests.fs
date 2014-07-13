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
       2 |> toString |> should equal "2d"
       3 |> toString |> should equal "2h"
       "2c" |> card |> rank |> should equal 0
       "2d" |> card |> rank |> should equal 0
       "2s" |> card |> rank |> should equal 0
       "2h" |> card |> rank |> should equal 0
       "Ac" |> card |> rank |> should equal 12
       "Td" |> card |> rank |> should equal 8

       "2c" |> card |> suit |> should equal 0
       "2d" |> card |> suit |> should equal 1
       "2h" |> card |> suit |> should equal 2
       "2s" |> card |> suit |> should equal 3
       

        