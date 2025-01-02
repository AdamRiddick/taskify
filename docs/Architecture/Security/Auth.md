# Auth

## Authentication (AuthN)

Authentication is handled via a third-party identity platform. Manual provisioning must occur before a user can login, as they must be assigned to the relevant tenant.

## Authorization (AuthZ)

Authorization must occur at the earliest possible point in an applications pipeline, to reduce the risk of a security breach, and prevent unneccessary processing.

All entry points must make use of the Identity domain to ensure the correct AuthZ is applied for a given request, at the point of entry, for ecample at the controller/endpoint level in an API.

Authorization of a given action can be performed globally, or contextually. For example, the global "User can read To-Do's" vs "User can read To-Do's for a given project".

All Authorization is tenant-aware.
