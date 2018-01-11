namespace ContactManagerCapacity.Data.Seeder

type private SeedType = Email | PhoneNumber | FirstName | LastName | Address

module SeederModule =

    open System
    open System.Resources
    open System.Configuration
    open System.Text.RegularExpressions
    open FSharp.Data

    let private CsvFileLocation seedType = 
        match seedType with
            | SeedType.FirstName -> ConfigurationManager.AppSettings.Item "SeedFirstNames"
            | SeedType.LastName -> ConfigurationManager.AppSettings.Item "SeedLastNames"
            | SeedType.Address -> ConfigurationManager.AppSettings.Item "SeedAddresses"
            | SeedType.PhoneNumber -> ConfigurationManager.AppSettings.Item "SeedPhoneNumbers"
            |> ResourceManager("Resources", System.Reflection.Assembly.GetExecutingAssembly()).GetString

    let private getFirstColumnOfCsv seedType =
        CsvFile.Parse(CsvFileLocation seedType, hasHeaders = true).Cache().Rows 
            |> Seq.map (fun row -> row.Columns.[0])
            |> Seq.toList

    let private parseAddress address =
        let matcher = Regex.Match(address, @"^(\d+\w?\s.*?)(?:\s{2,}|\s+[,\""]+)(.*?)-\s(\w{2})\s+(\d+)$")
        match matcher.Success with
            | false -> raise (new ArgumentException("Seeder address information does not match Regex pattern"))
            | true -> [ for i in 1..4 -> matcher.Groups.[i].Value ]


    let FirstName =
        getFirstColumnOfCsv SeedType.FirstName

    let LastName = 
        getFirstColumnOfCsv SeedType.LastName

    let Email =
        List.zip FirstName LastName
            |> List.map (fun (firstName, lastName) -> (firstName + lastName + "@gmail.com"))

    let PhoneNumber =
        getFirstColumnOfCsv SeedType.PhoneNumber

    let Address =
        getFirstColumnOfCsv SeedType.Address
            |> List.map parseAddress
