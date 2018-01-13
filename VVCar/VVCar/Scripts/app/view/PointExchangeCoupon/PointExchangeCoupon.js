Ext.define('WX.view.PointExchangeCoupon.PointExchangeCoupon', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.PointExchangeCoupon',
    title: '领券中心',
    store: Ext.create('WX.store.BaseData.PointExchangeCouponStore'),
    stripeRows: true,
    loadMask: true,
    closable: true,
    initComponent: function () {
        var me = this;
        me.tbar = [
            {
                action: 'AddExchangeSetting',
                xtype: 'button',
                text: '添加兑换设置',
                scope: me,
                iconCls: 'fa fa-plus-circle'
            },
            {
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
                    name: 'search',
                    fieldLabel: '',
                    width: 170,
                    labelWidth: 60,
                    emptyText: '标题/模板编号',
                    margin: '0 0 0 5'
                }, {
                    action: 'search',
                    xtype: 'button',
                    text: '搜索',
                    iconCls: 'fa fa-search',
                    cls: 'submitBtn',
                    margin: '0 0 0 10'
                }, {
                    action: 'reflash',
                    xtype: 'button',
                    text: '刷新',
                    iconCls: 'fa fa-refresh',
                    cls: '',
                    margin: '0 0 0 10'
                }]
            }];
        me.columns = [
            { header: '序号', xtype: 'rownumberer', width: 50, align: 'left' },
            { header: '优惠券模板编号', dataIndex: 'TemplateCode', width: 150, },
            { header: ' 券标题', dataIndex: 'CouponTitle', flex: 1 },
            { header: '投放日期', dataIndex: 'PutInDate', flex: 1 },
            { header: '兑换条件', dataIndex: 'ExchangePremise', width: 120 },
            { header: '兑换有效期', dataIndex: 'DateStr', flex: 1 },
            { header: '兑换次数', dataIndex: 'ExchangeCount', width: 100 },
            //{ dataIndex: 'ExchangeType', hidden: true },
            { header: '发布时间', dataIndex: 'CreateDate', flex: 1, xtype: 'datecolumn', format: 'Y-m-d H:i:s', },
            {
                xtype: 'actioncolumn',
                width: 80,
                sortable: false,
                menuDisabled: true,
                height: 30,
                align: 'center',
                text: '操作',
                items: [{
                    action: 'editItem',
                    iconCls: 'x-fa fa-pencil',
                    tooltip: '编辑',
                    scope: this,
                    margin: '10 10 10 10',
                    handler: function (grid, rowIndex, colIndex) {
                        var record = grid.getStore().getAt(rowIndex);
                        this.fireEvent('editPointExchangePoupon', grid, record);
                    },
                },
                { scope: this }, {
                    action: 'deleteItem',
                    iconCls: 'x-fa fa-close',
                    tooltip: '删除',
                    scope: this,
                    margin: '10 10 10 10',
                    handler: function (grid, rowIndex, colIndex) {
                        var record = grid.getStore().getAt(rowIndex);
                        this.fireEvent('deleteActionClick', grid, record);
                    },
                }]
            }
        ];
        me.dockedItems = [{
            xtype: 'pagingtoolbar',
            store: me.store,
            dock: 'bottom',
            displayInfo: true
        }];
        this.callParent();
    },

    afterRender: function () {
        this.callParent(arguments);
        this.getStore().proxy.extraParams = {};
        this.getStore().load();
    }
});