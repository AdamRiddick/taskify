@description('Name of the Key Vault')
param keyVaultName string

@description('Location of the Key Vault')
param location string = resourceGroup().location

@description('SKU for the Key Vault')
@allowed([
  'standard'
  'premium'
])
param skuName string = 'standard'

@description('Specifies whether the Key Vault is enabled for public or private network access')
@allowed([
  'Enabled'
  'Disabled'
])
param publicNetworkAccess string = 'Enabled'

@description('Tenant ID for RBAC access control (defaults to current subscription tenant)')
param tenantId string = subscription().tenantId

@description('Specifies if soft delete is enabled (default true)')
param enableSoftDelete bool = true

@description('Specifies the retention period for soft-deleted vaults (in days)')
@minValue(7)
@maxValue(90)
param softDeleteRetentionInDays int = 90

resource keyVault 'Microsoft.KeyVault/vaults@2023-02-01' = {
  name: keyVaultName
  location: location
  properties: {
    tenantId: tenantId
    sku: {
      family: 'A'
      name: skuName
    }
    enableSoftDelete: enableSoftDelete
    softDeleteRetentionInDays: softDeleteRetentionInDays
    publicNetworkAccess: publicNetworkAccess
    networkAcls: {
      bypass: 'AzureServices'
      defaultAction: publicNetworkAccess == 'Enabled' ? 'Allow' : 'Deny'
    }
  }
}

output keyVault resource = keyVault
