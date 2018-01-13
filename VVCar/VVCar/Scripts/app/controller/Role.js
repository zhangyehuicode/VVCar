Ext.define('WX.controller.Role', {
    extend: 'Ext.app.Controller',
    requires: ['WX.store.BaseData.RoleStore', 'WX.store.DataDict.DataDictStore'],
    models: ['BaseData.RoleModel', 'DataDict.DataDictModel'],
    views: ['Role.RoleList', 'Role.RoleEdit'],
    refs: [{
        ref: 'roleList',
        selector: 'RoleList'
    }, {
        ref: 'roleEdit',
        selector: 'RoleEdit'
    }],
    init: function () {
        var me = this;
        me.control({
            'RoleList button[action=addRole]': {
                click: me.addRole
            },
            'RoleList button[action=search]': {
                click: me.searchData
            },
            'RoleList': {
                itemdblclick: me.editRole,
                editActionClick: me.editRole,
                deleteActionClick: me.deleteItem
            },
            'RoleEdit button[action=save]': {
                click: me.saveRole
            },
        });
    },
    addRole: function (button) {
        var win = Ext.widget("RoleEdit");
        win.form.getForm().actionMethod = 'POST';
        win.setTitle('添加角色');
        win.show();
    },
    editRole: function (grid, record) {
        var win = Ext.widget("RoleEdit");
        win.form.loadRecord(record);
        win.form.getForm().actionMethod = 'PUT';
        win.setTitle('编辑角色');
        win.show();
    },
    saveRole: function (btn) {
        var me = this;
        var win = me.getRoleEdit();
        var form = win.form.getForm();
        var formValues = form.getValues();
        if (form.isValid()) {
            var roleStore = this.getRoleList().getStore();
            if (form.actionMethod == 'POST') {
                roleStore.create(formValues, {
                    callback: function (records, operation, success) {
                        if (!success) {
                            Ext.MessageBox.alert("提示", operation.error);
                            return;
                        } else {
                            roleStore.add(records[0].data);
                            roleStore.commitChanges();
                            Ext.MessageBox.alert("提示", "新增成功");
                            win.close();
                        }
                    }
                });
            } else {
                if (!form.isDirty()) {
                    win.close();
                    return;
                }
                form.updateRecord();
                roleStore.update({
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
        }
    },
    searchData: function (btn) {
        var roleStore = this.getRoleList().getStore();
        var queryValues = btn.up('form').getValues();
        if (queryValues != null) {
            queryValues.All = true;
            roleStore.load({ params: queryValues });
        } else {
            Ext.MessageBox.alert("系统提示", "请输入过滤条件！");
        }
    },
    deleteItem: function (grid, record) {
        var me = this;
        Ext.MessageBox.confirm('询问', '您确定要删除吗?', function (opt) {
            if (opt == 'yes') {
                Ext.Msg.wait('正在处理数据，请稍候……', '状态提示');
                var roleStore = me.getRoleList().getStore();
                roleStore.remove(record);
                roleStore.sync({
                    callback: function (batch, options) {
                        Ext.Msg.hide();
                        if (batch.hasException()) {
                            Ext.MessageBox.alert("操作失败", batch.exceptions[0].error);
                            roleStore.rejectChanges();
                        } else {
                            Ext.MessageBox.alert("操作成功", "删除成功");
                        }
                    }
                });
            }
        });
    }
});
