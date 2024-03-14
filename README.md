## Contents

_SmellyCatClient/_ contains the Angular frontend that provides the contact form.

_SmellyCatServer/_ contains an ASP.NET Core Minimal API to handle the form submission requests and dispatch emails.

## Usage

- Start the ASP.NET Core server with `dotnet run`
- Start a test _Mailpit_ SMTP server locally using `docker run -p 8025:8025 -p 1025:1025 axllent/mailpit` and access the email inbox at <http://localhost:8025>.
- Start the Angular development server with `ng serve` and access the interface at <http://localhost:4200>.
