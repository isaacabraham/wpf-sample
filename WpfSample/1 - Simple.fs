/// This sample represents a "basic" WPF application using raw event handlers etc. - essentially
/// directly modifying the view.
module Samples.Simple

open FsXaml
open System.Windows

type MainWindow = XAML<"Main.xaml">

let createWindow() =
    let window = MainWindow()
    let mutable value = 0

    let updateValue() = window.TextBlock1.Text <- string value
    
    window.Button1.Click |> Event.add(fun c -> value <- value + 1; updateValue())
    window.Button2.Click |> Event.add(fun c -> value <- value - 1; updateValue())
    window.Reset.Click |> Event.add(fun c -> value <- 0; updateValue())
    updateValue()
    window