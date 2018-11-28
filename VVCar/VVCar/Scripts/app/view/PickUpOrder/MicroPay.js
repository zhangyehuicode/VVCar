Ext.define('WX.view.PickUpOrder.MicroPay', {
    extend: 'Ext.window.Window',
    alias: 'widget.PickUpOrderMicroPay',
    title: '付款码结账',
    layout: 'vbox',
    width: 300,
    height: 200,
    modal: true,
    bodyPadding: 5,
    buttonAlign: 'center',
    initComponent: function () {
        var me = this;
        me.form = Ext.create('Ext.form.Panel', {
            border: false,
            width: '100%',
            fieldDefaults: {
                labelAlign: 'left',
                width: 180,
                labelWidth: 60,
                anchor: '100%',
                flex: 1,
                margin: '5'
            },
            items: [{
                xtype: 'textfield',
                name: 'TotalFee',
                fieldLabel: '支付金额（元）',
                margin: '5 5 5 5',
                readOnly: true
            }, {
                xtype: 'textfield',
                name: 'AuthCode',
                fieldLabel: '付款码',
                margin: '5 5 5 5'
            }]
        });

        me.buttons = [{
            text: '发起支付',
            action: 'confirm',
            cls: 'submitBtn'
        }, {
            text: '取消',
            scope: me,
            handler: me.close
        }];

        me.items = [me.form];

        me.callParent(arguments);
    }
});