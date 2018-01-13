(function () {
    Ext.define("WX.controller.SysMenu", {
        extend: "Ext.app.Controller",
        requires: ["WX.store.BaseData.SysMenuStore"],
        models: ["BaseData.SysMenuModel"],
        views: ["SysMenu.SysMenu", "SysMenu.AddSysMenu", "SysMenu.SysMenuTreeComboBox"],
        refs: [
            {
                ref: "SysMenu",
                selector: "SysMenu"
            }
        ],
        init: function () {
            var me = this;
            this.control({
                "SysMenu": {
                    deleteActionClick: me.deleteNode,
                    edit: function (editPlugin,view) {
                        var data = view.grid.getSelectionModel().getSelection()[0].data;
                        updateSysMenu(view,data);
                    }
                },
                "SysMenu button[action=addSysMenu]": {
                    click:me.showAddSysMenu
                },
                "SysMenu button[action=refreshSysMenu]": {
                    click: me.refreshSysMenu
                },
                "AddSysMenu button[action=save]": {
                    click:me.addSysMenu
                }
            });
        },
        showAddSysMenu: function () {
            Ext.widget("AddSysMenu");
        },
        addSysMenu: function (button) {
            var form = button.up("window").down("form");
            if (form.isValid()) {
                save(form,this.getSysMenu().store);
            }
        },
        deleteNode: function (grid, record) {
            var me = this;
            Ext.Msg.confirm('确认', '确认删除吗？', function (operation) {
                if(operation=="yes")
                    deleteNode(me.getSysMenu().store, record.data.ID);
            });
        },
        refreshSysMenu: function (button) {
            this.getSysMenu().store.load();
        }
    });

    function save(form,store)
    {
        var entity = form.getValues();
        store.addSysMenu(entity, function (request, success, response) {
            if (response.timedout) {
                Ext.Msg.alert('提示', '操作超时');
                store.load();
                return;
            }
            var result = JSON.parse(response.responseText);
            if (success) {
                if (result.IsSuccessful) {
                    Ext.Msg.alert('提示', '操作成功');
                    store.load();
                    form.up("window").close();
                } else {
                    Ext.Msg.alert('提示', result.ErrorMessage);
                }
            } else {
                Ext.Msg.alert('提示', result.Message);
            }
        });
    }

    function deleteNode(store,id)
    {
        store.deleteSysMenu(id, function (request, success, response) {
            if (response.timedout) {
                Ext.Msg.alert('提示', '操作超时');
                store.load();
                return;
            }
            var result = JSON.parse(response.responseText);
            if (success) {
                if (result.IsSuccessful&&result.Data) {
                    Ext.Msg.alert('提示', '操作成功');
                    store.load();
                } else {
                    Ext.Msg.alert('提示', "操作失败"+result.ErrorMessage);
                }
            } else {
                Ext.Msg.alert('提示', result.Message);
            }
        });
    }

    function updateSysMenu(view, formvalue) {
        formvalue["index"] = formvalue["Index"];
        var store = view.grid.store;
        Ext.MessageBox.show({
            msg: '正在请求数据, 请稍侯',
            progressText: '正在请求数据',
            width: 300,
            wait: true,
            waitConfig: { interval: 200 }
        });
        store.updateSysMenu(formvalue, function (request, success, response) {
            if (response.timedout) {
                Ext.Msg.alert('提示', '操作超时');
                store.load();
                return;
            }
            var result = JSON.parse(response.responseText);
            if (success) {
                if (result.IsSuccessful) {
                    Ext.Msg.alert('提示', '操作成功');
                    store.load({
                        callback: function () {
                            view.grid.getRootNode().expandChildren();
                        }
                    });
                    Ext.MessageBox.hide();
                } else {
                    Ext.Msg.alert('提示', result.ErrorMessage);
                }
            } else {
                Ext.Msg.alert('提示', result.Message);
            }
        });
    }

}());