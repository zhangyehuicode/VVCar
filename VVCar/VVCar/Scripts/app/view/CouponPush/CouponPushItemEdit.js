Ext.define('WX.view.CouponPush.CouponPushItemEdit', {
    extend: 'Ext.window.Window',
    alias: 'widget.CouponPushItemEdit',
    title: '游戏推送子项编辑',
    layout: 'fit',
    width: 600,
    bodyPadding: 5,
    modal: true,
    initComponent: function() {
        var me = this;
		var couponTemplateInfoStore = Ext.create('WX.store.BaseData.CouponPushTemplateInfoStore');
        me.grid = Ext.create('Ext.grid.Panel', {
            name: "couponTemplate",
			flex: 1,
			emptyText: '没有数据',
            store: couponTemplateInfoStore,
            stripeRows: true,
            selModel: Ext.create('Ext.selection.CheckboxModel', { model: 'SIMPLE' }),
            columns: [
				{ header: '模板编号', dataIndex: 'TemplateCode', flex: 1 },
                { header: '卡券类型', dataIndex: 'CouponTypeName', flex: 1 },
				{ header: '标题', dataIndex: 'Title', flex: 1 },
				{
					header: '投放开始时间', dataIndex: 'PutInStartDate', flex: 1,
					renderer: Ext.util.Format.dateRenderer('Y-m-d'),
				},
				{
					header: '投放结束时间', dataIndex: 'PutInEndDate', flex: 1,
					renderer: Ext.util.Format.dateRenderer('Y-m-d'),
				},
            ],
            bbar: {
                xtype: 'pagingtoolbar',
                displayInfo: true
            }
        });
        me.items = [me.grid];
        me.buttons = [
            {
                text: '保存',
                action: 'save',
                cls: 'submitBtn',
                scope: me
            },
            {
                text: '取消',
                scope: me,
                handler: me.close
            }
        ];
        me.callParent(arguments);
    }
})