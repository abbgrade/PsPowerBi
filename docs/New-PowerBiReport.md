---
external help file: PsPowerBi.dll-Help.xml
Module Name: PsPowerBi
online version:
schema: 2.0.0
---

# New-PowerBiReport

## SYNOPSIS
{{ Fill in the Synopsis }}

## SYNTAX

```
New-PowerBiReport [-Connection <PowerBIClient>] [-Workspace <Group>] [-Dataset <Dataset>] -PbixFile <FileInfo>
 [-Name <String>] [-ImportConflictHandlerMode <ImportConflictHandlerMode>] [<CommonParameters>]
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
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ImportConflictHandlerMode
{{ Fill ImportConflictHandlerMode Description }}

```yaml
Type: ImportConflictHandlerMode
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
{{ Fill Name Description }}

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

### -PbixFile
{{ Fill PbixFile Description }}

```yaml
Type: FileInfo
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Workspace
{{ Fill Workspace Description }}

```yaml
Type: Group
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### Microsoft.PowerBI.Api.PowerBIClient

### Microsoft.PowerBI.Api.Models.Group

## OUTPUTS

### Microsoft.PowerBI.Api.Models.Report

## NOTES

## RELATED LINKS
