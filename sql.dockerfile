FROM mcr.microsoft.com/mssql/server:2022-latest AS build
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=STRONGpassword!1

FROM mcr.microsoft.com/mssql/server:2022-latest AS release

EXPOSE 1433

ENV ACCEPT_EULA=Y