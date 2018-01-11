namespace ContactManagerCapacity.Main

module internal Main =

    open System
    open ContactManagerCapacity.Data.Repository.ApiRepository
    open ContactManagerCapacity.Data.Seeder
    open ContactManagerCapacity.Data.Models
    open ContactManagerCapacity.Data.Serializer
    open ContactManagerCapacity.Main.SNS

    let personApiRepository = new PersonApiRepository()

    let addContact (i : int, notifyAt : int) =
        personApiRepository.save (JsonSerializer.encode (Person.Seed i)) |> printfn "Added person %s"
        match i with
            | i when i <> 0 && i % notifyAt = 0 -> (SNSNotificationService.notify personApiRepository.findAll |> ignore)
        ()

    let addContacts (total : int, notifyAt : int) =
        printfn "Adding %d contacts" total
        [1..total] |> List.iter (fun i -> addContact(i, notifyAt))
        printfn "Added all contacts"

    [<EntryPoint>]
    let main argv = 
        printfn "Contact Capacity Program"
        addContacts(10, 1) |> ignore
        Console.ReadKey() |> ignore
        0
