Ext.define('WX.view.PickUpOrder.PickUpOrderPaymentDetails', {
    extend: 'Ext.window.Window',
    alias: 'widget.PickUpOrderPaymentDetails',
    title: '支付详情',
    layout: 'fit',
    width: 600,
    height: 500,
    bodyPadding: 5,
    autoShow: false,
    modal: true,
    buttonAlign: 'right',
    initComponent: function () {
        var me = this;
        var store = Ext.create('WX.store.BaseData.PickUpOrderPaymentDetailsStore');
        me.items = [{
            xtype: 'grid',
            name: 'gridPickUpOrderPaymentDetails',
            stripeRows: true,
            loadMask: true,
            store: store,
            columns: [
                //{ header: '订单编号', dataIndex: 'PickUpOrderID', flex: 1 },
                { header: '支付金额', dataIndex: 'PayMoney', flex: 1 },
                {
                    header: '支付类型', dataIndex: 'PayType', flex: 1,
                    renderer: function (value) {
                        if (value === 1)
                            return '<span>微信</span>';
                        else if (value === 2)
                            return '<span>现金</span>';
                        else if (value === 6)
                            return '<span>储值卡</span>';
                        else if (value === 7)
                            return '微信付款码支付';
                        else if (value === 9)
                            return '微信扫码支付';
                    }
                },
                { header: '支付信息', dataIndex: 'PayInfo', flex: 1 },
                { header: '收款店员', dataIndex: 'StaffName', flex: 1 },
                { header: '支付时间', dataIndex: 'CreatedDate', flex: 1 },
            ],
            bbar: {
                xtype: 'pagingtoolbar',
                displayInfo: true,
            },
        }];
        me.buttons = [{
            text: '取消',
            scope: me,
            handler: me.close
        }];
        me.callParent(arguments);
    }
})