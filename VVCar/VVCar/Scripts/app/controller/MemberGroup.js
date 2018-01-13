Ext.define('WX.controller.MemberGroup', {
    extend: 'Ext.app.Controller',
    requires: ['WX.store.BaseData.MemberGroupStore', 'WX.store.BaseData.MemberGroupTreeStore'],
    models: ['WX.model.BaseData.MemberGroupModel'],
    views: ['MemberGroup.MemberGroup', 'MemberGroup.MemberGroupList', 'MemberGroup.MemberGroupEdit'],
    refs: [{
        ref: 'memberGroupTreePanel',
        selector: 'MemberGroup treepanel'
    }, {
        ref: 'memberGroupList',
        selector: 'MemberGroupList grid'
    }],
    init: function () {
        var me = this;
        me.control({
            'MemberGroup button[action=editmembergroup]': {
                click: me.editmembergroup
            },
            'MemberGroup button[action=refresh]': {
                click: me.refreshmembergrouptree
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
            }
        });
    },
    membergrouplistdbclick: function (grid, record, item, index, e, eOpts) {
        var me = this;
        me.editgroupshow(record);
    },
    refreshmembergrouptree: function (btn) {
        this.getMemberGroupTreePanel().getStore().load();
    },
    editmembergroup: function (btn) {
        var win = Ext.widget('MemberGroupList');
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
            var myStore = me.getMemberGroupList().getStore();
            if (form.actionMethod == 'POST') {
                myStore.create(formValues, {
                    callback: function (records, operation, success) {
                        if (!success) {
                            Ext.MessageBox.alert("操作失败", operation.error);
                            return;
                        } else {
                            myStore.add(records[0].data);
                            myStore.commitChanges();
                            Ext.MessageBox.alert("操作成功", "新增成功");
                            win.close();
                            me.getMemberGroupTreePanel().getStore().load();
                        }
                    }
                });
            } else {
                if (!form.isDirty()) {
                    win.close();
                    return;
                }
                form.updateRecord();
                myStore.update({
                    callback: function (records, operation, success) {
                        if (!success) {
                            Ext.MessageBox.alert("操作失败", operation.error);
                            return;
                        } else {
                            Ext.MessageBox.alert("操作成功", "更新成功");
                            win.close();
                            me.getMemberGroupTreePanel().getStore().load();
                        }
                    }
                });
            }
        }
    },
    deletegroup: function (btn) {
        var selectedItems = this.getMemberGroupList().getSelectionModel().getSelection();
        if (selectedItems.length < 1) {
            Ext.MessageBox.alert("提示", "请先选择需要删除的分组");
            return;
        }
        var me = this;
        Ext.MessageBox.confirm('询问', '您确定要删除吗?', function (opt) {
            if (opt != 'yes') {
                return;
            }
            Ext.Msg.wait('正在处理数据，请稍候……', '状态提示');
            var myStore = me.getMemberGroupList().getStore();
            myStore.remove(selectedItems[0]);
            myStore.sync({
                callback: function (batch, options) {
                    Ext.Msg.hide();
                    if (batch.hasException()) {
                        Ext.MessageBox.alert("操作失败", batch.exceptions[0].error);
                        myStore.rejectChanges();
                    } else {
                        Ext.MessageBox.alert("操作成功", "删除成功");
                        me.getMemberGroupTreePanel().getStore().load();
                    }
                }
            });
        });
    },
    editgroup: function (btn) {
        var me = this;
        var grid = btn.up('grid');
        var selectedrecord = grid.getSelectionModel().getSelection();
        if (selectedrecord.length < 1) {
            Ext.MessageBox.alert("提示", "请先选择需要修改的分组");
            return;
        }
        me.editgroupshow(selectedrecord[0]);
    },
    editgroupshow: function (record) {
        var win = Ext.widget('MemberGroupEdit');
        win.form.loadRecord(record);
        win.form.getForm().actionMethod = 'PUT';
        win.setTitle("编辑分组");
        win.show();
    }
});