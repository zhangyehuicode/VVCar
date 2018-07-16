Ext.define('WX.view.CarBitCoin.CarBitCoinProductEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.CarBitCoinProductEdit',
	title: '',
	layout: 'fit',
	width: 600,
	modal: true,
	bodyPadding: 5,
	resizable: false,
	initComponent: function () {
		var me = this;
		var yesNoDictStore = Ext.create('WX.store.DataDict.YesNoTypeStore');
		var carBitCoinProductTypeStore = Ext.create('WX.store.DataDict.CarBitCoinProductTypeStore');
		me.form = Ext.create('Ext.form.Panel', {
			border: false,
			trackResetOnLoad: true,
			fieldDefaults: {
				labelAlign: 'left',
				labelWidth: 60,
				anchor: '100%',
				flex: 1,
				margin: '5',
			},
			items: [{
				xtype: 'textfield',
				name: 'CarBitCoinProductCategoryID',
				fieldLabel: '类别ID',
				hidden: true,
			}, {
				xtype: 'form',
				layout: 'hbox',
				items: [{
					xtype: 'textfield',
					margin: '5 10 5 5',
					name: 'Name',
					fieldLabel: '名称',
					maxLength: 50,
					allowBlank: false,
				}, {
					xtype: 'textfield',
					margin: '5 10 5 5',
					name: 'Code',
					fieldLabel: '编码',
					maxLength: 20,
					allowBlank: false,
				}]
			}, {
				xtype: 'form',
				layout: 'hbox',
				items: [{
					xtype: 'textfield',
					margin: '5 10 5 5',
					name: 'Unit',
					fieldLabel: '单位',
					maxLength: 10,
					allowBlank: true,
				}, {
					xtype: 'combobox',
					margin: '5 10 5 5',
					name: 'CarBitCoinProductType',
					store: carBitCoinProductTypeStore,
					loadMode: 'local',
					fieldLabel: '产品类型',
					displayField: 'DictName',
					valueField: 'DictValue',
					editable: false,
				}]
			}, {
				xtype: 'form',
				layout: 'hbox',
				items: [{
					xtype: 'form',
					border: false,
					layout: 'hbox',
					margin: '5 10 0 0',
					items: [{
						xtype: 'filefield',
						fieldLabel: '商品图片',
						width: 217,
						allowBlank: true,
						buttonText: '选择图片',
					}, {
						xtype: 'button',
						text: '上传',
						margin: '5 0 0 5',
						action: 'uploadpic',
					}]
				}, {
					xtype: 'box',
					margin: '5 10 0 5',
					name: 'ImgShow',
					width: 83,
					height: 50,
					margin: '0 0 5 72',
					autoEl: {
						tag: 'img',
						src: '',
					}
				}]
			}, {
				xtype: 'textfield',
				name: 'ImgUrl',
				fieldLabel: '图片路径',
				hidden: true,
			}, {
				xtype: 'form',
				layout: 'hbox',
				items: [{
					xtype: 'numberfield',
					margin: '5 10 5 5',
					name: 'Horsepower',
					fieldLabel: '增加马力',
					minValue: 0,
					allowBlank: true,
					value: 0,
				}, {
					xtype: 'numberfield',
					margin: '5 10 5 5',
					name: 'Stock',
					fieldLabel: '库存',
					minValue: 0,
					allowBlank: false,
					value: 0,
				}]
			}, {
				xtype: 'form',
				layout: 'hbox',
				items: [{
					xtype: 'numberfield',
					margin: '5 10 5 5',
					name: 'BasePrice',
					fieldLabel: '原单价',
					minValue: 0,
					allowBlank: false,
					value: 0,
				}, {
					xtype: 'numberfield',
					margin: '5 10 5 5',
					name: 'PriceSale',
					fieldLabel: '销售单价',
					minValue: 0,
					allowBlank: false,
					value: 0,
				}]
			}, {
				xtype: 'form',
				border: false,
				layout: 'hbox',
				margin: '0 0 10 0',
				items: [{
					xtype: 'numberfield',
					name: 'CommissionRate',
					fieldLabel: '抽成比例',
					minValue: 0,
					maxValue: 100,
					allowBlank: false,
					value: 0,
					hidden: true,
				}, {
					xtype: 'label',
					text: '%（0~100）',
					margin: '8 0 0 5',
					hidden: true,
				}]
			}, {
				xtype: 'form',
				layout: 'hbox',
				items: [{
					xtype: 'form',
					border: false,
					layout: 'hbox',
					margin: '0 10 5 0',
					items: [{
						xtype: 'checkboxfield',
						width: 98,
						labelWidth: 70,
						name: 'IsCanCarBitCoinExchange',
						fieldLabel: '车比特兑换',
						checked: false,
						inputValue: true,
					}, {
						xtype: 'numberfield',
						width: 170,
						margin: '5 0 5 5',
						name: 'CarBitCoin',
						labelWidth: 70,
						fieldLabel: '兑换车比特',
						minValue: 0,
						allowBlank: false,
						value: 0,
						disabled: true,
					}]
				}, {
					xtype: 'numberfield',
					margin: '5 10 5 5',
					name: 'UpperLimit',
					fieldLabel: '兑换上限',
					minValue: 0,
					allowBlank: false,
					value: 0,
					disabled: true,
				}]
			}, {
				xtype: 'form',
				layout: 'hbox',
				items: [{
					xtype: 'combobox',
					margin: '5 10 5 5',
					name: 'IsPublish',
					store: yesNoDictStore,
					displayField: 'DictName',
					valueField: 'DictValue',
					fieldLabel: '是否上架',
					editable: false,
					value: false,
				}, {
					xtype: 'combobox',
					margin: '5 10 5 5',
					name: 'IsRecommend',
					store: yesNoDictStore,
					displayField: 'DictName',
					valueField: 'DictValue',
					fieldLabel: '是否推荐',
					editable: false,
					value: false,
				}]
			}, {
				xtype: 'form',
				layout: 'hbox',
				items: [{
					xtype: 'textfield',
					margin: '5 10 5 5',
					name: 'Introduction',
					fieldLabel: '产品介绍',
					maxLength: 100,
					allowBlank: true,
				}, {
					xtype: 'textfield',
					margin: '5 10 5 5',
					name: 'DeliveryNotes',
					fieldLabel: '配送说明',
					maxLength: 50,
					allowBlank: true,
				}]
			},]
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
});