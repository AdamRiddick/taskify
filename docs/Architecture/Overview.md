# Overview

## Domain Project Breakdown

### Taskify.SharedKernel

All other projects in this application must be bound by bounded contexts in DDD. Each context has its own slice of Web, UseCases and Core, and must be segregated accordingly. This is reinforced using the Architectural Tests. This means the top level folders in each project must be named after the BoundedContext.

The SharedKernel is the single point where dependencies may cross-cut domains, and must not contain domain specific logic. Ultimately, the SharedKernel will become a NuGet package. Other items required by multiple domains must follow the same mode, and be packaged where the work is significant, or duplicated in the required domains.

#### Guidelines for using the SharedKernel

- Keep it small and stable to avoid cascading changes across bounded contexts.
- Use Value Objects or domain events as shared abstractions when possible, as they are simpler to manage than full entities.
- Clearly define ownership of the Shared Kernel code to resolve disputes over changes.

### Taskify.Core

The Core project is the center of the architecture, and all other project dependencies should point toward it. As such, it has very few external dependencies. The Core project should include the Domain Model including things like:

- Entities
- Aggregates
- Value Objects
- Domain Events
- Domain Event Handlers
- Domain Services
- Specifications
- Interfaces
- DTOs (sometimes)

### Taskify.Infrastructure

Most of the application's dependencies on external resources will be implemented in the Infrastructure project. These classes should implement interfaces defined in Core, allowing them to be used by UseCases.

This can be split into sub projects for specific purposes as an application grows. For example, Infrastructure.Data, Infrastructure.Email.

### Taskify.UseCases

In Clean Architecture, the Use Cases (or Application Services) project is a relatively thin layer that wraps the domain model.

Use Cases are typically organized by feature. These may be simple CRUD operations or much more complex activities.

Use Cases should not depend directly on infrastructure concerns, making them simple to unit test in most cases.

Use Cases are often grouped into Commands and Queries, following CQRS. Commands mutate the domain model and thus should always use Repository abstractions for their data access. Queries are readonly, and thus do not need to use the repository pattern, but instead can use whatever query service or approach is most convenient.

Having Use Cases as a separate project can reduce the amount of logic in UI and Infrastructure projects.

### Taskify.Api

The entry point. This is an API project that follows the REPR pattern.

### Taskify.Ui

Cross-platform UI library holding razor pages and blazor components.

### Taskify.App

Cross-Platform app using Blazor Hybrid. Reliant on Taskify.Ui, and calls out to Taskify.Api, without a direct reference.

### Taskify.Web

Web client using Blazor WASM. Reliant on Taskify.Ui, and calls out to Taskify.Api, without a direct reference.

## Domain

### Bounded Contexts

- [TODO] User Context: Authentication, roles, and user profiles.
- Task Context: Task creation, assignment, and tracking.
- [TODO] Notification Context (Microservice): Sending reminders and updates.
- [TODO] Billing Context (Microservice): Manages payment and subscription plans.
- [TODO] Reporting Context (Microservice): Handles data aggregation and visualization.

### Key Features

- [TODO] User authentication and role-based access control (Monolith module).
- Task management (CRUD operations for tasks, priorities, due dates).
- [TODO] Collaboration tools (comments, mentions, and notifications).
- [TODO] Reporting/analytics (microservice for generating reports and insights).
- [TODO] Payment processing (microservice to handle subscriptions and billing).

## Why It Fits

- Domain Complexity: Features like task management, notifications, and analytics align well with DDD principles for modeling bounded contexts (e.g., Users, Tasks, Billing).

- Scalability: Reporting and payment processing naturally lend themselves to being implemented as microservices because of their independent scalability needs and integration with external systems.

- Practical Hybrid: You can keep user-facing workflows in the monolith while decoupling heavy-lifting or specialized tasks like analytics into microservices.
