namespace ContactManagerCapacity.Main

module Main =

    open System
    open ContactManagerCapacity.Data.Repository
    open ContactManagerCapacity.Data.Seeder

    let addContacts (total : int, notificationCount : int) =
        let contacts = []
        [1..total] |> List.map (fun i -> printfn "%i")

    [<EntryPoint>]
    let main argv = 
        printfn "Hello world"
        //ApiRepository.count |> ignore
        addContacts(100, 10) |> ignore
        SeederModule.FirstName |> ignore
        Console.ReadKey() |> ignore
        0
