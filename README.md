# BillSave App
---
## Summary
The BillSave Backend is the server-side component of the BillSave application, designed to manage portfolios of financial documents (bills/invoices), calculate the Annual Effective Cost Rate (ACER) of each portfolio, and generate detailed reports.  

Built with a **Domain-Driven Design (DDD)** architecture, the backend focuses on bounded contexts such as **portfolio**, **sales (bills/invoices)**, **iam (Identity and Access Management)**, and **profiles**.   

The backend provides RESTful APIs for the frontend to interact with, enabling features like user authentication, portfolio management, document handling, and report generation. It integrates with external services like the **SUNAT API** for additional functionality. 

## Features
* Domain-Driven Design (DDD) architecture.
* RESTful API for seamless frontend integration.
* Portfolio management: Create, edit, delete, and list portfolios.
* Document management: Handle financial documents (bills/invoices) associated with portfolios.
* User authentication and management: Login, registration, and profile management.
* Automatic TCEA calculation: Compute the Annual Effective Cost Rate for portfolios.

## Framework  
The backend is built with [.NET](https://dotnet.microsoft.com/es-es/) and the following key dependencies:

* Entity Framework Core for database management.
* ASP.NET Core for building RESTful APIs.
* JWT (JSON Web Tokens) for secure authentication.
* Swagger for API documentation.

## Contribute to this project
You can contribute to this project by submitting issues or pull requests in the [GitHub repository](https://github.com/FinWorkTech/BillSave-backend/). We welcome new ideas, features, and improvements!
