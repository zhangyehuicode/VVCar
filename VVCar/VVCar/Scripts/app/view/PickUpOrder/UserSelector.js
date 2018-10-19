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
	buttonAlign: 'center',
	initComponent: function () {
		var me = this;
		var userStore = Ext.create('WX.store.BaseData.UserStore');
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
					name: 'Code',
					xtype: 'textfield',
					fieldLabel: '用户编号',
					width: 170,
					labelWidth: 60,
					margin: '0 0 0 5',
				}, {
					name: 'Name',
					xtype: 'textfield',
					fieldLabel: '用户名称',
					width: 170,
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
			buttons: [{
				text: '保存',
				action: 'save',
				cls: 'submitBtn',
				scope: me
			}, {
				text: '取消',
				scope: me,
				handler: me.close
			}]
		}];
		me.callParent(arguments);
	}
})