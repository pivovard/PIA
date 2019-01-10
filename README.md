# KIV/PIA Bank

This project is semestral work of KIV/PIA.
Web application represents web of the bank with internet banking.

## Technologies

Application is created by ASP.NET MVC using ASP.NET Core and Entity framework.
Database is realized as Microsoft SQL Server Database File.
Tests are writen as MSTest unit tests.

## Roles

### Not signed
Can view only public (static) pages.

### Admin
Can manage users (add, edit, delete).
Can't make bank operations.

### User
Can manage his credentials (not account information).
Can send payment. Can use template for payment.
Can add/edit/delete template for payment.

## Optional functions
Email notice when account is created/edited/deleted.
Email login verification.
Email payment confirmation.
Confirmation of paymen if destination account within same bank code doesn't exist.
Transfer money within same bank.