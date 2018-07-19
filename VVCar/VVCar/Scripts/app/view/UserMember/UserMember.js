Ext.define('WX.view.UserMember.UserMember', {
	extend: 'Ext.container.Container',
	alias: 'widget.UserMember',
	title: '客户分配',
	layout: 'hbox',
	align: 'stretch',
	loadMask: true,
	closable: true,
	initComponent: function () {
		var me = this;
		var userStore = Ext.create('WX.store.BaseData.UserStore');
		userStore.load();
		var userMemberStore = Ext.create('WX.store.BaseData.UserMemberStore');
		me.items = [
			{
				xtype: 'grid',
				name: 'gridUser',
				title: '用户列表',
				flex: 1,
				height: '100%',
				store: userStore,
				stripeRows: true,
				emptyText: '没有数据',
				tbar: [
					{
						action: 'addMember',
						xtype: 'button',
						text: '分配客户',
						iconCls: 'x-fa fa-plus-circle'
					},
				],
				columns: [
					{ header: '用户代码', dataIndex: 'Code', flex: 1, },
					{ header: '用户名称', dataIndex: 'Name', flex: 1, },
				],
			},
			{
				xtype: 'grid',
				name: 'gridUserMember',
				title: '关联会员列表',
				flex: 2,
				height: '100%',
				store: userMemberStore,
				stripeRows: true,
				selType: 'checkboxmodel',
				tbar: [
					{
						action: 'deleteMember',
						xtype: 'button',
						text: '删除关联',
						iconCls: 'x-fa fa-close'
					},
				],
				columns: [
					{ header: '会员分组', dataIndex: 'MemberGroup', flex: 1 },
					{ header: '会员姓名', dataIndex: 'MemberName', flex: 1 },
					{
						header: '性别', dataIndex: 'Sex', flex: 1,
						renderer: function (value) {
							if (value == 1)
								return "男";
							else if (value == 2)
								return "女";
							else
								return "未知";
						}
					},
					{ header: '分配时间', dataIndex: 'CreatedDate', flex: 1 },
				],
				bbar: {
					xtype: 'pagingtoolbar',
					store: userMemberStore,
					displayInfo: true
				}
			}
		];
		this.callParent();
	},
});
