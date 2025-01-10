param deploymentId string = take(newGuid(), 5)

@description('The environment')
@allowed([
  'dev'
  'prod'
])
param environment string

@description('The name of the project')
@minLength(3)
@maxLength(9)
param projectName string

module configurationModule './registry/config.bicep' = {
  name: 'configuration'
  params: {
    environment: environment
    projectName: projectName
  }
}
var configuration = configurationModule.outputs.configuration // For brevity

resource lock 'Microsoft.Authorization/locks@2020-05-01' = {
  name: 'lock-${resourceGroup().name}'
  properties: {
    level: 'CanNotDelete'
    notes: 'Lock to prevent resource group and its resources from being deleted'
  }
}

module virtualNetwork './registry/Networking/virtualNetwork.bicep' = {
  name: '${deploymentId}-vnet'
  params: {
    vnetName: configuration.networking.vnetname
  }
}

module appServicePlan 'registry/Compute/appServicePlan.bicep' = {
  name: '${deploymentId}-appserviceplan'
  params: {
    appServicePlanName: configuration.compute.appServicePlanName
  }
}

module appService 'registry/Compute/appService.bicep' = {
  name: '${deploymentId}-appservice'
  params: {
    appName: configuration.compute.appServiceName
    appServicePlanId: appServicePlan.outputs.appServicePlan.id
    vnetRouteAllEnabled: true
    vnetSubnetResource: virtualNetwork.outputs.vnet
  }
}
