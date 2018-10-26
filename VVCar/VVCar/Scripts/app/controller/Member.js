﻿Ext.define('WX.controller.Member', {
    extend: 'Ext.app.Controller',
    requires: ['WX.store.BaseData.MemberStore', "WX.store.BaseData.RechargeHistoryStore", "WX.store.BaseData.MemberCardStore", "WX.store.DataDict.MemberCardStatusStore", "WX.store.DataDict.AdjustTypeDicStore",
        'WX.store.DataDict.SexStore', 'WX.store.BaseData.MemberGroupStore', 'WX.store.BaseData.MemberGroupTreeStore', 'WX.store.BaseData.MemberGradeStore', 'WX.store.DataDict.AdjustMemberPointTypeDicStore', 'WX.store.BaseData.TradeHistoryStore'],
    models: ['BaseData.MemberModel', "WX.model.BaseData.MemberBaseInfoModel", 'WX.model.BaseData.MemberGroupModel'],
    views: ['Member.Member', "Member.EditMember", "Member.ChangeCard", "Member.AdjustBalance", 'Member.MemberGroupList', 'Member.MemberGroupEdit', 'Member.AdjustMemberPoint', 'Member.ChangeMemberGroup', 'Member.MemberDetail'],
    refs: [{
        ref: 'member',
        selector: 'Member grid'
    }, {
        ref: 'treegridMemberGroup',
        selector: 'MemberGroupList treepanel[name=treegridMemberGroup]'
    }, {
        ref: 'treeMemberGroup',
        selector: 'Member treepanel[name=treeMemberGroup]',
    }, {
        ref: 'winChangeCard',
        selector: 'ChangeCard'
    }, {
        ref: 'formChangeCard',
        selector: 'ChangeCard form[name=formChangeCard]'
    }, {
        ref: 'memberGroupList',
        selector: 'MemberGroupList grid'
    }],
    init: function () {
        var me = this;
        me.memberCardStore = Ext.create("WX.store.BaseData.MemberCardStore");
		me.control({
			"Member button[action=search]": {
				click: me.search
			},
			"Member button[action=importMemberTemplate]": {
				click: me.importMemberTemplate
			},
			"Member filefield[name=importMember]": {
				change: me.importMember
			},
			"Member button[action=export]": {
				click: me.exportMember
			},
			'Member button[action=detail]': {
				click: me.detail
			},
            "Member button[action=addMember]": {
                click: me.showAddMember
            },
            "Member button[action=manualAddMember]": {
                click: me.manualAddMember
            },
            "Member button[action=deleteMember]": {
                click: me.deleteMember
            },
            "Member button[action=resetPassword]": {
                click: me.resetPassword
            },
            "Member button[action=reportLoss]": {
                click: me.reportLoss
            },
            "Member button[action=cancelLoss]": {
                click: me.cancelLoss
            },
            "Member button[action=changeCard]": {
                click: me.changeCard
            },
            "Member button[action=editMember]": {
                click: me.editMember
            },
            "Member button[action=adjustBalance]": {
                click: me.showAdjustBalance
            },
            "Member button[action=adjustMemberPoint]": {
                click: me.showAdjustMemberPoint
            },
            "Member button[action=changeGroup]":
            {
                click: me.changeMemberGroup
            },
            'Member grid[name=gridMember]': {
				selectionchange: me.onMemberSelectionchange,
				itemdblclick: me.showMemberDetail
            },
            'Member': {
                afterrender: me.afterrender
            },
            'ChangeMemberGroup button[action=save]': {
                click: me.saveMemberGroupChange
            },
            "EditMember button[action=save]": {
                click: me.saveMember
            },
            "EditMember textfield[name=MobilePhoneNo]": {
                change: me.onMobilePhoneNoChange
            },
            "ChangeCard button[action=confirm]": {
                click: me.confirmChangeCard
            },
            "AdjustBalance button[action=save]": {
                click: me.adjustBalance
            },
            "AdjustMemberPoint button[action=save]": {
                click: me.adjustMemberPointSave
            },
            'Member button[action=manageMemberGroup]': {
                click: me.manageMemberGroup
            },
            'Member treepanel[name=treeMemberGroup]': {
                itemclick: me.ontreeMemberGroupItemClick
            },
            'MemberGroupList button[action=addgroup]': {
                click: me.addgroup
            },
            'MemberGroupList button[action=deletegroup]': {
                click: me.deletegroup
            },
            'MemberGroupList button[action=editgroup]': {
                click: me.editgroup
            },
            'MemberGroupList grid': {
                itemdblclick: me.membergrouplistdbclick
            },
            'MemberGroupEdit button[action=save]': {
                click: me.addgroupsave
            },
        });
	},
	detail: function () {
		var selectedItems = this.getMember().getSelectionModel().getSelection();
		if (selectedItems.length != 1) {
			Ext.Msg.alert("提示", "请选择一条数据");
			return;
		}
		this.showMemberDetail(null, selectedItems[0]);
	},
	showMemberDetail: function (grid, record) {
		var win = Ext.widget("MemberDetail");
		win.form.loadRecord(record);
		win.show();
	},
    getStore: function () {
        return this.getMember().getStore();
    },
    search: function (btn) {
        var filter = btn.up("form").getValues();
        var me = this;
        var store = me.getStore();
        Ext.apply(store.proxy.extraParams, filter);
        store.currentPage = 1;
        store.load();
    },
    showAddMember: function () {
        var win = Ext.widget("EditMember");
        //win.down("form[name=formExtraInfo]").hidden = true;
        //win.down("tabpanel[name=tabpanelExtraInfo]").hidden = true;
        win.show();
    },
    resetPassword: function () {
        var selectedItems = this.getMember().getSelectionModel().getSelection();
        if (selectedItems.length != 1) {
            Ext.Msg.alert("提示", "请选择一条数据");
            return;
        }
        var me = this;
        Ext.Msg.confirm('询问', '确认将密码重置为 <font color=red>123456</font> 吗？', function (opt) {
            if (opt == 'yes') {
                var memberStore = me.getStore();
                memberStore.resetPassword(selectedItems[0].data.ID, function () {
                    memberStore.reload();
                    Ext.MessageBox.alert("提示", "重置密码成功!");
                });
            }
        });
    },
    reportLoss: function () {
        var selectedItems = this.getMember().getSelectionModel().getSelection();
        if (selectedItems.length != 1) {
            Ext.Msg.alert("提示", "请选择一条数据");
            return;
        }
        var me = this;
        Ext.Msg.confirm('询问', '确认将卡片变更为 <font color=red>挂失</font> 状态吗？', function (opt) {
            if (opt == 'yes') {
                var memberStore = me.getStore();
                memberStore.reportLoss(selectedItems[0].data.CardNumber, function () {
                    Ext.MessageBox.alert("提示", "挂失成功!");
                    memberStore.reload();
                });
            }
        });
    },
    cancelLoss: function () {
        var selectedItems = this.getMember().getSelectionModel().getSelection();
        if (selectedItems.length != 1) {
            Ext.Msg.alert("提示", "请选择一条数据");
            return;
        }
        var me = this;
        Ext.Msg.confirm('询问', '确认将卡片变更为 <font color=blue>激活</font> 状态吗？', function (opt) {
            if (opt == 'yes') {
                var memberStore = me.getStore();
                memberStore.cancelLoss(selectedItems[0].data.CardNumber, function () {
                    Ext.MessageBox.alert("提示", "激活成功!");
                    memberStore.reload();
                });
            }
        });
    },
    changeCard: function () {
        var selectedItems = this.getMember().getSelectionModel().getSelection();
        if (selectedItems.length != 1) {
            Ext.Msg.alert("提示", "请选择一条数据");
            return;
        }
        var me = this;
        var win = Ext.widget("ChangeCard");
        var form = me.getFormChangeCard().getForm();
        form.setValues({
            MemberID: selectedItems[0].data.ID
        });
        win.show();
    },
    editMember: function () {
        var selectedItems = this.getMember().getSelectionModel().getSelection();
        if (selectedItems.length != 1) {
            Ext.Msg.alert("提示", "请选择一条数据");
            return;
        }
        this.showEdit(null, selectedItems[0]);
    },
    saveMember: function (btn) {
        var me = this;
        if (btn.up("window").down("form[name=MemberInfo]").Method === "PUT") {
            me.update(btn);
        } else {
            me.addMember(btn);
        }
    },
    addMember: function (btn) {
        var me = this;
        var form = btn.up("window").down("form[name=MemberInfo]");
        if (!form.isValid())
            return;
        var entity = form.getValues();
        var store = me.getStore();
        function success(response) {
            response = JSON.parse(response.responseText);
            if (!response.IsSuccessful) {
                Ext.Msg.alert("提示", response.ErrorMessage);
                return;
            }
            store.reload();
            //me.refreshMemberGroupTree();
            btn.up("window").close();
        }

        if (entity.Birthday.length === 10)
            entity.Birthday += " 00:00:00";
        store.manualAddMember(entity, success);
    },
    update: function (btn) {
        var me = this;
        var win = btn.up("window");
        var form = btn.up("window").down("form[name=MemberInfo]");
        if (!form.isValid())
            return;
        if (!form.isDirty()) {
            win.close();
            return;
        }
        form.updateRecord();
        var entity = form.getRecord().data;
        var store = me.getStore();
        function success(response) {
            response = JSON.parse(response.responseText);
            if (!response.IsSuccessful) {
                Ext.Msg.alert("提示", response.ErrorMessage);
                return;
            }
            //me.refreshMemberGroupTree();
            store.commitChanges();
            store.reload();
            win.close();
            Ext.MessageBox.alert("提示", "更新成功");
        }
        store.updateMember(entity, success);
    },
    showEdit: function (grid, record) {
        var me = this;
        var win = Ext.widget("EditMember");
        win.title = "会员信息编辑";
        win.down('textfield[name=PlateNumber]').hidden = true;
        var memberStore = me.getStore();
        var formMemberInfo = win.down("form[name=MemberInfo]");
        formMemberInfo.Method = "PUT";
        //formMemberInfo.down("displayfield[name=CardNumber]").readOnly = true;
        formMemberInfo.loadRecord(record);
        //var formExtraInfo = win.down("form[name=formExtraInfo]");
        //formExtraInfo.down('displayfield[name=Point]').setValue(record.data.Point);
        //function baseInfoCallback(response) {
        //    response = JSON.parse(response.responseText);
        //    if (!response.IsSuccessful)
        //        return;
        //    var baseInfoRecord = Ext.create("WX.model.BaseData.MemberBaseInfoModel", response.Data);
        //formExtraInfo.loadRecord(baseInfoRecord);
        //formMemberInfo.down("displayfield[name=EffectiveDate]").setValue(baseInfoRecord.data.EffectiveDate);
        //formMemberInfo.down("displayfield[name=ExpiredDate]").setValue(baseInfoRecord.data.ExpiredDate);
        //}
        //基本信息
        //memberStore.getBaseInfo(record.data.ID, baseInfoCallback);

        //储值记录和消费记录
        var rechargeStore = win.down("grid[name=gridRecharge]").store;
        var consumeStore = win.down("grid[name=gridConsume]").store;
        Ext.apply(rechargeStore.proxy.extraParams, { CardNumber: record.data.CardNumber, Limit: 1000 });
        Ext.apply(consumeStore.proxy.extraParams, { CardNumber: record.data.CardNumber, Limit: 1000 });
        rechargeStore.load();
        consumeStore.load();
        win.down("tabpanel[name=tabpanelExtraInfo]").show();
        //win.down("panel[name=membercardinfo]").show();
        //win.down("form[name=formExtraInfo]").show();
        win.show();
    },
    onMobilePhoneNoChange: function (txtField, newValue, oldValue, eOpts) {
        if (txtField.isVisible() === false)//绑定数据时不触发。
            return;
        var me = this;
        if (newValue.length === 11) {
            var store = me.getStore();
            store.getPhoneLocation(newValue, function (response) {
                var result = Ext.decode(response.responseText);
                if (result.IsSuccessful) {
                    txtField.up("form").down("textfield[name=PhoneLocation]").setValue(result.Data);
                }
            });
        }
    },
    confirmChangeCard: function (btn) {
        var me = this;
        var form = me.getFormChangeCard().getForm();
        if (form.isValid()) {
            var formValues = form.getValues();
            if (formValues.NewPassword != formValues.ConfirmPassword) {
                Ext.MessageBox.alert("提示", "密码不一致，请重新输入");
                return;
            }
            Ext.MessageBox.confirm('询问', '是否确认换卡?', function (opt) {
                if (opt == 'yes') {
                    var store = me.getStore();
                    store.changeCard(formValues, function (response) {
                        var result = Ext.decode(response.responseText);
                        if (result.IsSuccessful) {
                            if (result.Data == true) {
                                Ext.MessageBox.alert("提示", "换卡成功", function () {
                                    me.getWinChangeCard().close();
                                    store.reload();
                                });
                            } else {
                                Ext.MessageBox.alert("提示", "换卡失败, 请重试");
                            }
                        } else {
                            Ext.MessageBox.alert("提示", "换卡失败, " + result.ErrorMessage);
                        }
                    });
                }
            });
        }
    },
    showAdjustBalance: function (btn) {
        var selectedItems = this.getMember().getSelectionModel().getSelection();
        if (selectedItems.length != 1) {
            Ext.Msg.alert("提示", "请选择一条数据");
            return;
        }
        var win = Ext.widget("AdjustBalance");
        win.down("textfield[name=CardNumber]").setValue(selectedItems[0].data.CardNumber);
        win.show();
    },
    showAdjustMemberPoint: function (btn) {
        var selectedItems = this.getMember().getSelectionModel().getSelection();
        if (selectedItems.length != 1) {
            Ext.Msg.alert("提示", "请选择一条数据");
            return;
        }
        var win = Ext.widget("AdjustMemberPoint");
        //win.down("textfield[name=CardNumber]").setValue(selectedItems[0].data.CardNumber);
        win.down("textfield[name=MemberID]").setValue(selectedItems[0].data.ID);
        win.down("textfield[name=Point]").setValue(selectedItems[0].data.Point);
        win.show();
    },
    changeMemberGroup: function (btn) {
        var gridDept = btn.up('grid');
        var selectedItems = gridDept.getSelectionModel().getSelection();
        if (selectedItems.length < 1) {
            Ext.MessageBox.alert("提示", "请选择数据");
            return;
        }
        var selectedData = selectedItems[0].data;
        var win = Ext.widget("ChangeMemberGroup");
        win.form.getForm().actionMethod = 'PUT';
        win.form.loadRecord(selectedItems[0]);
        var cmbCurrentRegion = win.down('combo[name=CurrMemberGroup]');
        var cmbDestRegion = win.down('combo[name=DestMemberGroup]');
        //因为cmbCurrentRegion和cmbDestRegion绑定的store是同一对象，
        //所以这里对cmbCurrentRegion的store做的修改也会影响到cmbDestRegion
        cmbCurrentRegion.store.clearFilter();
        cmbCurrentRegion.store.load({
            params: {
                All: true
            }
        });
        cmbCurrentRegion.setValue(selectedData.MemberGroupID);
        cmbDestRegion.setValue(selectedData.MemberGroupID);
        win.show();
    },
    saveMemberGroupChange: function (btn) {
        var me = this;
        var win = btn.up('window');
        var form = win.form.getForm();
        var formValues = form.getValues();
        var record = form.getRecord().data;
        record.MemberGroupID = formValues.DestMemberGroup;
        if (form.isValid()) {
			var store = me.getMember().getStore();
			var storeCategory = me.getTreeMemberGroup().getStore();
            var rr = store.getById(record.ID);
            rr.MemberGroupID = record.MemberGroupID;
            Ext.Msg.show({
                msg: '正在请求数据, 请稍侯',
                progressText: '正在请求数据',
                width: 300,
                wait: true,
                waitConfig: {
                    interval: 200
                }
            });
            var url = Ext.GlobalConfig.ApiDomainUrl + 'api/Member';
            var tms = rr.data;
            Ext.Ajax.request({
                url: url,
                method: 'PUT',
                clientValidation: true,
                jsonData: Ext.JSON.encode(tms),
                callback: function (options, success, response) {
                    if (success) {
                        Ext.MessageBox.hide();
						store.load();
						storeCategory.load();
                        win.close();
                        Ext.MessageBox.alert("操作成功", "更新成功");
                    } else {
                        Ext.MessageBox.hide();
                        Ext.MessageBox.alert("失败，请重试", response.responseText);
                    }
                },
                failure: function (response, options) {
                    Ext.MessageBox.hide();
                    Ext.MessageBox.alert("警告", "出现异常错误！请联系管理员！");
                },
                success: function (response, options) {
                    Ext.MessageBox.hide();
                }
            });
            this.hasUpdateDeptRegion = true;
        }
    },
    adjustBalance: function (btn) {
        var me = this;
        var win = btn.up("window");
        var form = win.down('form').getForm();
        if (!form.isValid())
            return;
        if (!me.MemberCardStore) {
            me.MemberCardStore = Ext.create("WX.store.BaseData.MemberCardStore");
        }
        this.MemberCardStore.adjustBalance(form.getValues(), function (req, success, res) {
            if (success) {
                var result = JSON.parse(res.responseText);
                if (result.IsSuccessful) {
                    Ext.Msg.alert("提示", "操作成功");
                    win.close();
                    me.getMember().getStore().reload();
                } else {
                    Ext.Msg.alert("提示", "操作异常:" + result.ErrorMessage);
                }
            } else {
                Ext.Msg.alert("提示", "操作异常，网络异常：" + res.error);
            }
        });
    },
    adjustMemberPointSave: function (btn) {
        var me = this;
        var form = btn.up('window').down('form');
        if (!form.isValid())
            return;

        var values = form.getValues();
        var MemberStore = me.getMember().getStore();
        var params = {
            MemberID: '',
            PointType: '',
            AdjustPoints: '',
        };
        if (values != null) {
            params.MemberID = values.MemberID;
            params.PointType = 7;
            if (values.AdjustType == 2) {
                params.AdjustPoints = -values.AdjustMemberPoint;
                if (values.Point - values.AdjustMemberPoint < 0) {
                    Ext.MessageBox.alert('提示', '减少的积分不能大于已有积分');
                    return;
                }
            } else {
                params.AdjustPoints = values.AdjustMemberPoint;
            }
        }

        MemberStore.adjustMemberPoint(params, function (req, success, res) {
            if (success) {
                var result = JSON.parse(res.responseText);
                if (result.IsSuccessful) {
                    Ext.Msg.alert("提示", "操作成功");
                    form.up("window").close();
                    me.getMember().getStore().reload();
                } else {
                    Ext.Msg.alert("提示", "操作异常:" + result.ErrorMessage);
                }
            } else {
                Ext.Msg.alert("提示", "操作异常，网络异常：" + res.error);
            }
        });
    },
    onMemberSelectionchange: function (grid, selected, eOpts) {
        var me = this;
        var disabled = false;
        if (selected == null || selected.length > 1) {
            disabled = true;
        }
        me.getMember().down('button[action=resetPassword]').setDisabled(disabled);
        //me.getMember().down('button[action=reportLoss]').setDisabled(disabled);
        //me.getMember().down('button[action=cancelLoss]').setDisabled(disabled);
        //me.getMember().down('button[action=changeCard]').setDisabled(disabled);
        me.getMember().down('button[action=editMember]').setDisabled(disabled);
        //me.getMember().down('button[action=adjustBalance]').setDisabled(disabled);
    },
    importMemberTemplate: function () {
        var me = this;
        me.getMember().store.importMemberTemplate();
    },
    importMember: function (filefield, value, eOpts) {
        var me = this;
        var store = me.getMember().getStore();
        var form = filefield.up('form').getForm();
        if (form.isValid()) {
            Ext.Msg.alert('提示', '确定要导入数据吗?', function (optional) {
                if (optional === 'ok') {
                    form.submit({
                        getTreegridMemberGroup,
                        url: Ext.GlobalConfig.ApiDomainUrl + 'api/UploadFile/UploadMemberExcel',
                        waitMsg: '正在上传文件...',
                        headers: { 'Content-Type': 'multipart/form-data; charset=UTF-8' },
                        clientValidation: true,
                        success: function (form, action) {
                            Ext.Msg.alert('提示', '上传文件成功');
                            store.importMember(action.result.FileName, store);
                            store.reload();
                        },
                        failure: function (form, action) {
                            Ext.Msg.alert('提示', '上传文件失败, ' + action.result.errorMessage);
                        }
                    });
                }
            });
        }
    },
    exportMember: function (btn) {
        Ext.MessageBox.show({
            msg: '正在生成数据……, 请稍侯',
            progressText: '正在生成数据……',
            width: 300,
            wait: true
        });
        var me = this;
        var filter = btn.up("form").getValues();

        var treepanelselectionmodel = me.getTreeMemberGroup().getSelectionModel().getSelection();
        if (treepanelselectionmodel.length > 0) {
            filter.MemberGroupID = treepanelselectionmodel[0].data.ID;
        }

        var store = me.getStore();
        store.exportMember(filter, function (req, success, res) {
            Ext.MessageBox.hide();
            if (res.status === 200) {
                var response = JSON.parse(res.responseText);
                if (response.IsSuccessful) {
                    window.location.href = response.Data;
                } else {
                    Ext.Msg.alert("提示", response.ErrorMessage);
                }
            } else {
                Ext.Msg.alert("提示", "网络请求异常：" + res.status);
            }
        });
    },
    //会员分组
    membergrouplistdbclick: function (grid, record, item, index, e, eOpts) {
        var me = this;
        me.editgroupshow(record);
    },
    manageMemberGroup: function (btn) {
        var win = Ext.widget('MemberGroupList');
        win.setTitle('分类管理');
        win.show();
    },
    addgroup: function (btn) {
        var win = Ext.widget('MemberGroupEdit');
        win.form.getForm().actionMethod = 'POST';
        win.setTitle("新增分组");
        win.show();
    },
    addgroupsave: function (btn) {
        var me = this;
        var win = btn.up('window');
        var form = win.form.getForm();
        var formValues = form.getValues();
        if (form.isValid()) {
			var store = me.getTreegridMemberGroup().getStore();
            var treeMemberGroupStore = me.getTreeMemberGroup().getStore();
            if (form.actionMethod == 'POST') {
                store.addMemberGroup(formValues, function (request, success, response) {
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
                            treeMemberGroupStore.load();
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
                store.updateMemberGroup(formValues, function (request, success, response) {
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
                            treeMemberGroupStore.load();
                            store.reload();
                        } else {
                            Ext.Msg.alert('提示', result.ErrorMessage);
                        }
                    } else {
                        Ext.Msg.alert('提示', result.Message);
                    }
                });
            }
            this.hasUpdateDeptRegion = true;
        }
    },
    deletegroup: function (btn) {
        var me = this;
        var selectedItems = this.getTreegridMemberGroup().getSelectionModel().getSelection();
        if (selectedItems.length < 1) {
            Ext.MessageBox.alert("提示", "请先选择需要删除的分组");
            return;
        }
        var ID = selectedItems[0].get('ID');
        Ext.MessageBox.confirm('询问', '您确定要删除吗?', function (opt) {
            if (opt == 'yes') {
                var store = me.getTreegridMemberGroup().getStore();
                Ext.Msg.wait('正在处理数据，请稍候……', '状态提示');
                store.deleteMemberGroup(ID, function (request, success, response) {
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
                            me.getTreeMemberGroup().getStore().load();
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
    editgroup: function (btn) {
        var me = this;
        var selectedItems = me.getTreegridMemberGroup().getSelectionModel().getSelection();
        if (selectedItems.length < 1) {
            Ext.Msg.alert('提示', '请先选中需要编辑的数据');
            return;
        }
        var win = Ext.widget('MemberGroupEdit');
        win.form.loadRecord(selectedItems[0]);
        win.form.getForm().actionMethod = 'PUT';
        win.setTitle('修改分类');
        win.show();
        //var me = this;
        //var grid = btn.up('grid');
        //var selectedrecord = grid.getSelectionModel().getSelection();
        //if (selectedrecord.length < 1) {
        //	Ext.MessageBox.alert("提示", "请先选择需要修改的分组");
        //	return;
        //}
        //me.editgroupshow(selectedrecord[0]);
    },
    editgroupshow: function (record) {
        var win = Ext.widget('MemberGroupEdit');
        win.form.loadRecord(record);
        win.form.getForm().actionMethod = 'PUT';
        win.setTitle("编辑分组");
        win.show();
    },
    afterrender: function () {
        var memberStore = this.getMember().getStore();
        memberStore.load();
    },
    ontreeMemberGroupItemClick: function (tree, record, item) {
        var me = this;
        var store = me.getMember().getStore();
        Ext.apply(store.proxy.extraParams, {
            All: false, MemberGroupID: record.data.ID
        });
        store.loadPage(1);
    },
    refreshMemberGroupTree: function (firstTime) {
        var me = this;
        var gridMemberGroup = me.getTreeMemberGroup();
        gridMemberGroup.getStore().load({
            callback: function (records, operation, success) {
                if (records.length > 0) {
                    if (firstTime) {
                        gridMemberGroup.getSelectionModel().select(0);
                    }
                    for (var i = 0; i < records.length; i++) {
                        memberGroup = records[i].data;
                        if (memberGroup.ID == '00000000-0000-0000-0000-000000000000')
                            continue;

                    }
                }
            }
        });
    },
    manualAddMember: function (btn) {
        var win = Ext.widget("EditMember");
        var form = win.down('form[name=MemberInfo]');
        win.down('form[name=MemberInfo]').getForm().actionMethod = 'POST';
        win.down('textfield[name=Password]').hidden = true;
        win.setTitle('手动添加会员');
        win.show();
    },
    deleteMember: function (btn) {
        var me = this;
        var selectedItems = btn.up('grid').getSelectionModel().getSelection();
        if (selectedItems.length < 1) {
            Ext.Msg.alert('提示', '请先选择要删除的会员!');
            return;
        }
        Ext.Msg.confirm('提示', '确定要删除会员吗?', function (optional) {
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
    }
});
