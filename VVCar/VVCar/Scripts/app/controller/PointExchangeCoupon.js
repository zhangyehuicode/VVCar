/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.controller.PointExchangeCoupon', {
    extend: 'Ext.app.Controller',
    requires: ['WX.store.BaseData.PointExchangeCouponStore', 'WX.store.BaseData.CouponTemplateInfoStore'],
    stores: [],
    models: ['BaseData.PointExchangeCouponModel'],
    views: ["PointExchangeCoupon.PointExchangeCoupon", "PointExchangeCoupon.PointExchangeCouponEdit", "selector.CouponTemplateSelector"],
    refs: [{
        ref: 'pointExchangeCoupon',
        selector: 'PointExchangeCoupon'
    }, {
        ref: 'pointExchangeCouponEdit',
        selector: 'PointExchangeCouponEdit'
    }, {
        ref: 'couponTemplateSelector',
        selector: 'CouponTemplateSelector'
    }],
    init: function () {
        var me = this;
        me.control({
            "PointExchangeCoupon button[action=AddExchangeSetting]": {
                click: me.addExchangeSetting
            },
            "PointExchangeCoupon button[action=search]": {
                click: me.pointExchangeCouponSearch
            },
            "PointExchangeCoupon button[action=reflash]": {
                click: me.pointExchangeCouponReflash
            },
            "PointExchangeCoupon": {
                editPointExchangePoupon: me.onEditPointExchangePoupon,
                itemdblclick: me.onEditPointExchangePoupon,
                deleteActionClick: me.onDelPointExchangePoupon
            },
            "PointExchangeCouponEdit": {
                deleteActionClick: me.onDeleteCouponTemplate
            },
            "PointExchangeCouponEdit button[action=AddCoupontemplate]": {
                click: me.selectCounponTemplete
            },
            "PointExchangeCouponEdit button[action=save]": {
                click: me.savePointExchangeCoupon
            },
            "PointExchangeCouponEdit radio[name=ExchangeType]": {
                focus: me.pointExchangeCouponEditRadioClick
            },
            "CouponTemplateSelector button[action=search]": {
                click: me.onCouponTemplateSelectorSearch
            },
            "CouponTemplateSelector button[action=save]": {
                click: me.onCouponTemplateSelectorSave
            }
        });
    },
    addExchangeSetting: function () {
        let win = Ext.widget("PointExchangeCouponEdit");
        this.editgrid = win.down('grid[name=grid]');
        var grid = this.getPointExchangeCoupon();
        grid.getSelectionModel().clearSelections();
        grid.getView().refresh();
        win.show();
        win.down("numberfield[name=Point]").hide();
    },
    onEditPointExchangePoupon: function (grid, record) {
        //if (record.data.ExchangeCount > 0) {
        //    Ext.Msg.alert('提示', '用户已开始兑换,不能修改配置');
        //    return;
        //}
        let win = Ext.widget("PointExchangeCouponEdit");
        win.setTitle("编辑积分兑换");
        this.editgrid = win.down('grid[name=grid]');
        let store = win.down('grid[name=grid]').getStore();
        win.down('form').loadRecord(record);
        console.log(record);
        store.getCouponTemplateDto(record.data.CouponTemplateId, function (request, success, response) {
            if (response.timedout) {
                Ext.Msg.alert('提示', '操作超时');
                return;
            }
            var result = JSON.parse(response.responseText);
            if (success) {
                if (result.IsSuccessful) {
                    let data = result.Data;
                    console.log(data);
                    store.loadData([data]);
                } else {
                    Ext.Msg.alert('提示', result.ErrorMessage);
                }
            } else {
                Ext.Msg.alert('提示', result.Message);
            }
        });
        win.show();
        if (record.data.ExchangeType == 0) {
            win.down("numberfield[name=Point]").hide();
        }
    },
    selectCounponTemplete: function (btn) {
        btn.up('window').down("grid[name=grid]");
        let win = Ext.widget("CouponTemplateSelector");
        win.show();
    },
    onCouponTemplateSelectorSearch: function (btn) {
        let store = btn.up('window').down("grid[name=grid]").getStore();
        var formValue = btn.up('window').down('form[name=formSearch]').getValues();
        Ext.apply(store.proxy.extraParams, formValue);
        store.load();
    },
    onCouponTemplateSelectorSave: function (btn) {
        let records = btn.up('window').down("grid[name=grid]").getSelectionModel().getSelection();
        if (records.length < 1) {
            Ext.Msg.alert('提示', '未选中行');
        }
        console.log(records);
        this.editgrid.getStore().loadRecords(records);
        btn.up('window').close();
    },
    onDeleteCouponTemplate: function (grid, record) {
        grid.getStore().removeAll();
    }
    , savePointExchangeCoupon: function (btn) {
        let win = btn.up('window');
        let store = win.down('grid[name=grid]').getStore();
        let gridItems = store.data.items;

        var pointExchangeCouponSelected = this.getPointExchangeCoupon().getSelectionModel().getSelection();
        if (pointExchangeCouponSelected.length > 0 && pointExchangeCouponSelected[0].data.ExchangeCount > 0) {
            Ext.Msg.alert('提示', '用户已开始兑换,不能修改配置');
            return;
        }

        let form = win.down('form[name=form]');
        let startDate = win.down('datefield[name=BeginDate]').getValue();
        let finishDate = win.down('datefield[name=FinishDate]').getValue();
        let point = win.down('numberfield[name=Point]');
        var formValue = form.getValues();
        let pointExchangeCoupon = this.getPointExchangeCoupon();
        if (formValue.ExchangeType == 0) {
            point.setValue(0);
        }
        if (!form.isValid()) {
            Ext.Msg.alert('提示', '未填写完整');
            return;
        }
        if (gridItems.length < 1) {
            Ext.Msg.alert('提示', '未选择券');
        }
        var startDateTemp = new Date(startDate);
        var putInStartDateTemp = new Date(gridItems[0].data.PutInStartDate);
        var startDateTemp = new Date(startDateTemp.getFullYear() + '-' + (startDateTemp.getMonth() + 1) + '-' + startDateTemp.getDate());
        var putInStartDateTemp = new Date(putInStartDateTemp.getFullYear() + '-' + (putInStartDateTemp.getMonth() + 1) + '-' + putInStartDateTemp.getDate());
        if (startDateTemp < putInStartDateTemp) {
            Ext.Msg.alert('提示', '兑换有效期开始时间需大于优惠券投放开始时间');
            return;
        }
        if (finishDate < startDate) {
            Ext.Msg.alert('提示', '兑换结束时间需大于兑换开始时间');
            return;
        }

        formValue.CouponTemplateId = gridItems[0].data.ID;
        formValue.BeginDate = startDate;
        formValue.FinishDate = finishDate;
        if (formValue.ID) {
            pointExchangeCoupon
                .getStore()
                .updatePointExchangeCoupon(formValue, function (request, success, response) {
                    if (response.timedout) {
                        Ext.Msg.alert('提示', '操作超时');
                        pointExchangeCoupon.getStore().reload();
                        return;
                    }
                    var result = JSON.parse(response.responseText);
                    if (success) {
                        if (result.IsSuccessful) {
                            Ext.Msg.alert('提示', '操作成功');
                            store.removeAll();
                            win.close();
                            pointExchangeCoupon.getStore().reload();
                        } else {
                            Ext.Msg.alert('提示', result.ErrorMessage);
                        }
                    } else {
                        Ext.Msg.alert('提示', result.Message);
                    }
                })
        } else {
            pointExchangeCoupon
                .getStore()
                .savePointExchangeCoupon(formValue, function (request, success, response) {
                    if (response.timedout) {
                        Ext.Msg.alert('提示', '操作超时');
                        pointExchangeCoupon.getStore().reload();
                        return;
                    }
                    var result = JSON.parse(response.responseText);
                    if (success) {
                        if (result.IsSuccessful) {
                            Ext.Msg.alert('提示', '操作成功');
                            store.removeAll();
                            win.close();
                            pointExchangeCoupon.getStore().reload();
                        } else {
                            Ext.Msg.alert('提示', result.ErrorMessage);
                        }
                    } else {
                        Ext.Msg.alert('提示', result.Message);
                    }
                })
        }
    }
    , onDelPointExchangePoupon: function (grid, record) {
        Ext.MessageBox.confirm('询问', '您确定要删除吗?', function (opt) {
            if (opt == 'yes') {
                let store = grid.getStore();
                store.remove(record);
                store.sync({
                    callback: function (batch, options) {
                        if (batch.hasException()) {
                            Ext.MessageBox.alert("操作失败", batch.exceptions[0].error);
                            store.rejectChanges();
                        } else {
                            Ext.MessageBox.alert("操作成功", "删除成功");
                        }
                    }
                });
            }
        });
    }
    , pointExchangeCouponSearch: function (btn) {
        let store = btn.up('grid').getStore();
        let searchValue = btn.up('grid').down("textfield[name=search]").getValue();
        console.log(searchValue)
        store.proxy.extraParams = { "SearchValue": searchValue };
        store.reload();
    }
    , pointExchangeCouponReflash: function (btn) {
        let store = btn.up('grid').getStore();
        let searchValue = btn.up('grid').down("textfield[name=search]").setValue("");
        store.proxy.extraParams = { "SearchValue": "" };
        store.reload();
    }
    , pointExchangeCouponEditRadioClick: function (radio) {
        let isShow = radio.inputValue != 0;
        radio.up("window").down("numberfield[name=Point]").setVisible(isShow);
    }
});