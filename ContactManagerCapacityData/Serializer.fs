namespace ContactManagerCapacity.Data.Serializer

module JsonSerializer =

    open System.Runtime.Serialization
    open System.Runtime.Serialization.Json
    open System.IO
    open System.Xml
    open System.Text

    let encode<'t> (obj : 't) =
        use memoryStream = new MemoryStream()
        (new DataContractJsonSerializer(typeof<'t>)).WriteObject(memoryStream, obj)
        Encoding.Default.GetString(memoryStream.ToArray())

    let decode<'t> (json : string) =
        use memoryStream = new MemoryStream(ASCIIEncoding.Default.GetBytes(json)) 
        let obj = (new DataContractJsonSerializer(typeof<'t>)).ReadObject(memoryStream) 
        obj :?> 't
