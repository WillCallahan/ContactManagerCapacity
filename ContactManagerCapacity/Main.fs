namespace ContactManagerCapacity.Main

module internal Main =

    open System
    open ContactManagerCapacity.Data.Repository.ApiRepository
    open ContactManagerCapacity.Data.Seeder
    open ContactManagerCapacity.Data.Models
    open ContactManagerCapacity.Data.Serializer
    open ContactManagerCapacity.Main.SNS

    /// <summary>
    /// Total number of Contacts to Add
    /// Should not exceed 1000
    /// </summary>
    [<Literal>]
    let ContactsToAdd = 1000

    /// <summary>
    /// Interval of added contacts at which to publish a message to a topic
    /// </summary>
    [<Literal>]
    let CapacityNotificationInterval = 100

    /// <summary>
    /// Adds a single contact to the API
    /// </summary>
    /// <param name="total">Total number of contacts to add</param>
    /// <param name="notifyAt">Interval at which to notify a user of contacts</param>
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

    /// <summary>
    /// Asynchronously adds <see cref="ContactsToAdd"/> contacts
    /// </summary>
    /// <param name="total">Total number of contacts to add</param>
    /// <param name="notifyAt">Interval at which to notify a user of contacts</param>
    let addContacts (total : int, notifyAt : int) =
        printfn "Adding %d contacts\r\n%0*d" total 80 0
        seq [ for i in 1..total - 1 -> async { addContact(i, notifyAt) |> ignore }; ]
            |> Async.Parallel
            |> Async.RunSynchronously
            |> ignore
        printfn "%0*d\r\nAdded all contacts" 80 0

    [<EntryPoint>]
    let main argv = 
        printfn "Contact Capacity Program\r\n"
        addContacts(ContactsToAdd, CapacityNotificationInterval) |> ignore
        printfn "\r\nPress any key to exit..."
        Console.ReadKey() |> ignore
        0
