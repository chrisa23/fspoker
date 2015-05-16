module EquityGrid

open System.Windows
open System.Windows.Threading
open System.Threading
open System.Windows.Media
open System.IO
open fspoker
open Cards
open Holes
open Equity

type Board() as grid =
    inherit Controls.Grid(Margin=Thickness 1.0)
    let chk = new Controls.CheckBox()
    let button =
        Array2D.init 13 13 (fun _ _ ->  Controls.Button(FontSize = 14.))
    let fill hls = 
        hole169IndexMap
        |> Seq.iter (fun a -> 
            let y = a.Value%13
            let x = a.Value/13
            let hls = Array.append [|a.Key;|] hls
            let v = (getEquities hls 1000 10).[0]
            if chk.IsChecked.HasValue && chk.IsChecked.Value then
                button.[x,y].Content <- a.Key + "\n" +  ((1./v) - 1.).ToString("f2") + ":1"
            else
                button.[x,y].Content <- a.Key + "\n" +  (v*100.).ToString("f1") 
            let mutable c = Colors.Blue
            c.ScA <- float32 v
            button.[x,y].Background <- new SolidColorBrush(c))

    let clear() =
        hole169IndexMap
        |> Seq.iter (fun a -> 
            let y = a.Value%13
            let x = a.Value/13
            button.[x,y].Content <- a.Key)
                
    do
        for i in 0..12 do
            let column = Controls.ColumnDefinition()
            column.Width <- GridLength 50.0
            grid.ColumnDefinitions.Add column
            let row = Controls.RowDefinition()
            row.Height <- GridLength 50.0
            grid.RowDefinitions.Add row
            for j in 0..12 do
                let button = button.[i, j]
                Controls.Grid.SetRow(button, i)
                Controls.Grid.SetColumn(button, j)
                grid.Children.Add button |> ignore
        let row = Controls.RowDefinition()
        row.Height <- GridLength 20.0
        grid.RowDefinitions.Add row
        let lbl = Controls.TextBlock()
        let txt = Controls.TextBox()
        let bn = Controls.Button()
        Controls.Grid.SetColumnSpan(lbl, 5)
        Controls.Grid.SetColumnSpan(txt, 6)
        Controls.Grid.SetRow(chk,13)
        Controls.Grid.SetRow(lbl,13)
        Controls.Grid.SetRow(txt,13)
        Controls.Grid.SetRow(bn,13)
        Controls.Grid.SetColumn(chk,0)
        Controls.Grid.SetColumn(lbl,1)
        Controls.Grid.SetColumn(txt,6)
        Controls.Grid.SetColumn(bn,12)
        bn.Click.Add(fun _ -> 
            bn.IsEnabled <- false
            clear()
            lbl.Text <- txt.Text
            try fill (txt.Text.Split(' ')) 
            with _ -> txt.Text <- txt.Text + " -- ERROR"
            bn.IsEnabled <- true)
        grid.Children.Add txt|> ignore
        grid.Children.Add lbl |> ignore
        grid.Children.Add bn|> ignore
        grid.Children.Add chk |> ignore
        clear() |> ignore        

    member grid.Item
        with set (i, j) (x:string) = button.[i, j].Content <- x

[<System.STAThread>]
  do
    Window(Title="EquityGrid",
        Content=Controls.Viewbox(Child=Board()),
        Width=700.,
        Height=750.,
        Background=new SolidColorBrush(Colors.LightGray))
    |> Application().Run
    |> ignore

