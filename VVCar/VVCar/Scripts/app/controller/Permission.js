/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.controller.Permission', {
    extend: 'Ext.app.Controller',
    requires: ['WX.store.BaseData.PermissionStore'],
    stores: ['DataDict.PermissionTypeStore', 'DataDict.EnableDisableTypeStore'],
    models: ['BaseData.PermissionFuncModel'],
    views: ['Permission.PermissionList', 'Permission.PermissionEdit'],
    refs: [{
        ref: 'permissionList',
        selector: 'PermissionList'
    }, {
        ref: 'PermissionEdit',
        selector: 'PermissionEdit'
    }],
    init: function () {
        var me = this;
        me.control({
            'PermissionList button[action=addPermission]': {
                click: me.addPermission
            },
            'PermissionList button[action=syncPermission]': {
                click: me.syncPermission
            },
            'PermissionList button[action=search]': {
                click: me.searchName
            },
            'PermissionList': {
                itemdblclick: me.editPermission,
            },
            'PermissionEdit button[action=save]': {
                click: me.savePermission
            },
        });
    },
    addPermission: function () {
        var win = Ext.widget("PermissionEdit");
        win.form.getForm().actionMethod = 'POST';
        win.setTitle('添加权限');
        win.show();
    },
    editPermission: function (grid, record) {
        var win = Ext.widget("PermissionEdit");
        win.form.loadRecord(record);
        win.form.getForm().actionMethod = 'PUT';
        win.setTitle('编辑权限');
        win.down('[name=Code]').setReadOnly(true);
        if (record.data.IsManual == false) {
            win.down('[name=Name]').setReadOnly(true);
            win.down('[name=PermissionType]').setReadOnly(true);
        }
        win.show();
    },
    savePermission: function (btn) {
        var me = this;
        var win = me.getPermissionEdit();
        var form = win.form.getForm();
        var formValues = form.getValues();
        if (form.isValid()) {
            var permissionStore = this.getPermissionList().getStore();
            if (form.actionMethod == 'POST') {
                permissionStore.create(formValues, {
                    callback: function (records, operation, success) {
                        if (!success) {
                            Ext.MessageBox.alert("提示", operation.error);
                            return;
                        } else {
                            permissionStore.add(records[0].data);
                            permissionStore.commitChanges();
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
                permissionStore.update({
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
    syncPermission: function (btn) {
        var me = this;
        Ext.MessageBox.show({
            msg: '正在配置权限',
            progressText: '正在配置权限',
            width: 300,
            wait: true,
            waitConfig: { interval: 200 }
        });
        var permissionStore = me.getPermissionList().getStore();
        permissionStore.syncPermission(function (response, opts) {
            var result = Ext.decode(response.responseText);
            if (result.IsSuccessful) {
                permissionStore.load();
                Ext.MessageBox.alert("提示", "同步已完成!");
            } else {
                Ext.Msg.alert("提示", "同步失败，" + result.ErrorMessage);
            }
        });
    },
    searchName: function (btn) {
        var permissionStore = this.getPermissionList().getStore();
        var queryValues = btn.up('form').getValues();
        if (queryValues != null) {
            permissionStore.load({ params: queryValues });
        } else {
            Ext.MessageBox.alert("系统提示", "请输入过滤条件！");
        }
    }
});
