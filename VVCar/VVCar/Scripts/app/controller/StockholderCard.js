Ext.define('WX.controller.StockholderCard', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.CouponTemplateInfoStore'],
	models: ['BaseData.CouponTemplateInfoModel'],
	views: ['StockholderCard.StockholderCardList', 'StockholderCard.StockholderCardEdit'],
	refs: [{
		ref: 'stockholderCardList',
		selector: 'StockholderCardList'
	}, {
		ref: 'stockholderCardEdit',
		selector: 'StockholderCardEdit'
	}],
	init: function () {
		var me = this;
		me.control({
			'StockholderCardList button[action=setConsumePointRate]': {
				click: me.setConsumePointRate
			},
			'StockholderCardEdit button[action=save]': {
				click: me.save
			}
		});
	},
	setConsumePointRate: function () {
		var me = this;
		var selectedItems = me.getStockholderCardList().getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '未选择数据');
			return;
		}
		if (selectedItems.length > 1) {
			Ext.Msg.alert('提示', '请选择一条数据');
		}
		var win = Ext.widget('StockholderCardEdit');
		win.down('textfield[name=id]').setValue(selectedItems[0].data.ID);
		win.down('numberfield[name=rate]').setValue(selectedItems[0].data.ConsumePointRate);
		win.setTitle('修改消费返佣比例');
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
			if (form.actionMethod == 'POST') {
				store.create(formValues, {
					callback: function (records, operation, success) {
						if (!success) {
							Ext.MessageBox.alert('提示', operation.error);
							return;
						} else {
							couponPushStore.add(records[0].data);
							couponPushStore.commitChanges();
							Ext.MessageBox.alert('提示', '新增成功');
							win.close();
						}
					}
				});
			} else {
				if (!form.isDirty()) {
					win.close();
					return;
				}
				form.updateRecord();
				store.setConsumePointRate(formValues.id, formValues.rate, function (req, success, res) {
					var response = JSON.parse(res.responseText);
					if (response.IsSuccessful) {
						win.close();
						Ext.Msg.alert('提示', '更新成功');
						store.reload();
					} else {
						Ext.Msg.alert('提示', response.ErrorMessage);
					}
				});
			}
		}
		Ext.Msg.alert('提示', '保存');
	}
});