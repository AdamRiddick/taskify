@description('Id of the App Service Plan')
param appServicePlanId string

@description('Name of the App Service')
param appName string

@description('Location of the App Service')
param location string = resourceGroup().location

@description('Enable HTTPS only')
param httpsOnly bool = true

@description('Optional application settings')
param appSettings array = [
  {
    name: 'WEBSITE_RUN_FROM_PACKAGE'
    value: '1'
  }
]

@description('Specifies if all traffic should be routed through the vnet. Defaults to false.')
param vnetRouteAllEnabled bool = false

@description('The resource ID of the subnet to associate the web app with.')
param vnetSubnetResource resource 'Microsoft.Network/virtualNetworks@2023-02-01'

resource appService 'Microsoft.Web/sites@2022-03-01' = {
  name: appName
  location: location
  properties: {
    serverFarmId: appServicePlanId
    httpsOnly: httpsOnly
    siteConfig: {
      appSettings: appSettings
    }
    vnetRouteAllEnabled: vnetRouteAllEnabled
    virtualNetworkSubnetId: vnetSubnetResource.properties.subnets[0].id
  }
}
