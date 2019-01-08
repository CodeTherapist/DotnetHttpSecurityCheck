# dotnet http-security-check

A .NET Core global tool to do a http request/response security assessment. 
You can also find some information on my [Blog](https://codetherapist.github.io/blog/dotnet-http-security-check/).

## Installation

Download and install the [.NET Core 2.2 SDK](https://www.microsoft.com/net/download) or newer. Once installed, run the following command:

```text
dotnet tool install DotnetHttpSecurityCheck -g
```

If you already have a previous version of [DotnetHttpSecurityCheck](https://www.nuget.org/packages/DotnetHttpSecurityCheck/) installed, you can upgrade to the latest version using the following command:

```text
dotnet tool update DotnetHttpSecurityCheck -g
```

## Usage

```text
1.0.0

A dotnet tool to do a http request/response security assessment.

Usage: http-security-check [arguments] [options]

Arguments:
  Url                   A absolute URL for the security assessment (required).

Options:
  --version             Show version information
  -?|-h|--help          Show help information
  -o|--output <value>   The report output file path (optional).
  -f|--format <value>   Format of the report (optional). Default is Text.
  -v|--verbose <value>  Set the console verbosity level (optional). Default is normal. Allowed values are n[normal], q[uiet], d[etailed].

All Security Checks:

  > Header

    1# X-Content-Type-Options: Checks the response header value of the 'X-Content-Type-Options' header.
       Recommended is "X-Content-Type-Options: nosniff".

    2# X-Frame-Options: Checks the response header value of the 'X-Frame-Options' header.
       Recommended is "X-Frame-Options: deny".

    3# X-XSS-Protection: Checks the response header value of the 'X-XSS-Protection' header.
       Recommended is "X-XSS-Protection: 1;".

    4# Referrer-Policy: Checks the response header value of the 'Referrer-Policy' header.
       Recommended values: "strict-origin", "strict-origin-when-cross-origin" or "no-referrer".

    5# Content-Security-Policy: Checks the response header value of the 'Content-Security-Policy' header.
       Whitelisting sources of approved content. Example: "Content-Security-Policy: default-src 'self'"

    6# Strict-Transport-Security: Checks the response header value of the 'Strict-Transport-Security' header.
       Recommended is "Strict-Transport-Security: max-age=31536000; includeSubDomains".

    7# X-Powered-By: Checks the response header value of the 'X-Powered-By' header.
       Technology information should be removed.

    8# Server: Checks the response header value of the 'Server' header.
       Server information should be removed.

  > Request

    1# Server Certificate: Checks for server certificate validation errors.
       Recommended is a valid certificate that has no errors.

    2# HTTPS: Checks that the request/response is HTTPS.
       Recommended is to use HTTPS.
```

### Usage examples

Simplest usage:

```text
dotnet http-security-check https://example.com
```

Same as above, but writes a textual report:

```text
dotnet http-security-check https://example.com -o=.\Report.txt
```

The tool has basic support for reporting.
By default the report is written to the console.

## Contribution

Feel free to create issues and PR's (pull requests) to improve this tool - any help is appreciated!
The project is splitted in three projects:

**Core** 

_CodeTherapy.HttpSecurityCheck.csproj_ is the core library with all the security checks and infrastructure.

**Tool**

_DotnetHttpSecurityCheck.csproj_ is the dotnet tool console command - a lightweight wrapper around the core.

**Tests**

_CodeTherapy.HttpSecurityCheck.Tests.csproj_ contains unit tests and integration tests.

Code Coverage (powered by [Coverlet](https://github.com/tonerdo/coverlet))

| Module                        | Line   | Branch | Method |
|-------------------------------|--------|--------|--------|
| CodeTherapy.HttpSecurityCheck | 85.2%  | 78.4%  | 83.3%  |

## Build, Test & Pack

The project separates the three concerns _Building_, _Testing_ and _Packaging_.
All of these steps could be executed individually.

**BuildTestPack.ps1**

This command calls internally **Build.ps1**, **Test.ps1** and **Pack.ps1** and supports following parameters:

Parameter|Description|Type|
---------|-----------|----|
Configuration|Can be set to "Release" or "Debug"|string
CollectCoverage|When set, code coverage is calculated|switch 
NoIntegrationTests|When set, integration tests are skipped|switch
Pack|When set, nuget packages are created (call to Pack.ps1)|switch

**Build.ps1**

Builds all projects. Supports the _Configuration_ parameter.

**Test.ps1**

Runs all tests. Supports _Configuration_, _CollectCoverage_, _NoIntegrationTests_ and _NoBuild_ parameters.

Parameter|Description|Type
---------|-----------|----
NoBuild|The projects are not re-build|switch

**Pack.ps1**

Creates the nuget packages into the `.\artifacts` directory. Supports _Configuration_ and _NoBuild_ parameters.

## License

This project is licensed under the MIT License - see the LICENSE.md file for details.
