/// This sample represents a WPF application using a raw MVVM model, using a manual
/// implementation of INotifyPropertyChanged and ICommand etc., and a mutable view model.

module Samples.ManualMvvm

open FsXaml
open System.ComponentModel
open System.Windows
open System.Windows.Input

type MainWindow = XAML<"Main.xaml">

let private makeCommand onExecute =
    let event = Event<_,_>()
    { new ICommand with
        member __.CanExecute _ = true
        [<CLIEvent>]
        member __.CanExecuteChanged = event.Publish
        member __.Execute _ = onExecute() }

type ViewModel() =
    let notify = Event<_,_>()
    let mutable score = 0
    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member __.PropertyChanged = notify.Publish
    member this.Score
        with get() = score
        and set x =
            score <- x
            notify.Trigger(this, PropertyChangedEventArgs "Score")
    member this.Increment = makeCommand (fun () -> this.Score <- this.Score + 1)
    member this.Decrement = makeCommand (fun () -> this.Score <- this.Score - 1)
    member this.Reset = makeCommand (fun () -> this.Score <- 0)

let run() =
    let app = Application()
    let window = MainWindow(DataContext = ViewModel())
    app.Run window