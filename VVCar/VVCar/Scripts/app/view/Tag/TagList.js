Ext.define('WX.view.Tag.TagList', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.TagList',
	title: '商户管理',
	name: 'tag',
	store: Ext.create('WX.store.BaseData.TagStore'),
	stripeRows: true,
	loadMask: true,
	closable: true,
	viewConfig: { enableTextSelection: true },
	selType: 'checkboxmodel',
	initComponent: function () {
		var me = this;
		me.tbar = [{
			action: 'addTag',
			xtype: 'button',
			text: '添加标签',
			scope: this,
			iconCls: 'fa fa-plus-circle',
		}, {
			action: 'batchDelete',
			xtype: 'button',
			text: '删除标签',
			scope: this,
			iconCls: 'fa fa-close',
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
				name: 'Code',
				fieldLabel: '标签编码',
				width: 170,
				labelWidth: 80,
				margin: '0 0 0 5',
			}, {
				xtype: 'textfield',
				name: 'Name',
				fieldLabel: '标签名称',
				width: 170,
				labelWidth: 80,
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
			{ header: '标签编码', dataIndex: 'Code', flex: 1 },
			{ header: '标签名称', dataIndex: 'Name', flex: 1 },
			{ header: '创建日期', dataIndex: 'CreatedDate', flex: 1 },
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