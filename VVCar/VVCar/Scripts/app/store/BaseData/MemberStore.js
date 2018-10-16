Ext.define("WX.store.BaseData.MemberStore", {
	extend: "Ext.data.Store",
	model: "WX.model.BaseData.MemberModel",
	autoLoad: false,
	pageSize: 25,
	proxy: {
		type: "rest",
		url: Ext.GlobalConfig.ApiDomainUrl + "api/Member",
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + "api/Member?All=false",
			add: Ext.GlobalConfig.ApiDomainUrl + "api/Member",
			update: Ext.GlobalConfig.ApiDomainUrl + "api/Member",
			getBaseInfo: Ext.GlobalConfig.ApiDomainUrl + "api/Member/getBaseInfo",
			reportLoss: Ext.GlobalConfig.ApiDomainUrl + "api/Member/reportLoss",
			cancelLoss: Ext.GlobalConfig.ApiDomainUrl + "api/Member/cancelLoss",
			resetPassword: Ext.GlobalConfig.ApiDomainUrl + "api/Member/ResetPassword/",
			changeCard: Ext.GlobalConfig.ApiDomainUrl + "api/Member/ChangeCard/",
			exportMember: Ext.GlobalConfig.ApiDomainUrl + "api/Member/ExportMember?All=false",
			importMember: Ext.GlobalConfig.ApiDomainUrl + "api/Member/ImportMember",
			importMemberTemplate: Ext.GlobalConfig.ApiDomainUrl + "api/Member/ImportMemberTemplate",
			getPhoneLocation: Ext.GlobalConfig.ApiDomainUrl + "api/Member/GetPhoneLoaction/",
			changeMemberGroup: Ext.GlobalConfig.ApiDomainUrl + "api/Member/ChangeMemberGroup",
			adjustMemberPoint: Ext.GlobalConfig.ApiDomainUrl + "api/Member/AdjustMemberPoint",
			manualAddMember: Ext.GlobalConfig.ApiDomainUrl + "api/Member/ManualAddMember",
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + "api/Member/BatchDelete",
			setStockholder: Ext.GlobalConfig.ApiDomainUrl + "api/Member/SetStockholder",
			cancelStockholder: Ext.GlobalConfig.ApiDomainUrl + "api/Member/CancelStockholder",
			getMemberByPlateNumber: Ext.GlobalConfig.ApiDomainUrl + "api/MemberPlate/GetMemberByPlate",
		},
	},
	failure: function (response) {
		Ext.Msg.alert("提示", response.responseText);
	},
	addMember: function (entity, success) {
		Ext.Ajax.request({
			method: "POST",
			url: this.proxy.api.add,
			jsonData: entity,
			success: success,
			failure: this.failure
		});
	},
	updateMember: function (entity, success) {
		Ext.Ajax.request({
			method: "PUT",
			url: this.proxy.api.update,
			jsonData: entity,
			success: success,
			failure: this.failure
		});
	},
	getBaseInfo: function (memberId, success) {
		Ext.Ajax.request({
			method: "GET",
			url: this.proxy.api.getBaseInfo + "?memberID=" + memberId,
			success: success,
			failure: this.failure
		});
	},
	reportLoss: function (cardNumber, success) {
		Ext.Ajax.request({
			method: "POST",
			url: this.proxy.api.reportLoss,
			params: "=" + cardNumber,
			success: success,
			failure: this.failure
		});
	},
	cancelLoss: function (cardNumber, success) {
		Ext.Ajax.request({
			method: "POST",
			url: this.proxy.api.cancelLoss,
			params: "=" + cardNumber,
			success: success,
			failure: this.failure
		});
	},
	resetPassword: function (memberID, success) {
		Ext.Ajax.request({
			method: "POST",
			url: this.proxy.api.resetPassword + memberID,
			success: success,
			failure: this.failure
		});
	},
	changeCard: function (changeCardInfo, success) {
		Ext.Ajax.request({
			method: "POST",
			url: this.proxy.api.changeCard,
			jsonData: changeCardInfo,
			success: success,
			failure: this.failure
		});
	},
	importMember: function (filename, cb) {
		Ext.Ajax.request({
			method: "GET",
			url: this.proxy.api.importMember + "?fileName=" + filename,
			success: function (options, request) {
				var response = JSON.parse(options.responseText);
				if (response.IsSuccessful) {
					Ext.Msg.alert('提示', '数据上传成功');
				} else {
					Ext.Msg.alert('提示', response.ErrorMessage);
				}
			},
			failure: function (response, opts) {
				var responseText = JSON.parse(response.responseText);
				Ext.Msg.alert('提示', responseText.Message + responseText.MessageDetail);
			}
		});
	},
	exportMember: function (p, cb) {
		Ext.Ajax.request({
			method: "GET",
			url: this.proxy.api.exportMember,
			params: p,
			callback: cb
		});
	},
	importMemberTemplate: function () {
		Ext.MessageBox.show({
			msg: '正在生成导入模板文件……, 请稍侯',
			progressText: '正在生成导入模板文件……',
			width: 300,
			wait: true,
			waitConfig: { interval: 200 }
		});
		Ext.Ajax.request({
			method: "GET",
			url: this.proxy.api.importMemberTemplate,
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
	getPhoneLocation: function (phoneNumber, success) {
		Ext.Ajax.request({
			method: "GET",
			url: this.proxy.api.getPhoneLocation + phoneNumber,
			success: success
		});
	},
	changeMemberGroup: function (data, successHanlder) {
		Ext.Ajax.request({
			method: "PUT",
			url: this.proxy.api.changeMemberGroup,
			jsonData: data,
			success: successHanlder
		});
	},
	adjustMemberPoint: function (params, cb) {
		Ext.Ajax.request({
			method: "GET",
			url: this.proxy.api.adjustMemberPoint,
			params: params,
			callback: cb
		});
	},
	manualAddMember: function (entity, success) {
		Ext.Ajax.request({
			method: "POST",
			url: this.proxy.api.manualAddMember,
			jsonData: entity,
			success: success,
			failure: this.failure
		});
	},
	batchDelete: function (ids, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			url: this.proxy.api.batchDelete,
			method: 'DELETE',
			jsonData: { IdList: ids },
			callback: cb
		});
	},
	setStockholder(id, consumePointRate, discountRate, cb) {
		Ext.Ajax.request({
			method: "GET",
			timeout: 300000,
			url: this.proxy.api.setStockholder + '?id=' + id + '&consumePointRate=' + consumePointRate + '&discountRate=' + discountRate,
			// jsonData: entities,
			callback: cb
		});
	},
	cancelStockholder(id, cb) {
		Ext.Ajax.request({
			method: "GET",
			timeout: 300000,
			url: this.proxy.api.cancelStockholder + '?id=' + id,
			// jsonData: entities,
			callback: cb
		});
	},
	getMemberByPlateNumber: function (plateNumber, cb) {
		Ext.Ajax.request({
			method: "GET",
			timeout: 300000,
			url: this.proxy.api.getMemberByPlateNumber + '?plateNumber=' + plateNumber,
			callback: cb,
		});
	}
});