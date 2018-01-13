Ext.define('WX.view.RechargePlan.RechargePlanCoupon', {
    extend: 'Ext.window.Window',
    alias: 'widget.RechargePlanCoupon',
    title: '优惠券',
    layout: 'fit',
    width: 800,
    height: 500,
    modal: true,
    bodyPadding: 5,
    initComponent: function () {
        var me = this;
        var couponTypeStore = Ext.create('WX.store.DataDict.CouponTypeStore');
        var store = Ext.create('WX.store.BaseData.ValidCouponTemplateStore');
        store.load();
        me.items = [{
            xtype: 'grid',
            store: store,
            selType: 'checkboxmodel',
            tbar: [{
                xtype: 'form',
                layout: 'column',
                border: false,
                frame: false,
                labelAlign: 'left',
                buttonAlign: 'right',
                labelWidth: 100,
                padding: 5,
                autoWidth: true,
                autoScroll: true,
                columnWidth: 1,
                items: [{
                    xtype: 'textfield',
                    name: 'Title',
                    fieldLabel: '标题',
                    width: 170,
                    labelWidth: 40,
                    margin: '0 0 0 5',
                }, {
                    action: 'search',
                    xtype: 'button',
                    text: '搜 索',
                    iconCls: 'fa fa-search',
                    cls: 'submitBtn',
                    margin: '0 0 0 5',
                }]
            }],
            columns: [
                { header: '类型', dataIndex: 'CouponTypeName', flex: 1 },
                { header: '标题', dataIndex: 'Title', flex: 1 },
                { header: '有效期', dataIndex: 'Validity', width: 200 },
                { header: '状态', dataIndex: 'AproveStatusText', flex: 1 },
                { header: '库存', dataIndex: 'FreeStock', width: 100 },
            ],
            dockedItems: [{
                xtype: 'pagingtoolbar',
                store: store,
                dock: 'bottom',
                displayInfo: true
            }]
        }];

        me.buttons = [{
            text: '确定',
            cls: 'submitBtn',
            action: 'confirm'
        }, {
            text: '取消',
            scope: me,
            handler: me.close
        }];

        this.callParent();
    }
});