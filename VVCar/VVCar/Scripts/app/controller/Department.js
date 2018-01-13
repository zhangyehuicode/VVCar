/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.controller.Department', {
    extend: 'Ext.app.Controller',
    requires: ['WX.store.BaseData.DepartmentStore', 'WX.store.DataDict.DataDictStore'],
    models: ['BaseData.DepartmentModel'],
    views: ['Department.DepartmentList', 'Department.DepartmentEdit'],
    refs: [{
        ref: 'departmentList',
        selector: 'DepartmentList'
    }, {
        ref: 'departmentEdit',
        selector: 'DepartmentEdit'
    }],
    init: function () {
        var me = this;
        me.control({
            'DepartmentList button[action=AddDepartment]': {
                click: me.onAddDepartmentClick
            },
            'DepartmentList button[action=EditDepartment]': {
                click: me.onEditDepartmentClick
            },
            'DepartmentList button[action=DeleteDepartment]': {
                click: me.onDeleteDepartmentClick
            },
            'DepartmentList button[action=search]': {
                click: me.searchData
            },
            'DepartmentList': {
                itemdblclick: me.editDepartment,
            },
            'DepartmentEdit button[action=save]': {
                click: me.saveDepartment
            },
        });
    },
    onAddDepartmentClick: function (button) {
        var win = Ext.widget("DepartmentEdit");
        win.form.getForm().actionMethod = 'POST';
        win.setTitle('添加门店');
        win.show();
    },
    onEditDepartmentClick: function (button) {
        var selectedItems = this.getDepartmentList().getSelectionModel().getSelection();
        if (selectedItems.length < 1) {
            Ext.MessageBox.alert("提示", "请先选中需要编辑的数据");
            return;
        }
        this.editDepartment(null, selectedItems[0]);
    },
    onDeleteDepartmentClick: function (button) {
        var selectedItems = this.getDepartmentList().getSelectionModel().getSelection();
        if (selectedItems.length < 1) {
            Ext.MessageBox.alert("提示", "请先选中需要删除的数据");
            return;
        }
        var me = this;
        Ext.MessageBox.confirm('询问', '您确定要删除吗?', function (opt) {
            if (opt != 'yes') {
                return;
            }
            Ext.Msg.wait('正在处理数据，请稍候……', '状态提示');
            var myStore = me.getDepartmentList().getStore();
            myStore.remove(selectedItems[0]);
            myStore.sync({
                callback: function (batch, options) {
                    Ext.Msg.hide();
                    if (!batch.hasException()) {
                        Ext.MessageBox.alert("操作成功", "删除成功");
                    } else {
                        Ext.MessageBox.alert("操作失败", operation.error);
                        myStore.rejectChanges();
                    }
                }
            });
        });
    },
    searchData: function (btn) {
        var queryValues = btn.up('form').getValues();
        if (queryValues != null) {
            var store = this.getDepartmentList().getStore();
            store.proxy.extraParams = queryValues;
            store.load();
        } else {
            Ext.MessageBox.alert("系统提示", "请输入过滤条件！");
        }
    },
    editDepartment: function (grid, record) {
        var win = Ext.widget("DepartmentEdit");
        win.form.loadRecord(record);
        win.form.getForm().actionMethod = 'PUT';
        win.setTitle('编辑门店');
        win.down('[name=Code]').setReadOnly(true);
        win.show();
    },
    saveDepartment: function (btn) {
        var me = this;
        var win = btn.up('window');
        var form = win.form.getForm();
        var formValues = form.getValues();
        if (form.isValid()) {
            var myStore = me.getDepartmentList().getStore();
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
                //timefield控件更新时会格式化成时间类型，需要手动设置成文本型。
                form.getRecord().data.OpeningTime = formValues.OpeningTime;
                form.getRecord().data.ClosingTime = formValues.ClosingTime;
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
});
