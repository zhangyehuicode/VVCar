/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.controller.DataDict', {
    extend: 'Ext.app.Controller',
    requires: ['WX.store.DataDict.DataDictTypeStore', 'WX.store.DataDict.DataDictValueStore'],
    models: ['DataDict.DataDictTypeModel', 'DataDict.DataDictModel'],
    views: ['DataDict.DataDictList'],
    refs: [{
        ref: 'dataDictList',
        selector: 'DataDictList'
    }, {
        ref: 'gridDictType',
        selector: 'DataDictList grid[name=gridDictType]'
    }, {
        ref: 'gridDictValue',
        selector: 'DataDictList grid[name=gridDictValue]'
    }],
    init: function () {
        var me = this;
        me.control({
            'DataDictList': {
                afterrender: me.onDataDictListAfterRender,
            },
            'DataDictList grid[name=gridDictType]': {
                select: me.onGridDictTypeSelect,
            },
            'DataDictList button[action=addDictValue]': {
                click: me.onAddDictValueClick,
            },
            'DataDictList grid[name=gridDictValue]': {
                edit: me.onGridDictValueEdit,
            },
        });
    },
    onDataDictListAfterRender: function () {
        var gridDictType = this.getGridDictType();
        gridDictType.getStore().load({
            callback: function (records, operation, success) {
                if (records.length > 0) {
                    gridDictType.getSelectionModel().select(0);
                }
            }
        });
    },
    onGridDictTypeSelect: function( grid, record, index, eOpts){
        this.getGridDictValue().getStore().load({ params: { dictType: record.data.Code } });
    },
    onAddDictValueClick: function () {
        var dictType = this.getGridDictType().getSelectionModel().getSelection();
        if (dictType.length < 1) {
            Ext.MessageBox.alert("提示", "请先选择字典类型");
            return;
        }
        var rowEditing = this.getDataDictList().rowEditing;
        rowEditing.cancelEdit();
        var dictValueStore = this.getGridDictValue().getStore();
        var newDictValue = Ext.create('WX.model.DataDict.DataDictModel', {
            DictType: dictType[0].data.Code,
            Index: dictValueStore.count() + 1,
            DictValue: 'new code',
            DictName: 'new code',
            IsAvailable : true,
        });
        dictValueStore.add(newDictValue);
        rowEditing.startEdit(newDictValue, 0);
    },
    onGridDictValueEdit: function (editor, context, eOpts) {
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
