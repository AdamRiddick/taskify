@description('Name of the Application Insights instance')
param appInsightsName string

@description('Location for the Application Insights instance')
param location string = resourceGroup().location

@description('Application Type (e.g., web, other)')
@allowed([
  'web'
  'other'
])
param applicationType string = 'web'

@description('Retention period for logs (in days)')
@minValue(30)
@maxValue(730)
param retentionInDays int = 90

@description('Enable/disable ingestion of application telemetry')
param disableIpMasking bool = false

@description('Log Analytics workspace resource ID')
param workspaceResourceId string = ''

resource appInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: appInsightsName
  location: location
  kind: applicationType
  properties: {
    Application_Type: applicationType
    DisableIpMasking: disableIpMasking
    RetentionInDays: retentionInDays
    WorkspaceResourceId: workspaceResourceId
  }
}

output appInsights resource = appInsights
