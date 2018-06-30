Ext.define('WX.controller.CarBitCoinProduct', {
    extend: 'Ext.app.Controller',
    requires: ['WX.store.BaseData.CarBitCoinProductStore', 'WX.store.BaseData.CarBitCoinProductCategoryTreeStore'],
    models: ['BaseData.CarBitCoinProductModel', 'BaseData.CarBitCoinProductCategoryModel', 'BaseData.CarBitCoinProductCategoryTreeModel'],
    views: ['CarBitCoin.CarBitCoinProduct', 'CarBitCoin.CarBitCoinProductEdit', 'CarBitCoin.CarBitCoinProductCategoryList', 'CarBitCoin.CarBitCoinProductCategoryEdit', 'CarBitCoin.ChangeCategory', 'CarBitCoin.StockOutInEdit'],
    refs: [{
        ref: 'carBitCoinProduct',
        selector: 'CarBitCoinProduct grid'
    }, {
        ref: 'carBitCoinProductEdit',
        selector: 'CarBitCoinProductEdit'
    }, {
        ref: 'treegridCarBitCoinProductCategory',
        selector: 'CarBitCoinProductCategoryList treepanel[name=treegridCarBitCoinProductCategory]'
    }, {
        ref: 'treeCarBitCoinProductCategory',
        selector: 'CarBitCoinProduct treepanel[name = treeCarBitCoinProductCategory]',
    }, {
        ref: 'stockOutInEdit',
        selector: 'StockOutInEdit',
    }],
    init: function() {
        var me = this;
        me.control({
            'CarBitCoinProduct treepanel[name=treeCarBitCoinProductCategory]': {
                itemclick: me.ontreeCarBitCoinProductCategoryItemClick
            },
            'CarBitCoinProduct button[action=add]': {
                click: me.addCarBitCoinProduct
            },
            'CarBitCoinProduct button[action=search]': {
                click: me.search
            },
            'CarBitCoinProduct button[action=manageCarBitCoinProductCategory]': {
                click: me.manageCarBitCoinProductCategory
            },
            'CarBitCoinProduct grid': {
                itemdblclick: me.edit,
            },
            'CarBitCoinProduct': {
                beforerender: me.beforerender,
                editActionClick: me.edit,
                deleteActionClick: me.deleteCarBitCoinProduct,
                escActionClick: me.escActionClick,
                descActionClick: me.descActionClick,
                publishsoldoutActionClick: me.publishsoldoutActionClick,
                afterrender: me.afterrender,
            },
            'CarBitCoinProductEdit button[action=save]': {
                click: me.save
            },
            'CarBitCoinProductEdit button[action=uploadpic]': {
                click: me.uploadCarBitCoinProductPic
            },
            'CarBitCoinProductEdit checkboxfield[name=IsCanPointExchange]': {
                change: me.pointExchangeChange
            },
            'CarBitCoinProductCategoryList button[action=addCarBitCoinProductCategory]': {
                click: me.addCarBitCoinProductCategory
            },
            'CarBitCoinProductCategoryList button[action=editCarBitCoinProductCategory]': {
                click: me.EditCarBitCoinProductCategory
            },
            'CarBitCoinProductCategoryList button[action=delCarBitCoinProductCategory]': {
                click: me.delCarBitCoinProductCategory
            },
            'CarBitCoinProductCategoryEdit button[action=save]': {
                click: me.saveCarBitCoinProductCategory
            },
            'ChangeCategory button[action=saveCarBitCoinProductCategoryBtn]': {
                click: me.carBitCoinProductChangeCategory
            },
            'CarBitCoinProduct button[action=changeCategory]': {
                click: me.changeCarBitCoinProductCategory
            },
            'CarBitCoinProduct button[action=stockIn]': {
                click: me.stockIn
            },
            'CarBitCoinProduct button[action=stockOutIn]': {
                click: me.stockOutIn
            },
            'CarBitCoinProductEdit combobox[name=CarBitCoinProductType]': {
                change: me.producttypechange
            },
            'CarBitCoinProduct combobox[name=CarbitCoinProductType]': {
                change: me.productproducttypechange
            },
            'StockOutInEdit button[action=save]': {
                click: me.stockSave
            },
        });
    },
    productproducttypechange: function(com, newValue, oldValue, eOpts) {
        this.search(com);
    },
    producttypechange: function(com, newValue, oldValue, eOpts) {
        var me = this;
        var win = com.up('window');
        if (win.form.getForm().actionMethod == 'PUT') {
            if (newValue == 0) {
                com.up('window').down('checkboxfield[name=IsCanPointExchange]').setDisabled(true);
				com.up('window').down('checkboxfield[name=IsCanPointExchange]').setValue(false);
				com.up('window').down('textfield[name=DeliveryNotes]').hide();
            } else {
                com.up('window').down('checkboxfield[name=IsCanPointExchange]').setDisabled(false);
            }
        } else {
            if (newValue == 0) {
                //com.up('window').down('numberfield[name=Stock]').setDisabled(true);
                com.up('window').down('numberfield[name=Stock]').setValue(0);
                com.up('window').down('checkboxfield[name=IsCanPointExchange]').setDisabled(true);
				com.up('window').down('checkboxfield[name=IsCanPointExchange]').setValue(false);
				com.up('window').down('textfield[name=DeliveryNotes]').hide();
            } else {
                //com.up('window').down('numberfield[name=Stock]').setDisabled(false);
                com.up('window').down('checkboxfield[name=IsCanPointExchange]').setDisabled(false);
            }
        }
    },
    stockSave: function(btn) {
        var me = this;
        var win = btn.up('window');
        var form = win.form.getForm();
        if (!form.isValid) {
            return;
        }
        var values = form.getValues();
        var data = {
            ProductID: values.ID,
            Quantity: values.StockType ? -values.Stock : values.Stock,
            Reason: values.Reason,
            Source: 1,
        };
        var tips = win.down('textfield[name=Name]').getValue();
        tips += values.StockType ? ' 出库' : ' 入库';
        tips += '数量:' + values.Stock + values.Unit;
        Ext.MessageBox.confirm('询问', '确定 ' + tips + ' ？', function(opt) {
            if (opt == 'yes') {
                var store = me.getCarBitCoinProduct().getStore();
                store.stockOutIn(data, function(response) {
                    response = JSON.parse(response.responseText);
                    if (!response.IsSuccessful) {
                        Ext.Msg.alert('提示', response.ErrorMessage);
                        return;
                    }
                    store.load();
                    win.close();
                }, function(response) {
                    Ext.Msg.alert('提示', response.responseText);
                });
            }
        });
    },
    stockOutIn: function(btn) {
        var me = this;
        var selectedItems = me.getCarBitCoinProduct().getSelectionModel().getSelection();
        if (selectedItems == null || selectedItems.length < 1) {
            Ext.MessageBox.alert('提示', '请先选中一个商品');
            return;
        }
        if (selectedItems[0].data.CarBitCoinProductType != 1) {
            Ext.Msg.alert('提示', '不能为引擎进行库存操作，请选择一个商品');
            return;
        }
        var win = Ext.widget("StockOutInEdit");
        win.form.loadRecord(selectedItems[0]);
        win.show();
    },
    pointExchangeChange: function(checkbox, newValue, oldValue, eOpts) {
        var me = this;
        if (newValue) {
            me.getCarBitCoinProductEdit().down('numberfield[name=CarBitCoinPoints]').setDisabled(false);
            me.getCarBitCoinProductEdit().down('numberfield[name=UpperLimit]').setDisabled(false);
        } else {
			me.getCarBitCoinProductEdit().down('numberfield[name=CarBitCoinPoints]').setDisabled(true);
            me.getCarBitCoinProductEdit().down('numberfield[name=UpperLimit]').setDisabled(true);
			me.getCarBitCoinProductEdit().down('numberfield[name=CarBitCoinPoints]').setValue(0);
            me.getCarBitCoinProductEdit().down('numberfield[name=UpperLimit]').setValue(0);
        }
    },
    ontreeCarBitCoinProductCategoryItemClick: function(tree, record, item) {
        var myStore = this.getCarBitCoinProduct().getStore();
        Ext.apply(myStore.proxy.extraParams, {
            All: false, CarBitCoinProductCategoryID: record.data.ID
        });
        myStore.loadPage(1);
    },
    carBitCoinProductChangeCategory: function(btn) {
        var me = this;
        var win = btn.up('window');
        var form = win.form.getForm();
        var formValues = form.getValues();
        var record = form.getRecord().data;
        record.CarBitCoinProductCategoryID = formValues.DestCategory;
        if (form.isValid()) {
            var myStore = me.getCarBitCoinProduct().getStore();
            var rr = myStore.getById(record.ID);
            rr.CarBitCoinProductCategoryID = record.CarBitCoinProductCategoryID;
            Ext.MessageBox.show({
                msg: '正在请求数据, 请稍侯',
                progressText: '正在请求数据',
                width: 300,
                wait: true,
                waitConfig: {
                    interval: 200
                }
            });
            var url = Ext.GlobalConfig.ApiDomainUrl + 'api/CarBitCoinProduct';
            var tms = rr.data;
            Ext.Ajax.request({
                url: url,
                method: 'PUT',
                clientValidation: true,
                jsonData: Ext.JSON.encode(tms),
                callback: function(options, success, response) {
                    if (success) {
                        Ext.MessageBox.hide();
                        myStore.load();
                        win.close();
                        Ext.MessageBox.alert("操作成功", "更新成功");
                    } else {
                        Ext.MessageBox.hide();
                        Ext.MessageBox.alert("失败，请重试", response.responseText);
                    }
                },
                failure: function(response, options) {
                    Ext.MessageBox.hide();
                    Ext.MessageBox.alert("警告", "出现异常错误！请联系管理员！");
                },
                success: function(response, options) {
                    Ext.MessageBox.hide();
                }
            });

            this.hasUpdateDeptRegion = true;
        }
    },
    changeCarBitCoinProductCategory: function(btn) {
        var gridDept = btn.up('grid');
        var selectedItems = gridDept.getSelectionModel().getSelection();
        if (selectedItems.length < 1) {
            Ext.MessageBox.alert("提示", "请选择数据");
            return;
        }
        var selectedData = selectedItems[0].data;
        var win = Ext.widget("ChangeCategory");
        win.form.getForm().actionMethod = 'PUT';
        win.form.loadRecord(selectedItems[0]);
        var cmbCurrentRegion = win.down('combo[name=CurrCategory]');
        var cmbDestRegion = win.down('combo[name=DestCategory]');
        //因为cmbCurrentRegion和cmbDestRegion绑定的store是同一对象，
        //所以这里对cmbCurrentRegion的store做的修改也会影响到cmbDestRegion
        cmbCurrentRegion.store.clearFilter();
        cmbCurrentRegion.store.load({
            params: {
                All: true
            }
        });
        cmbCurrentRegion.setValue(selectedData.CarBitCoinProductCategoryID);
        cmbDestRegion.setValue(selectedData.CarBitCoinProductCategoryID);
        win.show();
    },
    saveCarBitCoinProductCategory: function(btn) {
        var me = this;
        var win = btn.up('window');
        var form = win.form.getForm();
        var formValues = form.getValues();
        if (form.isValid()) {
            var store = me.getTreegridCarBitCoinProductCategory().getStore();
            var treeCarBitCoinProductCategoryStore = me.getTreeCarBitCoinProductCategory().getStore();
            if (form.actionMethod == 'POST') {
                store.addCarBitCoinProductCategory(formValues, function(request, success, response) {
                    if (response.timedout) {
                        Ext.Msg.alert('提示', '操作超时');
                        store.reload();
                        return;
                    }
                    var result = JSON.parse(response.responseText);
                    if (success) {
                        if (result.IsSuccessful) {
                            Ext.Msg.alert('提示', '操作成功');
                            win.close();
                            store.reload();
                            treeCarBitCoinProductCategoryStore.load();
                        } else {
                            Ext.Msg.alert('提示', result.ErrorMessage);
                        }
                    } else {
                        Ext.Msg.alert('提示', result.Message);
                    }
                });
            } else {
                if (!form.isDirty()) {
                    win.close();
                    return;
                }
                form.updateRecord();
                store.updateCarBitCoinProductCategory(formValues, function(request, success, response) {
                    if (response.timedout) {
                        Ext.Msg.alert('提示', '操作超时');
                        store.reload();
                        return;
                    }
                    var result = JSON.parse(response.responseText);
                    if (success) {
                        if (result.IsSuccessful) {
                            Ext.Msg.alert('提示', '操作成功');
                            win.close();
                            treeCarBitCoinProductCategoryStore.load();
                            store.reload();
                        } else {
                            Ext.Msg.alert('提示', result.ErrorMessage);
                        }
                    } else {
                        Ext.Msg.alert('提示', result.Message);
                    }
                });
            }
            this.hasUpdateDeptRegion = true;
        }
    },
    delCarBitCoinProductCategory: function(btn) {
        var me = this;
        var selectedItems = this.getTreegridCarBitCoinProductCategory().getView().getSelectionModel().getSelection();
        if (selectedItems.length < 1) {
            Ext.MessageBox.alert("提示", "请先选中需要删除的数据");
            return;
        }
        var ID = selectedItems[0].get('ID');
        Ext.MessageBox.confirm('询问', '您确定要删除吗?', function(opt) {
            if (opt == 'yes') {
                var store = me.getTreegridCarBitCoinProductCategory().getStore();
                Ext.Msg.wait('正在处理数据，请稍候……', '状态提示');
                store.deleteCarBitCoinProductCategory(ID, function(request, success, response) {
                    if (response.timedout) {
                        Ext.Msg.alert('提示', '操作超时');
                        store.reload();
                        return;
                    }
                    var result = JSON.parse(response.responseText);
                    if (success) {
                        if (result.IsSuccessful && result.Data) {
                            Ext.Msg.alert('提示', '操作成功');
                            store.reload();
                            me.getTreeCarBitCoinProductCategory().getStore().load();
                        } else {
                            Ext.Msg.alert('提示', "操作失败" + result.ErrorMessage);
                        }
                    } else {
                        Ext.Msg.alert('提示', result.Message);
                    }
                });
            }
        });
    },
    EditCarBitCoinProductCategory: function(btn) {
        var selectedItems = this.getTreegridCarBitCoinProductCategory().getView().getSelectionModel().getSelection();
        if (selectedItems.length < 1) {
            Ext.MessageBox.alert("提示", "请先选中需要编辑的数据");
            return;
        }
        var win = Ext.widget("CarBitCoinProductCategoryEdit");
        win.form.loadRecord(selectedItems[0]);
        win.form.getForm().actionMethod = 'PUT';
        win.setTitle('修改分类');
        win.show();
    },
    addCarBitCoinProductCategory: function(button) {
        var win = Ext.widget("CarBitCoinProductCategoryEdit");
        win.form.getForm().actionMethod = 'POST';
        win.setTitle('添加分类');
        //win.down('[name=ParentProductCategoryID]').disable();
        win.show();
    },
    manageCarBitCoinProductCategory: function(btn) {
        var win = Ext.widget("CarBitCoinProductCategoryList");
        win.setTitle('分类管理');
        win.show();
    },
    addCarBitCoinProduct: function(button) {
        var me = this;
        var tree = Ext.ComponentQuery.query('treepanel[name=treeCarBitCoinProductCategory]');
        var selectedItems = tree[0].getView().getSelectionModel().getSelection();
        if (selectedItems.length > 0 && selectedItems[0].data.ID != '' && selectedItems[0].data.ID != null && selectedItems[0].data.ID != '00000000-0000-0000-0000-000000000001') {
            var win = Ext.widget("CarBitCoinProductEdit");
            win.down('textfield[name=CarBitCoinProductCategoryID]').setValue(selectedItems[0].data.ID);
            //win.down('numberfield[name=Stock]').hide();
            win.form.getForm().actionMethod = 'POST';
            win.setTitle('添加商品');
            win.show();
        } else {
            Ext.MessageBox.alert('提示', '未选择分类');
        }
    },
    edit: function(grid, record) {
        var win = Ext.widget("CarBitCoinProductEdit");
        win.form.loadRecord(record);
        win.down('box[name=ImgShow]').autoEl.src = record.data.ImgUrl;
        //win.down('numberfield[name=Stock]').setDisabled(true);
        //win.down('numberfield[name=Stock]').hide();
        win.form.getForm().actionMethod = 'PUT';
        win.setTitle('编辑商品');
        win.show();
    },
    save: function(btn) {
        var me = this;
        var win = me.getCarBitCoinProductEdit();
        var form = win.form.getForm();
        var formValues = form.getValues();
        if (formValues.ImgUrl == '') {
            Ext.Msg.alert('提示', '请先上传商品图片');
            return;
        }
        if (form.isValid()) {
            var store = me.getCarBitCoinProduct().getStore();
            if (form.actionMethod == 'POST') {
                store.create(formValues, {
                    callback: function(records, operation, success) {
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
                    callback: function(records, operation, success) {
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
    search: function(btn) {
        var store = this.getCarBitCoinProduct().getStore();
        var queryValues = btn.up('form').getValues();
        if (queryValues != null) {
            queryValues.All = true;
            store.load({ params: queryValues });
        }
    },
    deleteCarBitCoinProduct: function(grid, record) {
        var me = this;
        Ext.MessageBox.confirm('询问', '您确定要删除吗?', function(opt) {
            if (opt == 'yes') {
                Ext.Msg.wait('正在处理数据，请稍候……', '状态提示');
                var store = me.getCarBitCoinProduct().getStore();
                store.remove(record);
                store.sync({
                    callback: function(batch, options) {
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
    escActionClick: function(grid, record) {
        this.adjustIndexAction(grid, record, 1);
    },
    descActionClick: function(grid, record) {
        this.adjustIndexAction(grid, record, 2);
    },
    adjustIndexAction: function(grid, record, direction) {
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
    publishsoldoutActionClick: function(grid, record) {
        var store = grid.getStore();
        store.publishSoldOut(record.data.ID, function(response) {
            Ext.Msg.hide();
            response = JSON.parse(response.responseText);
            if (!response.IsSuccessful) {
                Ext.Msg.alert('提示', response.ErrorMessage);
                return;
            }
            store.load();
        }, function(response) {
            Ext.Msg.hide();
            Ext.Msg.alert('提示', response.responseText);
        });
    },
    uploadCarBitCoinProductPic: function(btn) {
        var form = btn.up('form').getForm();
        var win = btn.up('window');
        if (form.isValid()) {
            form.submit({
                url: '/api/UploadFile/UploadCarBitCoinProduct',
                waitMsg: '正在上传...',
                success: function(fp, o) {
                    if (o.result.success) {
                        Ext.Msg.alert('提示', '上传成功！');
                        win.down('textfield[name=ImgUrl]').setValue(o.result.FileUrl);
                        win.down('box[name=ImgShow]').el.dom.src = o.result.FileUrl;
                    } else {
                        Ext.Msg.alert('提示', o.result.errorMessage);
                    }
                },
                failure: function(fp, o) {
                    Ext.Msg.alert('提示', o.result.errorMessage);
                }
            });
        }
    },
    beforerender: function() {
        var me = this;
        var win = me.getCarBitCoinProduct();
    },
    afterrender: function() {
        var me = this;
        var carBitCoinProductStore = me.getCarBitCoinProduct().getStore();
        var params = {
            All: true,
        }
        Ext.apply(carBitCoinProductStore.proxy.extraParams, params);
        carBitCoinProductStore.load();
    }
});
