module Samples.ViewModule

open FsXaml
open ViewModule
open ViewModule.FSharp

type MainWindow = XAML<"Main.xaml">

type ViewModel() as self =
    inherit ViewModelBase()
    let score = self.Factory.Backing(<@ self.Score @>, 0)
    member __.Score with get() = score.Value and set value = score.Value <- value
    member __.Increment = self.Factory.CommandSync(fun _ -> self.Score <- self.Score + 1)
    member __.Decrement = self.Factory.CommandSync(fun _ -> self.Score <- self.Score - 1)
    member __.Reset = self.Factory.CommandSync(fun _ -> self.Score <- 0)

let createWindow() =
    let window = MainWindow()
    window.DataContext <- ViewModel()
    window