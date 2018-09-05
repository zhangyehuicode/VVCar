Ext.define('WX.controller.StockholderCard', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.CouponTemplateInfoStore'],
	models: ['BaseData.CouponTemplateInfoModel'],
	views: ['StockholderCard.StockholderCardList', 'StockholderCard.StockholderCardEdit', 'StockholderCard.StockholderCardSelector'],
	refs: [{
		ref: 'stockholderCardList',
		selector: 'StockholderCardList'
	}, {
		ref: 'stockholderCardEdit',
		selector: 'StockholderCardEdit'
	}, {
		ref: 'stockholderCardSelector',
		selector: 'StockholderCardSelector'
	}, {
		ref: 'gridStockholderCardSelector',
		selector: 'StockholderCardSelector grid[name=stockholderCardList]'
	}],
	init: function () {
		var me = this;
		me.control({
			'StockholderCardList button[action=addStockholderCard]': {
				click: me.addStockholderCard
			},
			'StockholderCardEdit button[action=save]': {
				click: me.save
			},
			'StockholderCardEdit button[action=selectMember]': {
				click: me.selectMember
			},
			'StockholderCardSelector grid[name=stockholderCardList]': {
				itemdblclick: me.chooseMember
			},
			'StockholderCardList': {
				edit: me.stockholderCardEdit,
				afterrender: me.afterrender,
				deleteActionClick: me.deleteStockholder
			}
		});
	},
	addStockholderCard: function () {
		var me = this;
		var win = Ext.widget('StockholderCardEdit');
		win.setTitle('新增股东');
		win.form.getForm().actionMethod = 'GET';
		win.show();
	},
	save: function (btn) {
		var me = this;
		var win = me.getStockholderCardEdit();
		var form = win.down('form').getForm();
		var formValues = form.getValues();
		if (form.isValid()) {
			var store = me.getStockholderCardList().getStore();
			if (form.actionMethod == 'GET') {
				if (!form.isDirty()) {
					win.close();
					return;
				}
				form.updateRecord();
				store.setStockholder(formValues.ID, formValues.ConsumePointRate, formValues.DiscountRate, function (req, success, res) {
					var response = JSON.parse(res.responseText);
					if (response.IsSuccessful) {
						win.close();
						Ext.Msg.alert('提示', '新增成功');
						store.reload();
					} else {
						Ext.Msg.alert('提示', response.ErrorMessage);
					}
				});
			}
		}
	},
	selectMember: function () {
		var win = Ext.widget('StockholderCardSelector');
		var store = win.down('grid').getStore();
		store.proxy.extraParams = {
			IsStockholder: false,
			All: true,
		};
		store.load();
		win.show();
	},
	chooseMember: function (grid, record) {
		var win = Ext.ComponentQuery.query('window[name=StockholderCardEdit]')[0];
		win.down('textfield[name=ID]').setValue(record.data.ID);
		win.down('textfield[name=Name]').setValue(record.data.Name);
		grid.up('window').close();
	},
	stockholderCardEdit: function (editor, context, eOpts) {
		if (context.record.phantom) {//表示新增
			context.store.create(context.record.data, {
				callback: function (records, operation, success) {
					if (!success) {
						Ext.Msg.alert("提示", operation.error);
						return;
					} else {
						context.record.copyFrom(records[0]);
						context.record.commit();
						Ext.Msg.alert("提示", "新增成功");
					}
				}
			});
		} else {
			if (!context.record.dirty)
				return;
			context.store.setStockholder(context.record.data.ID, context.record.data.ConsumePointRate, context.record.data.DiscountRate, function (req, success, res) {
				var response = JSON.parse(res.responseText);
				if (response.IsSuccessful) {
					Ext.Msg.alert('提示', '更新成功');
					store.reload();
				} else {
					Ext.Msg.alert('提示', response.ErrorMessage);
				}
			});
		}
	},
	deleteStockholder: function (grid, record) {
		var me = this;
		var store = me.getStockholderCardList().getStore();
		Ext.Msg.confirm('询问', '确定要删除吗？', function (opt) {
			if (opt === 'yes') {
				store.cancelStockholder(record.data.ID,  function (req, success, res) {
					var response = JSON.parse(res.responseText);
					if (response.IsSuccessful) {
						Ext.Msg.alert('提示', '删除成功');
						store.reload();
					} else {
						Ext.Msg.alert('提示', response.ErrorMessage);
					}
				});
			}
		});
	},
	afterrender: function () {
		var me = this;
		var store = me.getStockholderCardList().getStore();
		var params = {
			IsStockholder: true,
			All: true,
		};
		Ext.apply(store.proxy.extraParams, params);
		store.load();
	}
});