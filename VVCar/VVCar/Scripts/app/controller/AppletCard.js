Ext.define('WX.controller.AppletCard', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.CouponTemplateInfoStore'],
	models: ['BaseData.CouponTemplateInfoModel'],
	views: ['AppletCard.AppletCardList', 'AppletCard.AppletCardEdit', 'AppletCard.AppletCardSelector'],
	refs: [{
		ref: 'appletCardList',
		selector: 'AppletCardList'
	}, {
		ref: 'appletCardEdit',
		selector: 'AppletCardEdit'
	}, {
		ref: 'appletCardSelector',
		selector: 'AppletCardSelector'
	}, {
		ref: 'gridAppletCardSelector',
		selector: 'AppletCardSelector grid[name=appletCardList]'
	}],
	init: function () {
		var me = this;
		me.control({
			'AppletCardList button[action=addAppletCard]': {
				click: me.addAppletCard
			},
			'AppletCardEdit button[action=save]': {
				click: me.save
			},
			'AppletCardEdit button[action=selectCard]': {
				click: me.selectCard
			},
			'AppletCardSelector grid[name=appletCardList]': {
				itemdblclick: me.chooseCard
			},
		});
	},
	addAppletCard: function () {
		var me = this;
		var win = Ext.widget('AppletCardEdit');
		win.setTitle('新增小程序卡券');
		win.form.getForm().actionMethod = 'GET';
		win.show();
	},
	save: function (btn) {
		var me = this;
		var win = me.getAppletCardEdit();
		var form = win.down('form').getForm();
		var formValues = form.getValues();
		if (form.isValid()) {
			var store = me.getAppletCardList().getStore();
			if (form.actionMethod == 'GET') {
				if (!form.isDirty()) {
					win.close();
					return;
				}
				form.updateRecord();
				store.putInApplet(formValues.ID, function (req, success, res) {
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
	selectCard: function () {
		var win = Ext.widget('AppletCardSelector');
		var store = win.down('grid').getStore();
		store.proxy.extraParams = {
			IsPutApplet: false,
			CouponType: -1,
			AproveStatus: -2
		};
		store.load();
		win.show();
	},
	chooseCard: function (grid, record) {
		var win = Ext.ComponentQuery.query('window[name=AppletCardEdit]')[0];
		win.down('textfield[name=ID]').setValue(record.data.ID);
		win.down('textfield[name=Title]').setValue(record.data.Title);
		grid.up('window').close();
	},
})