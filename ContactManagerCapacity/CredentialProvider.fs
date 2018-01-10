namespace ContactManagerCapacity.Main.Credentials

module CredentialProvider =

    open System
    open System.Configuration
    open Amazon.Runtime.CredentialManagement

    let private AwsProfileName : string =
        ConfigurationManager.AppSettings.Item "AWSSNSTopicArn"

    let AwsCredentials =
        let success, awsCredentials = (new CredentialProfileStoreChain(AwsProfileName)).TryGetAWSCredentials AwsProfileName
        match success with
            | true -> awsCredentials
            | false -> raise (new InvalidOperationException("Cannot get AWS credentials"))
