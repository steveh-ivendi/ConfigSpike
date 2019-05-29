# To build a docker image locally execute `docker build -t ConsoleConfig .` in the root of this solution
FROM microsoft/dotnet:2.2-sdk AS test
WORKDIR /sln
COPY ./*.sln ./
COPY src/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p src/${file%.*}/ && mv $file src/${file%.*}/; done
RUN dotnet restore

COPY ./ ./
RUN dotnet build -c Release --no-restore

FROM test AS publish
RUN dotnet publish ./src/ConfigConsole/ConfigConsole.csproj -c Release -o /app --no-restore

# Create Release Image
FROM microsoft/dotnet:2.2-runtime AS base
WORKDIR /app

# Create Non Root User in the 10k range (avoids collisions)
RUN addgroup --gid 10000 --system netapp && \
    adduser -u 10000 --system netapp --ingroup netapp
USER netapp

# Copy build-release files here owned by netapp user
COPY --chown=netapp:netapp  --from=publish /app .

ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
# OPT OUT OF Diagnostic pipeline so we can run readonly.
ENV COMPlus_EnableDiagnostics=0
ENV PartEnvOverridden__StringValue="This string has come from the environment variables"
ENV AllEnvConfig__IntValue=105
ENV AllEnvConfig__StringValue="This string has also come from the environment variables"

FROM base AS final
WORKDIR /app
ENTRYPOINT ["dotnet", "ConfigConsole.dll"]
USER netapp