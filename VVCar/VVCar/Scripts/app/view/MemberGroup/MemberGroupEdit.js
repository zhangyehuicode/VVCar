Ext.define('WX.view.MemberGroup.MemberGroupEdit', {
    extend: 'Ext.window.Window',
    alias: 'widget.MemberGroupEdit',
    layout: 'fit',
    width: 300,
    bodyPadding: 5,
    modal: true,
    initComponent: function () {
        var me = this;

        me.form = Ext.create('Ext.form.Panel', {
            border: false,
            trackResetOnLoad: true,
            fieldDefaults: {
                labelAlign: 'left',
                labelWidth: 90,
                anchor: '100%',
            },
            items: [{
                xtype: 'textfield',
                name: 'Code',
                fieldLabel: '编号',
                maxLength: 20,
                allowBlank: false,
            }, {
                xtype: 'textfield',
                name: 'Name',
                fieldLabel: '分组名称',
                maxLength: 16,
                allowBlank: false,
            }, {
                xtype: 'numberfield',
                name: 'Index',
                fieldLabel: '排序',
                minValue: 1
            }, {
                xtype: 'checkboxfield',
                name: 'IsWholesalePrice',
                fieldLabel: '批发价',
                checked: false,
                inputValue: true,
            }]
        });

        me.items = [me.form];
        me.buttons = [{
            text: '保存',
            action: 'save',
            cls: 'submitBtn',
            scope: me,
        }, {
            text: '取消',
            scope: me,
            handler: me.close
        }];
        me.callParent(arguments);
    }
});