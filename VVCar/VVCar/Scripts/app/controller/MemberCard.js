/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.controller.MemberCard', {
    extend: 'Ext.app.Controller',
    requires: ['WX.store.BaseData.MemberCardStore', 'WX.store.BaseData.MakeCodeRuleStore', 'WX.store.BaseData.MemberGroupStore'],
    stores: ['DataDict.MemberCardStatusStore'],
    models: ['BaseData.MemberCardModel'],
    views: ["MemberCard.MemberCard", "MemberCard.GenerateMemberCard", "MemberCard.MemberCardEdit", "MemberCard.ModifyCardRemark"],
    refs: [{
        ref: 'memberCard',
        selector: 'MemberCard'
    }, {
        ref: 'memberCardEdit',
        selector: 'MemberCardEdit'
    }],
    init: function () {
        var me = this;
        me.control({
            "MemberCard button[action=showGenerateMemberCard]": {
                click: me.showGenerateMemberCard
            },
            "MemberCard button[action=search]": {
                click: me.search
            },
            "MemberCard button[action=export]": {
                click: me.exportMemberCard
            },
            "MemberCard button[action=batchmodifyremark]": {
                click: me.batchModifyRemark
            },
            "GenerateMemberCard button[action=preGenerate]": {
                click: me.generateMemberCard
            },
            "GenerateMemberCard button[action=saveGenerate]": {
                click: me.saveGenerate
            },
            "GenerateMemberCard button[action=cancel]": {
                click: me.cancelGenerate
            },
            "GenerateMemberCard": {
                deleteMemberCardClick: me.deletePreGeneratedMemberCard
            },
            "MemberCard": {
                itemdblclick: me.editMemberCard
            },
            'MemberCardEdit button[action=save]': {
                click: me.saveMemberCard
            },
            'ModifyCardRemark button[action=save]': {
                click: me.batchModifyRemarkSave
            }
        });
    },
    batchModifyRemarkSave: function (btn) {
        var me = this;
        Ext.Msg.confirm('询问', '您确定要修改吗？', function (opt) {
            if (opt != 'yes')
                return;
            Ext.Msg.wait('正在处理数据，请稍后...', '状态提示');
            var grid = me.getMemberCard();
            var selectedRecords = grid.getSelectionModel().getSelection();
            if (selectedRecords.length < 1) {
                Ext.Msg.alert('提示', '请先选择需要修改的记录');
                return;
            }
            var cardIds = [];
            for (var i = 0; i < selectedRecords.length; i++) {
                cardIds.push(selectedRecords[i].data.ID);
            }
            var win = btn.up('window');
            var store = grid.getStore();
            var data = {
                CardIds: cardIds,
                Remark: win.down('textfield[name=Remark]').getValue(),
            };
            function success(response) {
                Ext.Msg.hide();
                response = JSON.parse(response.responseText);
                if (!response.IsSuccessful) {
                    Ext.Msg.alert('提示', response.ErrorMessage);
                    return;
                }
                Ext.Msg.alert('提示', '修改备注成功！');
                store.load();
                win.close();
            };
            function failure(response) {
                Ext.Msg.hide();
                Ext.Msg.alert('提示', response.responseText);
            };
            store.batchModifyRemark(data, success, failure);
        });
    },
    batchModifyRemark: function (btn) {
        var grid = this.getMemberCard();
        var selectedRecords = grid.getSelectionModel().getSelection();
        if (selectedRecords.length < 1) {
            Ext.Msg.alert('提示', '请先选择需要修改的记录');
            return;
        }
        var win = Ext.widget('ModifyCardRemark');
        win.show();
    },
    showGenerateMemberCard: function () {
        var generateMemberCard = Ext.widget("GenerateMemberCard");
        generateMemberCard.show();
        Ext.create("WX.store.BaseData.MakeCodeRuleStore").getCode("MemberCardBatchCode", function (res) {
            if (res.status === 200) {
                var code = JSON.parse(res.responseText).Data;
                generateMemberCard.down("textfield[name=BatchCode]").setValue(code);
            }
        });
    },
    generateMemberCard: function (btn) {
        if (!btn.up("form").isValid())
            return;
        var formValues = btn.up("form").getValues();
        formValues.IsGenerate = true;
        var store = btn.up("window").down("grid").getStore();
        Ext.apply(store.proxy.extraParams, formValues);
        store.load({
            scope: this,
            callback: function (records, operation, success) {
                if (!success) {
                    Ext.Msg.alert("提示", operation.error);
                }
            }
        });
    },
    cancelGenerate: function (btn) {
        btn.up("window").close();
    },
    deletePreGeneratedMemberCard: function (grid, record) {
        grid.getStore().remove(record);
    },
    saveGenerate: function (btn) {
        var me = this;
        var store = btn.up("window").down("grid").getStore();
        if (store.data.items.length === 0)
            return;
        var entities = [];
        store.data.items.forEach(function (item) {
            entities.push(item.data);
        });
        function success(response) {
            Ext.MessageBox.close();
            response = JSON.parse(response.responseText);
            if (response.IsSuccessful && response.Data != null) {
                var downloadExcel = function () {
                    window.location.href = response.Data.DownloadLink;
                    btn.up("window").close();
                    me.getMemberCard().getStore().reload();
                };
                if (response.Data.BatchCode != '') {
                    Ext.Msg.alert("提示", "批次代码已重复，自动变更为" + response.Data.BatchCode, downloadExcel);
                } else {
                    downloadExcel();
                }
            }
            else {
                Ext.Msg.alert("提示", response.ErrorMessage);
            }
        }
        function failure(response) {
            Ext.MessageBox.close();
            Ext.Msg.alert("提示", response.responseText);
        }
        Ext.MessageBox.show({
            msg: '正在保存数据……, 请稍侯',
            progressText: '正在保存数据……',
            width: 300,
            wait: true
        });
        store.saveGenerate(entities, success, failure);
    },
    search: function (btn) {
        var store = btn.up("grid").getStore();
        store.currentPage = 1;
        var conditions = btn.up("form").getValues();
        Ext.apply(store.proxy.extraParams, conditions);
        store.load();
    },
    exportMemberCard: function (btn) {
        var grid = btn.up("grid");
        var store = grid.getStore();
        var entities = btn.up("form").getValues();
        function success(response) {
            Ext.MessageBox.close();
            response = JSON.parse(response.responseText);
            if (response.IsSuccessful) {
                window.location.href = response.Data;
            }
            else {
                Ext.Msg.alert("提示", response.ErrorMessage);
            }
        }
        function failure(response) {
            Ext.Msg.alert("提示", response.responseText);
        }
        Ext.MessageBox.show({
            msg: '正在生成数据……, 请稍侯',
            progressText: '正在生成数据……',
            width: 300,
            wait: true
            //waitConfig: { interval: 200 }
        });
        store.exportMemberCard(entities, success, failure);
    },
    editMemberCard: function (grid, record) {
        if (!Ext.validatePermission('MemberCard.AdjustBalance')) {
            Ext.Msg.alert('提示', '没有权限');
            return false;
        }
        var win = Ext.widget("MemberCardEdit");
        win.form.loadRecord(record);
        win.form.getForm().actionMethod = 'PUT';
        if (record.data.CardBalance != 0 || record.data.Status != 0) {
            win.form.down('numberfield[name=CardBalance]').hide();
        }
        win.form.down('datefield[name=ExpiredDate]').setValue(new Date(record.data.ExpiredDate));
        win.show();
    },
    saveMemberCard: function (btn) {
        var me = this;
        var win = me.getMemberCardEdit();
        var form = win.form.getForm();
        var formValues = form.getValues();
        if (!form.isValid())
            return;
        if (!form.isDirty()) {
            win.close();
            return;
        }
        form.updateRecord();
        me.getMemberCard().getStore().update({
            callback: function (records, operation, success) {
                if (!success) {
                    Ext.MessageBox.alert("提示", operation.error);
                    return;
                } else {
                    Ext.MessageBox.alert("提示", "更新成功");
                    win.close();
                }
            }
        });
    }
});