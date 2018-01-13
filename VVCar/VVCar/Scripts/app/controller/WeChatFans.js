Ext.define('WX.controller.WeChatFans', {
    extend: 'Ext.app.Controller',
    requires: ['WX.store.BaseData.WeChatFansStore', 'WX.store.DataDict.SexStore',
        'WX.store.BaseData.WeChatFansTagStore', 'WX.store.BaseData.WeChatFansTagTreeStore'],
    models: ['BaseData.WeChatFansModel', 'WX.model.BaseData.WeChatFansTagModel'],
    views: ['WeChatFans.WeChatFans', 'WeChatFans.WeChatFansTagList', 'WeChatFans.WeChatFansTagEdit', 'WeChatFans.WeChatFansSetTag'],
    refs: [{
        ref: "formSearch",
        selector: "WeChatFans form[name=formSearch]"
    }, {
        ref: 'gridWeChatFansTag',
        selector: 'WeChatFans grid[name=gridWeChatFansTag]'
    }, {
        ref: 'gridWeChatFans',
        selector: 'WeChatFans grid[name=gridWeChatFans]'
    }, {
        ref: 'WeChatFansTagList',
        selector: 'WeChatFansTagList'
    }, {
        ref: 'WeChatFansTagEdit',
        selector: 'WeChatFansTagEdit'
    }, {
        ref: 'WeChatFansSetTag',
        selector: 'WeChatFansSetTag'
    }, {
        ref: 'FansTagGroup',
        selector: 'WeChatFansSetTag checkboxgroup[name=FansTagGroup]'
    }],
    init: function () {
        var me = this;
        me.control({
            'WeChatFans': {
                afterrender: me.onWeChatFansAfterRender,
            },
            'WeChatFans grid[name=gridWeChatFansTag]': {
                select: me.onGridWeChatFansTagSelect,
            },
            'WeChatFans button[action=manageFansTag]': {
                click: me.onManageFansTagClick
            },
            'WeChatFans button[action=search]': {
                click: me.searchWeChatFans
            },
            'WeChatFans button[action=export]': {
                click: me.exportToWeChatFans
            },
            'WeChatFans button[action=setTag]': {
                click: me.setTagToWeChatFans
            },
            'WeChatFansTagList': {
                close: me.onWeChatFansTagListClosed
            },
            'WeChatFansTagList button[action=addFansTag]': {
                click: me.addFansTag
            },
            'WeChatFansTagList button[action=editFansTag]': {
                click: me.editFansTag
            },
            'WeChatFansTagList button[action=deleteFansTag]': {
                click: me.deleteFansTag
            },
            'WeChatFansTagEdit button[action=save]': {
                click: me.saveFansTag
            },
            'WeChatFansSetTag': {
                afterrender: me.onWeChatFansSetTagAfterRender,
            },
            'WeChatFansSetTag button[action=save]': {
                click: me.saveTagToWeChatFans
            }
        });
    },
    onWeChatFansAfterRender: function () {
        var gridWeChatFansTag = this.getGridWeChatFansTag();
        gridWeChatFansTag.getStore().load({
            callback: function (records, operation, success) {
                if (records.length > 0) {
                    gridWeChatFansTag.getSelectionModel().select(0);
                }
            }
        });
    },
    onGridWeChatFansTagSelect: function (grid, record, index, eOpts) {
        var store = this.getGridWeChatFans().getStore();
        Ext.apply(store.proxy.extraParams, {
            All: false,
            TagID: record.data.ID,
        });
        store.currentPage = 1;
        store.load();
    },
    onManageFansTagClick: function () {
        var win = Ext.widget("WeChatFansTagList");
        win.show();
    },
    searchWeChatFans: function () {
        var store = this.getGridWeChatFans().getStore();
        var queryValues = this.getFormSearch().getValues();
        if (queryValues != null) {
            store.currentPage = 1;
            Ext.apply(store.proxy.extraParams, queryValues);
            store.load();
        } else {
            Ext.Msg.alert("系统提示", "请输入过滤条件！");
        }
    },
    exportToWeChatFans: function (btn) {
        var grid = btn.up("grid");
        var store = grid.getStore();
        var filter = this.getGridWeChatFans().getStore().proxy.extraParams;//btn.up("form").getValues();
        console.log(filter);
        function success(response) {
            Ext.MessageBox.close();
            response = JSON.parse(response.responseText);
            if (response.IsSuccessful) {
                window.location.href = response.Data;
            }
            else {
                Ext.Msg.alert("提示", response.ErrorMessage);
            }
        }
        function failure(response) {
            Ext.Msg.alert("提示", response.responseText);
        }
        Ext.MessageBox.show({
            msg: '正在生成数据……, 请稍侯',
            progressText: '正在生成数据……',
            width: 300,
            wait: true
            //waitConfig: { interval: 200 }
        });
        store.exportWeChatFans(filter, success, failure);
    },
    setTagToWeChatFans: function () {
        var selectedItems = this.getGridWeChatFans().getSelectionModel().getSelection();
        if (selectedItems.length < 1) {
            Ext.Msg.alert("提示", "请先选中需要操作的数据");
            return;
        }
        var selectedFans = [];
        for (var i = 0; i < selectedItems.length; i++) {
            selectedFans.push(selectedItems[i].data.ID);
        }
        var win = Ext.widget("WeChatFansSetTag");
        win.SelectedFans = selectedFans;
        win.show();
    },
    onWeChatFansTagListClosed: function () {
        if (this.hasUpdateFansTags != null && this.hasUpdateFansTags == true) {
            this.hasUpdateFansTags = false;
            this.getGridWeChatFansTag().getStore().load();
        }
    },
    addFansTag: function () {
        var win = Ext.widget("WeChatFansTagEdit");
        win.form.getForm().actionMethod = 'POST';
        var fansTagList = this.getWeChatFansTagList();
        win.down('numberfield[name=Index]').setValue(fansTagList.grid.getStore().getCount() + 1);
        win.setTitle('添加标签');
        win.show();
    },
    editFansTag: function (btn) {
        var fansTagList = this.getWeChatFansTagList();
        var selectedItems = fansTagList.grid.getSelectionModel().getSelection();
        if (selectedItems.length < 1) {
            Ext.Msg.alert("提示", "请先选中需要编辑的数据");
            return;
        }
        var win = Ext.widget("WeChatFansTagEdit");
        win.form.loadRecord(selectedItems[0]);
        win.form.getForm().actionMethod = 'PUT';
        win.setTitle('编辑标签');
        win.show();
    },
    deleteFansTag: function () {
        var me = this;
        var fansTagList = this.getWeChatFansTagList();
        var selectedItems = fansTagList.grid.getSelectionModel().getSelection();
        if (selectedItems.length < 1) {
            Ext.Msg.alert("提示", "请先选中需要删除的数据");
            return;
        }
        Ext.MessageBox.confirm('询问', '您确定要删除吗?', function (opt) {
            if (opt != 'yes') {
                return;
            }
            Ext.Msg.wait('正在处理数据，请稍候……', '状态提示');
            var myStore = fansTagList.grid.getStore();
            myStore.remove(selectedItems[0]);
            myStore.sync({//destroy
                callback: function (batch, options) {//function (records, operation, success) {
                    Ext.Msg.hide();
                    if (!batch.hasException()) {
                        Ext.MessageBox.alert("操作成功", "删除成功");
                        me.hasUpdateFansTags = true;
                    } else {
                        Ext.MessageBox.alert("操作失败", batch.exceptions[0].error);
                        myStore.rejectChanges();
                    }
                }
            });
        });
    },
    saveFansTag: function () {
        var win = this.getWeChatFansTagEdit();
        var form = win.form.getForm();
        var formValues = form.getValues();
        if (form.isValid()) {
            var store = this.getWeChatFansTagList().grid.getStore();
            if (form.actionMethod == 'POST') {
                store.create(formValues, {
                    callback: function (records, operation, success) {
                        if (!success) {
                            Ext.Msg.alert("操作失败", operation.error);
                            return;
                        } else {
                            store.add(records[0].data);
                            store.commitChanges();
                            Ext.Msg.alert("操作成功", "新增成功");
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
                            Ext.Msg.alert("操作失败", operation.error);
                            return;
                        } else {
                            Ext.Msg.alert("操作成功", "更新成功");
                            win.close();
                        }
                    }
                });
            }
            this.hasUpdateFansTags = true;
        }
    },
    onWeChatFansSetTagAfterRender: function () {
        var me = this;
        var store = Ext.create('WX.store.BaseData.WeChatFansTagStore');
        store.load(function (records, operation, success) {
            if (records.length == 0)
                return;
            var fansTagGroup = me.getFansTagGroup();
            for (var i = 0; i < records.length; i++) {
                fansTag = records[i].data;
                fansTagGroup.add({
                    boxLabel: fansTag.Name,
                    name: 'FansTag',
                    inputValue: fansTag.ID,
                    checked: false
                });
            }
        });
    },
    saveTagToWeChatFans: function () {
        var me = this;
        var win = me.getWeChatFansSetTag();
        var fansIDList = win.SelectedFans;
        var tagIDList = [];
        var fansTags = me.getFansTagGroup().getValue().FansTag;
        tagIDList = tagIDList.concat(fansTags);
        var store = me.getGridWeChatFans().getStore();
        store.setFansTag({
            FansIDList: fansIDList,
            TagIDList: tagIDList
        }, function (response, opts) {
            var result = Ext.decode(response.responseText);
            if (result.IsSuccessful) {
                Ext.Msg.alert("提示", "操作成功");
                win.close();
                store.reload();
                me.getGridWeChatFansTag().getStore().load();
            } else {
                Ext.Msg.alert("提示", "操作失败，" + result.ErrorMessage);
            }
        });
    }
});
