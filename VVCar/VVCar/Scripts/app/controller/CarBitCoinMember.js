Ext.define('WX.controller.CarBitCoinMember', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.CarBitCoinMemberStore'],
	models: ['BaseData.CarBitCoinMemberModel'],
	views: ['CarBitCoinMember.CarBitCoinMemberList', 'CarBitCoinMember.CarBitCoinMemberEdit'],
	refs: [{
		ref: 'carBitCoinMemberList',
		selector: 'CarBitCoinMemberList'
	}, {
		ref: 'carBitCoinMemberEdit',
		selector: 'CarBitCoinMemberEdit'
	}],
	init: function () {
		var me = this;
		me.control({
			'CarBitCoinMemberList button[action=giveAwayCarBitCoin]': {
				click: me.giveAwayCarBitCoin
			},
			'CarBitCoinMemberList button[action=search]': {
				click: me.search
			},
			'CarBitCoinMemberEdit button[action=save]': {
				click: me.save
			},
		});
	},
	giveAwayCarBitCoin: function () {
		var me = this;
		var selectedItems = me.getCarBitCoinMemberList().getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请选择要赠送的会员!');
			return;
		}
		if (selectedItems.length > 1) {
			Ext.Msg.alert('提示', '一次只能赠送一个会员!');
			return;
		}
		me.task = selectedItems[0].data.ID;
		var win = Ext.widget('CarBitCoinMemberEdit');
		win.form.getForm().actionMethod = 'POST';
		win.setTitle('赠送车比特');
		win.show();
	},
	save: function () {
		var me = this;
		var win = me.getCarBitCoinMemberEdit();
		var form = win.form.getForm();
		var formValues = form.getValues();
		formValues.CarBitCoinMemberID = me.task;
		if (form.isValid()) {
			var store = this.getCarBitCoinMemberList().getStore();
			if (form.actionMethod == 'POST') {
				store.giveAwayCarBitCoin(formValues, function (response, opts) {
					var ajaxResult = JSON.parse(response.responseText);
					if (ajaxResult.Data == false) {
						Ext.Msg.alert("提示", ajaxResult.ErrorMessage);
						return;
					}
				}, function (response, opts) {
					var ajaxResult = JSON.parse(response.responseText);
					Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
				});
			} else {
				win.close();
			}
		}
	},
	search: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			var store = me.getCarBitCoinMemberList().getStore();
			store.proxy.extraParams = queryValues;
			store.load();
		} else {
			Ext.Msg.alert('提示', '请输入过滤条件');
		}
	},
});