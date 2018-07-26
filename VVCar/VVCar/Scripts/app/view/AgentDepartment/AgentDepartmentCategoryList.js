Ext.define('WX.view.AgentDepartment.AgentDepartmentCategoryList', {
	extend: 'Ext.window.Window',
	alias: 'widget.AgentDepartmentCategoryList',
	title: '分类管理',
	layout: 'fit',
	width: 400,
	height: 500,
	modal: true,
	initComponent: function () {
		var me = this;
		var treegridAgentDepartmentCategoryStore = Ext.create('WX.store.BaseData.AgentDepartmentCategoryTreeStore');
		me.tbar = [{
			action: 'addAgentDepartmentCategory',
			xtype: 'button',
			text: '新增',
			scope: this,
			iconCls: 'add'
		}, {
			action: 'editAgentDepartmentCategory',
			xtype: 'button',
			text: '修改',
			scope: this,
			iconCls: 'edit'
		}, {
			action: 'delAgentDepartmentCategory',
			xtype: 'button',
			text: '删除',
			scope: this,
			iconCls: 'delete'
		}];
		me.items = [{
			xtype: 'treepanel',
			name: 'treegridAgentDepartmentCategory',
			store: treegridAgentDepartmentCategoryStore,
			useArrows: true,
			rootVisible: false,
			stripeRows: true,
			columns: [
				{ xtype: 'treecolumn', header: '类别代码', dataIndex: 'Code', flex: 1, },
				{ header: '类别名称', dataIndex: 'Name', flex: 1, },
			],
		}];
		me.buttons = [{
			text: '关闭',
			action: 'close',
			scope: me,
			handler: me.close
		}];
		this.callParent();
	},
	afterRender: function () {
		this.callParent(arguments);
	}
});