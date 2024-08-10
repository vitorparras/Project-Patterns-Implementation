# Project-Patterns-Implementation

## Description

This repository is dedicated to implementing various design patterns. Our goal is to provide clear and well-documented examples that help developers understand and apply these patterns in their own projects. Here, you will find implementations of architectural patterns such as layered architecture, microservices, minimal APIs, as well as design patterns like singleton, factory, strategy, among others.

## Table of Contents

- [Architectural Patterns](#architectural-patterns)
  - [Layered Architecture](#layered-architecture)
  - [Microservices](#microservices)
  - [Minimal APIs](#minimal-apis)
- [Design Patterns](#design-patterns)
  - [Singleton](#singleton)
  - [Factory](#factory)
  - [Strategy](#strategy)
  - [Observer](#observer)
  - [Decorator](#decorator)
  - [Adapter](#adapter)
  - [Facade](#facade)
  - [Proxy](#proxy)
- [How to Contribute](#how-to-contribute)
- [License](#license)

## Architectural Patterns

### Layered Architecture

Implementation of an application using the layered architecture pattern. This pattern organizes the application into different layers, such as presentation, business logic, and data access, promoting a clear separation of responsibilities.

### Microservices

Example of a microservices architecture, where the application is divided into smaller, independent services that communicate with each other. This pattern promotes scalability and flexibility in application maintenance and development.

### Minimal APIs

Implementation of minimal APIs, which are designed to create HTTP services with minimal files and dependencies. This pattern is ideal for microservices, where simplicity and efficiency are essential.

## Design Patterns

### Singleton

The Singleton pattern ensures that a class has only one instance and provides a global point of access to it.

### Factory

The Factory pattern is a creational pattern that abstracts the instantiation of objects, allowing subclasses to alter the type of objects that will be created.

### Strategy

The Strategy pattern allows the definition of a family of algorithms, encapsulating each one of them and making them interchangeable. This allows the algorithm to vary independently of the clients that use it.

### Observer

The Observer pattern defines a one-to-many dependency between objects so that when one object changes state, all its dependents are notified and updated automatically.

### Decorator

The Decorator pattern allows behavior to be added to individual objects, dynamically, without affecting the behavior of other objects from the same class.

### Adapter

The Adapter pattern allows the interface of an existing class to be used as another interface. It is used to allow classes with incompatible interfaces to work together.

### Facade

The Facade pattern provides a simplified interface to a set of interfaces in a subsystem, making the subsystem easier to use.

### Proxy

The Proxy pattern provides a surrogate or placeholder through which an object can control access to another.

## How to Contribute

1. Fork the repository
2. Create a branch for your feature (`git checkout -b feature/new-feature`)
3. Commit your changes (`git commit -m 'Add new feature'`)
4. Push to the branch (`git push origin feature/new-feature`)
5. Open a Pull Request

