Ext.define('WX.view.CarBitCoin.CarBitCoinProductEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.CarBitCoinProductEdit',
	title: '',
	layout: 'fit',
	width: 350,
	modal: true,
	bodyPadding: 5,
	initComponent: function () {
		var me = this;
		var yesNoDictStore = Ext.create('WX.store.DataDict.YesNoTypeStore');
		var carBitCoinProductTypeStore = Ext.create('WX.store.DataDict.CarBitCoinProductTypeStore');
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
				name: 'CarBitCoinProductCategoryID',
				fieldLabel: '类别ID',
				hidden: true,
			}, {
				xtype: 'textfield',
				name: 'Name',
				fieldLabel: '名称',
				maxLength: 50,
				allowBlank: false,
			}, {
				xtype: 'textfield',
				name: 'Code',
				fieldLabel: '编码',
				maxLength: 20,
				allowBlank: false,
			}, {
				xtype: 'textfield',
				name: 'Unit',
				fieldLabel: '单位',
				maxLength: 10,
				allowBlank: true,
			}, {
				xtype: 'form',
				border: false,
				layout: 'hbox',
				margin: '0 0 10 0',
				items: [{
					xtype: 'filefield',
					fieldLabel: '商品图片',
					allowBlank: true,
					buttonText: '选择图片',
				}, {
					xtype: 'button',
					text: '上传',
					margin: '0 0 0 5',
					action: 'uploadpic',
				}]
			}, {
				xtype: 'box',
				name: 'ImgShow',
				width: 83,
				height: 50,
				margin: '0 0 10 75',
				autoEl: {
					tag: 'img',
					src: '',
				}
			}, {
				xtype: 'textfield',
				name: 'ImgUrl',
				fieldLabel: '图片路径',
				hidden: true,
			}, {
				xtype: 'combobox',
				name: 'CarBitCoinProductType',
				store: carBitCoinProductTypeStore,
				loadMode: 'local',
				fieldLabel: '产品类型',
				displayField: 'DictName',
				valueField: 'DictValue',
				editable: false,
			}, {
				xtype: 'numberfield',
				name: 'BasePrice',
				fieldLabel: '原单价',
				minValue: 0,
				allowBlank: false,
				value: 0,
			}, {
				xtype: 'numberfield',
				name: 'PriceSale',
				fieldLabel: '销售单价',
				minValue: 0,
				allowBlank: false,
				value: 0,
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
				xtype: 'checkboxfield',
				name: 'IsCanPointExchange',
				fieldLabel: '车比特兑换',
				checked: false,
				inputValue: true,
			}, {
				xtype: 'numberfield',
				name: 'CarBitCoinPoints',
				fieldLabel: '兑换车比特',
				minValue: 0,
				allowBlank: false,
				value: 0,
				disabled: true,
			}, {
				xtype: 'numberfield',
				name: 'UpperLimit',
				fieldLabel: '兑换上限',
				minValue: 0,
				allowBlank: false,
				value: 0,
				disabled: true,
			}, {
				xtype: 'combobox',
				name: 'IsPublish',
				store: yesNoDictStore,
				displayField: 'DictName',
				valueField: 'DictValue',
				fieldLabel: '是否上架',
				editable: false,
				value: false,
			}, {
				xtype: 'combobox',
				name: 'IsRecommend',
				store: yesNoDictStore,
				displayField: 'DictName',
				valueField: 'DictValue',
				fieldLabel: '是否推荐',
				editable: false,
				value: false,
			}, {
				xtype: 'numberfield',
				name: 'Stock',
				fieldLabel: '库存',
				minValue: 0,
				allowBlank: false,
				value: 0,
			}, {
				xtype: 'textfield',
				name: 'Introduction',
				fieldLabel: '产品介绍',
				maxLength: 100,
				allowBlank: true,
			}, {
				xtype: 'textfield',
				name: 'DeliveryNotes',
				fieldLabel: '配送说明',
				maxLength: 50,
				allowBlank: true,
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