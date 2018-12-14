Ext.define('WX.view.MaterialPublish.MaterialPublishItemEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.MaterialPublishItemEdit',
	title: '添加素材',
	layout: 'fit',
	width: 600,
	bodyPadding: 5,
	modal: true,
	initComponent: function () {
		var me = this;
		var materialStore = Ext.create('WX.store.BaseData.MaterialStore');
		me.grid = Ext.create('Ext.grid.Panel', {
			name: "couponTemplate",
			flex: 1,
			emptyText: '没有数据',
			store: materialStore,
			stripeRows: true,
			height: 420,
			selModel: Ext.create('Ext.selection.CheckboxModel', { model: 'SIMPLE' }),
			columns: [
				{ header: '素材名称', dataIndex: 'Name', flex: 1 },
				{ header: '创建人', dataIndex: 'CreatedUser', flex: 1 },
				{ header: '创建时间', dataIndex: 'CreatedDate', flex: 1 },
			],
			bbar: {
				xtype: 'pagingtoolbar',
				displayInfo: true,
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