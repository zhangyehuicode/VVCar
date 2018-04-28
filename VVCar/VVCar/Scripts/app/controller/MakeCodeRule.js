Ext.define('WX.controller.MakeCodeRule', {
    extend: 'Ext.app.Controller',
    requires: ['WX.store.BaseData.MakeCodeRuleStore'],
    models: ['BaseData.MakeCodeRuleModel'],
    views: ['MakeCodeRule.MakeCodeRuleList'],
    refs: [{
        ref: 'makeCodeRuleList',
        selector: 'MakeCodeRule'
    }],
    init: function () {
        var me = this;
        me.control({
            'MakeCodeRule button[action=addCodeRule]': {
                click: me.addCodeRule
            },
            'MakeCodeRule button[action=editCodeRule]': {
                click: me.editCodeRule
            },
            'MakeCodeRule button[action=deleteCodeRule]': {
                click: me.deleteCodeRule
            },
            'MakeCodeRule button[action=refresh]': {
                click: me.searchData
            },
            'MakeCodeRule': {
                edit: me.onCodeRuleListEdit,
            },
        });
    },
    addCodeRule: function (button) {
        var rowEditing = this.getMakeCodeRuleList().rowEditing;
        rowEditing.cancelEdit();
        var tasteTypeStore = this.getMakeCodeRuleList().getStore();
        var newTasteType = Ext.create('WX.model.BaseData.MakeCodeRuleModel', {
            Code: 'new code',
            Name: 'new Rule code',
        });
        tasteTypeStore.add(newTasteType);
        rowEditing.startEdit(newTasteType, 0);
    },
    editCodeRule: function (button) {
        var selectedItems = this.getMakeCodeRuleList().getSelectionModel().getSelection();
        if (selectedItems.length < 1) {
            Ext.MessageBox.alert("提示", "请先选中需要编辑的数据");
            return;
        }
        rowEditing.startEdit(selectedItems[0], 0);
    },
    deleteCodeRule: function (button) {
        var selectedItems = this.getMakeCodeRuleList().getSelectionModel().getSelection();
        if (selectedItems.length < 1) {
            Ext.MessageBox.alert("提示", "请先选中需要删除的数据");
            return;
        }
        var me = this;
        Ext.MessageBox.confirm('询问', '您确定要删除吗?', function (opt) {
            if (opt == 'yes') {
                Ext.Msg.wait('正在处理数据，请稍候……', '状态提示');
                var CodeRuleStore = me.getMakeCodeRuleList().getStore();
                CodeRuleStore.remove(selectedItems[0]);
                CodeRuleStore.sync({
                    callback: function (batch, options) {
                        Ext.Msg.hide();
                        if (batch.hasException()) {
                            Ext.MessageBox.alert("操作失败", batch.exceptions[0].error);
                            CodeRuleStore.rejectChanges();
                        } else {
                            Ext.MessageBox.alert("操作成功", "删除成功");
                        }
                    }
                });
            }
        });
    },
    searchData: function (btn) {
        var CodeRuleStore = this.getMakeCodeRuleList().getStore();
        CodeRuleStore.load();
    },
    onCodeRuleListEdit: function (editor, context, eOpts) {
        if (context.record.phantom) {//表示新增
            context.store.create(context.record.data, {
                callback: function (records, operation, success) {
                    if (!success) {
                        Ext.MessageBox.alert("提示", operation.error);
                        return;
                    } else {
                        context.record.copyFrom(records[0]);
                        context.record.commit();
                        Ext.MessageBox.alert("提示", "新增成功");
                    }
                }
            });
        } else {
            if (!context.record.dirty)
                return;
            context.store.update({
                callback: function (records, operation, success) {
                    if (!success) {
                        Ext.MessageBox.alert("提示", operation.error.statusText);
                        return;
                    } else {
                        Ext.MessageBox.alert("提示", "更新成功");
                    }
                }
            });
        }
    },
});
