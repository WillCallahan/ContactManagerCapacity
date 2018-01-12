namespace ContactManagerCapacity.Main.Credentials

/// <summary>
/// Provides credentials for accessing AWS resources
/// </summary>
module CredentialProvider =

    open System
    open System.Configuration
    open Amazon.Runtime.CredentialManagement

    let private AwsProfileName : string =
        ConfigurationManager.AppSettings.Item "AWSProfileName"

    let private AwsProfileLocation : string =
        ConfigurationManager.AppSettings.Item "AWSProfileLocation"

    let AwsCredentials =
        let success, awsCredentials = (new CredentialProfileStoreChain(AwsProfileLocation)).TryGetAWSCredentials AwsProfileName
        match success with
            | true -> awsCredentials
            | false -> raise (new InvalidOperationException("Cannot get AWS credentials"))
