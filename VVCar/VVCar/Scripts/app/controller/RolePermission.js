/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.controller.RolePermission', {
    extend: 'Ext.app.Controller',
    requires: ['WX.store.BaseData.RoleStore', 'WX.store.BaseData.PermissionStore'],
    models: ['BaseData.RoleModel', 'BaseData.RolePermissionModel', 'BaseData.PermissionFuncModel'],
    views: ['Permission.RolePermission'],
    refs: [{
        ref: 'gridRole',
        selector: 'RolePermission grid[name=gridRole]'
    }, {
        ref: 'permissionForm',
        selector: 'RolePermission form[name=permissionForm]'
    }],    
    init: function () {
        var me = this;
        me.control({
            'RolePermission': {
                afterrender: me.onRolePermissionAfterRender,
            },
            'RolePermission grid[name=gridRole]': {
                select: me.onGridRoleSelect
            },
        });
        me.permissionStore = Ext.create('WX.store.BaseData.PermissionStore');
    },
    onRolePermissionAfterRender: function () {
        var gridRole = this.getGridRole();
        gridRole.getStore().load({
            callback: function (records, operation, success) {
                if (records.length > 0) {
                    gridRole.getSelectionModel().select(0);
                }
            }
        });
    },
    onGridRoleSelect: function (grid, record, index, eOpts) {
        Ext.MessageBox.show({
            msg: '正在请求数据, 请稍侯',
            progressText: '正在请求数据',
            width: 300,
            wait: true,
            waitConfig: { interval: 200 }
        });
        var me = this;
        var roleCode = record.data.Code;
        me.permissionStore.getRolePermissionList(roleCode, function (response, opts) {
            var result = Ext.decode(response.responseText);
            if (result.IsSuccessful === false) {
                Ext.MessageBox.alert("获取权限列表错误", result.ErrorMessage);
                return;
            }
            var permissionList = result.Data;
            var groupList = [];
            var tempPermission = null;
            for (var i = 0; i < permissionList.length; i++) {
                tempPermission = permissionList[i];
                if (tempPermission.ItemList == null || tempPermission.ItemList.length < 1)
                    continue;
                var checkboxGroup = Ext.create('Ext.form.CheckboxGroup', {
                    columns: 5,
                    vertical: true
                });
                for (var j = 0; j < tempPermission.ItemList.length; j++) {
                    var tempItem = tempPermission.ItemList[j];
                    var checkbox = new Ext.form.Checkbox({
                        boxLabel: tempItem.PermissionName,
                        name: 'rbPermission',
                        inputValue: tempItem.PermissionCode,
                        checked: tempItem.IsChecked,
                        permissionType: tempItem.PermissionType,
                        listeners: {
                            change: function (cb, nv, ov) {
                                var assignModel = {};
                                assignModel.RoleCode = roleCode;
                                assignModel.PermissionCode = cb.inputValue;
                                assignModel.PermissionType = cb.permissionType;
                                assignModel.IsChecked = cb.getValue();
                                me.assignPermission(assignModel);
                            }
                        }
                    });
                    checkboxGroup.add(checkbox);
                }
                var fieldSet = Ext.create('Ext.form.FieldSet', {
                    title: permissionList[i].PermissionType,
                    defaultType: 'textfield',
                    items: [checkboxGroup]
                });
                groupList.push(fieldSet);
            }
            var permissionForm = me.getPermissionForm();
            permissionForm.removeAll();
            permissionForm.add(groupList);
            permissionForm.updateLayout();
            Ext.MessageBox.hide();
        });
    },
    assignPermission: function (assignModel) {
        this.permissionStore.assignPermission(assignModel, function (response, opts) {
            var result = Ext.decode(response.responseText);
            if (result.IsSuccessful === false) {
                Ext.MessageBox.alert("配置权限失败，", result.ErrorMessage);
                return;
            }
        });
    },
});
