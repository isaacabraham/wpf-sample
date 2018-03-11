/// This sample represents a WPF application using the Elmish model. The Elmish.WPF
/// library handles all mutation for us; we simply implement the update function, which
/// is completely immutable. The view function essentially ties up WPF bindings to Elmish
/// commands etc.
module Samples.Collections.Elmish

open Elmish
open Elmish.WPF
open FsXaml

type MainWindow = XAML< @"Collections/View.xaml">
type CustomerId = CustomerId of int override this.ToString() = match this with (CustomerId x) -> string x
type Customer = { CustomerId : CustomerId; Name : string; Balance : decimal; Country : string }
type NewCustomerRequest = { Name : string; Balance : decimal; Country : string }
type Msg = Add of NewCustomerRequest | Remove of Customer | Select of Customer | UpdateNewCustomerRequest of NewCustomerRequest
type Model =
    { Customers : Customer list
      SelectedCustomer : Customer option
      NewCustomerRequest : NewCustomerRequest
      Countries : string list
      NextId : CustomerId }
let init _ =
    { Customers =
        [ { CustomerId = CustomerId 1; Name = "Joe Bloggs"; Balance = 10M; Country = "GB" }
          { CustomerId = CustomerId 2; Name = "Sally Smith"; Balance = 130M; Country = "US" }
          { CustomerId = CustomerId 3; Name = "Tim Jones"; Balance = -40M; Country = "DE" } ]
      SelectedCustomer = None
      Countries = [ "US"; "DE"; "GB"; "CN"; "FR" ]
      NewCustomerRequest = { Name = ""; Balance = 0M; Country = "" }
      NextId = CustomerId 4 }
let update message model =
    match message with
    | Some (Select c) -> { model with SelectedCustomer = Some c }
    | Some (Remove c) -> { model with Customers = model.Customers |> List.filter ((<>) c) }
    | Some (UpdateNewCustomerRequest c) -> { model with NewCustomerRequest = c }
    | Some (Add c) ->
        { model with 
            Customers = model.Customers @ [ { Name = c.Name; Country = c.Country; Balance = c.Balance; CustomerId = model.NextId } ]
            NextId =
                let (CustomerId currentId) = model.NextId
                CustomerId (currentId + 1)
            NewCustomerRequest = { Name = ""; Balance = 0M; Country = "" } }
    | None -> model

let validateRequest request =
    if System.String.IsNullOrWhiteSpace request.Name then false
    elif System.String.IsNullOrWhiteSpace request.Country then false
    elif request.Balance < 0M then false
    else true

let view _ _ =
    [ "Customers" |> Binding.oneWay (fun m -> m.Customers)
      "SelectedCustomer"
        |> Binding.twoWay
            (fun m -> m.SelectedCustomer |> Option.map box |> Option.toObj)
            (fun c _ ->
                match c with
                | :? Customer as c -> Some(Select c)
                | _ -> None)
      "Remove" |> Binding.cmd(fun _ m -> m.SelectedCustomer |> Option.map Remove ) 
      "Countries" |> Binding.oneWay(fun m -> m.Countries)
      "NewCustomerRequestName" |> Binding.twoWay (fun m -> m.NewCustomerRequest.Name) (fun name m -> Some (UpdateNewCustomerRequest { m.NewCustomerRequest with Name = name }))
      "NewCustomerRequestBalance"
        |> Binding.twoWayValidation
            (fun m -> string m.NewCustomerRequest.Balance)
            (fun balance m ->
                match System.Decimal.TryParse balance with
                | true, balance -> Some (UpdateNewCustomerRequest { m.NewCustomerRequest with Balance = decimal balance }) |> Ok
                | false, _ -> Error "Balance must be a valid decimal.")
      "NewCustomerRequestCountry" |> Binding.twoWay (fun m -> m.NewCustomerRequest.Country) (fun country m -> Some (UpdateNewCustomerRequest { m.NewCustomerRequest with Country = country }))
      "Add" |> Binding.cmdIf(fun _ m -> Some(Add m.NewCustomerRequest)) (fun _ m -> validateRequest m.NewCustomerRequest)
    ]
let run() =
    Program.mkSimple init update view
    |> Program.runWindow (MainWindow())