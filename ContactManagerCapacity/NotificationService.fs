namespace ContactManagerCapacity.Main.SNS

/// <summary>
/// Publishes messages to AWS SNS Topic
/// </summary>
module SNSNotificationService =

    open System.Configuration
    open Amazon.SimpleNotificationService
    open Amazon.SimpleNotificationService.Model
    open ContactManagerCapacity.Main.Credentials

    let private TopicArn =
        ConfigurationManager.AppSettings.Item "AWSSNSTopicArn"

    let notify message = 
        let amazonSnsClient = new AmazonSimpleNotificationServiceClient(CredentialProvider.AwsCredentials, Amazon.RegionEndpoint.USEast1)
        amazonSnsClient.Publish(
            new PublishRequest(
                TopicArn = TopicArn,
                Message = message
            )
        ).ResponseMetadata.Metadata
