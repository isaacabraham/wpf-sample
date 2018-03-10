open System
open System.Windows

[<STAThread>]
[<EntryPoint>]
let main _ = 
    let app = Application()
    Samples.ManualMvvm.createWindow() |> app.Run