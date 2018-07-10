Ext.define('WX.controller.Combo', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.ProductStore'],
	models: ['BaseData.ProductModel'],
	views: ['Combo.ComboList', 'Combo.ComboEdit', 'Combo.ProductSelector'],
	refs: [{
		ref: 'comboList',
		selector: 'ComboList',
	}, {
		ref: 'gridCombo',
		selector: 'ComboList grid[name=gridCombo]',
	}, {
		ref: 'gridComboItem',
		selector: 'ComboList grid[name=gridComboItem]',
	}, {
		ref: 'productSelector',
		selector: 'ProductSelector',
	}, {
		ref: 'comboEdit',
		selector: 'ComboEdit',
	}, {
		ref: 'gridComboEdit',
		selector: 'ComboEdit grid[name=gridProduct]',
	}, {
		ref: 'gridProductSelector',
		selector: 'ProductSelector grid[name=productList]',
	}],
	init: function () {
		var me = this;
		me.control({
			'ComboList button[action=addComboItem]': {
				click: me.addComboItem,
			},
			'ComboList button[action=deleteComboItem]': {
				click: me.deleteComboItem,
			},
			'ComboList button[action=search]': {
				click: me.search,
			},
			'ComboList grid[name=gridCombo]': {
				select: me.gridComboSelect,
			},
			'ComboList grid[name=gridComboItem]': {
				edit: me.stockEdit
			},
			'ComboList': {
				afterrender: me.afterrender,
			},
			'ComboEdit button[action=selectProduct]': {
				click: me.selectProduct,
			},
			'ComboEdit button[action=save]': {
				click: me.saveComboItem,
			},
			'ProductSelector grid[name=productList]': {
				itemdblclick: me.chooseProduct
			},
			'ProductSelector button[action=search]': {
				click: me.searchProduct,
			},
		});
	},
	stockEdit: function (editor, context, eOpts) {
		if (context.record.phantom) {//表示新增
			context.store.create(context.record.data, {
				callback: function (records, operation, success) {
					if (!success) {
						Ext.MessageBox.alert("提示", operation.error);
						return;
					} else {
						context.record.copyFrom(records[0]);
						context.record.commit();
						Ext.MessageBox.alert("提示", "新增成功");
					}
				}
			});
		} else {
			if (!context.record.dirty)
				return;
			context.store.update({
				callback: function (records, operation, success) {
					if (!success) {
						Ext.MessageBox.alert("提示", operation.error.statusText);
						return;
					} else {
						Ext.MessageBox.alert("提示", "更新成功");
					}
				}
			});
		}
	},
	selectProduct: function () {
		var win = Ext.widget('ProductSelector');
		var grid = win.down('grid');
		var store = grid.getStore();
		store.proxy.extraParams = { IsCombo: false, ProductType: 0 };
		store.load();
		win.show();
	},
	chooseProduct: function (grid, record) {
		var win = Ext.ComponentQuery.query('window[name=ComboEdit]')[0];
		win.down('textfield[name=ProductID]').setValue(record.data.ID);
		win.down('textfield[name=ProductName]').setValue(record.data.Name);
		win.down('textfield[name=ProductCode]').setValue(record.data.Code);
		win.down('textfield[name=BasePrice]').setValue(record.data.BasePrice);
		win.down('textfield[name=PriceSale]').setValue(record.data.PriceSale);
		grid.up('window').close();
	},
	gridComboSelect: function (grid, record) {
		var me = this;
		var store = me.getGridComboItem().getStore();
		var params = {
			ComboID: record.data.ID,
		}
		Ext.apply(store.proxy.extraParams, params);
		store.load();
	},
	addComboItem: function () {
		var me = this;
		var selectedItems = me.getGridCombo().getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要添加的套餐');
			return;
		} else {
			me.tasks = selectedItems;
		}
		var win = Ext.widget('ComboEdit');
		win.form.getForm().actionMethod = 'POST';
		win.setTitle('添加套餐子项');
		win.show();
	},
	deleteComboItem: function (btn) {
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要删除的套餐子项!');
			return;
		}
		Ext.Msg.confirm('提示', '确定要删除套餐子项吗?', function (optional) {
			if (optional === 'yes') {
				var store = btn.up('grid').getStore();
				var ids = [];
				selectedItems.forEach(function (item) {
					ids.push(item.data.ID);
				});
				store.batchDelete(ids,
					function success(response, request, c) {
						var ajaxResult = JSON.parse(c.responseText);
						if (ajaxResult.IsSuccessful) {
							store.reload();
							Ext.Msg.alert('提示', '删除成功');
						} else {
							Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
						}
					},
					function failure(a, b, c) {
						Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
					}
				);
			}
		});
	},
	saveComboItem: function () {
		var me = this;
		var win = me.getComboEdit();
		var store = me.getGridComboItem().getStore();
		var ComboID = me.tasks[0].data.ID;
		var ProductID = win.down('textfield[name=ProductID]').getValue();
		if (ProductID == null || ProductID == '') {
			Ext.Msg.alert('提示', '请选择产品');
			return;
		}
		var ProductCode = win.down('textfield[name=ProductCode]').getValue();
		var ProductName = win.down('textfield[name=ProductName]').getValue();
		var BasePrice = win.down('textfield[name=BasePrice]').getValue();
		var PriceSale = win.down('textfield[name=PriceSale]').getValue();
		var Quantity = win.down('textfield[name=Quantity]').getValue();
		var comboItem = {
			ComboID: ComboID,
			ProductID: ProductID,
			ProductCode: ProductCode,
			ProductName: ProductName,
			BasePrice: BasePrice,
			PriceSale: PriceSale,
			Quantity: Quantity,
		};
		//store.batchAdd(comboItem, function (response, opts) {
		//	var ajaxResult = JSON.parse(response.responseText);
		//	if (ajaxResult.Data == false) {
		//		Ext.Msg.alert("提示", ajaxResult.ErrorMessage);
		//		return;
		//	}
		//}, function (response, opts) {
		//	var ajaxResult = JSON.parse(response.responseText);
		//	Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
		//});
		store.create(comboItem, {
			callback: function (records, operation, success) {
				if (!success) {
					Ext.MessageBox.alert("提示", operation.error);
					return;
				} else {
					store.add(records[0].data);
					store.commitChanges();
					Ext.MessageBox.alert("提示", "新增成功");
					win.close();
				}
			}
		})
		store.reload();
		win.close();
	},
	search: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			queryValues.IsCombo = true;
			var store = me.getGridCombo().getStore();
			store.proxy.extraParams = queryValues;
			store.load();
			me.getGridComboItem().getStore().removeAll();
		} else {
			Ext.MessageBox.alert('系统提示', '请输入过滤条件!');
		}
	},
	searchProduct: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			var store = me.getGridProductSelector().getStore();
			store.load({ params: queryValues });
		} else {
			Ext.Msg.alert('提示', '请输入过滤条件');
		}
	},
	afterrender: function () {
		var me = this;
		var store = me.getGridCombo().getStore();
		var params = {
			IsCombo: true
		}
		Ext.apply(store.proxy.extraParams, params);
		store.load();
	}
});