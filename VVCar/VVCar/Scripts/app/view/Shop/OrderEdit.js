Ext.define('WX.view.Shop.OrderEdit', {
    extend: 'Ext.window.Window',
    alias: 'widget.OrderEdit',
    title: '',
    layout: 'fit',
    width: 350,
    modal: true,
    bodyPadding: 5,
    initComponent: function () {
        var me = this;
        var statusStore = Ext.create('Ext.data.Store', {
            fields: ['Name', 'Value'],
            data: [
                { 'Name': '未付款', 'Value': 0 },
                { 'Name': '已发货', 'Value': 2 },
                { 'Name': '已完成', 'Value': 3 },
                { 'Name': '已付款未发货', 'Value': 1 },
                { 'Name': '付款不足', 'Value': -1 },
            ]
        });
        me.form = Ext.create('Ext.form.Panel', {
            border: false,
            trackResetOnLoad: true,
            fieldDefaults: {
                labelAlign: 'left',
                labelWidth: 70,
                anchor: '100%'
            },
            items: [{
                xtype: 'textfield',
                name: 'ExpressNumber',
                fieldLabel: '快递单号',
                maxLength: 50,
                allowBlank: false,
            }, {
                xtype: 'combobox',
                name: 'Status',
                store: statusStore,
                displayField: 'Name',
                valueField: 'Value',
                fieldLabel: '发货状态',
                editable: false,
                value: false,
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