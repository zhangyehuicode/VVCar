Ext.define('WX.controller.AgentDepartment', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.AgentDepartmentStore'],
	models: ['BaseData.AgentDepartmentModel'],
	views: ['AgentDepartment.AgentDepartmentList', 'AgentDepartment.AgentDepartmentCategoryList', 'AgentDepartment.AgentDepartmentChangeCategory', 'AgentDepartment.ManageUserSelector', 'AgentDepartment.TagSelector', 'AgentDepartment.AgentDepartmentCategoryEdit', 'AgentDepartment.AgentDepartmentEdit', 'AgentDepartment.AgentDepartmentTagEdit'],
	refs: [{
		ref: 'agentDepartmentList',
		selector: 'AgentDepartmentList grid'
	}, {
		ref: 'agentDepartmentEdit',
		selector: 'AgentDepartmentEdit'
	}, {
		ref: 'agentDepartmentTagEdit',
		selector: 'AgentDepartmentTagEdit'
	}, {
		ref: 'gridAgentDepartmentTagEdit',
		selector: 'AgentDepartmentTagEdit grid'
	}, {
		ref: 'manageUserSelector',
		selector: 'ManageUserSelector'
	}, {
		ref: 'tagSelector',
		selector: 'TagSelector'
	}, {
		ref: 'gridTagSelector',
		selector: 'TagSelector grid'
	}, {
		ref: 'treegridAgentDepartmentCategory',
		selector: 'AgentDepartmentCategoryList treepanel[name=treegridAgentDepartmentCategory]'
	}, {
		ref: 'treeAgentDepartmentCategory',
		selector: 'AgentDepartmentList treepanel[name=treeAgentDepartmentCategory]',
	}],
	init: function () {
		var me = this;
		me.control({
			'AgentDepartmentList treepanel[name=treeAgentDepartmentCategory]': {
				itemclick: me.ontreeAgentDepartmentCategoryItemClick
			},
			'AgentDepartmentList button[action=manageAgentDepartmentCategory]': {
				click: me.manageAgentDepartmentCategory
			},
			'AgentDepartmentList button[action=addAgentDepartment]': {
				click: me.addAgentDepartment
			},
			'AgentDepartmentList button[action=manageTag]': {
				click: me.manageTag
			},
			'AgentDepartmentList button[action=search]': {
				click: me.search
			},
			'AgentDepartmentList button[action=changeCategory]': {
				click: me.changeAgentDepartmentCategory
			},
			'AgentDepartmentList grid': {
				itemdblclick: me.editAgentDepartment,
			},
			'AgentDepartmentList': {
				deleteActionClick: me.deleteAgentDepartment,
				editActionClick: me.editAgentDepartment,
				afterrender: me.afterrender,
			},
			'AgentDepartmentCategoryList button[action=addAgentDepartmentCategory]': {
				click: me.addAgentDepartmentCategory
			},
			'AgentDepartmentCategoryList button[action=delAgentDepartmentCategory]': {
				click: me.delAgentDepartmentCategory
			},
			'AgentDepartmentCategoryEdit button[action=save]': {
				click: me.saveAgentDeaprtmentCategory
			},
			'AgentDepartmentChangeCategory button[action=saveAgentDepartmentCategory]': {
				click: me.saveAgentDepartmentCategory
			},
			'AgentDepartmentEdit button[action=selectManageUser]': {
				click: me.selectManageUser
			},
			'AgentDepartmentEdit button[action=uploadLicensePic]': {
				click: me.uploadLicensePic
			},
			'AgentDepartmentEdit button[action=uploadDepartmentPic]': {
				click: me.uploadDepartmentPic
			},
			'AgentDepartmentEdit button[action=uploadIDCardFrontPic]': {
				click: me.uploadIDCardFrontPic
			},
			'AgentDepartmentEdit button[action=uploadIDCardBehindPic]': {
				click: me.uploadIDCardBehindPic
			},
			'AgentDepartmentEdit button[action=save]': {
				click: me.save
			},
			'AgentDepartmentTagEdit button[action=addAgentDepartmentTag]': {
				click: me.addAgentDepartmentTag
			},
			'AgentDepartmentTagEdit button[action=deleteAgentDepartmentTag]': {
				click: me.deleteAgentDepartmentTag
			},
			'ManageUserSelector grid[name=manageUserList]': {
				itemdblclick: me.chooseManageUser
			},
			'TagSelector button[action=save]': {
				click: me.saveTag
			}
		});
	},
	selectManageUser: function () {
		var win = Ext.widget('ManageUserSelector');
		var grid = win.down('grid');
		var store = grid.getStore();
		store.load();
		win.show();
	},
	chooseManageUser: function (grid, record) {
		var win = Ext.ComponentQuery.query('AgentDepartmentEdit')[0];
		win.down('textfield[name=UserName]').setValue(record.data.Name);
		win.down('textfield[name=UserID]').setValue(record.data.ID);
		grid.up('window').close();
	},
	addAgentDepartment: function () {
		var me = this;
		var tree = Ext.ComponentQuery.query('treepanel[name=treeAgentDepartmentCategory]');
		var selectedItems = tree[0].getView().getSelectionModel().getSelection();
		//if (selectedItems.length > 0 && selectedItems[0].data.ID != '' && selectedItems[0].data.ID != null && selectedItems[0].data.ID != '00000000-0000-0000-0000-000000000001') {
		if (selectedItems.length > 0 && selectedItems[0].data.ID != '' && selectedItems[0].data.ID != null) {
			var win = Ext.widget('AgentDepartmentEdit');
			if (selectedItems[0].data.ID != '00000000-0000-0000-0000-000000000001') {
				win.down('textfield[name=AgentDepartmentCategoryID]').setValue(selectedItems[0].data.ID);
			}
			win.down('combobox[name=Type]').setValue(0);
			win.form.getForm().actionMethod = 'POST';
			win.setTitle('添加客户');
			win.show();
		} else {
			Ext.Msg.alert('提示', '未选择分类');
		}
	},
	manageTag: function () {
		var me = this;
		var selectedItems = me.getAgentDepartmentList().getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择一条数据');
			return;
		}
		if (selectedItems.length > 1) {
			Ext.Msg.alert('提示', '一次只能选择一条数据');
			return;
		}
		me.tasks = selectedItems;
		var win = Ext.widget('AgentDepartmentTagEdit');
		var grid = win.down('grid');
		var store = grid.getStore();
		store.pageSize = 10;
		var params = {
			AgentDepartmentID: selectedItems[0].data.ID
		};
		Ext.apply(store.proxy.extraParams, params);
		store.load();

		win.show();
	},
	saveTag: function () {
		var me = this;
		var win = me.getTagSelector();
		var store = me.getGridAgentDepartmentTagEdit().getStore();
		var selectedItems = me.getGridTagSelector().getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '未选择要添加的标签');
			return;
		}
		me.tasks.forEach(function (item) {
			var tags = [];
			selectedItems.forEach(function (index) {
				tags.push({ AgentDepartmentID: item.data.ID, TagID: index.data.ID });
			});
			store.batchAdd(tags, function (response, opts) {
				var ajaxResult = JSON.parse(response.responseText);
				if (ajaxResult.Data == false) {
					Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
					return;
				} else {
					Ext.Msg.alert('提示', '添加成功');
				}
			}, function (response, opts) {
				var ajaxResult = JSON.parse(response.responseText);
				Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
			})
		});
		store.reload();
		win.close();
	},
	uploadLicensePic: function (btn) {
		var form = btn.up('form').getForm();
		var win = btn.up('window');
		if (form.isValid()) {
			form.submit({
				url: '/api/UploadFile/UploadLicense',
				waitMsg: '正在上传...',
				success: function (fp, o) {
					if (o.result.success) {
						Ext.Msg.alert('提示', '上传成功');
						win.down('textfield[name=BusinessLicenseImgUrl]').setValue(o.result.FileUrl);
						win.down('box[name=ImgLicenseShow]').el.dom.src = o.result.FileUrl;
					} else {
						Ext.Msg.alert('提示', o.result.errorMessage);
					}
				},
				failure: function (fp, o) {
					Ext.Msg.alert('提示', o.result.errorMessage);
				}
			});
		}
	},
	uploadDepartmentPic: function (btn) {
		var form = btn.up('form').getForm();
		var win = btn.up('window');
		if (form.isValid()) {
			form.submit({
				url: '/api/UploadFile/UploadDepartment',
				waitMsg: '正在上传...',
				success: function (fp, o) {
					if (o.result.success) {
						Ext.Msg.alert('提示', '上传成功');
						win.down('textfield[name=DepartmentImgUrl]').setValue(o.result.FileUrl);
						win.down('box[name=ImgDepartmentShow]').el.dom.src = o.result.FileUrl;
					} else {
						Ext.Msg.alert('提示', o.result.errorMessage);
					}
				},
				failure: function (fp, o) {
					Ext.Msg.alert('提示', o.result.errorMessage);
				}
			});
		}
	},
	uploadIDCardFrontPic: function (btn) {
		var form = btn.up('form').getForm();
		var win = btn.up('window');
		if (form.isValid()) {
			form.submit({
				url: '/api/UploadFile/UploadIDCard',
				waitMsg: '正在上传...',
				success: function (fp, o) {
					if (o.result.success) {
						Ext.Msg.alert('提示', '上传成功');
						win.down('textfield[name=LegalPersonIDCardFrontImgUrl]').setValue(o.result.FileUrl);
						win.down('box[name=ImgIDCardFrontShow]').el.dom.src = o.result.FileUrl;
					} else {
						Ext.Msg.alert('提示', o.result.errorMessage);
					}
				},
				failure: function (fp, o) {
					Ext.Msg.alert('提示', o.result.errorMessage);
				}
			});
		}
	},
	uploadIDCardBehindPic: function (btn) {
		var form = btn.up('form').getForm();
		var win = btn.up('window');
		if (form.isValid()) {
			form.submit({
				url: '/api/UploadFile/UploadIDCard',
				waitMsg: '正在上传...',
				success: function (fp, o) {
					if (o.result.success) {
						Ext.Msg.alert('提示', '上传成功');
						win.down('textfield[name=LegalPersonIDCardBehindImgUrl]').setValue(o.result.FileUrl);
						win.down('box[name=ImgIDCardBehindShow]').el.dom.src = o.result.FileUrl;
					} else {
						Ext.Msg.alert('提示', o.result.errorMessage);
					}
				},
				failure: function (fp, o) {
					Ext.Msg.alert('提示', o.result.errorMessage);
				}
			});
		}
	},
	save: function () {
		var me = this;
		var win = me.getAgentDepartmentEdit();
		var form = win.form.getForm();
		var formValues = form.getValues();
		formValues.DataSource = 0;
		if (form.isValid()) {
			var store = me.getAgentDepartmentList().getStore();
			if (form.actionMethod == 'POST') {
				store.create(formValues, {
					callback: function (records, operation, success) {
						if (!success) {
							Ext.Msg.alert('提示', operation.error);
							store.rejectChanges();
							return;
						} else {
							store.add(records[0].data);
							store.commitChanges();
							Ext.Msg.alert('提示', '新增成功');
							store.reload();
							win.close();
						}
					}
				});
			} else {
				if (!form.isDirty()) {
					win.close();
					return;
				}
				var selectedItems = me.getAgentDepartmentList().getSelectionModel().getSelection();
				if (selectedItems[0].data.ApproveStatus != 0) {
					Ext.Msg.alert('提示', '请选择未审核的数据进行修改');
					return;
				}
				form.updateRecord();
				store.update({
					callback: function (records, operation, success) {
						if (!success) {
							Ext.Msg.alert('提示', operation.error);
							store.rejectChanges();
							return;
						} else {
							Ext.Msg.alert('提示', '更新成功');
							store.commitChanges();
							store.reload();
							win.close();
						}
					}
				});
			}
		}
	},
	editAgentDepartment: function (grid, record) {
		var win = Ext.widget('AgentDepartmentEdit');
		win.form.loadRecord(record);
		win.down('box[name=ImgLicenseShow]').autoEl.src = record.data.BusinessLicenseImgUrl;
		win.down('box[name=ImgIDCardFrontShow]').autoEl.src = record.data.LegalPersonIDCardFrontImgUrl;
		win.down('box[name=ImgIDCardBehindShow]').autoEl.src = record.data.LegalPersonIDCardBehindImgUrl;
		win.form.getForm().actionMethod = 'PUT';
		win.setTitle('编辑客户');
		win.show();
	},
	deleteAgentDepartment: function (grid, record) {
		var me = this;
		Ext.Msg.confirm('询问', '您确定要删除吗?', function (opt) {
			if (opt == 'yes') {
				Ext.Msg.wait('正在处理数据,请稍后...', '状态显示');
				var store = me.getAgentDepartmentList().getStore();
				store.remove(record);
				store.sync({
					callback: function (batch, options) {
						Ext.Msg.hide();
						if (batch.hasException()) {
							Ext.Msg.alert('操作失败', batch.exceptions[0].error);
							store.rejectChanges();
						} else {
							Ext.Msg.alert('操作成功', '删除成功');
							store.reload();
						}
					}
				});
			}
		});
	},
	search: function (btn) {
		var me = this;
		var selectedItems = me.getTreeAgentDepartmentCategory().getSelectionModel().getSelection();
		var store = me.getAgentDepartmentList().getStore();
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			if (selectedItems.length == 1) {
				queryValues.AgentDepartmentCategoryID = selectedItems[0].data.ID;
			}
			queryValues.All = true;
			store.proxy.extraParams = queryValues;
			store.load({ params: queryValues });
		} else {
			Ext.Msg.alert('提示', '请输入过滤条件');
		}
	},
	manageAgentDepartmentCategory: function () {
		var win = Ext.widget('AgentDepartmentCategoryList');
		win.setTitle('分类管理');
		win.show();
	},
	changeAgentDepartmentCategory: function (btn) {
		var gridAgentDepartment = btn.up('grid');
		var selectedItems = gridAgentDepartment.getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请选择数据');
			return;
		}
		if (selectedItems.length > 1) {
			Ext.Msg.alert('提示', '一次只能选择一条数据');
			return;
		}
		var selectedData = selectedItems[0].data;
		var win = Ext.widget("AgentDepartmentChangeCategory");
		win.form.getForm().actionMethod = 'PUT';
		win.form.loadRecord(selectedItems[0]);
		var cmbCurrentRegion = win.down('combo[name=CurrCategory]');
		var cmbDestRegion = win.down('combo[name=DestCategory]');
		cmbCurrentRegion.store.clearFilter();
		cmbCurrentRegion.store.load({
			params: {
				All: true
			}
		});
		if (selectedData.AgentDepartmentCategoryID == null) {
			cmbCurrentRegion.setValue('全部分类');
		} else {
			cmbCurrentRegion.setValue(selectedData.AgentDepartmentCategoryID);
		}
		cmbDestRegion.setValue(selectedData.AgentDepartmentCategoryID);
		win.show();
	},
	saveAgentDepartmentCategory: function (btn) {
		var me = this;
		var win = btn.up('window');
		var form = win.form.getForm();
		var formValues = form.getValues();
		var record = form.getRecord().data;
		record.AgentDepartmentCategoryID = formValues.DestCategory;
		if (form.isValid()) {
			var store = me.getAgentDepartmentList().getStore();
			var rr = store.getById(record.ID);
			if (record.AgentDepartmentCategoryID != '00000000-0000-0000-0000-000000000000') {
				rr.AgentDepartmentCategoryID = record.AgentDepartmentCategoryID;
			} else {
				rr.AgentDepartmentCategoryID = '';
			}
			Ext.Msg.show({
				msg: '正在请求数据，请稍候',
				progressText: '正在请求数据',
				width: 300,
				wait: true,
				waitConfig: {
					interval: 200
				}
			});
			var url = Ext.GlobalConfig.ApiDomainUrl + 'api/AgentDepartment';
			var tms = rr.data;
			Ext.Ajax.request({
				url: url,
				method: 'PUT',
				clientValidation: true,
				jsonData: Ext.JSON.encode(tms),
				callback: function (options, success, response) {
					if (success) {
						Ext.Msg.hide();
						store.load();
						win.close();
						Ext.Msg.alert('操作成功', '更新成功');
					} else {
						Ext.Msg.hide();
						Ext.Msg.alert('失败,请重试', response.responseText);
					}
				},
				failure: function (response, options) {
					Ext.Msg.hide();
					Ext.Msg.alert('警告', '出现异常错误');
				},
				success: function (response, options) {
					Ext.Msg.hide();
				}
			});
			this.hasUpdateDeptRegion = true;
		}
	},
	addAgentDepartmentCategory: function (btn) {
		var win = Ext.widget('AgentDepartmentCategoryEdit');
		win.form.getForm().actionMethod = 'POST';
		win.setTitle('添加分类');
		win.show();
	},
	delAgentDepartmentCategory: function (btn) {
		var me = this;
		var selectedItems = me.getTreegridAgentDepartmentCategory().getView().getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.MessageBox.alert("提示", "请先选中需要删除的数据");
			return;
		}
		var ID = selectedItems[0].get('ID');
		Ext.Msg.confirm('询问', '您确定要删除吗?', function (opt) {
			if (opt == 'yes') {
				var store = me.getTreegridAgentDepartmentCategory().getStore();
				Ext.Msg.wait('正在处理数据，请稍候……', '状态提示');
				store.deleteAgentDepartmentCategory(ID, function (request, success, response) {
					if (response.timedout) {
						Ext.Msg.alert('提示', '操作超时');
						store.reload();
						return;
					}
					var result = JSON.parse(response.responseText);
					if (success) {
						if (result.IsSuccessful && result.Data) {
							Ext.Msg.alert('提示', '操作成功').setStyle('z-index', '20000');
							store.reload();
							me.getTreeProductCategory().getStore().load();
						} else {
							Ext.Msg.alert('提示', "操作失败" + result.ErrorMessage).setStyle('z-index', '20000');
						}
					} else {
						Ext.Msg.alert('提示', result.Message).setStyle('z-index', '20000');
					}
				});
			}
		});
	},
	saveAgentDeaprtmentCategory: function (btn) {
		var me = this;
		var win = btn.up('window');
		var form = win.form.getForm();
		var formValues = form.getValues();
		if (form.isValid()) {
			var store = me.getTreegridAgentDepartmentCategory().getStore();
			var treeAgentDepartmentCategoryStore = me.getTreeAgentDepartmentCategory().getStore();
			if (form.actionMethod == "POST") {
				store.addAgentDepartmentCategory(formValues, function (request, success, response) {
					if (response.timedout) {
						Ext.Msg.alert('提示', '操作超时');
						store.reload();
						return;
					}
					var result = JSON.parse(response.responseText);
					if (success) {
						if (result.IsSuccessful) {
							Ext.Msg.alert('提示', '操作成功');
							win.close();
							store.reload();
							treeAgentDepartmentCategoryStore.load();
						} else {
							Ext.Msg.alert('提示', result.ErrorMessage);
						}
					} else {
						Ext.Msg.alert('提示', result.Message);
					}
				});
			} else {
				if (!form.isDirty()) {
					win.close();
					return;
				}
				form.updateRecord();
				store.updateAgentDepartmentCategory(formValues, function (request, success, response) {
					if (response.timedout) {
						Ext.Msg.alert('提示', '操作超时');
						store.reload();
						return;
					}
					var result = JSON.parse(response.responseText);
					if (success) {
						if (result.IsSuccessful) {
							Ext.Msg.alert('提示', '操作成功');
							win.close();
							treeAgentDepartmentCategoryStore.load();
							store.reload();
						} else {
							Ext.Msg.alert('提示', result.Message);
						}
					} else {
						Ext.Msg.alert('提示', result.Message);
					}
				});
			}
			this.hasUpdateDeptRegion = true;
		}
	},
	ontreeAgentDepartmentCategoryItemClick: function (tree, record, item) {
		var me = this;
		var store = me.getAgentDepartmentList().getStore();
		Ext.apply(store.proxy.extraParams, {
			All: false,
			AgentDepartmentCategoryID: record.data.ID
		});
		store.loadPage(1);
	},
	afterrender: function () {
		var me = this;
		var agentDepartmentStore = me.getAgentDepartmentList().getStore();
		var params = {
			All: true,
		}
		Ext.apply(agentDepartmentStore.proxy.extraParams, params);
		agentDepartmentStore.load();
	},
	addAgentDepartmentTag: function () {
		var win = Ext.widget('TagSelector');
		var grid = win.down('grid');
		var store = grid.getStore();
		store.pageSize = 10;
		store.load();
		win.show();
	},
	deleteAgentDepartmentTag: function (btn) {
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要删除的标签!');
			return;
		}
		Ext.Msg.confirm('提示', '确定要删除选中的标签吗?', function (optional) {
			if (optional === 'yes') {
				var store = btn.up('grid').getStore();
				var ids = [];
				selectedItems.forEach(function (item) {
					ids.push(item.data.ID);
				});
				store.batchDelete(ids,
					function success(response, request, c) {
						var ajaxResult = JSON.parse(c.responseText);
						if (ajaxResult.IsSuccessful) {
							store.reload();
							Ext.Msg.alert('提示', '删除成功');
						} else {
							Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
						}
					},
					function failure(a, b, c) {
						Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
					}
				);
			}
		})
	},
});