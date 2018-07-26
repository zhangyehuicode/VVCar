Ext.define('WX.view.AgentDepartment.TagSelector', {
	extend: 'Ext.window.Window',
	alias: 'widget.TagSelector',
	title: '选择标签',
	layout: 'fit',
	width: 600,
	height: 515,
	bodyPadding: 5,
	modal: true,
	initComponent: function () {
		var me = this;
		var store = Ext.create('WX.store.BaseData.TagStore');
		me.grid = Ext.create('Ext.grid.Panel', {
			name: "Tag",
			flex: 1,
			emptyText: '没有数据',
			store: store,
			stripeRows: true,
			selModel: Ext.create('Ext.selection.CheckboxModel', { model: 'SIMPLE' }),
			columns: [
				{ header: '标签编码', dataIndex: 'Code', flex: 1 },
				{ header: '标签名称', dataIndex: 'Name', flex: 1 },
				{
					header: '创建时间', dataIndex: 'CreatedDate', flex: 1,
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