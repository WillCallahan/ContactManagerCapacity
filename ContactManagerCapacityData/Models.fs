namespace ContactManagerCapacity.Data.Models

open System

type PhoneTypeEnum =
    | Mobile = 0
    | Landing = 1
    | Fax = 2
    | Andriod = 3
    | IPhone = 4

[<Serializable>]
type PhoneType(phoneNumber : string, phoneType : PhoneTypeEnum) =
    member this.PhoneNumber = phoneNumber
    member this.PhoneType = phoneType

    new() = new PhoneType("", PhoneTypeEnum.Landing)

[<Serializable>]
type AddressType(street : string, city : string, state : string, postalCode : string) =
    member this.Street = street
    member this.City = city
    member this.State = state
    member this.PostalCode = postalCode

    new() = new AddressType("", "", "", "")

[<Serializable>]
type PersonType(id : string, firstName : string, lastName : string, primaryEmail : string, secondaryEmailList : string array, phoneList : PhoneType array, addressList : AddressType array) =
    member this.Id = id
    member this.FirstName = firstName
    member this.LastName = lastName
    member this.PrimaryEmail = primaryEmail
    member this.SecondayEmailList = secondaryEmailList
    member this.PhoneList = phoneList
    member this.AddressList = addressList

    new() = new PersonType("", "", "", "", [| |], [| new PhoneType() |], [| new AddressType() |])

module Phone =

    open ContactManagerCapacity.Data.Seeder

    let Seed seed =
        new PhoneType(SeederModule.PhoneNumber.[seed], System.Random().Next(0, 4) |> enum<PhoneTypeEnum>)

module Address =

    open ContactManagerCapacity.Data.Seeder

    let Seed seed =
        let address = SeederModule.Address
        new AddressType(address.[seed].[0], address.[seed].[1], address.[seed].[2], address.[seed].[3])

module Person =

    open ContactManagerCapacity.Data.Seeder
    
    let Seed seed =
        new PersonType(null, SeederModule.FirstName.[seed], SeederModule.LastName.[seed], SeederModule.Email.[seed], [|  |], [| Phone.Seed seed |], [| Address.Seed seed |])
