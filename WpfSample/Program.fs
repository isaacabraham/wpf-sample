open System
open System.Windows

[<STAThread>]
[<EntryPoint>]
let main _ = 
    let app = Application()
    Samples.Simple.createWindow() |> app.Run