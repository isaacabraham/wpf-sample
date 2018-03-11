/// This sample represents a WPF application using an MVVM model via the ViewModule
/// library. Note that the model is still mutable. However, the ViewModelBase library
/// automatically handles INotifyPropertyChanged and ICommand creation.
module Samples.Basics.ViewModule

open FsXaml
open ViewModule
open ViewModule.FSharp
open System.Windows

type MainWindow = XAML< @"Basics/View.xaml">

type ViewModel() as self =
    inherit ViewModelBase()
    let score = self.Factory.Backing(<@ self.Score @>, 0)
    member __.Score with get() = score.Value and set value = score.Value <- value
    member __.Increment = self.Factory.CommandSync(fun _ -> self.Score <- self.Score + 1)
    member __.Decrement = self.Factory.CommandSync(fun _ -> self.Score <- self.Score - 1)
    member __.Reset = self.Factory.CommandSync(fun _ -> self.Score <- 0)

let run() =
    let app = Application()
    let window = MainWindow(DataContext = ViewModel())
    app.Run window