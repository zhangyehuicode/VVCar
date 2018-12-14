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
                        switch (value) {
                            case 1:
                                return '微信';
                                break;
                            case 2:
                                return '现金';
                                break;
                            case 3:
                                return '会员卡';
                                break;
                            case 4:
                                return '优惠券';
                                break;
                            case 5:
                                return '车比特';
                                break;
                            case 6:
                                return '储值卡';
                                break;
                            case 7:
                                return '微信付款码支付';
                                break;
                            case 8:
                                return '公众号支付';
                                break;
                            case 9:
                                return '微信扫码支付';
                                break;
                            default:
                                return '其他';
                        }
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