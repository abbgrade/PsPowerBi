---
external help file: PsPowerBi.dll-Help.xml
Module Name: PsPowerBi
online version:
schema: 2.0.0
---

# Connect-PowerBiService

## SYNOPSIS
{{ Fill in the Synopsis }}

## SYNTAX

### Properties_IntegratedSecurity (Default)
```
Connect-PowerBiService -ClientId <Guid> -TenantId <Guid> [<CommonParameters>]
```

### Properties_Credential
```
Connect-PowerBiService -ClientId <Guid> -TenantId <Guid> -Username <String> -Password <SecureString>
 [<CommonParameters>]
```

### Properties_InteractiveAuthentication
```
Connect-PowerBiService -ClientId <Guid> -TenantId <Guid> [-InteractiveAuthentication] [<CommonParameters>]
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

### -ClientId
{{ Fill ClientID Description }}

```yaml
Type: Guid
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InteractiveAuthentication
{{ Fill InteractiveAuthentication Description }}

```yaml
Type: SwitchParameter
Parameter Sets: Properties_InteractiveAuthentication
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Password
{{ Fill Password Description }}

```yaml
Type: SecureString
Parameter Sets: Properties_Credential
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -TenantId
{{ Fill TenantId Description }}

```yaml
Type: Guid
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Username
{{ Fill Username Description }}

```yaml
Type: String
Parameter Sets: Properties_Credential
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

### System.Security.SecureString

## OUTPUTS

### Microsoft.PowerBI.Api.PowerBIClient

## NOTES

## RELATED LINKS
