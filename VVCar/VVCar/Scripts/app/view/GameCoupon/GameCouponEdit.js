Ext.define('WX.view.GameCoupon.GameCouponEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.GameCouponEdit',
	title: '游戏卡券编辑',
	layout: 'fit',
	width: 450,
	bodyPadding: 5,
	modal: true,
	initComponent: function () {
		var me = this;
		var couponTemplateInfoStore = Ext.create('WX.store.BaseData.CouponTemplateInfoStore');
		couponTemplateInfoStore.load({ params: { CouponType: -1, AproveStatus: 2 } });
		me.grid = Ext.create('Ext.grid.Panel', {
			name: 'gameCoupon',
			flex: 1,
			store: couponTemplateInfoStore,
			selModel: Ext.create('Ext.selection.CheckboxModel', { mode: 'SIMPLE' }),
			columns: [
				{ header: '模板编号', dataIndex: 'TemplateCode', flex: 1 },
				{ header: '卡券类型', dataIndex: 'CouponTypeName', flex: 1 },
				{ header: '标题', dataIndex: 'Title', flex: 1 }
			]
		});
		me.items = [me.grid];
		me.buttons = [
			{
				text: '保存',
				action: 'save',
				cls: 'submitBtn',
				scope: me,
			},
			{
				text: '取消',
				scope: me,
				handler: me.close
			}
		];
		me.callParent(arguments);
	}
});