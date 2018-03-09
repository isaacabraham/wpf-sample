open FsXaml
open System
open System.Windows

type MainWindow = XAML<"Main.xaml">

[<STAThread>]
[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    let window = MainWindow()
    
    window.Button1.Click |> Event.add(fun c -> MessageBox.Show "You clicked button 1!" |> ignore)
    window.Button2.Click |> Event.add(fun c -> MessageBox.Show "You clicked button 2!" |> ignore)    
    
    let app = Application()
    app.Run(window)