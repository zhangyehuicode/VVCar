Ext.define('WX.store.BaseData.ConsumeHistoryStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.ConsumeHistoryModel',
	pageSize: 25,
	autoLoad: false,
	proxy: {
		type: 'rest',
		enablePaging: false,
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/GetConsumeHistory',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/GetConsumeHistory?All=false',
			getConsumeHistory: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/GetConsumeHistory',
			export: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/ExportConsumeHistory?All=false',
			importConsumeHistoryDataTemplate: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/ImportConsumeHistoryDataTemplate',
			importConsumeHistoryData: Ext.GlobalConfig.ApiDomainUrl + "api/Reporting/ImportConsumeHistoryData",
		},
	},
	export: function (p, cb) {
		Ext.Ajax.request({
			method: "GET",
			url: this.proxy.api.export,
			params: p,
			callback: cb
		});
	},
	importConsumeHistoryDataTemplate: function () {
		Ext.MessageBox.show({
			msg: '正在生成导入模板文件……, 请稍侯',
			progressText: '正在生成导入模板文件……',
			width: 300,
			wait: true,
			waitConfig: { interval: 200 }
		});
		Ext.Ajax.request({
			method: "GET",
			url: this.proxy.api.importConsumeHistoryDataTemplate,
			success: function (options, request) {
				var response = JSON.parse(options.responseText);
				if (response.IsSuccessful) {
					window.location.href = response.Data;
				}
				Ext.MessageBox.hide();
			},
			failure: function (a, b, c) {
				Ext.MessageBox.hide();
			}
		});
	},
	importConsumeHistoryData: function (filename, cb) {
		Ext.Msg.wait('提示', '正在上传数据，请稍候');
		Ext.Ajax.request({
			method: 'GET',
			url: this.proxy.api.importConsumeHistoryData + "?fileName=" + filename,
			timeout: 480000,
			success: function (options, request) {
				var response = JSON.parse(options.responseText);
				if (response.IsSuccessful) {
					Ext.Msg.alert('提示', '数据上传成功');
					Ext.Msg.hide();
				} else {
					Ext.Msg.alert('提示', response.ErrorMessage);
				}
			},
			failure: function (response, opts) {
				if (response.statusText == 'communication failure') {
					Ext.Msg.alert('提示', '导入数据超时, 请联系管理员');
				}
				var responseText = JSON.parse(response.responseText);
				Ext.Msg.alert('提示', responseText.Message + responseText.MessageDetail);
			}
		});
	}
});