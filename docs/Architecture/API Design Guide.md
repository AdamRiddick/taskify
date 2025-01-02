# Overview

## Paths

- Endpoint paths must be in the form /api/domain/resource
  - /api/tasks/todoitems
- Paths must be lowercase.
- Resource names must always be represented as nouns, excluding commands
- Resource names must be plural
- Resource paths may contain primary key identifiers, but must not contain any additional filters
- Parameter names must be named `id` where the resource matches the primary key only
  - /todoitems/{id}

## Resources

- Filter criteria must be passed by querystring.
  - todoitems/students?completed=$false
- Files, photos and other binary data must be returned as raw binary data along with a MIME type.
- POST actions must return a self-reference to the newly created resource within the Location header.

## Relationships/Associations

- Foreign key relationships must be represented as navigable URL references to the related resources.
- Foreign key relationships must contain ONLY primary key information in addition to the resource reference.
  - { id: 1, href: “/projects/1” }
- One-to-many relationships must be represented as sub resources on the primary resource.
  - POST /todoitems/01234/labels -> Links a Label to a ToDo Item.
- Many-to-one relationships must allow associations by Id on the secondary resource.
  - POST  /todoitems/ -> ProjectId property links to a Project resource
- POST/PATCH requests for a resource must not include sub resources; they should be managed as their own resource.

## Commands

- Commands must only support POST
- Command endpoints are not considered RESTful. To achieve this use /commands in the path.
  - POST /api/domain/commands
- Where a command is associated to a resource, the resource must be included in the path.
  - POST /api/domain/resource/{id}/dosomework

## Breaking Changes

Below is a matrix that highlights breaking changes within the REST API. Where breaking changes are required, endpoints *must* be versioned.

✔ - the change is safe
❌ - the change is a risk
❕ - the change may be a risk

| Verb | Add a Property | Rename a Property | Remove a Property | Change a Property Type | Add an Endpoint | Rename an Endpoint | Remove an Endpoint |
|--|:--:|:--:|:--:|:--:|:--:|:--:|:--:|:--:|
| GET | ✔ | ❌ | ❌ | ❕ | ✔ | ❌ | ❌ |
| POST | ❌ | ❌ | ❕ | ❕ | ✔ | ❌ | ❌ |
| PUT | ❌ | ❌ | ❕ | ❕ | ✔ | ❌ | ❌ |
| PATCH | ❌ | ❌ | ❕ | ❕ | ✔ | ❌ | ❌ |
| DELETE | ❌ | ❌ | ❕ | ❕ | ✔ | ❌ | ❌ |

### Add a Property

Adding a property is a breaking change for any write commands, as integrators may not be aware the new property is available. This is only a problem for POST & PATCH where the property is mandatory.

Adding a property is not a breaking change for read operations.

### Rename a Property

Renaming a property is a breaking change as it alters the existing public contract.

### Remove a Property

Removing a property is a breaking change as the property may be in use by existing integrations.

### Change a Property Type

Changing the type of a property may result in a breaking change if the previous data type does **not** convert to the new data type without assistance; e.g. changing a `string` to a `DateTime`.

### Add an Endpoint

Introducing a new endpoint will **not** break existing integrations.

### Rename an Endpoint

Renaming an endpoint is a breaking change because integrations may be calling this endpoint.

### Remove an Endpoint

Removing an endpoint is a breaking change because integrations may be calling this endpoint.
