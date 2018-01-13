Ext.define('WX.view.Role.RoleEdit', {
    extend: 'Ext.window.Window',
    alias: 'widget.RoleEdit',
    title: '编辑角色',
    layout: 'fit',
    width: 350,
    modal: true,
    bodyPadding: 5,
    initComponent: function () {
        var me = this;
        //var roleTypes = Ext.getStore('RoleTypeDictStore');
        me.form = Ext.create('Ext.form.Panel', {
            border: false,
            trackResetOnLoad: true,
            fieldDefaults: {
                labelAlign: 'left',
                labelWidth: 90,
                anchor: '100%'
            },
            items: [
                 {
                     xtype: 'textfield',
                     name: 'Code',
                     fieldLabel: '角色代码',
                     maxLength: 20,
                     maxLengthText: '角色代码的最大长度为20个字符',
                     blankText: '角色代码不能为空,请输入!',
                     allowBlank: false,

                 },
                 {
                     xtype: 'textfield',
                     name: 'Name',
                     fieldLabel: '角色名称',
                     maxLength: 20,
                     maxLengthText: '角色名称的最大长度为20个字符',
                     blankText: '角色名称不能为空,请输入!',
                     allowBlank: false,

                 },
                 //{
                 //    xtype: 'combobox',
                 //    name: 'RoleType',
                 //    fieldLabel: '角色类型',
                 //    store: roleTypes,
                 //    queryMode: 'local',
                 //    displayField: 'DictName',
                 //    valueField: 'DictValue',
                 //    emptyText: '请选择...',
                 //    blankText: '请选择角色类型',
                 //    allowBlank: false,
                 //},
            ]
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