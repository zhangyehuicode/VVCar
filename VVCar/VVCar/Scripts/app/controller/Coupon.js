Ext.define('WX.controller.Coupon', {
	extend: 'Ext.app.Controller',
	requires: [],
	views: ['WX.view.Coupon.Coupon', 'WX.view.Coupon.QRCode', 'WX.view.Coupon.CouponPush'],
	refs: [{
		ref: 'coupon',
		selector: 'Coupon'
	}, {
		ref: 'QRCode',
		selector: 'QRCode'
	}, {
		ref: 'couponPush',
		selector: 'CouponPush',
	}, {
		ref: 'treeMemberGroup',
		selector: 'CouponPush treepanel[name=treeMemberGroup]'
	}, {
		ref: 'gridMember',
		selector: 'CouponPush grid[name=gridMember]'
	}, {
		ref: 'gridCoupontemplate',
		selector: 'CouponPush grid[name=coupontemplate]',
	}],
	init: function () {
		var me = this;
		me.control({
			'Coupon button[action=search]': {
				click: me.search
			},
			'QRCode button[action=download]': {
				click: me.download
			},
			'QRCode button[action=copy]': {
				click: me.copy
			},
			'Coupon': {
				publishActionClick: me.publish,
				detailsActionClick: me.details,
				updateItemActionClick: me.updateItem,
				deleteItemActionClick: me.deleteItem,
				immediatePushActionClick: me.immediatePush,
				qrCodeActionClick: me.qrCode,
				receiveHistoryActionClick: me.receiveHistory,
				afterrender: me.afterrender,
			},
			'CouponPush button[action=search]': {
				click: me.searchcouponpushmember
			},
			'CouponPush button[action=grouppush]': {
				click: me.grouppush
			},
			'CouponPush button[action=batchpush]': {
				click: me.batchpush
			},
			'CouponPush treepanel[name=treeMemberGroup]': {
				itemclick: me.ontreeMemberGroupItemClick
			},
			'CouponPush': {
				pushActionClick: me.push,
				afterrender: me.couponpushafterrender
			}
		});
	},
	ontreeMemberGroupItemClick: function (tree, record, item) {
		var me = this;
		var store = me.getGridMember().getStore();
		Ext.apply(store.proxy.extraParams, {
			All: false, MemberGroupID: record.data.ID
		});
		store.loadPage(1);
	},
	publish: function (grid, record) {
		var me = this;
		var win = Ext.widget("QRCode");
		var store = grid.getStore();
		if (record.data.Stock < 1) {
			Ext.Msg.alert('提示', '库存小于1, 不能投放.');
			return;
		} else if (record.data.AproveStatus == 0) {
			Ext.Msg.alert('提示', '未审核');
			return;
		} else if (record.data.AproveStatus == -1) {
			Ext.Msg.alert('提示', '审核已拒绝,审核未通过.');
			return;
		} else if (record.data.AproveStatus == 1) {
			record.data.DeliveryMode = 2;
			record.data.AproveStatus = 2;
			store.updatestatus(record.data, function (request, success, response) {
				if (response.timedout) {
					Ext.Msg.alert('提示', '操作超时');
					store.reload();
					return;
				}
				var result = JSON.parse(response.responseText);
				if (success) {
					if (result.IsSuccessful) {
						if (result.Data) {
							Ext.Msg.alert('提示', '投放成功');
							var url = '';
							if (record.data.IsSpecialCoupon) {
								var coupon = {
									ReceiveOpenID: 'specialcoupon',
									CouponTemplateIDs: [record.data.ID],
								};
								store.receivecouponswidthcode(coupon, function (request, success, response) {
									var result = JSON.parse(response.responseText);
									if (success) {
										if (result.IsSuccessful) {
											store.qrcode(encodeURIComponent(result.Data),
												function success(response, request, c) {
													var result = Ext.decode(c.responseText);
													if (result.IsSuccessful) {
														store.reload();
													} else {
														Ext.Msg.alert("提示", result.ErrorMessage);
													}
												},
												function failure(a, b, c) {
													Ext.Msg.alert("提示", "二维码生成失败");
												}
											);
										} else {
											Ext.Msg.alert('提示', result.Message);
										}
									} else {
										Ext.Msg.alert('提示', result.Message);
									}
								});
							} else {
								store.reload();
							}
						}
					} else {
						Ext.Msg.alert('提示', result.Message);
					}
				} else {
					Ext.Msg.alert('提示', result.Message);
				}
			});
		}

	},
	details: function (grid, record) {
		sessionStorage.setItem('couponTemp', JSON.stringify(record.data));
		window.parent.navigateLink("/CouponAdmin/CouponDetails", record.data.Title + "详情");
	},
	updateItem: function (grid, record) {
		sessionStorage.setItem('coupontemplate', JSON.stringify(record.data));
		window.parent.navigateLink("/CouponAdmin?isfrommodify=true", "修改" + record.data.Title);
	},
	deleteItem: function (grid, record) {
		var me = this;
		Ext.MessageBox.confirm('询问', '您确定要删除吗?', function (opt) {
			if (opt == 'yes') {
				Ext.Msg.wait('正在处理数据，请稍候……', '状态提示');
				var store = me.getCoupon().getStore();
				store.remove(record);
				store.sync({
					callback: function (batch, options) {
						Ext.Msg.hide();
						if (batch.hasException()) {
							Ext.MessageBox.alert("提示", batch.exceptions[0].error);
							store.rejectChanges();
						} else {
							Ext.MessageBox.alert("提示", "删除成功");
						}
					}
				});
			}
		});
	},
	immediatePush: function (grid, record) {
		sessionStorage.setItem('couponTemp', JSON.stringify(record.data));
		//window.parent.navigateLink("/CouponAdmin/ImmediatePush?mch=" + sessionStorage.getItem('companyCode'), record.data.Title + "立即推送");
		cname = 'Coupon';
		mname = 'CouponPush';
		text = '立即推送';
		var mainPanel = parent.Ext.getCmp("tabPanelMain");
		parent.Ext.require('WX.controller.' + cname, function () {
			parent.Ext.onReady(function () {
				parent.Ext.getApplication().loadModule(cname);
				var panel = mainPanel.down(mname);
				if (!panel) {
					var panel = parent.Ext.widget(mname, { title: text, minWidth: 1100 });
					parent.Ext.checkActionPermission(panel);
					mainPanel.add(panel);
					mainPanel.setActiveTab(panel);
				} else {
					//mainPanel.setActiveTab(panel);
					Ext.Msg.alert('提示', '请先关闭之前打开的立即推送界面');
				}
			});
		});
	},
	push: function (grid, record) {
		var me = this;
		Ext.Msg.confirm('询问', '是否确定推送卡[' + JSON.parse(sessionStorage.getItem('couponTemp')).Title + ']给会员[' + record.data.Name + ']?', function (opt) {
			if(opt == 'yes') {
				var coupontemplate = {
					CouponTemplateIDs: [],
					MemberIDs: [],
				};
				coupontemplate.CouponTemplateIDs.push(JSON.parse(sessionStorage.getItem('couponTemp')).ID);
				coupontemplate.MemberIDs.push(record.data.ID);
				var store = me.getGridCoupontemplate().getStore();
				store.push(coupontemplate, function (request, success, response) {
					var result = JSON.parse(response.responseText);
					if (success) {
						if (result.IsSuccessful) {
							Ext.Msg.alert('提示', '推送成功');
						} else {
							Ext.Msg.alert('提示', result.ErrorMessage);
						}
					} else {
						Ext.Msg.alert('提示', result.ErrorMessage);
					}
				})
			}
		});
	},
	grouppush: function (btn) {
		Ext.Msg.alert('提示', '分组推送');
	},
	batchpush: function (btn) {
		var me = this;
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请选择要推送的会员');
			return;
		}
		Ext.Msg.confirm('询问', '是否确定推送卡[' + JSON.parse(sessionStorage.getItem('couponTemp')).Title + ']给所选的' + selectedItems.length + '个会员?', function (opt) {
			if (opt == 'yes') {
				var coupontemplate = {
					CouponTemplateIDs: [],
					MemberIDs: [],
				};
				coupontemplate.CouponTemplateIDs.push(JSON.parse(sessionStorage.getItem('couponTemp')).ID);
				selectedItems.forEach(function (item) {
					coupontemplate.MemberIDs.push(item.data.ID);
				});
				var store = me.getGridCoupontemplate().getStore();
				store.push(coupontemplate, function (request, success, response) {
					var result = JSON.parse(response.responseText);
					if (success) {
						if (result.IsSuccessful) {
							Ext.Msg.alert('提示', '推送成功');
						} else {
							Ext.Msg.alert('提示', result.ErrorMessage);
						}
					} else {
						Ext.Msg.alert('提示', result.ErrorMessage);
					}
				})
			}
		});
	},
	qrCode: function (grid, record) {
		var win = Ext.widget("QRCode");
		var store = grid.getStore();
		if (record.data.IsSpecialCoupon) {
			store.qrcode(record.data.CouponCode,
				function success(response, request, c) {
					var result = Ext.decode(c.responseText);
					if (result.IsSuccessful) {
						win.down('box[name=ImgShow]').autoEl.src = result.Data;
						win.down('textfield[name=Url]').setValue(result.Data);
						win.show();
					} else {
						Ext.Msg.alert("提示", result.ErrorMessage);
					}
				},
				function failure(a, b, c) {
					Ext.Msg.alert("提示", "二维码生成失败");
				}
			);
		} else {
			store.configinfo(
				function success(response, request, c) {
					var result = Ext.decode(c.responseText);
					if (result.IsSuccessful) {
						var url = window.location.href + 'Coupon/CouponInfo?mch=' + result.Data.CompanyCode + "&ctid=" + record.data.ID;
						store.qrcode(encodeURIComponent(url),
							function success(response, request, c) {
								var result = Ext.decode(c.responseText);
								if (result.IsSuccessful) {
									win.down('box[name=ImgShow]').autoEl.src = result.Data;
									win.down('textfield[name=Url]').setValue(result.Data);
									win.show();
								} else {
									Ext.Msg.alert("提示", result.ErrorMessage);
								}
							},
							function failure(a, b, c) {
								Ext.Msg.alert("提示", "二维码生成失败");
							}
						);
					} else {
						Ext.Msg.alert("提示", result.ErrorMessage);
					}
				},
				function failure(a, b, c) {
					Ext.Msg.alert("提示", "二维码生成失败");
				}
			);
		}		
	},
	receiveHistory: function (grid, record) {
		window.parent.navigateLink("/CouponAdmin/ReceivedCouponRecord?CouponType=" + record.data.CouponType + "&TemplateCode=" + record.data.TemplateCode, record.data.Title + "领取记录");
	},
	afterrender: function () {
		var me = this;
		var win = me.getCoupon();
		var store = win.getStore();
		var params = {
			CouponType: -1,
			AproveStatus: -2,
			Nature: -1,
		};
		Ext.apply(store.proxy.extraParams, params);
		win.down('combobox[name=Nature]').setValue([-1, '全部']);
		win.down('combobox[name=CouponType]').setValue([-1, '全部']);
		win.down('combobox[name=AproveStatus]').setValue([-2, '全部状态']);
		store.load();
	},
	couponpushafterrender: function () {
		var me = this;
		var store = me.getGridMember().getStore();
		store.load();
		var coupontemplateStore = me.getGridCoupontemplate().getStore();
		var params = {
			ID: JSON.parse(sessionStorage.getItem('couponTemp')).ID,
			CouponType: -1,
			AproveStatus: -2,
			Nature: -1,
		};
		Ext.apply(coupontemplateStore.proxy.extraParams, params);
		coupontemplateStore.load();
	},
	search: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			var store = me.getCoupon().getStore();
			store.proxy.extraParams = queryValues;
			store.load();
		} else {
			Ext.Msg.alert('提示', '请输入过滤条件');
		}
	},
	searchcouponpushmember: function (btn) {
		var params = btn.up("form").getValues();
		var me = this;
		var store = me.getGridMember().getStore();
		Ext.apply(store.proxy.extraParams, params);
		store.currentPage = 1;
		store.load();
	},
	download: function (btn) {
		var win = btn.up('window');
		var url = win.down('textfield[name=Url]').getValue();
		var alink = document.createElement("a");
		alink.href = url;
		alink.download = 'qrcode';
		alink.click();
	},
	copy: function (btn) {
		var url = Ext.getCmp('linktext');
		Ext.getCmp('linktext').selectText();
		var test = document.execCommand('Copy');
		Ext.Msg.alert('提示', '已复制');
	}
});

