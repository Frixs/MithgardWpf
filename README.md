<p align="center">
  <img src="assets/mithgardwpf-logo.png" height="125" />
</p>

<p align="center">
  <em>Kickstart your C# .NET WPF app with a ready-to-use architecture. Navigation, pages, animations, and core abstractions are already set up, so you can jump straight into building your business logic.</em>
</p>

<div align="center">

[![Build](https://github.com/Frixs/MithgardWpf/actions/workflows/dotnet-desktop-tests.yml/badge.svg)](https://github.com/Frixs/MithgardWpf/actions/workflows/dotnet-desktop-tests.yml) 
![GitHub tag (latest by date)](https://img.shields.io/github/v/tag/Frixs/MithgardWpf?color=blue)

[![GitHub Stars](https://img.shields.io/github/stars/Frixs/MithgardWpf.svg)](https://github.com/Frixs/MithgardWpf/stargazers) 
[![GitHub Forks](https://img.shields.io/github/forks/Frixs/MithgardWpf.svg)](https://github.com/Frixs/MithgardWpf/network/members) 
[![GitHub license](https://img.shields.io/github/license/Frixs/MithgardWpf?color=brightgreen)](https://github.com/Frixs/MithgardWpf/blob/main/LICENSE) 

</div>

## Technologies
- [C# .NET](https://learn.microsoft.com/en-us/dotnet/)
- [WPF](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/)
- [Community MVVM Toolkit](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/)
- [Dependency Injection](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)
- [Microsoft's Logging](https://learn.microsoft.com/en-us/dotnet/core/extensions/logging)
- [NetArchTest](https://github.com/BenMorris/NetArchTest)
- [xUnit](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-csharp-with-xunit)

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

## Architecture
One of the most popular architectural patterns for WPF is [MVVM](https://learn.microsoft.com/en-us/dotnet/architecture/maui/mvvm). This project follows it to maintain standard practices. To reduce some of the boilerplate code that MVVM usually requires, this project uses the MVVM Toolkit. Using the toolkit is optional, and it’s easy to remove if you prefer. The demonstration pages implemented show examples both with and without it. Overall, the toolkit helps keep the code clean and focused.

MVVM is excellent for defining the flow of your code and how the application behaves, but it doesn’t directly solve issues like project organization or long-term maintainability. For that, patterns like [Clean Architecture](https://www.milanjovanovic.tech/blog/clean-architecture-and-the-benefits-of-structured-software-design) (CA) or [Vertical Slice Architecture](https://www.milanjovanovic.tech/blog/vertical-slice-architecture) (VSA) are more suitable. That's why they got implemented as well!

In this project, we combine both approaches. VSA defines the main folder structure, while CA can be optionally applied within each "feature." For more guidance, why is that, check out [The Missing Chapter of Clean Architecture](https://www.milanjovanovic.tech/blog/clean-architecture-the-missing-chapter). I recommend reading that, as it explains a lot of architectural background and provides insight into organizing your project structure.

This structure also supports [Screaming Architecture](https://www.milanjovanovic.tech/blog/screaming-architecture), which emphasizes clear, self-explanatory code organization. Using these approaches altogether is strongly recommended in this project, as it makes the code easier to maintain and more readable for any kind of development.

## Getting Started
TODO

## Acknowledgements
I have built my knowledge through years of dedicated practice and learning. Along the way, I have been inspired by many talented developers and innovative projects. Below are the most influential sources that helped shape my skills in C# regarding this project.

- @angelsix ([Fasetto Word](https://github.com/angelsix/fasetto-word))
- @ardalis ([Clean Architecture](https://github.com/ardalis/CleanArchitecture))
- @m-jovanovic (Milan Jovanović)
- @Elfocrash (Nick Chapsas)
