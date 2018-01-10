namespace ContactManagerCapacity.Data.Models

type Address(street : string, city : string, state : string, zipCode : string) =
    member this.Street = street
    member this.City = city
    member this.State = state
    member this.ZipCode = zipCode

    new() = new Address("", "", "", "")

type Person(firstName : string, lastName : string, email : string, phoneNumber : string, address : Address) =
    member this.FirstName = firstName
    member this.LastName = lastName
    member this.Email = email
    member this.PhoneNumber = phoneNumber
    member this.Address = address

    new() = new Person("", "", "", "", new Address())

module Models =
    
    ()
