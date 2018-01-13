Ext.define('WX.view.Permission.PermissionEdit', {
    extend: 'Ext.window.Window',
    alias: 'widget.PermissionEdit',
    title: '编辑权限',
    layout: 'fit',
    width: 350,
    modal: true,
    bodyPadding: 5,
    initComponent: function () {
        var me = this;
        me.form = Ext.create('Ext.form.Panel', {
            border: false,
            trackResetOnLoad: true,
            fieldDefaults: {
                labelAlign: 'left',
                labelWidth: 90,
                anchor: '100%'
            },
            items: [{
                xtype: 'textfield',
                name: 'Code',
                fieldLabel: '权限代码',
                maxLength: 200,
                maxLengthText: '权限代码的最大长度为20个字符',
                blankText: '权限代码不能为空,请输入!',
                allowBlank: false,
            }, {
                xtype: 'textfield',
                name: 'Name',
                fieldLabel: '权限名称',
                maxLength: 20,
                maxLengthText: '权限名称的最大长度为20个字符',
                blankText: '权限名称不能为空,请输入!',
                allowBlank: false,
            }, {
                xtype: 'combobox',
                name: 'PermissionType',
                fieldLabel: '权限类型',
                store: Ext.getStore('DataDict.PermissionTypeStore'),
                queryMode: 'local',
                displayField: 'DictName',
                valueField: 'DictValue',
                emptyText: '请选择...',
                blankText: '请选择权限类型',
                editable: false,
                allowBlank: false,
            }, {
                xtype: 'combobox',
                name: 'IsAvailable',
                fieldLabel: '启用',
                store: Ext.getStore('DataDict.EnableDisableTypeStore'),
                queryMode: 'local',
                displayField: 'DictName',
                valueField: 'DictValue',
                emptyText: '请选择...',
                blankText: '请选择是否启用',
                editable: false,
                allowBlank: false,
                value: true,
            }]
        });

        me.items = [me.form];
        me.buttons = [
            {
                text: '保存',
                cls: 'submitBtn',
                action: 'save'
            },
            {
                text: '取消',
                scope: me,
                handler: me.close
            }
        ];
        me.callParent(arguments);
    }
});