# Introduction

When creating new resources in Azure, it is important to follow our naming conventions to improve consistency, identification, maintainability and minimise naming conflicts.

There are different conventions to apply depending on use case.

## Examples

Resource groups:

- ```rg-ty-[project]-[environment]```
- e.g. ```rg-ty-webapp-dev```

General:

- ```[resource_type]-ty-[project]-[region_code]-[environment]```
- e.g. ```app-ty-webapp-suk-prod```

Restricted:

- ```[resource_type/3]ty[project/9][region_code/3][environment/4]```
- e.g. ```sttyhostingsukprod```

## Structure

The following naming format should be used:

```[resource_type]-ty-[project]-[region_code]-[environment]```

To explain each component:

- **[resource_type]** : Prefix identifying the type of the resource. Use these [recommended abbreviations](https://docs.microsoft.com/en-us/azure/cloud-adoption-framework/ready/azure-best-practices/resource-abbreviations).
- **ty** : Global namespace to prevent global naming conflicts.
- **[project]** : Project identifier, limit to 9 characters.
- **[region_code]** : The 3 character code for the deployed Azure region e.g. southuk = "suk". This is not required for non-regional resources, such as Resource Groups.
- **[environment]** : The stage of the development lifecycle the resources relate to. Use ```prod```, ```dev``` or ```test```.
- **[instance]** : a two digit integer to support cases where multiple instances are required. This is not required for Resource Groups, and should only be specified where required.
