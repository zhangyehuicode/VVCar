Ext.define('WX.view.PickUpOrder.UserSelector', {
	extend: 'Ext.window.Window',
	alias: 'widget.UserSelector',
	title: '选择人员',
	layout: 'fit',
	width: 600,
	height: 500,
	bodyPadding: 5,
	autoShow: false,
	modal: true,
	buttonAlign: 'right',
	initComponent: function () {
		var me = this;
		var userStore = Ext.create('WX.store.BaseData.UserStore');
		userStore.limit = 10;
		userStore.pageSize = 10;
		userStore.load();
		me.items = [{
			xtype: 'grid',
			name: 'gridUser',
			stripeRows: true,
			loadMask: true,
			store: userStore,
			selType: 'checkboxmodel',
			tbar: {
				xtype: 'form',
				layout: 'column',
				border: false,
				frame: false,
				labelAlign: 'left',
				buttonAlign: 'right',
				labelWidth: 100,
				padding: 5,
				autoWidth: true,
				autoScroll: true,
				columnWidth: 1,
				items: [{
					name: 'UserID',
					xtype: 'textfield',
					fieldLabel: '用户ID',
					width: 170,
					labelWidth: 60,
					margin: '0 0 0 5',
					hidden: true,
				}, {
					name: 'PeopleType',
					xtype: 'textfield',
					fieldLabel: '用户类型',
					width: 170,
					labelWidth: 60,
					margin: '0 0 0 5',
					hidden: true,
				},{
					name: 'Keyword',
					xtype: 'textfield',
					fieldLabel: '关键字',
					emptyText: '用户编码/名称',
					width: 200,
					labelWidth: 60,
					margin: '0 0 0 5',
				}, {
					action: 'search',
					xtype: 'button',
					text: '搜索',
					margin: '0 0 0 5',
				}]
			},
			columns: [
				{ header: '用户编号', dataIndex: 'Code', flex: 1 },
				{ header: '用户名称', dataIndex: 'Name', flex: 1 },
			],
			bbar: {
				xtype: 'pagingtoolbar',
				displayInfo: true,
			},
		}];
		me.buttons = [{
			text: '保存',
			action: 'save',
			cls: 'submitBtn',
			scope: me
		}, {
			text: '取消',
			scope: me,
			handler: me.close
		}];
		me.callParent(arguments);
	}
})