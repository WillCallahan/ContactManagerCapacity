namespace ContactManagerCapacity.Data.Models

type AddressType(street : string, city : string, state : string, zipCode : string) =
    member this.Street = street
    member this.City = city
    member this.State = state
    member this.ZipCode = zipCode

    new() = new AddressType("", "", "", "")

type PersonType(firstName : string, lastName : string, email : string, phoneNumber : string, addresses : List<AddressType>) =
    member this.FirstName = firstName
    member this.LastName = lastName
    member this.Email = email
    member this.PhoneNumber = phoneNumber
    member this.Addresses = addresses

    new() = new PersonType("", "", "", "", [ new AddressType() ])

module Address =

    open ContactManagerCapacity.Data.Seeder

    let Seed seed =
        let address = SeederModule.Address
        new AddressType(address.[seed].[0], address.[seed].[1], address.[seed].[2], address.[seed].[3])

module Person =

    open ContactManagerCapacity.Data.Seeder
    
    let Seed seed =
        new PersonType(SeederModule.FirstName.[seed], SeederModule.LastName.[seed], SeederModule.Email.[seed], SeederModule.PhoneNumber.[seed], [ Address.Seed seed ])
