Ext.define('WX.view.AgentDepartment.AgentDepartmentTagEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.AgentDepartmentTagEdit',
	title: '客户标签',
	layout: 'fit',
	width: 600,
	height: 515,
	bodyPadding: 5,
	modal: true,
	initComponent: function () {
		var me = this;
		var store = Ext.create('WX.store.BaseData.AgentDepartmentTagStore');
		me.grid = Ext.create('Ext.grid.Panel', {
			name: "Tag",
			flex: 1,
			emptyText: '没有数据',
			store: store,
			stripeRows: true,
			selModel: Ext.create('Ext.selection.CheckboxModel', { model: 'SIMPLE' }),
			tbar: [{
				action: 'addAgentDepartmentTag',
				xtype: 'button',
				text: '添加标签',
				scope: this,
				iconCls: 'fa fa-plus-circle',
			}, {
				action: 'deleteAgentDepartmentTag',
				xtype: 'button',
				text: '删除标签',
				scope: this,
				iconCls: 'fa fa-close',
			}],
			columns: [
				{ header: '标签编码', dataIndex: 'TagCode', flex: 1 },
				{ header: '标签名称', dataIndex: 'TagName', flex: 1 },
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
		//me.buttons = [
		//	{
		//		text: '保存',
		//		action: 'save',
		//		cls: 'submitBtn',
		//		scope: me
		//	},
		//	{
		//		text: '取消',
		//		scope: me,
		//		handler: me.close
		//	}
		//];
		me.callParent(arguments);
	}
})