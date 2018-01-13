Ext.define('WX.view.MemberCard.ModifyCardRemark', {
    extend: 'Ext.window.Window',
    alias: 'widget.ModifyCardRemark',
    title: '修改备注',
    layout: 'fit',
    width: 300,
    bodyPadding: 10,
    modal: true,
    initComponent: function () {
        var me = this;
        me.form = Ext.create('Ext.form.Panel', {
            border: false,
            layout: 'fit',
            items: [{
                xtype: 'textfield',
                name: 'Remark',
                fieldLabel: '备注',
                labelWidth: 60,
                allowBlank: false,
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