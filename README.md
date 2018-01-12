# Contact Manager Capacity

This application operates by posting data to the [`contact-manager` API][contact-manager-git].
When the application starts, up to 1000 threads will be created so that 1000 requests can be made to the
API. For every 100 requests, a message will be published to an AWS SNS Topic.

Application Entry Point: [ContactManagerCapacity\Main.fs#main](https://github.com/WillCallahan/ContactManagerCapacity/blob/master/ContactManagerCapacity/Main.fs)

## Configuration

### Application Configuration

You must configure this application to connect to a live AWS account. The account should
have access to AWS SNS.

1. Create a [credentials file][aws-keys] on your local disk for access to AWS
```
[production]
aws_access_key_id = {accessKey}
aws_secret_access_key = {secretKey}
```
2. Set the `AWSProfilesLocation` appSetting in the [App.config][app-config] to the fully
qualified location of the credentials file

### AWS Configuration

A connection to AWS SNS must be configured so that this application can operate.

1. Open the AWS SNS Console
2. Create a *Topic*
3. Create a *Subscriber* in the SNS Topic (i.e. Email Address, AWS SQS, etc.)
4. Set the `AWSSNSTopicArn` appSetting in the [App.config][app-config] to the *ARN* of the new Topic

## Building

The application can be built using multiple methods. Use the following documentation to build the application.

#### Visual Studio 2015+

##### Dependencies

- Visual Studio 2015
- F# SDK
- .NET Framework 4.5.2

<!--
#### Command Line Interface

1. Download the [NuGet Package Manager][nuget] executable (v4.4.1)
2. Install the Nuget packages for the application

Type the following command into the CLI to build the application, replacing the second parameter of the command with the
fully qualified path to the project solution file.

```bash
msbuild "C:\Users\William Callahan\Documents\Visual Studio 2015\Projects\ContactManagerCapacity\ContactManagerCapacity.sln" /t:Rebuild /p:Configuration=Release /p:Platform="Any CPU"
```

Note: If `msbuild` is not on you path, then fully qualify the path to the `msbuild.exe`. `msbuild`
can usually be found under `\Windows\Microsoft.NET\Framework64\v4.0.30319`

##### CLI Build Dependencies

- [NuGet][nuget]

-->

## Running

After building the application, the application can be run by executing the newly created exe in the CLI.
From the project directory, type the following command into the CLI:

> Release

```bash
ContactManagerCapacity\bin\Release\ContactManagerCapacity.exe
```

> Debug

```bash
ContactManagerCapacity\bin\Debug\ContactManagerCapacity.exe
```

## Pitfalls

- Code is not organized into directories due to Visual Studio limitations/issues
- Code is not well commented due to the inability of Visual Studio to generate documentation templates for F#

## Technologies

- F#
- .NET 4.5
- AWS SNS

[contact-manager-git]: https://github.com/WillCallahan/contact-manager
[aws-keys]: https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/net-dg-config-creds.html#creds-file
[app-config]: https://github.com/WillCallahan/ContactManagerCapacity/blob/master/ContactManagerCapacity/App.config
[nuget]: https://www.nuget.org/downloads