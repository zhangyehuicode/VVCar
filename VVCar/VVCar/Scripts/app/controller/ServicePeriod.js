Ext.define('WX.controller.ServicePeriod', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.ServicePeriodStore'],
	stores: ['DataDict.EnableDisableTypeStore'],
	models: ['BaseData.ServicePeriodModel'],
	views: ['ServicePeriod.ServicePeriodList', 'ServicePeriod.ServicePeriodEdit', 'ServicePeriod.ServicePeriodCouponEdit', 'ServicePeriod.ServiceSelector'],
	refs: [{
		ref: 'servicePeriodList',
		selector: 'ServicePeriodList'
	}, {
		ref: 'servicePeriodEdit',
		selector: 'ServicePeriodEdit'
	}, {
		ref: 'servicePeriodCouponEdit',
		selector: 'ServicePeriodCouponEdit'
	}, {
		ref: 'gridServicePeriod',
		selector: 'ServicePeriodList grid[name=gridServicePeriod]'
	}, {
		ref: 'gridServicePeriodCoupon',
		selector: 'ServicePeriodList grid[name=gridServicePeriodCoupon]'
	}, {
		ref: 'gridServicePeriodCouponEdit',
		selector: 'ServicePeriodCouponEdit grid[name=couponTemplate]'
	}, {
		ref: 'serviceSelector',
		selector: 'ServiceSelector'
	}, {
		ref: 'gridServiceSelector',
		selector: 'ServiceSelector grid[name=serviceList]'
	}],
	init: function () {
		var me = this;
		me.control({
			'ServicePeriodList button[action=addServicePeriod]': {
				click: me.addServicePeriod
			},
			'ServicePeriodList button[action=addServicePeriodCoupon]': {
				click: me.addServicePeriodCoupon
			},
			'ServicePeriodList button[action=enableServicePeriod]': {
				click: me.enableServicePeriod
			},
			'ServicePeriodList button[action=disableServicePeriod]': {
				click: me.disableServicePeriod
			},
			'ServicePeriodList grid[name=gridServicePeriod]': {
				select: me.gridServicePeriodSelect,
				itemdblclick: me.editServicePeriod,
			},
			'ServicePeriodList button[action=deleteServicePeriod]': {
				click: me.deleteServicePeriod
			},
			'ServicePeriodList button[action=deleteServicePeriodCoupon]': {
				click: me.deleteServicePeriodCoupon
			},
			'ServicePeriodList button[action=search]': {
				click: me.searchData
			},
			'ServicePeriodEdit button[action=selectService]': {
				click: me.selectService
			},
			'ServicePeriodEdit button[action=save]': {
				click: me.saveServicePeriod
			},
			'ServicePeriodCouponEdit button[action=save]': {
				click: me.saveServicePeriodCoupon
			},
			'ServiceSelector grid[name=serviceList]': {
				itemdblclick: me.chooseService
			},
			'ServiceSelector button[action=search]': {
				click: me.searchProduct
			}
		});
	},
	gridServicePeriodSelect: function (grid, record, index, eOpts) {
		var store = this.getGridServicePeriodCoupon().getStore();
		Ext.apply(store.proxy.extraParams, {
			ServicePeriodSettingID: record.data.ID
		});
		store.reload();
	},
	addServicePeriod: function () {
		var win = Ext.widget('ServicePeriodEdit');
		win.form.getForm().actionMethod = 'POST';
		win.setTitle('添加服务周期配置');
		win.show();
	},
	enableServicePeriod: function () {
		var me = this;
		var store = me.getGridServicePeriod().getStore();
		var selectedItems = me.getGridServicePeriod().getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '未选择数据');
			return;
		}
		var ids = [];
		var existEnableData = false;
		selectedItems.forEach(function (item) {
			if (item.data.IsAvailable == true) {
				existEnableData = true;
			} else {
				ids.push(item.data.ID);
			}
		})
		if (existEnableData) {
			Ext.Msg.alert('提示', '请选择未启用的数据');
			return;
		}
		Ext.Msg.confirm('询问', '您确定要启用所选服务配置吗?', function (operational) {
			if (operational == 'yes') {
				store.enableServicePeriod(ids,
					function success(response, request, c) {
						var result = Ext.decode(c.responseText);
						if (result.IsSuccessful) {
							store.reload();
							Ext.Msg.alert('提示', '启用成功');
						} else {
							Ext.Msg.alert('提示', result.ErrorMessage);
						}
					},
					function failure(a, b, c) {
						Ext.Msg.alert('提示', '启用失败!');
					})
				Ext.Msg.alert('提示', '启用成功');
			}
		})
	},
	disableServicePeriod: function () {
		var me = this;
		var store = me.getGridServicePeriod().getStore();
		var selectedItems = me.getGridServicePeriod().getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '未选择数据');
			return;
		}
		var ids = [];
		var existDisableData = false;
		selectedItems.forEach(function (item) {
			if (item.data.IsAvailable == false) {
				existDisableData = true;
			} else {
				ids.push(item.data.ID);
			}
		})
		if (existDisableData) {
			Ext.Msg.alert('提示', '请选择已启用的数据');
			return;
		}
		Ext.Msg.confirm('询问', '您确定要启用所选服务配置吗?', function (operational) {
			if (operational == 'yes') {
				store.disableServicePeriod(ids,
					function success(response, request, c) {
						var result = Ext.decode(c.responseText);
						if (result.IsSuccessful) {
							store.reload();
							Ext.Msg.alert('提示', '禁用成功');
						} else {
							Ext.Msg.alert('提示', result.ErrorMessage);
						}
					},
					function failure(a, b, c) {
						Ext.Msg.alert('提示', '禁用失败!');
					})
				Ext.Msg.alert('提示', '禁用成功');
			}
		})
	},
	editServicePeriod: function (gird, record) {
		var win = Ext.widget('ServicePeriodEdit');
		win.form.loadRecord(record);
		win.form.getForm().actionMethod = 'PUT';
		win.setTitle('编辑服务周期配置');
		win.show();
	},
	addServicePeriodCoupon: function () {
		var me = this;
		var gridServicePeriod = me.getGridServicePeriod();
		var selectedItems = gridServicePeriod.getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要添加的服务周期');
			return;
		} else {
			me.tasks = selectedItems;
		}
		var win = Ext.widget('ServicePeriodCouponEdit');
		win.setTitle('添加服务周期卡券');
		win.show();
	},
	selectService: function () {
		var win = Ext.widget('ServiceSelector');
		var grid = win.down('grid');
		var store = grid.getStore();
		store.proxy.extraParams = { ProductType: 0 };
		store.load();
		win.show();
	},
	chooseService: function (grid, record) {
		var win = Ext.ComponentQuery.query('window[name=ServicePeriodEdit]')[0];
		win.down('textfield[name=ProductName]').setValue(record.data.Name);
		win.down('textfield[name=ProductID]').setValue(record.data.ID);
		win.down('textfield[name=ProductCode]').setValue(record.data.Code);
		grid.up('window').close();
	},
	saveServicePeriod: function () {
		var me = this;
		var win = me.getServicePeriodEdit();
		var form = win.form.getForm();
		var formValues = form.getValues();
		if (form.isValid()) {
			var store = this.getGridServicePeriod().getStore();
			if (form.actionMethod == 'POST') {
				store.create(formValues, {
					callback: function (records, operation, success) {
						if (!success) {
							Ext.MessageBox.alert('提示', operation.error);
							return;
						} else {
							store.add(records[0].data);
							store.commitChanges();
							Ext.MessageBox.alert('提示', '新增成功');
							win.close();
						}
					}
				})
			} else {
				if (!form.isDirty()) {
					win.close();
					return;
				}
				form.updateRecord();
				store.update({
					callback: function (records, operation, success) {
						if (!success) {
							Ext.Msg.alert('提示', operation.error);
							return;
						} else {
							Ext.Msg.alert('提示', '更新成功');
							win.close();
						}
					}
				});
			}
		}
	},
	saveServicePeriodCoupon: function () {
		var me = this;
		var win = me.getServicePeriodCouponEdit();
		var store = me.getGridServicePeriodCoupon().getStore();
		var selectedItems = me.getGridServicePeriodCouponEdit().getSelectionModel().getSelection();

		if (selectedItems.length === 0) {
			Ext.Msg.alert('提示', '未选择操作数据');
			return;
		}
		me.tasks.forEach(function (item) {
			var servicePeriodCoupon = [];
			selectedItems.forEach(function (index) {
				servicePeriodCoupon.push({ ServicePeriodSettingID: item.data.ID, CouponTemplateID: index.data.ID, CouponTemplateTitle: index.data.Title });
			});
			store.batchAdd(servicePeriodCoupon, function (response, opts) {
				var ajaxResult = JSON.parse(response.responseText);
				if (ajaxResult.Data == false) {
					Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
					return;
				}
			}, function (response, opts) {
				var ajaxResult = JSON.parse(response.responseText);
				Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
			});
		});
		store.reload();
		win.close();
	},
	deleteServicePeriod: function (btn) {
		var me = this;
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选中要删除的服务周期配置!');
			return;
		}
		Ext.Msg.confirm('提示', '确定要删除配置吗', function (optional) {
			if (optional == 'yes') {
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
							me.getGridServicePeriodCoupon().getStore().reload();
							Ext.Msg.alert('提示', '删除成功');
						} else {
							Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
						}
					},
					function failure(a, b, c) {
						Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
					});
			}
		});
	},
	deleteServicePeriodCoupon: function (btn) {
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选中要删除的卡券!');
			return;
		}
		Ext.Msg.confirm('提示', '确定要删除卡券吗?', function (optional) {
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
							Ext.Msg.alert('提示', result.ErrorMessage);
						}
					},
					function failure(a, b, c) {
						Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
					}
				);
			}
		});
	},
	searchData: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			var store = me.getGridServicePeriod().getStore();
			me.getGridServicePeriod().getSelectionModel().clearSelections();
			store.proxy.extraParams = queryValues;
			store.load();
			me.getGridServicePeriodCoupon().getStore().removeAll();
		} else {
			Ext.MessageBox.alert('提示', '请输入过滤条件!');
		}
	},
	searchProduct: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			queryValues.ProductType = 0;
			var store = me.getGridServiceSelector().getStore();
			store.load({ params: queryValues });
		} else {
			Ext.Msg.alert('提示', '请输入过滤条件');
		}

	}
});