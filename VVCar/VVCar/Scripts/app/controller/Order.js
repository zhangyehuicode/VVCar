Ext.define('WX.controller.Order', {
    extend: 'Ext.app.Controller',
    requires: ['WX.store.BaseData.OrderStore'],
    models: ['BaseData.OrderModel'],
    views: ['Shop.Order', 'Shop.OrderEdit'],
    refs: [{
        ref: 'Order',
        selector: 'Order'
    }, {
        ref: 'OrderEdit',
        selector: 'OrderEdit'
    }],
    init: function () {
        var me = this;
        me.control({
            'Order button[action=search]': {
                click: me.search
            },
            'Order': {
                itemdblclick: me.edit,
                editActionClick: me.edit,
                deleteActionClick: me.deleteOrder,
            },
            'OrderEdit button[action=save]': {
                click: me.save
            },
        });
    },
    edit: function (grid, record) {
        var win = Ext.widget("OrderEdit");
        win.form.loadRecord(record);
        win.form.getForm().actionMethod = 'PUT';
        win.setTitle('编辑积分订单');
        win.show();
    },
    save: function (btn) {
        var me = this;
        var win = me.getOrderEdit();
        var form = win.form.getForm();
        var formValues = form.getValues();
        if (form.isValid()) {
            var store = me.getOrder().getStore();
            if (form.actionMethod == 'POST') {
                store.create(formValues, {
                    callback: function (records, operation, success) {
                        if (!success) {
                            Ext.MessageBox.alert("提示", operation.error);
                            return;
                        } else {
                            store.add(records[0].data);
                            store.commitChanges();
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
                store.update({
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
    search: function (btn) {
        var store = this.getOrder().getStore();
        var queryValues = btn.up('form').getValues();
        if (queryValues != null) {
            queryValues.All = true;
            store.load({ params: queryValues });
        }
    },
    deleteOrder: function (grid, record) {
        var me = this;
        Ext.MessageBox.confirm('询问', '您确定要删除吗?', function (opt) {
            if (opt == 'yes') {
                Ext.Msg.wait('正在处理数据，请稍候……', '状态提示');
                var store = me.getOrder().getStore();
                store.remove(record);
                store.sync({
                    callback: function (batch, options) {
                        Ext.Msg.hide();
                        if (batch.hasException()) {
                            Ext.MessageBox.alert("提示", batch.exceptions[0].error);
                            roleStore.rejectChanges();
                        } else {
                            Ext.MessageBox.alert("提示", "删除成功");
                        }
                    }
                });
            }
        });
    },
});
