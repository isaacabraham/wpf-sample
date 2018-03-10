open System

[<STAThread; EntryPoint>]
let main _ = 
    Samples.Simple.run()
    //Samples.ManualMvvm.run()
    //Samples.ViewModule.run()
    //Samples.Elmish.run()