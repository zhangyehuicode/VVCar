Ext.define('WX.view.CarBitCoin.StockOutInEdit', {
    extend: 'Ext.window.Window',
    alias: 'widget.StockOutInEdit',
    title: '出/入库',
    layout: 'fit',
    width: 280,
    modal: true,
    bodyPadding: 5,
    initComponent: function() {
        var me = this;
        var stocktype = Ext.create('Ext.data.Store', {
            fields: ['Name', 'Value'],
            data: [
                { 'Name': '出库', 'Value': true },
                { 'Name': '入库', 'Value': false },
            ]
        });
        me.form = Ext.create('Ext.form.Panel', {
            border: false,
            trackResetOnLoad: true,
            fieldDefaults: {
                labelAlign: 'left',
                labelWidth: 60,
                anchor: '100%'
            },
            items: [{
                xtype: 'textfield',
                name: 'ID',
                fieldLabel: '产品ID',
                hidden: true,
            }, {
                xtype: 'textfield',
                name: 'Name',
                fieldLabel: '产品名称',
                disabled: true,
            }, {
                xtype: 'textfield',
                name: 'Stock',
                fieldLabel: '库存',
                disabled: true,
            }, {
                xtype: 'textfield',
                name: 'Unit',
                fieldLabel: '产品单位',
                hidden: true,
            }, {
                xtype: 'combobox',
                name: 'StockType',
                fieldLabel: '类型',
                queryMode: 'local',
                store: stocktype,
                displayField: 'Name',
                valueField: 'Value',
                anchor: '',
                editable: false,
                value: true,
                width: '100%',
            }, {
                xtype: 'numberfield',
                name: 'Stock',
                fieldLabel: '数量',
                minValue: 1,
                allowBlank: false,
                value: 1,
            }, {
                xtype: 'textarea',
                name: 'Reason',
                fieldLabel: '备注',
                maxLength: 50,
                allowBlank: true,
            }]
        });
        me.items = [me.form];
        me.buttons = [{
            text: '保存',
            cls: 'submitBtn',
            action: 'save'
        }, {
            text: '取消',
            scope: me,
            handler: me.close
        }];
        me.callParent(arguments);
    }
});