module Samples.ManualMvvm

open FsXaml
open System.Windows
open System.ComponentModel
open System.Windows.Input
open System

type MainWindow = XAML<"Main.xaml">

type ViewModel() =
    let notify = Event<_,_>()
    let mutable score = 1
    let makeCommand onExecute =
        let event = Event<_,_>()
        { new ICommand with
              member __.CanExecute _ = true
              [<CLIEvent>]
              member __.CanExecuteChanged = event.Publish
              member __.Execute _ = onExecute() }
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

let createWindow() =
    let window = MainWindow()
    window.DataContext <- ViewModel()
    window