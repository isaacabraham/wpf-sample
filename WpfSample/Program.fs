open System

[<STAThread; EntryPoint>]
let main _ = 
    Samples.Basics.Simple.run()
    //Samples.Basics.ManualMvvm.run()
    //Samples.ViewModule.run()
    //Samples.Elmish.run()

    //Samples.Collections.Elmish.run()