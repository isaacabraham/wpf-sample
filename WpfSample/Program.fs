open System
open System.Windows

[<STAThread>]
[<EntryPoint>]
let main _ = 
    let app = Application()
    Samples.ViewModule.createWindow() |> app.Run