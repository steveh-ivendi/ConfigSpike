## A spike of how config overrides behave in .NET Core 2.2

This simple console app picks up config from various places and tests that it is available. When it is available the config is output. The expected output is as follows:

```
The NotOverridden config element is present:
	IntValue=101
	StringValue=This is a test string
The PartOverridden config element is present:
	IntValue=102
	StringValue=This string comes from the override config
The PartEnvOverridden config element is present:
	IntValue=104
	StringValue=This string has come from the environment variables
The AllEnvConfig config element is present:
	IntValue=105
	StringValue=This string has also come from the environment variables
```