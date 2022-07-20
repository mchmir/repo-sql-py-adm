CREATE TABLE [dbo].[Temp_Sad] (
  [Number] [float] NULL,
  [FIO] [nvarchar](255) NULL,
  [Dolznost] [nvarchar](255) NULL,
  [Street] [nvarchar](255) NULL,
  [House] [nvarchar](255) NULL,
  [Flat] [nvarchar](255) NULL,
  [WorkPlace] [nvarchar](255) NULL
)
ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty N'Attributes', N'0', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad'
GO

EXEC sys.sp_addextendedproperty N'DateCreated', N'24.10.2012 9:47:06', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad'
GO

EXEC sys.sp_addextendedproperty N'DisplayViewsOnSharePointSite', N'1', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad'
GO

EXEC sys.sp_addextendedproperty N'FilterOnLoad', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad'
GO

EXEC sys.sp_addextendedproperty N'HideNewField', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad'
GO

EXEC sys.sp_addextendedproperty N'LastUpdated', N'24.10.2012 9:47:48', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad'
GO

EXEC sys.sp_addextendedproperty N'MS_DefaultView', N'2', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad'
GO

EXEC sys.sp_addextendedproperty N'MS_OrderByOn', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad'
GO

EXEC sys.sp_addextendedproperty N'MS_Orientation', N'0', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad'
GO

EXEC sys.sp_addextendedproperty N'Name', N'Temp_Sad', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad'
GO

EXEC sys.sp_addextendedproperty N'OrderByOnLoad', N'True', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad'
GO

EXEC sys.sp_addextendedproperty N'RecordCount', N'659', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad'
GO

EXEC sys.sp_addextendedproperty N'TotalsRow', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad'
GO

EXEC sys.sp_addextendedproperty N'Updatable', N'True', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad'
GO

EXEC sys.sp_addextendedproperty N'AggregateType', N'-1', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Number'
GO

EXEC sys.sp_addextendedproperty N'AllowZeroLength', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Number'
GO

EXEC sys.sp_addextendedproperty N'AppendOnly', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Number'
GO

EXEC sys.sp_addextendedproperty N'Attributes', N'2', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Number'
GO

EXEC sys.sp_addextendedproperty N'CollatingOrder', N'1033', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Number'
GO

EXEC sys.sp_addextendedproperty N'ColumnHidden', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Number'
GO

EXEC sys.sp_addextendedproperty N'ColumnOrder', N'1', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Number'
GO

EXEC sys.sp_addextendedproperty N'ColumnWidth', N'-1', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Number'
GO

EXEC sys.sp_addextendedproperty N'DataUpdatable', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Number'
GO

EXEC sys.sp_addextendedproperty N'MS_DecimalPlaces', N'255', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Number'
GO

EXEC sys.sp_addextendedproperty N'MS_DisplayControl', N'109', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Number'
GO

EXEC sys.sp_addextendedproperty N'MS_Format', N'General Number', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Number'
GO

EXEC sys.sp_addextendedproperty N'Name', N'Number', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Number'
GO

EXEC sys.sp_addextendedproperty N'OrdinalPosition', N'0', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Number'
GO

EXEC sys.sp_addextendedproperty N'Required', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Number'
GO

EXEC sys.sp_addextendedproperty N'Size', N'8', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Number'
GO

EXEC sys.sp_addextendedproperty N'SourceField', N'Number', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Number'
GO

EXEC sys.sp_addextendedproperty N'SourceTable', N'Temp_Sad', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Number'
GO

EXEC sys.sp_addextendedproperty N'TextAlign', N'0', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Number'
GO

EXEC sys.sp_addextendedproperty N'Type', N'7', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Number'
GO

EXEC sys.sp_addextendedproperty N'AggregateType', N'-1', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'FIO'
GO

EXEC sys.sp_addextendedproperty N'AllowZeroLength', N'True', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'FIO'
GO

EXEC sys.sp_addextendedproperty N'AppendOnly', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'FIO'
GO

EXEC sys.sp_addextendedproperty N'Attributes', N'2', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'FIO'
GO

EXEC sys.sp_addextendedproperty N'CollatingOrder', N'1033', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'FIO'
GO

EXEC sys.sp_addextendedproperty N'ColumnHidden', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'FIO'
GO

EXEC sys.sp_addextendedproperty N'ColumnOrder', N'2', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'FIO'
GO

EXEC sys.sp_addextendedproperty N'ColumnWidth', N'-1', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'FIO'
GO

EXEC sys.sp_addextendedproperty N'DataUpdatable', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'FIO'
GO

EXEC sys.sp_addextendedproperty N'MS_DisplayControl', N'109', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'FIO'
GO

EXEC sys.sp_addextendedproperty N'MS_Format', N'@', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'FIO'
GO

EXEC sys.sp_addextendedproperty N'MS_IMEMode', N'0', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'FIO'
GO

EXEC sys.sp_addextendedproperty N'MS_IMESentMode', N'3', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'FIO'
GO

EXEC sys.sp_addextendedproperty N'Name', N'FIO', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'FIO'
GO

EXEC sys.sp_addextendedproperty N'OrdinalPosition', N'1', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'FIO'
GO

EXEC sys.sp_addextendedproperty N'Required', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'FIO'
GO

EXEC sys.sp_addextendedproperty N'Size', N'255', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'FIO'
GO

EXEC sys.sp_addextendedproperty N'SourceField', N'FIO', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'FIO'
GO

EXEC sys.sp_addextendedproperty N'SourceTable', N'Temp_Sad', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'FIO'
GO

EXEC sys.sp_addextendedproperty N'TextAlign', N'0', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'FIO'
GO

EXEC sys.sp_addextendedproperty N'Type', N'10', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'FIO'
GO

EXEC sys.sp_addextendedproperty N'UnicodeCompression', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'FIO'
GO

EXEC sys.sp_addextendedproperty N'AggregateType', N'-1', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Dolznost'
GO

EXEC sys.sp_addextendedproperty N'AllowZeroLength', N'True', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Dolznost'
GO

EXEC sys.sp_addextendedproperty N'AppendOnly', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Dolznost'
GO

EXEC sys.sp_addextendedproperty N'Attributes', N'2', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Dolznost'
GO

EXEC sys.sp_addextendedproperty N'CollatingOrder', N'1033', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Dolznost'
GO

EXEC sys.sp_addextendedproperty N'ColumnHidden', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Dolznost'
GO

EXEC sys.sp_addextendedproperty N'ColumnOrder', N'3', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Dolznost'
GO

EXEC sys.sp_addextendedproperty N'ColumnWidth', N'-1', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Dolznost'
GO

EXEC sys.sp_addextendedproperty N'DataUpdatable', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Dolznost'
GO

EXEC sys.sp_addextendedproperty N'MS_DisplayControl', N'109', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Dolznost'
GO

EXEC sys.sp_addextendedproperty N'MS_Format', N'@', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Dolznost'
GO

EXEC sys.sp_addextendedproperty N'MS_IMEMode', N'0', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Dolznost'
GO

EXEC sys.sp_addextendedproperty N'MS_IMESentMode', N'3', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Dolznost'
GO

EXEC sys.sp_addextendedproperty N'Name', N'Dolznost', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Dolznost'
GO

EXEC sys.sp_addextendedproperty N'OrdinalPosition', N'2', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Dolznost'
GO

EXEC sys.sp_addextendedproperty N'Required', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Dolznost'
GO

EXEC sys.sp_addextendedproperty N'Size', N'255', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Dolznost'
GO

EXEC sys.sp_addextendedproperty N'SourceField', N'Dolznost', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Dolznost'
GO

EXEC sys.sp_addextendedproperty N'SourceTable', N'Temp_Sad', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Dolznost'
GO

EXEC sys.sp_addextendedproperty N'TextAlign', N'0', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Dolznost'
GO

EXEC sys.sp_addextendedproperty N'Type', N'10', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Dolznost'
GO

EXEC sys.sp_addextendedproperty N'UnicodeCompression', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Dolznost'
GO

EXEC sys.sp_addextendedproperty N'AggregateType', N'-1', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Street'
GO

EXEC sys.sp_addextendedproperty N'AllowZeroLength', N'True', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Street'
GO

EXEC sys.sp_addextendedproperty N'AppendOnly', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Street'
GO

EXEC sys.sp_addextendedproperty N'Attributes', N'2', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Street'
GO

EXEC sys.sp_addextendedproperty N'CollatingOrder', N'1033', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Street'
GO

EXEC sys.sp_addextendedproperty N'ColumnHidden', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Street'
GO

EXEC sys.sp_addextendedproperty N'ColumnOrder', N'4', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Street'
GO

EXEC sys.sp_addextendedproperty N'ColumnWidth', N'-1', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Street'
GO

EXEC sys.sp_addextendedproperty N'DataUpdatable', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Street'
GO

EXEC sys.sp_addextendedproperty N'MS_DisplayControl', N'109', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Street'
GO

EXEC sys.sp_addextendedproperty N'MS_Format', N'@', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Street'
GO

EXEC sys.sp_addextendedproperty N'MS_IMEMode', N'0', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Street'
GO

EXEC sys.sp_addextendedproperty N'MS_IMESentMode', N'3', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Street'
GO

EXEC sys.sp_addextendedproperty N'Name', N'Street', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Street'
GO

EXEC sys.sp_addextendedproperty N'OrdinalPosition', N'3', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Street'
GO

EXEC sys.sp_addextendedproperty N'Required', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Street'
GO

EXEC sys.sp_addextendedproperty N'Size', N'255', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Street'
GO

EXEC sys.sp_addextendedproperty N'SourceField', N'Street', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Street'
GO

EXEC sys.sp_addextendedproperty N'SourceTable', N'Temp_Sad', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Street'
GO

EXEC sys.sp_addextendedproperty N'TextAlign', N'0', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Street'
GO

EXEC sys.sp_addextendedproperty N'Type', N'10', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Street'
GO

EXEC sys.sp_addextendedproperty N'UnicodeCompression', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Street'
GO

EXEC sys.sp_addextendedproperty N'AggregateType', N'-1', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'House'
GO

EXEC sys.sp_addextendedproperty N'AllowZeroLength', N'True', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'House'
GO

EXEC sys.sp_addextendedproperty N'AppendOnly', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'House'
GO

EXEC sys.sp_addextendedproperty N'Attributes', N'2', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'House'
GO

EXEC sys.sp_addextendedproperty N'CollatingOrder', N'1033', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'House'
GO

EXEC sys.sp_addextendedproperty N'ColumnHidden', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'House'
GO

EXEC sys.sp_addextendedproperty N'ColumnOrder', N'5', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'House'
GO

EXEC sys.sp_addextendedproperty N'ColumnWidth', N'-1', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'House'
GO

EXEC sys.sp_addextendedproperty N'DataUpdatable', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'House'
GO

EXEC sys.sp_addextendedproperty N'MS_DisplayControl', N'109', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'House'
GO

EXEC sys.sp_addextendedproperty N'MS_Format', N'@', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'House'
GO

EXEC sys.sp_addextendedproperty N'MS_IMEMode', N'0', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'House'
GO

EXEC sys.sp_addextendedproperty N'MS_IMESentMode', N'3', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'House'
GO

EXEC sys.sp_addextendedproperty N'Name', N'House', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'House'
GO

EXEC sys.sp_addextendedproperty N'OrdinalPosition', N'4', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'House'
GO

EXEC sys.sp_addextendedproperty N'Required', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'House'
GO

EXEC sys.sp_addextendedproperty N'Size', N'255', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'House'
GO

EXEC sys.sp_addextendedproperty N'SourceField', N'House', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'House'
GO

EXEC sys.sp_addextendedproperty N'SourceTable', N'Temp_Sad', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'House'
GO

EXEC sys.sp_addextendedproperty N'TextAlign', N'0', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'House'
GO

EXEC sys.sp_addextendedproperty N'Type', N'10', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'House'
GO

EXEC sys.sp_addextendedproperty N'UnicodeCompression', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'House'
GO

EXEC sys.sp_addextendedproperty N'AggregateType', N'-1', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Flat'
GO

EXEC sys.sp_addextendedproperty N'AllowZeroLength', N'True', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Flat'
GO

EXEC sys.sp_addextendedproperty N'AppendOnly', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Flat'
GO

EXEC sys.sp_addextendedproperty N'Attributes', N'2', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Flat'
GO

EXEC sys.sp_addextendedproperty N'CollatingOrder', N'1033', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Flat'
GO

EXEC sys.sp_addextendedproperty N'ColumnHidden', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Flat'
GO

EXEC sys.sp_addextendedproperty N'ColumnOrder', N'6', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Flat'
GO

EXEC sys.sp_addextendedproperty N'ColumnWidth', N'-1', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Flat'
GO

EXEC sys.sp_addextendedproperty N'DataUpdatable', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Flat'
GO

EXEC sys.sp_addextendedproperty N'MS_DisplayControl', N'109', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Flat'
GO

EXEC sys.sp_addextendedproperty N'MS_Format', N'@', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Flat'
GO

EXEC sys.sp_addextendedproperty N'MS_IMEMode', N'0', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Flat'
GO

EXEC sys.sp_addextendedproperty N'MS_IMESentMode', N'3', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Flat'
GO

EXEC sys.sp_addextendedproperty N'Name', N'Flat', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Flat'
GO

EXEC sys.sp_addextendedproperty N'OrdinalPosition', N'5', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Flat'
GO

EXEC sys.sp_addextendedproperty N'Required', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Flat'
GO

EXEC sys.sp_addextendedproperty N'Size', N'255', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Flat'
GO

EXEC sys.sp_addextendedproperty N'SourceField', N'Flat', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Flat'
GO

EXEC sys.sp_addextendedproperty N'SourceTable', N'Temp_Sad', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Flat'
GO

EXEC sys.sp_addextendedproperty N'TextAlign', N'0', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Flat'
GO

EXEC sys.sp_addextendedproperty N'Type', N'10', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Flat'
GO

EXEC sys.sp_addextendedproperty N'UnicodeCompression', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'Flat'
GO

EXEC sys.sp_addextendedproperty N'AggregateType', N'-1', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'WorkPlace'
GO

EXEC sys.sp_addextendedproperty N'AllowZeroLength', N'True', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'WorkPlace'
GO

EXEC sys.sp_addextendedproperty N'AppendOnly', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'WorkPlace'
GO

EXEC sys.sp_addextendedproperty N'Attributes', N'2', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'WorkPlace'
GO

EXEC sys.sp_addextendedproperty N'CollatingOrder', N'1033', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'WorkPlace'
GO

EXEC sys.sp_addextendedproperty N'ColumnHidden', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'WorkPlace'
GO

EXEC sys.sp_addextendedproperty N'ColumnOrder', N'7', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'WorkPlace'
GO

EXEC sys.sp_addextendedproperty N'ColumnWidth', N'-1', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'WorkPlace'
GO

EXEC sys.sp_addextendedproperty N'DataUpdatable', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'WorkPlace'
GO

EXEC sys.sp_addextendedproperty N'MS_DisplayControl', N'109', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'WorkPlace'
GO

EXEC sys.sp_addextendedproperty N'MS_Format', N'@', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'WorkPlace'
GO

EXEC sys.sp_addextendedproperty N'MS_IMEMode', N'0', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'WorkPlace'
GO

EXEC sys.sp_addextendedproperty N'MS_IMESentMode', N'3', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'WorkPlace'
GO

EXEC sys.sp_addextendedproperty N'Name', N'WorkPlace', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'WorkPlace'
GO

EXEC sys.sp_addextendedproperty N'OrdinalPosition', N'6', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'WorkPlace'
GO

EXEC sys.sp_addextendedproperty N'Required', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'WorkPlace'
GO

EXEC sys.sp_addextendedproperty N'Size', N'255', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'WorkPlace'
GO

EXEC sys.sp_addextendedproperty N'SourceField', N'WorkPlace', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'WorkPlace'
GO

EXEC sys.sp_addextendedproperty N'SourceTable', N'Temp_Sad', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'WorkPlace'
GO

EXEC sys.sp_addextendedproperty N'TextAlign', N'0', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'WorkPlace'
GO

EXEC sys.sp_addextendedproperty N'Type', N'10', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'WorkPlace'
GO

EXEC sys.sp_addextendedproperty N'UnicodeCompression', N'False', 'SCHEMA', N'dbo', 'TABLE', N'Temp_Sad', 'COLUMN', N'WorkPlace'
GO