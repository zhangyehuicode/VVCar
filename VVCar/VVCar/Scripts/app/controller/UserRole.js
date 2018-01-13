/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.controller.UserRole', {
    extend: 'Ext.app.Controller',
    requires: ['WX.store.BaseData.RoleStore', 'WX.store.BaseData.UserRoleStore', 'WX.view.selector.UserSelector'],
    models: ['BaseData.RoleModel', 'BaseData.UserRoleModel'],
    views: ['UserRole.UserRole'],
    refs: [{
        ref: 'gridRole',
        selector: 'UserRole grid[name=gridRole]'
    }, {
        ref: 'gridUserRole',
        selector: 'UserRole grid[name=gridUserRole]'
    }],
    init: function () {
        var me = this;
        me.control({
            'UserRole': {
                afterrender: me.onUserRoleAfterRender,
            },
            'UserRole grid[name=gridRole]': {
                select: me.onGridRoleSelect,
            },
            'UserRole button[action=addUser]': {
                click: me.onAddUserClick
            },
            'UserRole button[action=deleteUser]': {
                click: me.onDeleteUserClick
            },
        });
    },
    onUserRoleAfterRender: function () {
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
        var store = this.getGridUserRole().getStore();
        Ext.apply(store.proxy.extraParams, {
            All: false,
            roleId: record.data.ID,
        });
        store.reload();
    },
    onAddUserClick: function (button) {
        var me = this;
        var selectedRoles = me.getGridRole().getSelectionModel().getSelection();
        if (selectedRoles.length < 1) {
            Ext.Msg.alert("提示", "请先选择角色");
            return;
        }
        me.userSelector = Ext.create('WX.view.selector.UserSelector', {
            confirmSelectFn: Ext.Function.bind(function () {
                var targetStore = me.userSelector.targetUserGrid.store;
                if (targetStore.getCount() > 0) {
                    var roleId = selectedRoles[0].data.ID;
                    var userRoles = [];
                    for (var i = 0; i < targetStore.getCount() ; i++) {
                        userRoles.push({ RoleID: roleId, UserID: targetStore.getAt(i).data.ID });
                    }
                    me.getGridUserRole().store.batchAdd(userRoles, function (response, opts) {
                        var ajaxResult = JSON.parse(response.responseText);
                        if (ajaxResult.Data == false) {
                            Ext.Msg.alert("操作失败", ajaxResult.ErrorMessage);
                            return;
                        } else {
                            me.getGridUserRole().getStore().reload();
                            me.userSelector.close();
                        }
                    }, function (response, opts) {
                        var ajaxResult = JSON.parse(response.responseText);
                        Ext.Msg.alert("操作失败", ajaxResult.ErrorMessage);
                    });
                } else {
                    me.userSelector.close();
                }
            }, this)
        }).show();
    },
    onDeleteUserClick: function (button) {
        var me = this;
        var selectedItems = me.getGridUserRole().getSelectionModel().getSelection();
        if (selectedItems.length < 1) {
            Ext.Msg.alert("提示", "请选择需要删除关联的数据");
            return;
        }
        var userRoleIds = [];
        for (var i = 0; i < selectedItems.length; i++) {
            userRoleIds.push(selectedItems[i].data.ID);
        }
        var store = me.getGridUserRole().getStore();
        store.batchDelete(userRoleIds, function (response, opts) {
            var ajaxResult = JSON.parse(response.responseText);
            if (ajaxResult.Data == false) {
                Ext.Msg.alert("操作失败", ajaxResult.ErrorMessage);
                return;
            } else {
                store.reload();
            }
        }, function (response, opts) {
            var ajaxResult = JSON.parse(response.responseText);
            Ext.Msg.alert("操作失败", ajaxResult.ErrorMessage);
        });
    },
});
