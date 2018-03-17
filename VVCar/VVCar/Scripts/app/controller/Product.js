Ext.define('WX.controller.Product', {
    extend: 'Ext.app.Controller',
    requires: ['WX.store.BaseData.ProductStore'],
    models: ['BaseData.ProductModel'],
    views: ['Shop.Product', 'Shop.ProductEdit'],
    refs: [{
        ref: 'product',
        selector: 'Product'
    }, {
        ref: 'productEdit',
        selector: 'ProductEdit'
    }],
    init: function () {
        var me = this;
        me.control({
            'Product button[action=add]': {
                click: me.addProduct
            },
            'Product button[action=search]': {
                click: me.search
            },
            'Product': {
                itemdblclick: me.edit,
                editActionClick: me.edit,
                deleteActionClick: me.deleteProduct,
                escActionClick: me.escActionClick,
                descActionClick: me.descActionClick,
                publishsoldoutActionClick: me.publishsoldoutActionClick,
            },
            'ProductEdit button[action=save]': {
                click: me.save
            },
            'ProductEdit button[action=uploadpic]': {
                click: me.uploadProductPic
            },
        });
    },
    addProduct: function (button) {
        var win = Ext.widget("ProductEdit");
        win.form.getForm().actionMethod = 'POST';
        win.setTitle('添加商品');
        win.show();
    },
    edit: function (grid, record) {
        var win = Ext.widget("ProductEdit");
        win.form.loadRecord(record);
        win.down('box[name=ImgShow]').autoEl.src = record.data.ImgUrl;
        win.form.getForm().actionMethod = 'PUT';
        win.setTitle('编辑商品');
        win.show();
    },
    save: function (btn) {
        var me = this;
        var win = me.getProductEdit();
        var form = win.form.getForm();
        var formValues = form.getValues();
        if (formValues.ImgUrl == '') {
            Ext.Msg.alert('提示', '请先上传商品图片');
            return;
        }
        if (form.isValid()) {
            var store = me.getProduct().getStore();
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
        var store = this.getProduct().getStore();
        var queryValues = btn.up('form').getValues();
        if (queryValues != null) {
            queryValues.All = true;
            store.load({ params: queryValues });
        }
    },
    deleteProduct: function (grid, record) {
        var me = this;
        Ext.MessageBox.confirm('询问', '您确定要删除吗?', function (opt) {
            if (opt == 'yes') {
                Ext.Msg.wait('正在处理数据，请稍候……', '状态提示');
                var store = me.getProduct().getStore();
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
    escActionClick: function (grid, record) {
        this.adjustIndexAction(grid, record, 1);
    },
    descActionClick: function (grid, record) {
        this.adjustIndexAction(grid, record, 2);
    },
    adjustIndexAction: function (grid, record, direction) {
        var store = grid.getStore();
        var data = {
            ID: record.data.ID,
            Direction: direction,
        }
        function success(response) {
            Ext.Msg.hide();
            response = JSON.parse(response.responseText);
            if (!response.IsSuccessful) {
                Ext.Msg.alert('提示', response.ErrorMessage);
                return;
            }
            if (!response.Data) {
                Ext.Msg.alert('提示', direction != 1 ? '降序' : '升序' + '失败！');
                return;
            }
            store.load();
        };
        function failure(response) {
            Ext.Msg.hide();
            Ext.Msg.alert('提示', response.responseText);
        };
        store.adjustIndex(data, success, failure);
    },
    publishsoldoutActionClick: function (grid, record) {
        var store = grid.getStore();
        store.publishSoldOut(record.data.ID, function (response) {
            Ext.Msg.hide();
            response = JSON.parse(response.responseText);
            if (!response.IsSuccessful) {
                Ext.Msg.alert('提示', response.ErrorMessage);
                return;
            }
            store.load();
        }, function (response) {
            Ext.Msg.hide();
            Ext.Msg.alert('提示', response.responseText);
        });
    },
    uploadProductPic: function (btn) {
        var form = btn.up('form').getForm();
        var win = btn.up('window');
        if (form.isValid()) {
            form.submit({
                url: '/api/UploadFile/UploadProduct',
                waitMsg: '正在上传...',
                success: function (fp, o) {
                    if (o.result.success) {
                        Ext.Msg.alert('提示', '上传成功！');
                        win.down('textfield[name=ImgUrl]').setValue(o.result.FileUrl);
                        win.down('box[name=ImgShow]').el.dom.src = o.result.FileUrl;
                    } else {
                        Ext.Msg.alert('提示', o.result.errorMessage);
                    }
                },
                failure: function (fp, o) {
                    Ext.Msg.alert('提示', o.result.errorMessage);
                }
            });
        }
    },
});
