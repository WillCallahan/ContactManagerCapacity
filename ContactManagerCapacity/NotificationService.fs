namespace ContactManagerCapacity.Main.SNS

module SNSNotificationService =

    open System.Configuration
    open Amazon.SimpleNotificationService
    open Amazon.SimpleNotificationService.Model
    open ContactManagerCapacity.Main.Credentials

    let private TopicArn =
        ConfigurationManager.AppSettings.Item "AWSSNSTopicArn"

    let private Region = 
        Amazon.RegionEndpoint.USEast1

    let notify message = 
        let amazonSnsClient = new AmazonSimpleNotificationServiceClient(CredentialProvider.AwsCredentials, Region)
        amazonSnsClient.Publish(
            new PublishRequest(
                TopicArn = TopicArn,
                Message = message
            )
        ) |> ignore
        ()
