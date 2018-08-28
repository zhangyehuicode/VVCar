﻿Ext.define('WX.view.CrowdOrder.CrowdOrderEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.CrowdOrderEdit',
	title: '编辑拼单',
	name: 'CrowdOrderEdit',
	layout: 'fit',
	width: 250,
	initComponent: function () {
		var me = this;
		var yesNoDictStore = Ext.create('WX.store.DataDict.YesNoTypeStore');
		me.form = Ext.create('Ext.form.Panel', {
			border: false,
			trackResetOnLoad: true,
			fieldDefaults: {
				labelAlign: 'left',
				labelWidth: 60,
				anchor: '100%',
				flex: 1,
				margin: 5,
			},
			items: [{
				xtype: 'panel',
				layout: 'hbox',
				border: false,
				padding: '0 0 5 0',
				items: [{
					xtype: 'textfield',
					name: 'ProductName',
					fieldLabel: '产品',
					readOnly: true,
					allowBlank: false,
				}, {
					action: 'selectProduct',
					xtype: 'button',
					text: '查找',
					cls: 'submitBtn',
					margin: '5 5 0 5',
				}]
			}, {
				xtype: 'textfield',
				name: 'ProductID',
				fieldLabel: '产品ID',
				hidden: true,
			}, {
				xtype: 'form',
				layout: 'vbox',
				items: [{
					xtype: 'textfield',
					margin: '5 10 5 5',
					name: 'Name',
					fieldLabel: '拼单名称',
					allowBlank: false,
				}, {
					xtype: 'combobox',
					margin: '5 10 5 5',
					name: 'IsAvailable',
					store: yesNoDictStore,
					displayField: 'DictName',
					valueField: 'DictValue',
					fieldLabel: '是否启用',
					editable: false,
					value: false,
				}, {
					xtype: 'numberfield',
					margin: '5 10 5 5',
					name: 'PeopleCount',
					fieldLabel: '拼单人数',
					minValue: 0,
					allowBlank: false,
					value: 0,
				}, {
					xtype: 'datefield',
					name: 'PutawayTime',
					fieldLabel: '上架时间',
					margin: '5 10 5 5',
					allowBlank: true,
					minValue: new Date(),
					format: 'Y-m-d',
					editable: false,
					allowBlank: true,
					value: new Date()
				}, {
					xtype: 'datefield',
					name: 'SoleOutTime',
					fieldLabel: '下架时间',
					margin: '5 10 5 5',
					allowBlank: true,
					minValue: new Date(),
					format: 'Y-m-d',
					editable: false,
					allowBlank: true,
					value: new Date()
				}]
			}]
		});
		me.items = [me.form];
		me.buttons = [{
			text: '保存',
			cls: 'submitBtn',
			action: 'save'
		}, {
			text: '取消',
			scope: me,
			handler: me.close
		}];
		me.callParent(arguments);
	}
})