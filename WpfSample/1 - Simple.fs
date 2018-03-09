/// This sample represents a "basic" WPF application using raw event handlers etc. - essentially
/// directly modifying the view.
module Samples.Simple

open FsXaml
open System.Windows

type MainWindow = XAML<"Main.xaml">

let createWindow() =
    let window = MainWindow()
    
    window.Button1.Click
    |> Event.add(fun c ->
        MessageBox.Show "You clicked button 1!" |> ignore
        window.TextBox1.Text <- "Button 1")

    window.Button2.Click
    |> Event.add(fun c ->
        sprintf "You clicked button 2! Text is '%s'." window.TextBox1.Text
        |> MessageBox.Show 
        |> ignore)

    window