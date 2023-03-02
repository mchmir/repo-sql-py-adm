<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.bStart = New System.Windows.Forms.Button
        Me.bStop = New System.Windows.Forms.Button
        Me.tmrStart = New System.Windows.Forms.Timer(Me.components)
        Me.lblInfo = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'bStart
        '
        Me.bStart.Location = New System.Drawing.Point(12, 12)
        Me.bStart.Name = "bStart"
        Me.bStart.Size = New System.Drawing.Size(75, 23)
        Me.bStart.TabIndex = 0
        Me.bStart.Text = "Старт"
        Me.bStart.UseVisualStyleBackColor = True
        '
        'bStop
        '
        Me.bStop.Location = New System.Drawing.Point(93, 12)
        Me.bStop.Name = "bStop"
        Me.bStop.Size = New System.Drawing.Size(75, 23)
        Me.bStop.TabIndex = 1
        Me.bStop.Text = "Стоп"
        Me.bStop.UseVisualStyleBackColor = True
        '
        'tmrStart
        '
        Me.tmrStart.Interval = 600000
        '
        'lblInfo
        '
        Me.lblInfo.Location = New System.Drawing.Point(174, 12)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(493, 23)
        Me.lblInfo.TabIndex = 2
        Me.lblInfo.Text = "Label1"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmMain
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(482, 46)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.bStop)
        Me.Controls.Add(Me.bStart)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximumSize = New System.Drawing.Size(490, 80)
        Me.MinimumSize = New System.Drawing.Size(490, 80)
        Me.Name = "frmMain"
        Me.Text = "Отправка собщений v1710"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents bStart As System.Windows.Forms.Button
    Friend WithEvents bStop As System.Windows.Forms.Button
    Friend WithEvents tmrStart As System.Windows.Forms.Timer
    Friend WithEvents lblInfo As System.Windows.Forms.Label

End Class
