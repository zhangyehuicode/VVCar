Ext.define('WX.view.AppletCard.AppletCardSelector', {
    extend: 'Ext.window.Window',
    alias: 'widget.AppletCardSelector',
    title: '选择小程序卡券',
    layout: 'fit',
    width: 650,
    height: 500,
    bodyPadding: 5,
    modal: true,
    initComponent: function () {
        var me = this;
        var couponTemplateInfoStore = Ext.create('WX.store.BaseData.CouponTemplateInfoStore');
        me.grid = Ext.create('Ext.grid.Panel', {
            name: 'appletCardList',
            flex: 1,
            emptyText: '没有数据',
            store: couponTemplateInfoStore,
            stripeRows: true,
            selModel: Ext.create('Ext.selection.CheckboxModel', { model: 'SIMPLE', mode: 'single' }),
            columns: [
                { header: '编号', dataIndex: 'TemplateCode', width: 120 },
                { header: '优惠类型', dataIndex: 'CouponTypeName', width: 80 },
                { header: '标题', dataIndex: 'Title', flex: 1 },
                { header: '投放时间', dataIndex: 'PutInDate', width: 180 },
            ],
            bbar: {
                xtype: 'pagingtoolbar',
                displayInfo: true
            }
        });
        me.items = [me.grid];
        me.buttons = [{
            text: '保存',
            action: 'save',
            cls: 'submitBtn',
            scope: me
        },
        {
            text: '取消',
            scope: me,
            handler: me.close
        }];
        me.callParent(arguments);
    }
});