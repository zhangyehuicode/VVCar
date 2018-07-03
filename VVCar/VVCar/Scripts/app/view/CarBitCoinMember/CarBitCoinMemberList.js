Ext.define('WX.view.CarBitCoinMember.CarBitCoinMemberList', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.CarBitCoinMemberList',
    title: '商户管理',
    name: 'gridMerchant',
    store: Ext.create('WX.store.BaseData.CarBitCoinMemberStore'),
    stripeRows: true,
    loadMask: true,
    closable: true,
    viewConfig: { enableTextSelection: true },
    selType: 'checkboxmodel',
    selModel: {
        selection: 'rowmodel',
        mode: 'single'
    },
    initComponent: function () {
        var me = this;
        me.tbar = [
            {
                action: 'giveAwayCarBitCoin',
                xtype: 'button',
                text: '赠送车比特',
                scope: this,
                iconCls: 'fa fa-plus-circle',
            }, {
                xtype: 'form',
                layout: 'column',
                border: false,
                frame: false,
                labelAlign: 'left',
                buttonAlign: 'right',
                labelWidth: 100,
                padding: 5,
                autoWidth: true,
                autoScroll: false,
                columnWidth: 1,
                items: [{
                    xtype: 'textfield',
                    name: 'MobilePhoneNo',
                    fieldLabel: '电话号码',
                    width: 170,
                    labelWidth: 60,
                    margin: '0 0 0 5',
                }, {
                    action: 'search',
                    xtype: 'button',
                    text: '搜索',
                    iconCls: 'fa fa-search',
                    cls: 'submitBtn',
                    margin: '0 0 0 5',
                }, {
                    action: 'export',
                    xtype: 'button',
                    text: '导出',
                    iconCls: '',
                    margin: '0 0 0 5',
                }]
            },
        ];
        me.columns = [
            { header: '姓名', dataIndex: 'Name', flex: 1 },
            { header: '电话号码', dataIndex: 'MobilePhoneNo', flex: 1 },
            { header: '性别', dataIndex: 'Sex', flex: 1 },
            { header: '马力', dataIndex: 'Horsepower', flex: 1 },
            { header: '车比特', dataIndex: 'CarBitCoin', flex: 1 },
            {
                header: '冻结币', dataIndex: 'FrozenCoin', flex: 1,
                renderer: function (value) {
                    if (value > 0)
                        return '<span style="color:red;">' + value + '</span>';
                }
            },
            { header: '创建时间', dataIndex: 'CreatedDate', flex: 1 },
        ];
        me.dockedItems = [{
            xtype: 'pagingtoolbar',
            store: me.store,
            dock: 'bottom',
            displayInfo: true
        }];
        me.callParent();
    },
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
        me.getStore().load();
    }
});