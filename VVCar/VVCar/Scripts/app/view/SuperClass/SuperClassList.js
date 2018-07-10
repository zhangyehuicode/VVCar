Ext.define('WX.view.SuperClass.SuperClassList', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.SuperClassList',
	title: '超能课堂',
	name: 'gridSuperClass',
	store: Ext.create('WX.store.BaseData.SuperClassStore'),
	stripeRows: true,
	loadMask: true,
	closable: true,
	viewConfig: { enableTextSelection: true },
	selType: 'checkboxmodel',
	initComponent: function () {
		var me = this;
		me.tbar = [{
			action: 'addVideo',
			xtype: 'button',
			text: '添加课程',
			scope: this,
			iconCls: 'fa fa-plus-circle',
			permissionCode: 'SuperClass.SuperClassEdit',
		}, {
			action: 'editVideo',
			xtype: 'button',
			text: '修改课程',
			scope: this,
			iconCls: 'fa fa-pencil',
			permissionCode: 'SuperClass.SuperClassEdit',
		}, {
			action: 'delVideo',
			xtype: 'button',
			text: '删除课程',
			scope: this,
			iconCls: 'fa fa-close',
			permissionCode: 'SuperClass.SuperClassEdit',
		}, {
			xtype: 'form',
			layout: 'column',
			border: false,
			frame: false,
			labelAlign: 'left',
			buttonAlign: 'right',
			labelWidth: 100,
			padding: 5,
			autoWidth: true,
			autoScroll: false,
			columnWidth: 1,
			items: [{
				xtype: 'textfield',
				name: 'Name',
				fieldLabel: '标题',
				width: 170,
				labelWidth: 30,
				margin: '0 0 0 5',
			}, {
				action: 'search',
				xtype: 'button',
				text: '搜索',
				iconCls: 'fa fa-search',
				cls: 'submitBtn',
				margin: '0 0 0 5',
			}]
		}];
		me.columns = [
			{ header: '课程名称', dataIndex: 'Name', flex: 1 },
			{
				header: '视频', dataIndex: 'VideoUrl', width: 300,
				renderer: function (value) {
					if (value != "" && value != null) {
						return '<video src="' + value + '" width=200 height=100 controls></video>';
					}
				},
			},
			{ header: '视频简介', dataIndex: 'Description', flex: 2 },
			{ header: '创建人', dataIndex: 'CreatedUser', width: 200 },
			{
				header: '创建时间', dataIndex: 'CreatedDate', width: 100,
				renderer: Ext.util.Format.dateRenderer('Y-m-d'),
			},
		];
		me.dockedItems = [{
			xtype: 'pagingtoolbar',
			store: me.store,
			dock: 'bottom',
			displayInfo: true
		}];
		me.callParent();
	},
	afterRender: function () {
		var me = this;
		me.callParent(arguments);
		me.getStore().load();
	}
});