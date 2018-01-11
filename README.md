# Contact Manager Capacity

Simple application that accesses the [`contact-manager` API][contact-manager-git].

## Configuration

You must configure this application to connect to a live AWS account. The account should
have access to AWS SNS.

1. Create a [credentials file][aws-keys] for access to AWS
```
[production]
aws_access_key_id = {accessKey}
aws_secret_access_key = {secretKey}
```
2. Set the `AWSProfilesLocation` appSetting in the [App.config][app-config] to the fully
qualified location of the credentials file

# Technologies

- F#
- .NET
- AWS SNS

[contact-manager-git]: https://github.com/WillCallahan/contact-manager
[aws-keys]: https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/net-dg-config-creds.html#creds-file
[app-config]: https://github.com/WillCallahan/ContactManagerCapacity/blob/master/ContactManagerCapacity/App.config