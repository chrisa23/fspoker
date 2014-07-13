namespace fspoker

module Boards =

    open Cards


    let mask3 a b c = mask a ||| mask b ||| mask c 
    let mask4 a b c d = mask a ||| mask b ||| mask c ||| mask d 
    let mask5 a b c d e = mask a ||| mask b ||| mask c ||| mask d ||| mask e
    let mask (a, b, c, d, e) = mask5 a b c d e 
    

    let allBoards =
        [|for i in [1..48] do
            for j in [i+1..49] do
                for k in [j+1..50] do
                    for l in [k+1..51] do
                        for m in [l+1..52] ->
                           (i, j, k, l, m), (mask5 i j k l m)|]

    let bd5Lgth = allBoards.Length
