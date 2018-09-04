Ext.define('WX.view.SystemSetting.SystemSetting', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.SystemSetting',
	title: '系统参数设置',
	store: Ext.create('WX.store.BaseData.SystemSettingStore'),
	stripeRows: true,
	loadMask: true,
	closable: true,
	viewConfig: { enableTextSelection: true },
	initComponent: function () {
		var me = this;
		me.rowEditing = Ext.create('Ext.grid.plugin.RowEditing', {
			saveBtnText: '保存',
			cancelBtnText: "取消",
			autoCancel: false,
			listeners: {
				cancelEdit: function (rowEditing, context) {
					//如果是新增的数据，则删除
					if (context.record.phantom) {
						me.store.remove(context.record);
					}
				},
				beforeedit: function (editor, context, eOpts) {
					if (editor.editing == true)
						return false;
				},
			}
		});
		me.tbar = [{
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
				xtype: 'textfield',
				name: 'MerchantCode',
				fieldLabel: '商户编号',
				labelWidth: 60,
				margin: '0 0 0 5'
			}, {
				xtype: 'textfield',
				name: 'MerchantName',
				fieldLabel: '商户名称',
				labelWidth: 60,
				margin: '0 0 0 5'
			}, {
				action: 'search',
				xtype: 'button',
				text: '搜索',
				iconCls: 'fa fa-search',
				cls: 'submitBtn',
				margin: '0 0 0 5'
			}, {
				action: 'addSystemSetting',
				xtype: 'button',
				text: '新增',
				iconCls: 'fa fa-plus-circle',
				margin: '0 0 0 5'
				}]
		}];
		me.columnLines = true;
		me.plugins = [me.rowEditing];
		me.columns = [
			{ header: '商户编号', dataIndex: 'MerchantCode', width: 200 },
			{ header: '商户名称', dataIndex: 'MerchantName', width: 200 },
			{ header: '模版名称', dataIndex: 'Caption', width: 200, editor: { xtype: 'textfield', allowBlank: false } },
			{ header: '模板编码', dataIndex: 'Name', width: 200, editor: { xtype: 'textfield', allowBlank: false } },
			{ header: '模板标题(公众号)', dataIndex: 'TemplateName', width: 200, editor: { xtype: 'textfield', allowBlank: true } },
			{ header: '模板数值', dataIndex: 'SettingValue', flex: 1, editor: { xtype: 'textfield', allowBlank: true } },
			{
				text: '操作',
				xtype: 'actioncolumn',
				width: 100,
				sortable: false,
				menuDisabled: true,
				height: 30,
				align: 'center',
				items: [{
					action: 'deleteItem',
					iconCls: 'x-fa fa-close',
					tooltip: '删除',
					scope: this,
					margin: '10 10 10 10',
					handler: function (grid, rowIndex, colIndex) {
						var record = grid.getStore().getAt(rowIndex);
						this.fireEvent('deleteActionClick', grid, record);
					}
				}]
			}
		];
		me.dockedItems = [{
			xtype: "pagingtoolbar",
			store: me.store,
			dock: "bottom",
			displayInfo: true
		}];
		this.callParent();
	},
	afterRender: function () {
		this.callParent(arguments);
		this.getStore().load();
	}
});

