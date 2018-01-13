Ext.define('WX.controller.MemberGrade', {
    extend: 'Ext.app.Controller',
    requires: ['WX.store.BaseData.MemberGradeStore', 'WX.view.selector.PosRetailPlanSelector', 'WX.view.selector.PosProductSelector'],
    stores: ['DataDict.MemberGradeStatusStore'],
    models: ['BaseData.MemberGradeModel'],
    views: ['MemberGrade.MemberGradeList', 'MemberGrade.MemberGradeEdit'],
    refs: [{
        ref: 'MemberGradeList',
        selector: 'MemberGradeList'
    }, {
        ref: 'gridMemberGrade',
        selector: 'MemberGradeList grid[name=gridMemberGrade]'
    }, {
        ref: 'MemberGradeEdit',
        selector: 'MemberGradeEdit'
    }, {
        ref: 'panelPosRightDiscount',
        selector: 'MemberGradeEdit container[name=panelPosRightDiscount]'
    }, {
        ref: 'panelPosRightProduct',
        selector: 'MemberGradeEdit container[name=panelPosRightProduct]'
    }],
    init: function () {
        var me = this;
        me.control({
            'MemberGradeList button[action=addMemberGrade]': {
                click: me.addMemberGrade
            },
            'MemberGradeList button[action=editMemberGrade]': {
                click: me.onEditMemberGradeClick
            },
            'MemberGradeList button[action=deleteMemberGrade]': {
                click: me.deleteMemberGrade
            },
            'MemberGradeList button[action=enableMemberGrade]': {
                click: me.enableMemberGrade
            },
            'MemberGradeList button[action=disableMemberGrade]': {
                click: me.disableMemberGrade
            },
            'MemberGradeList button[action=openMemberGrade]': {
                click: me.openMemberGrade
            },
            'MemberGradeList button[action=closeMemberGrade]': {
                click: me.closeMemberGrade
            },
            'MemberGradeList button[action=search]': {
                click: me.searchData
            },
            'MemberGradeList grid[name=gridMemberGrade]': {
                itemdblclick: me.editMemberGrade,
            },
            'MemberGradeEdit button[name=btnSelectPosRightDiscount]': {
                click: me.selectPosRightDiscount
            },
            'MemberGradeEdit button[name=btnSelectPosRightProduct]': {
                click: me.selectPosRightProduct
            },
            'MemberGradeEdit button[action=save]': {
                click: me.saveMemberGrade
            },
            'MemberGradeEdit button[action=cancel]': {
                click: me.backToList
            },
            'MemberGradeEdit button[action=delPosRightDiscountItem]': {
                click: me.delPosRightDiscountItem
            },
            'MemberGradeEdit button[action=delPosRightProductItem]': {
                click: me.delPosRightProductItem
            },
        });
    },
    addMemberGrade: function (button) {
        var me = this;
        var win = Ext.widget("MemberGradeEdit");
        var newGrade = Ext.create('WX.model.BaseData.MemberGradeModel', {
            IsNeverExpires: true,
            IsAllowPointPayment: false,
            IsYunPosIntegration: true,
            Level: me.getGridMemberGrade().getStore().getTotalCount() + 1
        });
        win.form.loadRecord(newGrade);
        win.form.getForm().actionMethod = 'POST';
        me.currentMemberGrade = {
            GradeRights: [],
        };
        me.getGridMemberGrade().hide();
        me.getMemberGradeList().add(win);
    },
    onEditMemberGradeClick: function () {
        var selectedItems = this.getGridMemberGrade().getSelectionModel().getSelection();
        if (selectedItems.length < 1) {
            Ext.Msg.alert("提示", "请先选中需要编辑的数据");
            return;
        }
        this.editMemberGrade(null, selectedItems[0]);
    },
    editMemberGrade: function (grid, record) {
        var me = this;
        var win = Ext.widget("MemberGradeEdit");
        win.form.loadRecord(record);
        win.form.getForm().actionMethod = 'PUT';
        me.currentMemberGrade = {
            GradeRights: [],
        };
        if (record.data.GradeRights !== null && record.data.GradeRights.length > 0) {
            for (var i = 0; i < record.data.GradeRights.length; i++) {
                if (record.data.GradeRights[i].RightType === 0) {
                    me.addPosRightDiscount(record.data.GradeRights[i]);
                } else {
                    me.addPosRightProduct(record.data.GradeRights[i]);
                }
            }
        }
        me.currentMemberGrade = record.data;
        me.getGridMemberGrade().hide();
        me.getMemberGradeList().add(win);
    },
    deleteMemberGrade: function () {
        var selectedItems = this.getGridMemberGrade().getSelectionModel().getSelection();
        if (selectedItems.length < 1) {
            Ext.Msg.alert("提示", "请先选中需要删除的数据");
            return;
        }
        var me = this;
        Ext.Msg.confirm('询问', '您确定要删除吗?', function (opt) {
            if (opt !== 'yes') {
                return;
            }
            var myStore = me.getGridMemberGrade().getStore();
            myStore.remove(selectedItems[0]);
            Ext.Msg.wait('正在处理数据，请稍候……', '提示');
            myStore.sync({
                callback: function (batch, options) {
                    Ext.Msg.hide();
                    if (batch.hasException()) {
                        Ext.Msg.alert("提示", batch.exceptions[0].error);
                        myStore.rejectChanges();
                    } else {
                        Ext.Msg.alert("提示", "删除成功");
                    }
                }
            });
        });
    },
    enableMemberGrade: function (grid, record) {
        this.changeStatus(1);
    },
    disableMemberGrade: function (grid, record) {
        this.changeStatus(0);
    },
    changeStatus: function (status) {
        var selectedItems = this.getGridMemberGrade().getSelectionModel().getSelection();
        if (selectedItems.length < 1) {
            Ext.Msg.alert("提示", "请先选中需要操作的数据");
            return;
        }
        var me = this;
        var myStore = me.getGridMemberGrade().getStore();
        myStore.changeStatus(selectedItems[0].data.ID, status, function (response, opts) {
            var result = Ext.decode(response.responseText);
            if (result.IsSuccessful) {
                Ext.Msg.alert("提示", "操作成功");
                myStore.reload();
            } else {
                Ext.Msg.alert("提示", "操作失败，" + result.ErrorMessage);
            }
        });
    },
    openMemberGrade: function (grid, record) {
        this.changeOpen(false);
    },
    closeMemberGrade: function (grid, record) {
        this.changeOpen(true);
    },
    changeOpen: function (status) {
        var selectedItems = this.getGridMemberGrade().getSelectionModel().getSelection();
        if (selectedItems.length < 1) {
            Ext.Msg.alert("提示", "请先选中需要操作的数据");
            return;
        }
        if (selectedItems[0].data.IsNotOpen == status) {
            Ext.Msg.alert("提示", "操作成功");
            return;
        }
        var me = this;
        var myStore = me.getGridMemberGrade().getStore();
        myStore.changeOpen(selectedItems[0].data.ID, status, function (response, opts) {
            var result = Ext.decode(response.responseText);
            if (result.IsSuccessful) {
                Ext.Msg.alert("提示", "操作成功");
                myStore.reload();
            } else {
                Ext.Msg.alert("提示", "操作失败，" + result.ErrorMessage);
            }
        });
    },
    searchData: function (btn) {
        var myStore = this.getGridMemberGrade().getStore();
        var queryValues = btn.up('form').getValues();
        if (queryValues !== null) {
            queryValues.All = true;
            myStore.load({ params: queryValues });
        } else {
            Ext.Msg.alert("系统提示", "请输入查询条件！");
        }
    },
    backToList: function (btn) {
        var me = this;
        me.getMemberGradeList().remove(me.getMemberGradeEdit(), true);
        me.getGridMemberGrade().show();
        me.getGridMemberGrade().getStore().reload();
    },
    addPosRightDiscount: function (gradeRight) {
        if (gradeRight === null || gradeRight.RightType !== 0)
            return;
        var me = this;
        if (me.currentMemberGrade.GradeRights !== null && me.currentMemberGrade.GradeRights.length > 0) {
            var hasAdded = false;
            for (var i = 0; i < me.currentMemberGrade.GradeRights.length; i++) {
                if (me.currentMemberGrade.GradeRights[i].RightType === gradeRight.RightType && me.currentMemberGrade.GradeRights[i].PosRightID === gradeRight.PosRightID) {
                    hasAdded = true;
                    break;
                }
            }
            if (hasAdded)
                return;
        }
        me.currentMemberGrade.GradeRights.push(gradeRight);
        me.getPanelPosRightDiscount().add({
            xtype: 'container',
            name: 'pnlPosRightDiscountItem',
            layout: 'hbox',
            border: false,
            padding: '0 0 5 0',
            defaults: {
                labelAlign: 'left',
                labelWidth: 30,
                margin: '0 20 0 0',
            },
            items: [{
                xtype: 'hiddenfield',
                name: 'RightType',
                value: gradeRight.RightType,
            }, {
                xtype: 'hiddenfield',
                name: 'PosRightID',
                value: gradeRight.PosRightID,
            }, {
                xtype: 'displayfield',
                name: "PosRightCode",
                fieldLabel: '编号',
                value: gradeRight.PosRightCode,
            }, {
                xtype: 'displayfield',
                name: 'PosRightName',
                fieldLabel: '名称',
                value: gradeRight.PosRightName
            }, {
                xtype: 'button',
                action: 'delPosRightDiscountItem',
                text: '删除',
            }]
        });
    },
    addPosRightProduct: function (gradeRight) {
        if (gradeRight === null || gradeRight.RightType !== 1)
            return;
        var me = this;
        if (me.currentMemberGrade.GradeRights !== null && me.currentMemberGrade.GradeRights.length > 0) {
            var hasAdded = false;
            for (var i = 0; i < me.currentMemberGrade.GradeRights.length; i++) {
                if (me.currentMemberGrade.GradeRights[i].RightType === gradeRight.RightType && me.currentMemberGrade.GradeRights[i].PosRightID === gradeRight.PosRightID) {
                    hasAdded = true;
                    break;
                }
            }
            if (hasAdded)
                return;
        }
        me.currentMemberGrade.GradeRights.push(gradeRight);
        me.getPanelPosRightProduct().add({
            xtype: 'container',
            name: 'pnlPosRightProductItem',
            layout: 'hbox',
            border: false,
            padding: '0 0 5 0',
            defaults: {
                labelAlign: 'left',
                labelWidth: 30,
                margin: '0 20 0 0',
            },
            items: [{
                xtype: 'hiddenfield',
                name: 'RightType',
                value: gradeRight.RightType,
            }, {
                xtype: 'hiddenfield',
                name: 'PosRightID',
                value: gradeRight.PosRightID,
            }, {
                xtype: 'displayfield',
                name: "PosRightCode",
                fieldLabel: '编号',
                value: gradeRight.PosRightCode,
            }, {
                xtype: 'displayfield',
                name: 'PosRightName',
                fieldLabel: '名称',
                value: gradeRight.PosRightName
            }, {
                xtype: 'button',
                action: 'delPosRightProductItem',
                text: '删除',
            }]
        });
    },
    selectPosRightDiscount: function () {
        var me = this;
        Ext.widget('PosRetailPlanSelector', {
            multiSelect: true,
            onConfirm: function (selectedData) {
                if (selectedData === null || selectedData.length < 1)
                    return;
                var tempData;
                for (var i = 0; i < selectedData.length; i++) {
                    tempData = selectedData[i];
                    me.addPosRightDiscount({
                        RightType: 0,
                        PosRightID: tempData.ID,
                        PosRightCode: tempData.Code,
                        PosRightName: tempData.Name,
                        PosRightDiscount: tempData.Discount,
                    });
                }
            }
        });
    },
    selectPosRightProduct: function () {
        var me = this;
        Ext.widget('PosProductSelector', {
            multiSelect: true,
            onConfirm: function (selectedData) {
                if (selectedData === null || selectedData.length < 1)
                    return;
                var tempData;
                for (var i = 0; i < selectedData.length; i++) {
                    tempData = selectedData[i];
                    me.addPosRightProduct({
                        RightType: 1,
                        PosRightID: tempData.ID,
                        PosRightCode: tempData.Code,
                        PosRightName: tempData.Name,
                    });
                }
            }
        });
    },
    delPosRightDiscountItem: function (btn) {
        var me = this;
        var pnlPosRightDiscountItem = btn.up('container[name=pnlPosRightDiscountItem]');
        var posRightID = pnlPosRightDiscountItem.down('hiddenfield[name=PosRightID]').getValue();
        for (var i = 0; i < me.currentMemberGrade.GradeRights.length; i++) {
            if (me.currentMemberGrade.GradeRights[i].RightType === 0 && me.currentMemberGrade.GradeRights[i].PosRightID === posRightID) {
                me.currentMemberGrade.GradeRights.splice(i, 1);
                break;
            }
        }
        me.getPanelPosRightDiscount().remove(pnlPosRightDiscountItem);
    },
    delPosRightProductItem: function (btn) {
        var me = this;
        var pnlPosRightProductItem = btn.up('container[name=pnlPosRightProductItem]');
        var posRightID = pnlPosRightProductItem.down('hiddenfield[name=PosRightID]').getValue();
        for (var i = 0; i < me.currentMemberGrade.GradeRights.length; i++) {
            if (me.currentMemberGrade.GradeRights[i].RightType === 1 && me.currentMemberGrade.GradeRights[i].PosRightID === posRightID) {
                me.currentMemberGrade.GradeRights.splice(i, 1);
                break;
            }
        }
        me.getPanelPosRightProduct().remove(pnlPosRightProductItem);
    },
    saveMemberGrade: function (btn) {
        var me = this;
        var win = me.getMemberGradeEdit();
        var form = win.form.getForm();
        var formValues = form.getValues();
        if (!form.isValid()) {
            return;
        }
        var saveAction = function () {
            Ext.Msg.show({
                msg: '正在保存中……, 请稍侯',
                progressText: '正在保存中……',
                width: 300,
                wait: true,
                waitConfig: { interval: 200 }
            });
            var myStore = me.getGridMemberGrade().getStore();
            if (form.actionMethod === 'POST') {
                formValues.GradeRights = me.currentMemberGrade.GradeRights;
                myStore.create(formValues, {
                    callback: function (records, operation, success) {
                        Ext.Msg.hide();
                        if (!success) {
                            Ext.Msg.alert("提示", operation.error);
                            return;
                        } else {
                            myStore.add(records[0].data);
                            myStore.commitChanges();
                            Ext.Msg.alert("提示", "新增成功");
                            me.backToList();
                        }
                    }
                });
            } else {
                form.getRecord().set('GradeRights', me.currentMemberGrade.GradeRights);
                form.updateRecord();
                myStore.update({
                    callback: function (records, operation, success) {
                        Ext.Msg.hide();
                        if (!success) {
                            Ext.Msg.alert("提示", operation.error);
                            return;
                        } else {
                            Ext.Msg.alert("提示", "更新成功");
                            me.backToList();
                        }
                    }
                });
            }
        };
        if (!formValues.IsDefault) {
            saveAction();
        } else {
            Ext.Msg.confirm('询问', '已有等级设置为默认，是否替换成当前等级!', function (opt) {
                if (opt !== 'yes') {
                    return;
                }
                saveAction();
            });
        }
    },
});
