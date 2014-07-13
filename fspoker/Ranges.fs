namespace fspoker

module Ranges =

    open Cards
    open Holes
    open System

    //TODO:  improve random
    let private r = new Random((int)DateTime.Now.Ticks);

    type range = 
        | Hole of Hole 
        | Range of Hole list
        static member (+) (a,b) =
            match a,b with
            | Hole a, Hole b -> Range [a;b;]
            | Range a, Hole b -> Range (b::a)
            | Hole a, Range b -> Range (a::b)
            | Range a, Range b -> Range (a @ b)

    exception BadRange

    let holeCheck msk msk2 = 
        match msk with 
        | None -> None
        | Some m -> if (m &&& msk2) = 0UL then Some (m ||| msk2) else None

    let checkHoles hls = 
        Array.map mask hls 
        |> Array.fold holeCheck (Some 0UL)
        |> (fun x -> 
                match x with 
                | None -> None
                | Some m -> Some (m,hls))

    let pickHole hls = 
        match hls with
        | Hole x -> x
        | Range h -> List.nth h (r.Next(h.Length))

    let pickHoles (ranges:range[]) =  
        let rec tryPick h c=
            let r = checkHoles (Array.map pickHole h)
            match r with
            | None -> if (c-1) > 0 then tryPick h (c-1) else raise BadRange
            | Some (m,h) -> (m,h)
        tryPick ranges 100

    let getXHoles (ranges:range[]) x = seq{for i in 1..x -> pickHoles ranges}

    let parseRange hl =
        match hl with
        | "XxXx" -> Range (Holes.allHoles |> Array.toList)
       //TODO | a when a.Contains("+") ->
       //TODO | a when a.Contains("-") ->
        | _ ->
            match hl.Length with
            | 4 -> Hole(hole hl)
            | 3 -> Range(holes hl |> Array.toList)
            | 2 -> 
                if hl.[0] = hl.[1] then Range(holes(hl + "o") |> Array.toList)
                else Range ((holes(hl + "o") |> Array.toList ) @ (holes(hl + "s") |> Array.toList))
            | _ -> raise BadRange
        
    let getRange (hl:string) = 
        hl.Split(',')
        |> Array.map parseRange 
        |> Array.fold (+) (Range([]))

    let getHoles (holes:string[]) = Array.map getRange holes