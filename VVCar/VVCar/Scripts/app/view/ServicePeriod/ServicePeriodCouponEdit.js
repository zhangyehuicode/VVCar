Ext.define('WX.view.ServicePeriod.ServicePeriodCouponEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.ServicePeriodCouponEdit',
	title: '服务周期卡券编辑',
	layout: 'fit',
	width: 600,
	bodyPadding: 5,
	modal: true,
	initComponent: function () {
		var me = this;
		var couponTemplateInfoStore = Ext.create('WX.store.BaseData.CouponTemplateInfoStore');
		couponTemplateInfoStore.on('beforeload', function (s) {
			var params = s.getProxy().extraParams;
			Ext.apply(params, { CouponType: -1, AproveStatus: 2, limit: 10 });
		});
		couponTemplateInfoStore.limit = 10,
			couponTemplateInfoStore.pageSize = 10,
			couponTemplateInfoStore.load();
		me.grid = Ext.create('Ext.grid.Panel', {
			name: "couponTemplate",
			flex: 1,
			store: couponTemplateInfoStore,
			stripeRows: true,
			selModel: Ext.create('Ext.selection.CheckboxModel', { model: 'SIMPLE' }),
			columns: [
				{ header: '模板编号', dataIndex: 'TemplateCode', flex: 1 },
				{ header: '卡券类型', dataIndex: 'CouponTypeName', flex: 1 },
				{ header: '标题', dataIndex: 'Title', flex: 1 }
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