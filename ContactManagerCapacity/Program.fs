namespace ContactManagerCapacity.Main

module internal Main =

    open System
    open ContactManagerCapacity.Data.Repository.ApiRepository
    open ContactManagerCapacity.Data.Seeder
    open ContactManagerCapacity.Data.Models
    open ContactManagerCapacity.Data.Serializer
    open ContactManagerCapacity.Main.SNS

    [<Literal>]
    let ContactsToAdd = 1000

    [<Literal>]
    let CapacityNotificationLimit = 100

    let addContact (i : int, notifyAt : int) =
        let personApiRepository = new PersonApiRepository()
        i |> Person.Seed |> personApiRepository.save |> JsonSerializer.encode |> printfn "Added person %d \r\n\r\n\t%s\r\n" i
        match i with
            | i when i <> 0 && i % notifyAt = 0 -> (personApiRepository.findAll
                                                        |> JsonSerializer.encode
                                                        |> SNSNotificationService.notify 
                                                        |> ignore)
            | _ -> ()
        ()

    let addContacts (total : int, notifyAt : int) =
        printfn "Adding %d contacts\r\n%0*d" total 80 0
        seq [ for i in 1..total -> async { addContact(i, notifyAt) |> ignore }; ]
            |> Async.Parallel
            |> Async.RunSynchronously
            |> ignore
        printfn "%0*d\r\nAdded all contacts" 80 0

    [<EntryPoint>]
    let main argv = 
        printfn "Contact Capacity Program\r\n"
        addContacts(ContactsToAdd, CapacityNotificationLimit) |> ignore
        printfn "\r\nPress any key to exit..."
        Console.ReadKey() |> ignore
        0
