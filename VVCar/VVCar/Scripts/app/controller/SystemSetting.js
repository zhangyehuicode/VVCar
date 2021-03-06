﻿/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.controller.SystemSetting', {
    extend: 'Ext.app.Controller',
    requires: ['WX.store.BaseData.SystemSettingStore'],
    models: ['BaseData.SystemSettingModel'],
    views: ['SystemSetting.SystemSetting'],
    refs: [{
        ref: "grdSystemSetting",
        selector: "SystemSetting"
    }],
    init: function () {
        var me = this;
        me.control({
            'SystemSetting': {
                edit: me.onSystemSettingEdit,
            },
        });
    },
    onSystemSettingEdit: function (editor, context, eOpts) {
        if (context.record.phantom) {//表示新增
            context.store.create(context.record.data, {
                callback: function (records, operation, success) {
                    if (!success) {
                        Ext.MessageBox.alert("提示", operation.error);
                        return;
                    } else {
                        context.record.copyFrom(records[0]);
                        context.record.commit();
                        Ext.MessageBox.alert("提示", "新增成功");
                    }
                }
            });
        } else {
            if (!context.record.dirty)
                return;
            context.store.update({
                callback: function (records, operation, success) {
                    if (!success) {
                        Ext.MessageBox.alert("提示", operation.error);
                        return;
                    } else {
                        Ext.MessageBox.alert("提示", "更新成功");
                    }
                }
            });
        }
    },
});
