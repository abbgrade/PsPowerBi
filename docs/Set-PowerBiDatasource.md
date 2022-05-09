---
external help file: PsPowerBi.dll-Help.xml
Module Name: PsPowerBi
online version:
schema: 2.0.0
---

# Set-PowerBiDatasource

## SYNOPSIS
{{ Fill in the Synopsis }}

## SYNTAX

```
Set-PowerBiDatasource [-Connection <PowerBIClient>] -Datasource <PSObject> [-WhatIf]
 [-Credential <PSCredential>] [-Username <String>] [-Password <SecureString>] [-PrivacyLevel <PrivacyLevel>]
 [-EncryptedConnection <EncryptedConnection>] [<CommonParameters>]
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

### -Credential
{{ Fill Credential Description }}

```yaml
Type: PSCredential
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Datasource
{{ Fill Datasource Description }}

```yaml
Type: PSObject
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -EncryptedConnection
{{ Fill EncryptedConnection Description }}

```yaml
Type: EncryptedConnection
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Password
{{ Fill Password Description }}

```yaml
Type: SecureString
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PrivacyLevel
{{ Fill PrivacyLevel Description }}

```yaml
Type: PrivacyLevel
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Username
{{ Fill Username Description }}

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs.
The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### Microsoft.PowerBI.Api.PowerBIClient

### System.Management.Automation.PSObject

## OUTPUTS

### System.Object
## NOTES

## RELATED LINKS
