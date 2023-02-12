# Para que nos pida login

az login

# Establecemos la suscripción sobre la que vamos a estar trabajando

az account set -s "Pago por uso"

# Declaro variables

resourceGroup="pluralsightfuncs"
location="westeurope"
storageAccount="pluralsightfuncsjrn"
appInsights="pluralsightfuncsjrn"
functionAppName="pluralsightfuncsjrn"

# Creo el grupo de recursos

az group create -n $resourceGroup -l $location

# Creo la cuenta de almacenamiento
az storage account create \
    -n $storageAccount \
    -l $location \
    -g $resourceGroup \
    --sku Standard_LRS

# Creo Application Insights
az monitor app-insights component create \
    --app $appInsights \
    --location $location \
    --kind web \
    --resource-group $resourceGroup \
    --application-type web

# Creo la Azure Function
az functionapp create \
    -n $functionAppName \
    -g $resourceGroup \
    --storage-account $storageAccount \
    --app-insights $appInsights \
    --consumption-plan-location $location \
    --functions-version 4 \
    --runtime dotnet

# Establecer configuraciones (variables de entorno) para la function app
az functionapp config appsettings set \
    -n $functionAppName -g $resourceGroup \
    --settings "MySetting1=Hello" "MySetting2=World"