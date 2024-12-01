# NUnit Unit Testing Demonstration

This repository serves as a demonstration project for **unit testing** using **NUnit**, a popular testing framework for .NET applications. It provides a hands-on example of setting up and running unit tests in a .NET application, specifically focusing on the NUnit framework to validate application logic.

## Project Overview

The goal of this project is to showcase how NUnit can be used to:

- Write unit tests for .NET methods and classes.
- Run tests and analyze results.
- Use mock dependencies and assertions to ensure code quality.
- Integrate NUnit tests into a .NET-based application using the appropriate libraries.

## Key Features

- **Unit Testing with NUnit**: The project contains examples of writing unit tests to verify individual pieces of functionality in isolation.
- **Test Structure**: Learn how to structure tests in NUnit, including the use of attributes such as `[Test]`, `[SetUp]`, and `[TearDown]`.
- **Mocking Dependencies**: Use of mocks to simulate dependencies that might be costly or impractical to instantiate for testing purposes.
- **Test Assertions**: Learn to make various assertions in NUnit (e.g., `Assert.AreEqual()`, `Assert.Throws()`).

## Packages Required

```bash
dotnet add package NUnit
dotnet add package NUnit3TestAdapter
dotnet add package Microsoft.NET.Test.Sdk
dotnet add package Moq
```
