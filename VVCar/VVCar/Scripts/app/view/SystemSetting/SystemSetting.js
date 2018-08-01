Ext.define('WX.view.SystemSetting.SystemSetting', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.SystemSetting',
	title: '系统参数设置',
	store: Ext.create('WX.store.BaseData.SystemSettingStore'),
	stripeRows: true,
	loadMask: true,
	closable: true,
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
				fieldLabel: '商户号',
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
			}]
		}];
		me.columnLines = true;
		me.plugins = [me.rowEditing];
		me.columns = [
			{ header: '商户编号', dataIndex: 'MerchantCode', width: 200 },
			{ header: '商户名称', dataIndex: 'MerchantName', width: 200 },
			{ header: '名称', dataIndex: 'Caption', width: 200, },
			{ header: '值', dataIndex: 'SettingValue', flex: 1, editor: { xtype: 'textfield', allowBlank: true } }
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

