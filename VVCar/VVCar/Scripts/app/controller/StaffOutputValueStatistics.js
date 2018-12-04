﻿Ext.define('WX.controller.StaffOutputValueStatistics', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.StaffOutputValueStatisticsStore'],
	models: ['BaseData.StaffOutputValueStatisticsModel'],
	views: ['Report.StaffOutputValueStatisticsList'],
	refs: [{
		ref: 'staffOutputValueStatisticsList',
		selector: 'StaffOutputValueStatisticsList',
	}],
	init: function () {
		var me = this;
		me.control({
			'StaffOutputValueStatisticsList button[action=search]': {
				click: me.search
			},
			'StaffOutputValueStatisticsList button[action=reset]': {
				click: me.reset
			},
			'StaffOutputValueStatisticsList button[action=export]': {
				click: me.export
			}
		});
	},
	search: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			var store = me.getStaffOutputValueStatisticsList().getStore();
			store.proxy.extraParams = queryValues;
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
		var store = me.getStaffOutputValueStatisticsList().getStore();
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