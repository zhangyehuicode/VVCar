Ext.define('WX.controller.RechargePlan', {
    extend: 'Ext.app.Controller',
    requires: ['WX.store.BaseData.RechargePlanStore'],
    stores: ['DataDict.RechargePlanTypeStore', 'DataDict.EnableDisableTypeStore'],
    models: ['BaseData.RechargePlanModel'],
    views: ['RechargePlan.RechargePlanList', 'RechargePlan.RechargePlanEdit', 'RechargePlan.RechargePlanCoupon'],
    refs: [{
        ref: 'rechargePlanList',
        selector: 'RechargePlanList'
    }, {
        ref: 'rechargePlanEdit',
        selector: 'RechargePlanEdit'
    }],
    init: function () {
        var me = this;
        me.control({
            'RechargePlanList button[action=addRechargePlan]': {
                click: me.addRechargePlan
            },
            'RechargePlanList button[action=search]': {
                click: me.searchData
            },
            'RechargePlanList': {
                itemdblclick: me.editRechargePlan,
                editActionClick: me.editRechargePlan,
                enableActionClick: me.enableRechargePlan,
                disableActionClick: me.disableRechargePlan,
            },
            'RechargePlanEdit button[action=save]': {
                click: me.saveRechargePlan
            },
            'RechargePlanEdit datefield[name=ExpiredDate]': {
                select: me.dxpiredDateSelect
            },
            'RechargePlanEdit button[action=SelectCoupon]': {
                click: me.selectCoupon
            },
            'RechargePlanCoupon button[action=search]': {
                click: me.couponSearch
            },
            'RechargePlanCoupon button[action=confirm]': {
                click: me.couponConfirm
            },
            'RechargePlanEdit button[action=DeleteCoupon]': {
                click: me.deleteCoupon
            }
        });
    },
    deleteCoupon: function (btn) {
        var me = this;
        var item = btn.up('container');
        var couponContainer = me.getRechargePlanEdit().down('container[name=CouponContainer]');
        couponContainer.remove(item);
    },
    couponConfirm: function (btn) {
        var me = this;
        var grid = btn.up('window').down('grid');
        var selectedCoupons = grid.getSelectionModel().getSelection();
        if (selectedCoupons.length < 1) {
            Ext.Msg.alert('提示', '请选择优惠券');
            return;
        }
        var couponContainer = me.getRechargePlanEdit().down('container[name=CouponContainer]');
        var couponContainerItems = couponContainer.items.items;

        for (var i = 0; i < selectedCoupons.length; i++) {
            var item = selectedCoupons[i];

            var flag = false;
            for (var j = 0; j < couponContainerItems.length; j++) {
                var couponTemplateIdTextField = couponContainerItems[j].down('textfield[name=CouponTemplateID]');
                var id = couponTemplateIdTextField.getValue();
                if (id == item.data.ID) {
                    var couponTemplateQuantityNumberField = couponContainerItems[j].down('numberfield[name=CouponTemplateQuantity]');
                    couponTemplateQuantityNumberField.setValue(couponTemplateQuantityNumberField.getValue() + 1);
                    flag = true;
                }
            }

            if (flag)
                continue;

            couponContainer.add({
                xtype: 'container',
                layout: 'hbox',
                padding: '5 0 0 0',
                items: [{
                    xtype: 'textfield',
                    name: 'CouponTemplateID',
                    fieldLabel: 'ID',
                    labelWidth: 30,
                    width: 150,
                    value: item.data.ID,
                    margin: '0 0 0 5',
                    hidden: true,
                }, {
                    xtype: 'textfield',
                    name: 'CouponTemplateTitle',
                    fieldLabel: '标题',
                    labelWidth: 30,
                    width: 150,
                    value: item.data.Title,
                    margin: '0 0 0 5',
                    hidden: true,
                }, {
                    xtype: 'label',
                    html: '<div style="height:25px;line-height:25px;padding-left:20px;width:150px;">' + item.data.Title + '</div>',
                }, {
                    xtype: 'numberfield',
                    name: 'CouponTemplateQuantity',
                    minValue: 1,
                    width: 80,
                    margin: '0 0 0 100',
                    value: 1,
                    allowBlank: false,
                }, {
                    xtype: 'button',
                    text: '删除',
                    action: 'DeleteCoupon',
                    margin: '0 0 0 30',
                }]
            });
        }
        btn.up('window').close();
    },
    couponSearch: function (btn) {
        var form = btn.up('form').getForm();
        var values = form.getValues();
        var store = btn.up('grid').getStore();
        store.proxy.extraParams = values;
        store.load();
    },
    selectCoupon: function (btn) {
        var win = Ext.widget('RechargePlanCoupon');
        win.show();
    },
    dxpiredDateSelect: function (field, value, eOpts) {
        field.setValue(new Date(new Date(new Date(value).toLocaleDateString()).getTime() + 24 * 60 * 60 * 1000 - 1));
    },
    addRechargePlan: function (button) {
        var win = Ext.widget("RechargePlanEdit");
        win.form.getForm().actionMethod = 'POST';
        win.setTitle('新增储值方案');
        win.show();
    },
    editRechargePlan: function (grid, record) {
        var me = this;
        var win = Ext.widget("RechargePlanEdit");
        win.form.loadRecord(record);
        win.form.getForm().actionMethod = 'PUT';
        win.setTitle('编辑储值方案');
        win.down('[name=Code]').setReadOnly(true);
        var matchcardtype = record.data.MatchCardType;
        if (matchcardtype != null) {
            win.down('checkboxfield[name=MatchRechargeCard]').setValue(matchcardtype.indexOf("00000000-0000-0000-0000-000000000001") != -1);
            win.down('checkboxfield[name=MatchDiscountCard]').setValue(matchcardtype.indexOf("00000000-0000-0000-0000-000000000002") != -1);
            win.down('checkboxfield[name=MatchGiftCard]').setValue(matchcardtype.indexOf("00000000-0000-0000-0000-000000000003") != -1);
        } else {
            win.down('checkboxfield[name=MatchRechargeCard]').setValue(false);
            win.down('checkboxfield[name=MatchDiscountCard]').setValue(false);
            win.down('checkboxfield[name=MatchGiftCard]').setValue(false);
        }
        var rechargePlanCouponTemplates = record.data.RechargePlanCouponTemplates;
        if (rechargePlanCouponTemplates.length > 0) {
            var couponContainer = me.getRechargePlanEdit().down('container[name=CouponContainer]');
            for (var i = 0; i < rechargePlanCouponTemplates.length; i++) {
                var item = rechargePlanCouponTemplates[i];
                couponContainer.add({
                    xtype: 'container',
                    layout: 'hbox',
                    padding: '5 0 0 0',
                    items: [{
                        xtype: 'textfield',
                        name: 'CouponTemplateID',
                        fieldLabel: 'ID',
                        labelWidth: 30,
                        width: 150,
                        value: item.CouponTemplateID,
                        margin: '0 0 0 5',
                        hidden: true,
                    }, {
                        xtype: 'textfield',
                        name: 'CouponTemplateTitle',
                        fieldLabel: '标题',
                        labelWidth: 30,
                        width: 150,
                        value: item.Title,
                        margin: '0 0 0 5',
                        hidden: true,
                    }, {
                        xtype: 'label',
                        html: '<div style="height:25px;line-height:25px;padding-left:20px;width:150px;">' + item.Title + '</div>',
                    }, {
                        xtype: 'numberfield',
                        name: 'CouponTemplateQuantity',
                        minValue: 1,
                        width: 80,
                        margin: '0 0 0 100',
                        value: item.Quantity,
                        allowBlank: false,
                    }, {
                        xtype: 'button',
                        text: '删除',
                        action: 'DeleteCoupon',
                        margin: '0 0 0 30',
                    }]
                });
            }
        }
        win.show();
    },
    saveRechargePlan: function (btn) {
        var me = this;
        var win = me.getRechargePlanEdit();
        var form = win.form.getForm();
        var formValues = form.getValues();
        if (formValues.RechargeAmount === '0' && formValues.VisibleAtWeChat === true) {
            Ext.MessageBox.alert("提示", "储值金额为0时，不允许勾选微信在线储值");
            return;
        }

        var couponContainerItems = me.getRechargePlanEdit().down('container[name=CouponContainer]').items.items;

        var rechargePlanCouponTemplates = new Array();
        for (var i = 0; i < couponContainerItems.length; i++) {
            var id = couponContainerItems[i].down('textfield[name=CouponTemplateID]').getValue();
            var title = couponContainerItems[i].down('textfield[name=CouponTemplateTitle]').getValue();
            var quantity = couponContainerItems[i].down('numberfield[name=CouponTemplateQuantity]').getValue();
            if (id == null || id == '' || title == '' || title == null) {
                Ext.Msg.alert('提示', '优惠券信息丢失');
                return;
            }
            if (quantity == '' || quantity == null) {
                Ext.Msg.alert('提示', '优惠券数量不可空');
                return;
            }
            rechargePlanCouponTemplates.push({
                CouponTemplateID: id,
                Title: title,
                Quantity: quantity,
            });
        }

        formValues.RechargePlanCouponTemplates = rechargePlanCouponTemplates;

        //var params = {
        //    ID: formValues.ID,
        //    Code: formValues.Code,
        //    Name: formValues.Name,
        //    EffectiveDate: formValues.EffectiveDate,
        //    ExpiredDate: formValues.ExpiredDate,
        //    GiveAmount: formValues.GiveAmount,
        //    IsAvailable: formValues.IsAvailable,
        //    MatchDiscountCard: formValues.MatchDiscountCard,
        //    MatchGiftCard: formValues.MatchGiftCard,
        //    MatchRechargeCard: formValues.MatchRechargeCard,
        //    MaxRechargeCount: formValues.MaxRechargeCount,
        //    PlanType: formValues.PlanType,
        //    RechargeAmount: formValues.RechargeAmount,
        //    VisibleAtPortal: formValues.VisibleAtPortal,
        //    VisibleAtWeChat: formValues.VisibleAtWeChat,
        //    RechargePlanCouponTemplates: rechargePlanCouponTemplates,
        //};
        //console.log(params);
        //return;
        if (form.isValid()) {
            console.log(formValues);
            //return;
            Ext.MessageBox.show({
                msg: '正在保存中……, 请稍侯',
                progressText: '正在保存中……',
                width: 300,
                wait: true,
                waitConfig: { interval: 200 }
            });
            var myStore = this.getRechargePlanList().getStore();
            if (form.actionMethod === 'POST') {
                //myStore.create(formValues, {
                //    callback: function (records, operation, success) {
                //        Ext.MessageBox.hide();
                //        if (!success) {
                //            Ext.MessageBox.alert("提示", operation.error);
                //            return;
                //        } else {
                //            myStore.add(records[0].data);
                //            myStore.commitChanges();
                //            Ext.MessageBox.alert("提示", "新增成功");
                //            win.close();
                //        }
                //    }
                //});
                myStore.newRechargePlan(formValues, function (response, opts) {
                    Ext.MessageBox.hide();
                    var result = Ext.decode(response.responseText);
                    if (result.IsSuccessful) {
                        Ext.MessageBox.alert("提示", "新增成功");
                        myStore.load();
                        win.close();
                    } else {
                        Ext.Msg.alert("提示", "新增失败，" + result.ErrorMessage);
                    }
                });
            } else {
                //if (!form.isDirty()) {
                //    Ext.MessageBox.hide();
                //    win.close();
                //    return;
                //}
                //form.updateRecord();
                //myStore.update({
                //    callback: function (records, operation, success) {
                //        Ext.MessageBox.hide();
                //        if (!success) {
                //            Ext.MessageBox.alert("提示", operation.error);
                //            return;
                //        } else {
                //            Ext.MessageBox.alert("提示", "更新成功");
                //            win.close();
                //        }
                //    }
                //});
                myStore.updateRechargePlan(formValues, function (response, opts) {
                    Ext.MessageBox.hide();
                    var result = Ext.decode(response.responseText);
                    if (result.IsSuccessful) {
                        Ext.MessageBox.alert("提示", "更新成功");
                        myStore.load();
                        win.close();
                    } else {
                        Ext.Msg.alert("提示", "更新失败" + result.ErrorMessage);
                    }
                });
            }
        }
    },
    searchData: function (btn) {
        var myStore = this.getRechargePlanList().getStore();
        var queryValues = btn.up('form').getValues();
        if (queryValues != null) {
            queryValues.All = true;
            myStore.load({ params: queryValues });
        } else {
            Ext.MessageBox.alert("系统提示", "请输入过滤条件！");
        }
    },
    enableRechargePlan: function (grid, record) {
        var me = this;
        var myStore = me.getRechargePlanList().getStore();
        myStore.changeStatus(record.data.ID, true, function (response, opts) {
            var result = Ext.decode(response.responseText);
            if (result.IsSuccessful) {
                Ext.MessageBox.alert("提示", "启用成功");
                record.data.IsAvailable = true;
                record.commit();
                myStore.commitChanges();
            } else {
                Ext.Msg.alert("提示", "启用失败，" + result.ErrorMessage);
            }
        });
    },
    disableRechargePlan: function (grid, record) {
        var me = this;
        var myStore = me.getRechargePlanList().getStore();
        myStore.changeStatus(record.data.ID, false, function (response, opts) {
            var result = Ext.decode(response.responseText);
            if (result.IsSuccessful) {
                Ext.MessageBox.alert("提示", "禁用成功");
                record.data.IsAvailable = false;
                record.commit();
                myStore.commitChanges();
            } else {
                Ext.Msg.alert("提示", "禁用失败，" + result.ErrorMessage);
            }
        });
    }
});
