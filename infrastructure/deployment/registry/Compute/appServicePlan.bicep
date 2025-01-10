@description('Name of the app service plan')
param appServicePlanName string

@description('The SKU to use for the app service plan. Defaults to B1')
param appServicePlanSku string = 'B1'

@description('Resource group location')
param location string = resourceGroup().location

resource appServicePlan 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: appServicePlanSku
    capacity: 1
  }
}

output appServicePlan resource = appServicePlan
