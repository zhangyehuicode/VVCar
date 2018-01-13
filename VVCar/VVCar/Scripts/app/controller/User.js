/// <reference path="../../ext/ext-all-dev.js" />
(function () {
    
    Ext.define('WX.controller.User', {
        extend: 'Ext.app.Controller',
        requires: ['WX.store.BaseData.UserStore', 'WX.store.BaseData.DepartmentStore', 'WX.store.BaseData.DepartmentTreeStore'],
        models: ['BaseData.UserModel', 'BaseData.DepartmentModel'],
        views: ['User.UserList', 'User.UserEdit'],
        refs: [{
            ref: 'userList',
            selector: 'UserList grid[name=userList]'
        }, {
            ref: 'userEdit',
            selector: 'UserEdit'
        }],
        init: function () {
            var me = this;
            me.control({
                'UserList button[action=AddUser]': {
                    click: me.onAddUserClick
                },
                'UserList button[action=EditUser]': {
                    click: me.onEditUserClick
                },
                'UserList button[action=search]': {
                    click: me.searchData
                },
                'UserList button[action=resetPwd]': {
                    click: me.onResetPwdClick
                },
                "UserList treepanel[name=departmentTree]": {
                    selectionchange: me.selectionChange
                },
                'UserList button[action=deleteUsers]': {
                    click: me.onDeleteUsersClick
                },
                'UserList': {
                    itemdblclick: me.editUser,
                },
                'UserEdit button[action=save]': {
                    click: me.saveUser
                },
            });
        },
        onAddUserClick: function (button) {
            var win = Ext.widget("UserEdit");
            win.form.getForm().actionMethod = 'POST';
            win.setTitle('添加用户');
            win.show();
        },
        onEditUserClick: function (button) {
            var selectedItems = this.getUserList().getSelectionModel().getSelection();
            if (selectedItems.length < 1) {
                Ext.MessageBox.alert("提示", "请先选中需要编辑的数据");
                return;
            }
            this.editUser(null, selectedItems[0]);
        },
        searchData: function (btn) {
            var queryValues = btn.up('form').getValues();
            if (queryValues != null) {
                btn.up("grid").down("pagingtoolbar").store.currentPage = 1;
                var store = this.getUserList().getStore();
                store.proxy.extraParams = queryValues;
                store.load();
            } else {
                Ext.MessageBox.alert("系统提示", "请输入过滤条件！");
            }
        },
        editUser: function (grid, record) {
            var win = Ext.widget("UserEdit");
            win.form.loadRecord(record);
            win.form.getForm().actionMethod = 'PUT';
            win.setTitle('编辑用户');
            win.show();
        },
        onResetPwdClick: function (btn) {
            Ext.Msg.confirm('提示？', '确定要重置密码吗？', function (optional) {
                if (optional === "yes") {
                    var selectedItems = btn.up("grid").getSelectionModel().getSelection();
                    if (selectedItems.length === 0)
                        Ext.Msg.alert("未选择操作数据。");
                    var store = btn.up("grid").store;
                    var ids = [];
                    selectedItems.forEach(function (item) {
                        ids.push(item.data.ID);
                    });
                    store.resetPwds(ids,
                    function success(response, request, c) {
                        var result = Ext.decode(c.responseText);
                        if (result.IsSuccessful) {
                            store.reload();
                            Ext.Msg.alert("提示", "重置成功");
                        } else {
                            Ext.Msg.alert("提示", "重置失败。" + result.ErrorMessage);
                        }
                    },
                    function failure(a, b, c) {
                        Ext.Msg.alert("提示", "重置失败");
                    });

                }
            });
        },
        onDeleteUsersClick: function (btn) {
            Ext.Msg.confirm('提示？', '确定要删除用户吗？', function (optional) {
                if (optional === "yes") {
                    var selectedItems = btn.up("grid").getSelectionModel().getSelection();
                    if (selectedItems.length === 0)
                        Ext.Msg.alert("未选择操作数据。");
                    var store = btn.up("grid").store;
                    var ids = [];
                    selectedItems.forEach(function (item) {
                        ids.push(item.data.ID);
                    });
                    store.deleteUsers(ids,
                    function success(response, request, c) {
                        var result = Ext.decode(c.responseText);
                        if (result.IsSuccessful) {
                            store.reload();
                            Ext.Msg.alert("提示", "删除成功");
                        } else {
                            Ext.Msg.alert("提示", "删除失败。" + result.ErrorMessage);
                        }
                    },
                    function failure(a, b, c) {
                        Ext.Msg.alert("提示", "删除失败");
                    });

                }
            });
        },
        saveUser: function (btn) {
            var me = this;
            var win = me.getUserEdit(); //btn.up('window');
            var form = win.form.getForm();
            var formValues = form.getValues();
            if (form.isValid()) {
                var myStore = me.getUserList().getStore();
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
                            }
                        }
                    });
                }
            }
        },
        selectionChange:function(departmentTree, selected, eOpts) {
        if (selected.length === 1) {
            var departmentId = selected[0].data.ID;
            loadByDepartmentId(departmentId, this.getUserList().getStore());
        }
}
    });


    function loadByDepartmentId(departmentId,userStore) {
        var store = userStore;
        store.currentPage = 1;
        Ext.apply(store.proxy.extraParams, { Department: departmentId });
        store.load();
    }

    //function selectionChange(departmentTree, selected, eOpts) {
    //    if (selected.length === 1) {
    //        var departmentId = selected[0].data.ID;
    //        loadByDepartmentId(departmentId, departmentTree.up("container").down("grid").getStore());
    //    }
    //}
}());