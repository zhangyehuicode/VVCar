Ext.define('WX.view.MemberCardType.MemberCardTypeEdit', {
    extend: 'Ext.window.Window',
    alias: 'widget.MemberCardTypeEdit',
    title: '编辑卡片类型',
    layout: 'fit',
    width: 400,
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
                name: 'Name',
                fieldLabel: '卡片名称',
                maxLength: 20,
                maxLengthText: '卡片名称的最大长度为20个字符',
                blankText: '卡片名称不能为空,请输入!',
                allowBlank: false,
            }, {
                xtype: 'combobox',
                name: 'AllowStoreActivate',
                fieldLabel: '允许门店激活',
                store: Ext.getStore('DataDict.YesNoTypeStore'),
                displayField: 'DictName',
                valueField: 'DictValue',
                editable: false,
                allowBlank: false,
                value: false
            }, {
                xtype: 'combobox',
                name: 'AllowDiscount',
                fieldLabel: '允许折扣',
                store: Ext.getStore('DataDict.YesNoTypeStore'),
                displayField: 'DictName',
                valueField: 'DictValue',
                editable: false,
                allowBlank: false,
                value: false
            }, {
                xtype: 'combobox',
                name: 'AllowRecharge',
                fieldLabel: '允许储值',
                store: Ext.getStore('DataDict.YesNoTypeStore'),
                displayField: 'DictName',
                valueField: 'DictValue',
                editable: false,
                allowBlank: false,
                value: false
            }, {
                xtype: 'fieldcontainer',
                fieldLabel: '储值余额上限',
                combineErrors: false,
                layout: 'hbox',
                items: [{
                    name: 'MaxRecharge',
                    xtype: 'numberfield',
                    width: 165,
                    minValue: 0,
                    hideLabel: true,
                    readOnly: true
                }, {
                    xtype: 'displayfield',
                    value: '元'
                }]
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