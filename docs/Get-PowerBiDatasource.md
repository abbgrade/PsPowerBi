---
external help file: PsPowerBi.dll-Help.xml
Module Name: PsPowerBi
online version:
schema: 2.0.0
---

# Get-PowerBiDatasource

## SYNOPSIS
{{ Fill in the Synopsis }}

## SYNTAX

### ByDataset
```
Get-PowerBiDatasource [-Connection <PowerBIClient>] -Dataset <Dataset> [<CommonParameters>]
```

### ByGateway
```
Get-PowerBiDatasource [-Connection <PowerBIClient>] -Gateway <Gateway> [<CommonParameters>]
```

## DESCRIPTION
{{ Fill in the Description }}

## EXAMPLES

### Example 1
```powershell
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -Connection
{{ Fill Connection Description }}

```yaml
Type: PowerBIClient
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Dataset
{{ Fill Dataset Description }}

```yaml
Type: Dataset
Parameter Sets: ByDataset
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -Gateway
{{ Fill Gateway Description }}

```yaml
Type: Gateway
Parameter Sets: ByGateway
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### Microsoft.PowerBI.Api.PowerBIClient

### Microsoft.PowerBI.Api.Models.Dataset

### Microsoft.PowerBI.Api.Models.Gateway

## OUTPUTS

### Microsoft.PowerBI.Api.Models.Datasource

## NOTES

## RELATED LINKS
