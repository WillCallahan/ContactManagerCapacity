namespace ContactManagerCapacity.Data.Repository

module ApiRepository = 

    open System.Configuration
    open FSharp.Data

    [<AbstractClass>]
    type ApiRepository(endPointUrl : string) =
        member this.BaseUrl = ConfigurationManager.AppSettings.Item "ApiBaseUrl"
        member this.EndPointUrl = endPointUrl
        member this.Url = this.BaseUrl.Trim('/') + "/" + this.EndPointUrl.Trim('/')

        /// <summary>
        /// Gets the total number of objects available at an API endpoint
        /// </summary>
        /// <returns>Total number of objects available at endpoint</returns>
        member this.count =
            Http.RequestString
                (this.Url + "/count",
                httpMethod = "GET")
        
        /// <summary>
        /// Gets all the available objects from the API endpoint
        /// </summary>
        /// <returns>All available objects from the API endpoint</returns>
        member this.findAll =
            Http.RequestString
                (this.Url,
                httpMethod = "GET")

        /// <summary>
        /// Saves an object to the API
        /// </summary>
        /// <param name="apiUrl">Url of the API Endpoint</param>
        /// <param name="body">Request body to send to the server</param>
        /// <returns>Response body from the server</returns>
        member this.save (body : string) =
            Http.RequestString
                (this.Url,
                httpMethod = "POST",
                headers = ["Content-Type", "application/json"],
                body = HttpRequestBody.TextRequest body)

    type PersonApiRepository() =
        inherit ApiRepository("/persons")

    
