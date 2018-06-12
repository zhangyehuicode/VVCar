Ext.define("WX.view.ServicePeriod.ServicePeriodList", {
	extend: 'Ext.container.Container',
	alias: 'widget.ServicePeriodList',
	title: '服务周期配置',
	layout: 'hbox',
	align: 'stretch',
	loadMask: true,
	closable: true,
	initComponent: function () {
		var me = this;
		var servicePeriodStore = Ext.create('WX.store.BaseData.ServicePeriodStore');
		servicePeriodStore.load();
		var servicePeriodCouponStore = Ext.create('WX.store.BaseData.ServicePeriodCouponStore');
		me.items = [{
			xtype: 'grid',
			name: 'gridServicePeriod',
			title: '服务周期配置',
			flex: 6,
			height: '100%',
			store: servicePeriodStore,
			stripeRow: true,
			selType: 'checkboxmodel',
			selModel: {
				selection: 'rowmodel',
				mode: 'single',
			},
			tbar: [
				{
					action: 'addServicePeriod',
					xtype: 'button',
					text: '添加配置',
					iconCls: 'x-fa fa-plus-circle',
				}, {
					action: 'deleteServicePeriod',
					xtype: 'button',
					text: '删除配置',
					iconCls: 'x-fa fa-close',
				}, {
					action: 'enableServicePeriod',
					xtype: 'button',
					text: '启用',
					iconCls: 'x-fa fa-key',
				}, {
					action: 'disableServicePeriod',
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
						name: 'ProductName',
						fieldLabel: '服务名称',
						width: 170,
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
				}
			],
			columns: [
				{ header: '服务编号', dataIndex: 'ProductCode', flex: 1 },
				{ header: '服务名称', dataIndex: 'ProductName', flex: 1 },
				{ header: '服务周期(天)', dataIndex: 'PeriodDays', flex: 1 },
				{ header: '提示语', dataIndex: 'ExpirationNotice', flex: 1 },
				{
					header: '是否启用', dataIndex: 'IsAvailable', flex: 1,
					renderer: function (value) {
						if (value == true)
							return '<span><font color="green">启用</font></span>';
						else
							return '<span><font color="red">禁用</font></span>';
					}
				},
				{ header: '创建日期', dataIndex: 'CreatedDate', flex: 1 },
			],
			bbar: {
				xtype: 'pagingtoolbar',
				displayInfo: true
			}
		}, {
			xtype: 'splitter',
		}, {
			xtype: 'grid',
			name: 'gridServicePeriodCoupon',
			title: '卡券',
			flex: 4,
			height: '100%',
			stripeRows: true,
			store: servicePeriodCouponStore,
			selType: 'checkboxmodel',
			tbar: [
				{
					action: 'addServicePeriodCoupon',
					xtype: 'button',
					text: '添加卡券',
					iconCls: 'x-fa fa-plus-circle',
					margin: '5 5 5 5',
				}, {
					action: 'deleteServicePeriodCoupon',
					xtype: 'button',
					text: '删除卡券',
					iconCls: 'x-fa fa-close',
					margin: '5 5 5 5',
				}
			],
			columns: [
				{ header: '卡券编号', dataIndex: 'TemplateCode', flex: 1 },
				{ header: '优惠券模板标题', dataIndex: 'CouponTemplateTitle', flex: 1 },
			],
			bbar: {
				xtype: 'pagingtoolbar',
				displayInfo: true
			}
		}]
		this.callParent();
	}
});