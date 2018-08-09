Ext.define('WX.view.Member.MemberGroupList', {
	extend: 'Ext.window.Window',
	alias: 'widget.MemberGroupList',
	title: '编辑分组',
	layout: 'fit',
	width: 400,
	height: 500,
	modal: true,
	initComponent: function () {
		var me = this;
		var treegridMemberGroupStore = Ext.create('WX.store.BaseData.MemberGroupTreeStore');
		me.tbar = [{
			xtype: 'button',
			action: 'addgroup',
			text: '新增',
			iconCls: 'fa fa-plus-circle'
		}, {
			xtype: 'button',
			action: 'editgroup',
			text: '修改',
			iconCls: 'x-fa fa-pencil'
		}, {
			xtype: 'button',
			action: 'deletegroup',
			text: '删除',
			iconCls: 'x-fa fa-close'
		}];
		me.items = [{
			xtype: 'treepanel',
			name: 'treegridMemberGroup',
			store: treegridMemberGroupStore,
			useArrows: true,
			rootVisible: false,
			stripeRows: true,
			columns: [
				{ xtype: 'treecolumn', header: '类别代码', dataIndex: 'Code', flex: 1 },
				{ header: '类别名称', dataIndex: 'Name', flex: 1 },
				{
					header: '批发价', dataIndex: 'IsWholesalePrice', flex: 1,
					renderer: function (value) {
						if (value)
							return "<span style='color:green;'>是</span>";
						else
							return "<span style='color:red;'>否</span>";
					}
				},
			],
		}];
		me.buttons = [{
			text: '关闭',
			action: 'close',
			scope: me,
			handler: me.close
		}];
		me.callParent();
	},
	afterRender: function () {
		this.callParent(arguments);
	}
});