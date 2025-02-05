@description('Name of the Storage Account')
param storageAccountName string

@description('Location of the Storage Account')
param location string = resourceGroup().location

@description('Replication type for the Storage Account')
@allowed([
  'LRS'  // Locally redundant storage
  'GRS'  // Geo-redundant storage
  'ZRS'  // Zone-redundant storage
  'RAGRS' // Read-access geo-redundant storage
])
param skuName string = 'LRS'

@description('Access tier for the Storage Account')
@allowed([
  'Hot'
  'Cool'
])
param accessTier string = 'Hot'

@description('Enable HTTPS traffic only')
param enableHttpsTrafficOnly bool = true

@description('Allow Blob public access')
param allowBlobPublicAccess bool = false

resource storageAccount 'Microsoft.Storage/storageAccounts@2022-09-01' = {
  name: storageAccountName
  location: location
  sku: {
    name: skuName
  }
  kind: 'StorageV2'
  properties: {
    accessTier: accessTier
    allowBlobPublicAccess: allowBlobPublicAccess
    minimumTlsVersion: 'TLS1_2'
    supportsHttpsTrafficOnly: enableHttpsTrafficOnly
  }
}

output storageAccount resource = storageAccount
