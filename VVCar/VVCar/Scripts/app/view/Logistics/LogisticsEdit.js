Ext.define('WX.view.Logistics.LogisticsEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.LogisticsEdit',
	title: '',
	layout: 'fit',
	width: 350,
	modal: true,
	bodyPadding: 5,
	initComponent: function () {
		var me = this;
		var statusStore = Ext.create('Ext.data.Store', {
			fields: ['Name', 'Value'],
			data: [
				{ 'Name': '未付款', 'Value': 0 },
				{ 'Name': '已发货', 'Value': 2 },
				{ 'Name': '已完成', 'Value': 3 },
				{ 'Name': '已付款未发货', 'Value': 1 },
				{ 'Name': '付款不足', 'Value': -1 },
			]
		});
		me.form = Ext.create('Ext.form.Panel', {
			border: false,
			trackResetOnLoad: true,
			fieldDefaults: {
				labelAlign: 'left',
				labelWidth: 70,
				anchor: '100%'
			},
			items: [{
				xtype: 'textfield',
				name: 'ExpressNumber',
				fieldLabel: '快递单号',
				maxLength: 50,
				allowBlank: false,
			}, {
				xtype: 'textfield',
				name: 'LogisticsCompany',
				fieldLabel: '物流公司',
				maxLength: 50,
				allowBlank: false,
			}, {
				xtype: 'form',
				layout: 'hbox',
				margin: '5 5 5 0',
				items: [{
					xtype: 'numberfield',
					name: 'RevisitDays',
					fieldLabel: '回访时间',
					minValue: 0,
					allowBlank: false,
				}, {
					margin: '5 5 5 5',
					xtype: 'label',
					text: '天后',
				}]
			}, {
				xtype: 'textareafield',
				name: 'RevisitTips',
				fieldLabel: '回访提示',
				maxLength: 50,
				allowBlank: true,
				}, {
					xtype: 'textareafield',
					name: 'DeliveryTips',
					fieldLabel: '发货提醒',
					maxLength: 50,
					allowBlank: true,
				}, {
				xtype: 'form',
				layout: 'hbox',
				items: [{
					xtype: 'textfield',
					name: 'UserName',
					fieldLabel: '业务员',
					readOnly: true,
					allowBlank: true,
				}, {
					action: 'selectSalesman',
					xtype: 'button',
					text: '查找',
					cls: 'submitBtn',
					margin: '0 5 5 10',
				}]
			}, {
				xtype: 'textfield',
				name: 'ID',
				fieldLabel: '订单ID',
				hidden: true,
			}, {
				xtype: 'textfield',
				name: 'UserID',
				fieldLabel: '用户ID',
				hidden: true,
			}]
		});
		me.items = [me.form];
		me.buttons = [{
			text: '确认',
			cls: 'submitBtn',
			action: 'save'
		}, {
			text: '取消',
			scope: me,
			handler: me.close
		}];
		me.callParent(arguments);
	}
});