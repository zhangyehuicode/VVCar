Ext.define('WX.controller.ConsumeHistory', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.ConsumeHistoryStore'],
	models: ['BaseData.ConsumeHistoryModel'],
	views: ['Report.ConsumeHistoryList'],
	refs: [{
		ref: 'consumeHistoryList',
		selector: 'ConsumeHistoryList',
	}],
	init: function () {
		var me = this;
		me.control({
			'ConsumeHistoryList button[action=search]': {
				click: me.search
			},
			'ConsumeHistoryList button[action=reset]': {
				click: me.reset
			},
			'ConsumeHistoryList button[action=export]': {
				click: me.export
			},
			'ConsumeHistoryList button[action=importConsumeHistoryDataTemplate]': {
				click: me.importConsumeHistoryDataTemplate
			},
			'ConsumeHistoryList filefield[name=importConsumeHistoryData]': {
				change: me.importConsumeHistoryData
			}
		});
	},
	importConsumeHistoryData: function (filefield, value, eOpts) {
		var me = this;
		var store = me.getConsumeHistoryList().getStore();
		var form = filefield.up('form').getForm();
		if (form.isValid()) {
			Ext.Msg.alert('提示', '确定要导入数据吗?', function (optional) {
				if (optional === 'ok') {
					form.submit({
						url: Ext.GlobalConfig.ApiDomainUrl + 'api/UploadFile/UploadConsumeHistoryExcel',
						waitMsg: '正在上传文件...',
						headers: { 'Content-Type': 'multipart/form-data; charset=UTF-8' },
						clientValidation: true,
						success: function (form, action) {
							Ext.Msg.alert('提示', '上传文件成功');
							store.importConsumeHistoryData(action.result.FileName, store);
							store.reload();
						},
						failure: function (form, action) {
							Ext.Msg.alert('提示', '上传文件失败,' + action.result.ErrorMessage);
						}
					});
				}
			});
		}
	},
	importConsumeHistoryDataTemplate: function () {
		var me = this;
		me.getConsumeHistoryList().store.importConsumeHistoryDataTemplate();
	},
	search: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			var store = me.getConsumeHistoryList().getStore();
			store.proxy.extraParams = queryValues;
			store.currentPage = 1;
			store.load();
		} else {
			Ext.Msg.alert('提示', '请输入过滤条件');
		}
	},
	reset: function (btn) {
		btn.up('form').form.reset();
	},
	export: function (btn) {
		var me = this;
		Ext.Msg.show({
			msg: '正在生成数据...,请稍候',
			progressText: '正在生成数据...',
			width: 300,
			wait: true,
		});
		var queryValues = btn.up('form').getValues();
		var store = me.getConsumeHistoryList().getStore();
		store.export(queryValues, function (req, success, res) {
			Ext.Msg.hide();
			if (res.status === 200) {
				var response = JSON.parse(res.responseText);
				if (response.IsSuccessful) {
					window.location.href = response.Data;
				} else {
					Ext.Msg.alert('提示', response.ErrorMessage);
				}
			} else {
				Ext.Msg.alert('提示', '网络请求异常:' + res.status);
			}
		});
	},
});