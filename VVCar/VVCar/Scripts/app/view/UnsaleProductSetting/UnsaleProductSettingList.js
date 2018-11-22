Ext.define('WX.view.UnsaleProductSetting.UnsaleProductSettingList', {
	extend: 'Ext.container.Container',
	alias: 'widget.UnsaleProductSettingList',
	title: '滞销产品提醒配置',
	layout: 'hbox',
	align: 'stretch',
	loadMask: true,
	closable: true,
	initComponent: function () {
		var me = this;
		var unsaleProductSettingStore = Ext.create('WX.store.BaseData.UnsaleProductSettingStore');
		unsaleProductSettingStore.load();
		var unsaleProductSettingItemStore = Ext.create('WX.store.BaseData.UnsaleProductSettingItemStore');
		me.items = [{
			xtype: 'grid',
			name: 'gridUnsaleProductSetting',
			title: '滞销产品提醒配置',
			flex: 6,
			height: '100%',
			store: unsaleProductSettingStore,
			stripeRow: true,
			selType: 'checkboxmodel',
			selModel: {
				selection: 'rowmodel',
				mode: 'single',
			},
			tbar: [
				{
					action: 'addUnsaleProductSetting',
					xtype: 'button',
					text: '添加配置',
					iconCls: 'x-fa fa-plus-circle',
				}, {
					action: 'delUnsaleProductSetting',
					xtype: 'button',
					text: '删除配置',
					iconCls: 'x-fa fa-close',
				}, {
					action: 'enableUnsaleProductSetting',
					xtype: 'button',
					text: '启用',
					iconCls: 'x-fa fa-key',
				}, {
					action: 'disableUnsaleProductSetting',
					xtype: 'button',
					text: '禁用',
					iconCls: 'x-fa fa-lock',
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
					autoScroll: true,
					columWidth: 1,
					items: [{
						xtype: 'textfield',
						name: 'Keyword',
						fieldLabel: '关键字',
						width: 170,
						labelWidth: 60,
						emptyText: '配置编码/名称',
						margin: '0 0 0 5'
					}, {
						action: 'search',
						xtype: 'button',
						text: '搜索',
						iconCls: 'fa fa-search',
						cls: 'submitBtn',
						margin: '0 0 0 5'
					}, {
						action: 'analyse',
						xtype: 'button',
						text: '产品分析',
						iconCls: 'fa  fa-calculator',
						cls: 'submitBtn',
						margin: '0 0 0 5'
					}]
				}
			],
			columns: [
				{ header: '配置编码', dataIndex: 'Code', flex: 1 },
				{ header: '配置名称', dataIndex: 'Name', flex: 1 },
				{ header: '滞销数量上限', dataIndex: 'UnsaleQuantity', flex: 1 },
				{ header: '畅销数量下限', dataIndex: 'SaleWellQuantity', flex: 1 },
				{
					header: '是否启用', dataIndex: 'IsAvailable', flex: 1,
					renderer: function (value) {
						if (value == true)
							return '<span><font color="green">启用</font></span>';
						else
							return '<span><font color="red">禁用</font></span>';
					}
				},
				{
					header: '创建时间', dataIndex: 'CreatedDate', width: 100,
					renderer: Ext.util.Format.dateRenderer('Y-m-d'),
				},
			],
			bbar: {
				xtype: 'pagingtoolbar',
				displayInfo: true
			}
		}, {
			xtype: 'splitter',
		}, {
			xtype: 'grid',
			name: 'gridUnsaleProductSettingItem',
			title: '产品',
			flex: 4,
			height: '100%',
			stripeRows: true,
			store: unsaleProductSettingItemStore,
			selType: 'checkboxmodel',
			tbar: [
				{
					action: 'addProduct',
					xtype: 'button',
					text: '添加产品',
					iconCls: 'x-fa fa-plus-circle',
					margin: '5 5 5 5',
				}, {
					action: 'delProduct',
					xtype: 'button',
					text: '删除产品',
					iconCls: 'x-fa fa-close',
					margin: '5 5 5 5',
				}
			],
			columns: [
				{ header: '产品编号', dataIndex: 'ProductCode', flex: 1 },
				{ header: '产品名称', dataIndex: 'ProductName', flex: 1 },
			],
			bbar: {
				xtype: 'pagingtoolbar',
				displayInfo: true
			}
		}]
		this.callParent();
	}
});