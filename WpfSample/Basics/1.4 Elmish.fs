/// This sample represents a WPF application using the Elmish model. The Elmish.WPF
/// library handles all mutation for us; we simply implement the update function, which
/// is completely immutable. The view function essentially ties up WPF bindings to Elmish
/// commands etc.
module Samples.Basics.Elmish

open Elmish
open Elmish.WPF
open FsXaml

type MainWindow = XAML< @"Basics/View.xaml">
type Model = { Score : int }
type Msg = Increment | Decrement | Reset

let init _ = { Score = 0 }
let update message model =
    match message with
    | Increment -> { Score = model.Score + 1 }
    | Decrement -> { Score = model.Score - 1 }
    | Reset -> { Score = 0 }
let view _ _ =
    [ "Increment" |> Binding.cmd (fun _ _ -> Increment)
      "Decrement" |> Binding.cmd (fun _ _ -> Decrement)
      "Reset" |> Binding.cmd (fun _ _ -> Reset)
      "Score" |> Binding.oneWay (fun m -> m.Score) ]

let run() =
    Program.mkSimple init update view
    |> Program.runWindow (MainWindow())