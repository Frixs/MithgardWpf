<p align="center">
  <img src="assets/mithgardwpf-logo.png" height="125" />
</p>

<p align="center">
  <em>Kickstart your C# .NET WPF app with a ready-to-use architecture. Navigation, pages, animations, and core abstractions are already set up, so you can jump straight into building your business logic.</em>
</p>

<div align="center">

[![Build](https://github.com/Frixs/MithgardWpf/actions/workflows/dotnet-desktop-tests.yml/badge.svg)](https://github.com/Frixs/MithgardWpf/actions/workflows/dotnet-desktop-tests.yml) 
![GitHub tag (latest by date)](https://img.shields.io/github/v/tag/Frixs/MithgardWpf?color=blue) 
[![GitHub license](https://img.shields.io/github/license/Frixs/MithgardWpf?color=brightgreen)](https://github.com/Frixs/MithgardWpf/blob/main/LICENSE) 

[![GitHub Stars](https://img.shields.io/github/stars/Frixs/MithgardWpf.svg)](https://github.com/Frixs/MithgardWpf/stargazers) 
[![GitHub Forks](https://img.shields.io/github/forks/Frixs/MithgardWpf.svg)](https://github.com/Frixs/MithgardWpf/network/members) 

</div>

## Features
- Project Structure
  - Ready-to-use folder organization for WPF projects
  - Example pages demonstrating navigation and animations
- WPF Utilities
  - Attached Properties base abstraction
  - Value Converters base abstraction with common implementations
  - Animations for WPF elements
  - Navigation between `Page` elements
- MVVM Support
  - View Models base abstraction with common implementations
- Infrastructure
  - Dependency Injection (DI) integration
  - Logging based on `ILogger`
- Testing
  - xUnit architectural unit tests

## Technologies
- [C# .NET](https://learn.microsoft.com/en-us/dotnet/)
- [WPF](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/)
- [Community MVVM Toolkit](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/)
- [Dependency Injection](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)
- [Microsoft's Logging](https://learn.microsoft.com/en-us/dotnet/core/extensions/logging)
- [NetArchTest](https://github.com/BenMorris/NetArchTest)
- [xUnit](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-csharp-with-xunit)

## Architecture
One of the most popular architectural patterns for WPF is [MVVM](https://learn.microsoft.com/en-us/dotnet/architecture/maui/mvvm). This project follows it to maintain standard practices. To reduce some of the boilerplate code that MVVM usually requires, this project uses the MVVM Toolkit. Using the toolkit is optional, and it’s easy to remove if you prefer. The demonstration pages implemented show examples both with and without it. Overall, the toolkit helps keep the code clean and focused.

MVVM is excellent for defining the flow of your code and how the application behaves, but it doesn’t directly solve issues like project organization or long-term maintainability. For that, patterns like [Clean Architecture](https://www.milanjovanovic.tech/blog/clean-architecture-and-the-benefits-of-structured-software-design) (CA) or [Vertical Slice Architecture](https://www.milanjovanovic.tech/blog/vertical-slice-architecture) (VSA) are more suitable. That's why they got implemented as well!

In this project, we combine both approaches. VSA defines the main folder structure, while CA can be optionally applied within each "feature." For more guidance, why is that, check out [The Missing Chapter of Clean Architecture](https://www.milanjovanovic.tech/blog/clean-architecture-the-missing-chapter). I recommend reading that, as it explains a lot of architectural background and provides insight into organizing your project structure.

This structure also supports [Screaming Architecture](https://www.milanjovanovic.tech/blog/screaming-architecture), which emphasizes clear, self-explanatory code organization. Using these approaches altogether is strongly recommended in this project, as it makes the code easier to maintain and more readable for any kind of development.

## Getting Started
Are you interested in using it? Great! There’s no installation required. To use this template, you have a couple of options:

1. **GitHub template**: You can create a new repository directly from this one. Look for the "Use this template" button at the top of this repository’s page.
2. **Manual download**: Download the code from the `main` branch (or the latest tag) and start your project using this codebase.

### Solution & Project Structure
The project repository follows a standard structure:
- `/src` contains the implementation.
- `/test` contains the test projects for the implementation.

The main project is organized into several primary folders:
- `Common`
- `Core`
- `Features`
- `FeaturesInfrastructure`
- `FeaturesShared`
- `Pages`

Let's discuss each folder separately into more detail.

#### Common
Holds shared utilities, helper functions, converters, attached properties, and extensions that can be used across the entire project.

#### Core
This folder contains the truly fundamental parts of the project. In this project, `Core` folder is where we define the essential functionality specific to the project, such as base classes for MVVM, core services, and critical features like page navigation or animations. 

Do not confuse this with the "Core" or "Domain" layer in Clean Architecture. Unlike a pure domain layer, it does not need to be [POCO](https://en.wikipedia.org/wiki/Plain_old_CLR_object). The `Core` folder depends on `Common`, since `Common` provides the shared codebase that can also be used inside the core features of the project.

#### Features
Encapsulates functionality around specific business capabilities. Each feature is self-contained and usually follows a modular structure (e.g., data access, services, UI components related to that feature). Architecture within the feature is independent of the project.

#### FeaturesInfrastructure
Whenever an implementation is needed across multiple features, the question arises: is it business logic or infrastructure?

- If it’s business logic specific to a feature → keep it inside the feature.
- If it’s infrastructure (e.g., HTTP client for a 3rd-party API, serialization helpers, authentication with that API, retry policies, etc.) → extract it into a separate infrastructure/service layer.

The separation into `Features`, `FeaturesInfrastructure`, and `FeaturesShared` is similar to Clean Architecture’s `Application`, `Domain`, and `Infrastructure`. The idea is similar, see the dependency relations:

- `Features` can depend on `FeaturesShared`.
- `FeaturesInfrastructure` can depend on `FeaturesShared` as well.
- `FeaturesShared` cannot depend on either.

In this structure, the infrastructure layer only contains the implementation. Interfaces (contracts) are defined in the `FeaturesShared` folder.

#### FeaturesShared
Reserve `FeaturesShared` only for feature-to-feature communication and infrastructure contracts. 

If you mean utility code (helpers, extensions, base classes), don’t dump them in `FeaturesShared`. That folder usually ends up a junk drawer. Instead, use `Common` or `Core`.

What about the feature-to-feature communication?  
You want feature isolation, but reality forces some features to talk to each other. Features should not directly depend on each other. There are a couple of ways to handle this: 
- Use Messages (Commands / Queries / Events); via [MediatR](https://github.com/LuckyPennySoftware/MediatR) or [Mediator](https://github.com/martinothamar/Mediator)
- Shared Contracts / Interfaces; via `FeaturesShared` (see details below)

You can create a shared folder that only contains:
- DTOs used in cross-feature requests/responses
- Command/Query/Event definitions
- Interfaces for abstractions

An example usage could look like this:
##### Folder Layout
```
Features/
   Customers/
      CustomerService.cs
   Orders/
      OrderService.cs
FeaturesShared/
   Contracts/
      ICustomerReader.cs
      CustomerDto.cs
```
##### ICustomerReader.cs
```cs
public interface ICustomerReader
{
    CustomerDto? GetById(Guid id);
}
```
##### CustomerDto.cs
```cs
public record CustomerDto(Guid Id, string Name, string Email);
```
##### CustomerService.cs
```cs
using FeaturesShared.Contracts;

public class CustomerService : ICustomerReader
{
    private readonly AppDbContext _db;

    public CustomerService(AppDbContext db)
    {
        _db = db;
    }

    public CustomerDto? GetById(Guid id)
    {
        var c = _db.Customers.Find(id);
        return c == null ? null : new CustomerDto(c.Id, c.Name, c.Email);
    }
}
```
##### OrderService.cs
```cs
using FeaturesShared.Contracts;

public class OrderService
{
    private readonly ICustomerReader _customers;
    private readonly AppDbContext _db;

    public OrderService(ICustomerReader customers, AppDbContext db)
    {
        _customers = customers;
        _db = db;
    }

    public Guid CreateOrder(Guid customerId, string product)
    {
        var customer = _customers.GetById(customerId);
        ...
    }
}
```

#### Pages
Defines the UI pages or views, typically composed of features and shared components, representing user-facing application screens.

In this project, pages are defined using the [`AppPage`](src/MithgardWpf.App/Core/Navigation/Views/AppPage.cs) class, which inherits from `System.Windows.Controls.Page`. It acts as the base for all WPF pages in the project. This is required in order to use the navigation logic implemented in this project.

## Acknowledgements
I have built my knowledge through years of dedicated practice and learning. Along the way, I have been inspired by many talented developers and innovative projects. Below are the most influential sources that helped shape my skills in C# regarding this project.

- @angelsix ([Fasetto Word](https://github.com/angelsix/fasetto-word))
- @ardalis ([Clean Architecture](https://github.com/ardalis/CleanArchitecture))
- @m-jovanovic (Milan Jovanović)
- @Elfocrash (Nick Chapsas)
