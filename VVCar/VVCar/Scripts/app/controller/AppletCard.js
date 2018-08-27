Ext.define('WX.controller.AppletCard', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.CouponTemplateInfoStore'],
	models: ['BaseData.CouponTemplateInfoModel'],
	views: ['AppletCard.AppletCardList', 'AppletCard.AppletCardSelector'],
	refs: [{
		ref: 'appletCardList',
		selector: 'AppletCardList'
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
				click: me.selectCard
			},
			'AppletCardSelector button[action=save]': {
				click: me.save
			},
		});
	},
	save: function (btn) {
		var me = this;
		var win = me.getGridAppletCardSelector();
		var selectedItems = win.getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '未选择数据');
		}
		var store = me.getAppletCardList().getStore();
		store.putInApplet(selectedItems[0].data.ID, function (req, success, res) {
			var response = JSON.parse(res.responseText);
			if (response.IsSuccessful) {
				win.close();
				Ext.Msg.alert('提示', '新增成功');
				store.reload();
				me.getAppletCardSelector().close();
			} else {
				Ext.Msg.alert('提示', response.ErrorMessage);
			}
		});
	},
	selectCard: function () {
		var win = Ext.widget('AppletCardSelector');
		var store = win.down('grid').getStore();
		store.proxy.extraParams = {
			IsPutApplet: false,
			CouponType: -1,
			AproveStatus: -2
		};
		store.limit = 10;
		store.pageSize = 10;
		store.load();
		win.show();
	},
})