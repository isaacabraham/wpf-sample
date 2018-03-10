/// This sample represents a "basic" WPF application using raw event handlers etc. - essentially
/// directly modifying the view.
module Samples.Simple

open FsXaml
open System.Windows

type MainWindow = XAML<"Main.xaml">

let run() =
    let window = MainWindow()

    let setScore v = window.TextBlock1.Text <- string v
    
    window.Button1.Click |> Event.add(fun _ -> setScore ((int window.TextBlock1.Text) + 1))
    window.Button2.Click |> Event.add(fun _ -> setScore ((int window.TextBlock1.Text) - 1))
    window.Reset.Click |> Event.add(fun _ -> setScore 0)
    setScore 0
    let app = Application()
    app.Run window