Ext.define('WX.view.Material.Material', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.Material',
	title: '素材管理',
	store: Ext.create('WX.store.BaseData.MaterialStore'),
	stripeRows: true,
	loadMask: true,
	closable: true,
	viewConfig: { enableTextSelection: true },
	selType: 'checkboxmodel',
	initComponent: function () {
		var me = this;
		me.tbar = [{
			action: 'addMaterial',
			xtype: 'button',
			text: '添加素材',
			scope: this,
			iconCls: 'fa fa-plus-circle',
		}, {
			action: 'editMaterial',
			xtype: 'button',
			text: '修改素材',
			scope: this,
			iconCls: 'fa fa-pencil',
		}, {
			action: 'delMaterial',
			xtype: 'button',
			text: '删除素材',
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
				name: 'Name',
				fieldLabel: '素材名称',
				width: 170,
				labelWidth: 60,
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
			{ header: '素材名称', dataIndex: 'Name', width: 300},
			{
				header: '素材', dataIndex: 'Url', flex: 1,
				renderer: function (value) {
					if (value != "" && value != null) {
						var extension = value.substring(value.lastIndexOf("."), value.length);
						var reg = new RegExp('.jpg|.gif|.png|.jpeg');
						if (reg.test(extension)) {
							return '<img src="' + value + '" style="width:200px; height: 100px;" />';
						} else {
							return '<video src="' + value + '" width=200 height=100 controls></video>';
						}
					}
				},
			},
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
});