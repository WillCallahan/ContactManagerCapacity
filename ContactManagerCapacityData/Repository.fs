namespace ContactManagerCapacity.Data.Repository

module ApiRepository = 

    open System.Configuration
    open FSharp.Data
    open ContactManagerCapacity.Data.Models
    open ContactManagerCapacity.Data.Serializer

    /// <summary>
    /// Provides standard API repository methods
    /// </summary>
    [<AbstractClass>]
    type ApiRepository<'t>(endPointUrl : string) =
        member this.BaseUrl = ConfigurationManager.AppSettings.Item "ApiBaseUrl"
        member this.EndPointUrl = endPointUrl
        member this.Url = this.BaseUrl.Trim('/') + "/" + this.EndPointUrl.Trim('/')

        /// <summary>
        /// Gets the total number of objects available at an API endpoint
        /// </summary>
        /// <returns>Total number of objects available at endpoint</returns>
        member this.count =
            Http.RequestString(
                this.Url + "/count",
                httpMethod = "GET"
            ) |> int
        
        /// <summary>
        /// Gets all the available objects from the API endpoint
        /// </summary>
        /// <returns>All available objects from the API endpoint</returns>
        member this.findAll : 't array =
            Http.RequestString(
                this.Url,
                headers = ["Accept", "application/json"],
                httpMethod = "GET"
            ) |> JsonSerializer.decode
        
        /// <summary>
        /// Gets an object from the API by its ID
        /// </summary>
        /// <param name="id">Id of the object</param>
        /// <returns>Object with a matching ID</returns>
        member this.findSomething id : 't =
            Http.RequestString(
                this.Url + "/" + (string id),
                headers = ["Accept", "application/json"],
                httpMethod = "GET"
            ) |> JsonSerializer.decode

        /// <summary>
        /// Saves an object to the API
        /// </summary>
        /// <param name="apiUrl">Url of the API Endpoint</param>
        /// <param name="body">Request body to send to the server</param>
        /// <returns>Response body from the server</returns>
        member this.save (body : 't) : 't =
            Http.RequestString(
                this.Url,
                httpMethod = "POST",
                headers = [
                    "Content-Type", "application/json";
                    "Accept", "application/json"
                ],
                body = (body |> JsonSerializer.encode |> HttpRequestBody.TextRequest)
            ) |> JsonSerializer.decode

        /// <summary>
        /// Deletes an object by its Id
        /// </summary>
        /// <param name="id">ID of the object to delete</param>
        member this.delete id =
            Http.RequestString(
                this.Url + "/" + (string id),
                httpMethod = "DELETE",
                headers = [
                    "Accept", "application/json"
                ]
            ) |> ignore

    /// <summary>
    /// Repository for accessing a <seealso cref="PersonType"/>
    /// </summary>
    type PersonApiRepository() =
        inherit ApiRepository<PersonType>("/persons")
