# Auth

## Authentication (AuthN)

Authentication is handled via a third-party identity platform. Manual provisioning must occur before a user can login, as they must be assigned to the relevant tenant.

## Authorization (AuthZ)

### Overview

Authorization must occur at the earliest possible point in an applications pipeline, to reduce the risk of a security breach, and prevent unneccessary processing.

All entry points must make use of the Identity domain to ensure the correct AuthZ is applied for a given request, at the point of entry, for ecample at the controller/endpoint level in an API.

Authorization of a given action can be performed globally, or contextually. For example, the global "User can read To-Do's" vs "User can read To-Do's for a given project". Each domain or microservice is in control of the granularity of authZ required, however consistency is key.

All Authorization is tenant-aware.

### Roles

Roles are static & defined by the Identity domain. The allowed roles are;

Reader - Can perform read operations only.
Contributor - Can perform read & write operations.
Owner - Can perform all operations.

> To follow the principle of least privilege, the highest role must always be required for a given action. Readers must never be allowed to perform operations that change state.

### Contexts

The authZ system is context aware, with domains/microservices being responsible for defining their contexts.

Typically, contexts should be based on the aggregate roots defined by the domain. There may be fringe cases where this is not possible, however those must be carefully considered.

> Domains & Microservices have freedomt to define contexts based on the product requirements. For example, "Tasks" could be used, or "Tasks.ToDo", or even as far as "Tasks.ToDo.Project" to limit access based on another entity. In this case, the EntityId on the ContextRole would be specified.

### Context Roles

A context role is the entity used to define a users permission set. Each user can have multiple Context Roles, each one granting the permissions of a Role in a specific Context.
