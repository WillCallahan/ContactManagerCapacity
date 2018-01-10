namespace ContactManagerCapacity.Data.Seeder

type private SeedType = Email | PhoneNumber | FirstName | LastName | Address

module SeederModule =

    open System.Resources
    open System.Configuration
    open FSharp.Data

    let private CsvFileLocation seedType = 
        match seedType with
            | SeedType.FirstName -> ConfigurationManager.AppSettings.Item "SeedFirstNames"
            | SeedType.LastName -> ConfigurationManager.AppSettings.Item "SeedLastNames"
            | SeedType.Address -> ConfigurationManager.AppSettings.Item "SeedAddresses"
            |> ResourceManager("Resources", System.Reflection.Assembly.GetExecutingAssembly()).GetString

    let private getFirstColumnOfCsv seedType =
        printfn "%s" (CsvFileLocation seedType)
        CsvFile.Parse(CsvFileLocation seedType, hasHeaders = true).Rows 
            |> Seq.map (fun row -> (row.GetColumn 0))
            |> Seq.toList

    let FirstName =
        getFirstColumnOfCsv SeedType.FirstName

    let LastName = 
        getFirstColumnOfCsv SeedType.LastName

    let Email =
        List.zip FirstName LastName
            |> List.map (fun (firstName, lastName) -> firstName + lastName + "@gmail.com")

    let PhoneNumber =
        getFirstColumnOfCsv SeedType.PhoneNumber

    let Address =
        getFirstColumnOfCsv SeedType.Address
