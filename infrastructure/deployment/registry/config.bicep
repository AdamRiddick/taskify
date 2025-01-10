@description('The environment')
@allowed([
  'dev'
  'prod'
  'test'
])
param environment string

@description('The name of the project')
@minLength(3)
@maxLength(9)
param projectName string

// todo: Can location code be obtained from location in a generic way?
var suffix = 'ty-${projectName}-suk-${environment}'

var configuration = {
  compute: {
    appServicePlanName: 'asp-${suffix}'
    appName: 'app-${suffix}'
  }
  networking: {
    vnetname: 'vnet-${suffix}'
  }
}

output configuration object = configuration
