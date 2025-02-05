@description('Name of the Virtual Network')
param vnetName string

@description('Location of the Virtual Network')
param location string = resourceGroup().location

@description('Address space for the Virtual Network')
param addressSpace array = [
  '10.0.0.0/16'
]

@description('Subnets configuration')
param subnets array = [
  {
    name: 'default'
    addressPrefix: '10.0.1.0/24'
  }
]

@description('Default security rules for subnets')
param securityRules array = [
  // Inbound: Allow HTTPS
  {
    name: 'AllowHttpsInbound'
    priority: 100
    direction: 'Inbound'
    access: 'Allow'
    protocol: 'Tcp'
    sourceAddressPrefix: '*'
    sourcePortRange: '*'
    destinationAddressPrefix: '*'
    destinationPortRange: '443'
  }
  // Outbound: Allow Azure services
  {
    name: 'AllowAzureServicesOutbound'
    priority: 200
    direction: 'Outbound'
    access: 'Allow'
    protocol: '*'
    sourceAddressPrefix: '*'
    sourcePortRange: '*'
    destinationAddressPrefix: 'AzureCloud'
    destinationPortRange: '*'
  }
  // Outbound: Deny all
  {
    name: 'DenyAllOutbound'
    priority: 300
    direction: 'Outbound'
    access: 'Deny'
    protocol: '*'
    sourceAddressPrefix: '*'
    sourcePortRange: '*'
    destinationAddressPrefix: '*'
    destinationPortRange: '*'
  }
]

resource vnet 'Microsoft.Network/virtualNetworks@2023-02-01' = {
  name: vnetName
  location: location
  properties: {
    addressSpace: {
      addressPrefixes: addressSpace
    }
    subnets: [for subnet in subnets: {
      name: subnet.name
      properties: {
        addressPrefix: subnet.addressPrefix
        networkSecurityGroup: {
          id: securityGroups[subnet.name].id
        }
      }
    }]
  }
}

resource securityGroups 'Microsoft.Network/networkSecurityGroups@2023-02-01' = [for subnet in subnets: {
  name: '${vnetName}-${subnet.name}-nsg'
  location: location
  properties: {
    securityRules: [for rule in securityRules: {
      name: rule.name
      properties: {
        priority: rule.priority
        direction: rule.direction
        access: rule.access
        protocol: rule.protocol
        sourceAddressPrefix: rule.sourceAddressPrefix
        sourcePortRange: rule.sourcePortRange
        destinationAddressPrefix: rule.destinationAddressPrefix
        destinationPortRange: rule.destinationPortRange
      }
    }]
  }
}]

output vnet resource = vnet
