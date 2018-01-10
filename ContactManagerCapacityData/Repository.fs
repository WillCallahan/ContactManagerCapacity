namespace ContactManagerCapacity.Data.Repository

type ApiRepositoryRecord = { ApiUrl : string }

module ApiRepository = 
    
    open FSharp.Data

    let create (apiUrl : string) = { ApiUrl = apiUrl }

    /// <summary>
    /// Saves an object to the API
    /// </summary>
    /// <param name="apiUrl">Url of the API Endpoint</param>
    /// <param name="body">Request body to send to the server</param>
    /// <returns>Response body from the server</returns>
    let save { ApiUrl = apiUrl } (body : string) =
        Http.RequestString
            (apiUrl,
            httpMethod = "POST",
            headers = ["Content-Type", "application/json"],
            body = HttpRequestBody.TextRequest body)

    /// <summary>
    /// Gets the total number of objects available at an API endpoint
    /// </summary>
    /// <returns>Total number of objects available at endpoint</returns>
    let count { ApiUrl = apiUrl } =
        Http.RequestString
            (apiUrl + "/count",
            httpMethod = "GET")
