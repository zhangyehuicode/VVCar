Ext.define('WX.view.selector.UserSelector', {
    extend: 'Ext.window.Window',
    requires: ['WX.store.BaseData.DepartmentTreeStore', 'WX.store.BaseData.UserStore'],
    alias: 'widget.UserSelector',
    title: '用户选择',
    layout: 'hbox',
    width: 850,
    height: 500,
    defaults: { margin: '5 5 5 0' },
    modal: true,
    initComponent: function () {
        var me = this;
        
        me.deptStore = Ext.create('WX.store.BaseData.DepartmentTreeStore');
        me.sourceStore = Ext.create('WX.store.BaseData.UserStore');
        me.targetStore = Ext.create('Ext.data.Store', {
            model: 'WX.model.BaseData.UserModel',
        });
        me.userCols = [
            { header: '用户代码', dataIndex: 'Code', width: 100 },
            { header: '用户名称', dataIndex: 'Name', width: 100 },
            { header: '手机', dataIndex: 'MobilePhoneNo', width: 100 },
            {
                header: '是否可用', dataIndex: 'IsAvailable', width: 80, renderer: function (value) {
                    return value == false ? '<span style="color:red;">否</span>' : '<span style="color:green;">是</span>';
                },
            },
            {
                header: '是否允许登录', dataIndex: 'IsLoginUser', width: 100, renderer: function (value) {
                    return value == false ? '<span style="color:red;">否</span>' : '<span style="color:green;">是</span>';
                },
            }
        ];

        me.deptTree = Ext.create('Ext.tree.Panel', {
            store: me.deptStore,
            height: '100%',
            width: 150,
            margin: '5 0 5 5',
            useArrows: true,
            rootVisible: false,
            displayField: 'Text',
        });
        me.sourceUserGrid = Ext.create('Ext.grid.Panel', {
            title: '待选用户列表',
            store: me.sourceStore,
            height: '100%',
            flex: 1,
            selType: 'checkboxmodel',
            columns: me.userCols,
            bbar: {
                xtype: 'pagingtoolbar',
                store: me.sourceStore,
                displayInfo: true
            },
        });
        me.menuPanel = Ext.create('Ext.panel.Panel', {
            layout: {
                type: 'vbox',
                padding: '5',
                pack: 'center',
                align: 'center'
            },
            border: false,
            height: '100%',
            width: 90,
            defaults: { margin: '0 0 5 0', width: 80 },
            items: [{
                xtype: 'button',
                text: '选择全部',
                scope: me,
                handler: me.addAll
            }, {
                xtype: 'button',
                text: '选择',
                scope: me,
                handler: me.addSelected
            }, {
                xtype: 'button',
                text: '移除所有',
                scope: me,
                handler: me.removeAll
            }, {
                xtype: 'button',
                text: '移除',
                scope: me,
                handler: me.removeSelected
            }]
        });
        me.targetUserGrid = Ext.create('Ext.grid.Panel', {
            title: '已选用户列表',
            store: me.targetStore,
            height: '100%',
            flex: 1,
            selType: 'checkboxmodel',
            columns: me.userCols,
        });

        me.items = [me.deptTree, me.sourceUserGrid, me.menuPanel, me.targetUserGrid];
        me.buttons = [
            {
                text: '确定',
                scope: me,
                handler: me.confirmSelectFn,
            },
            {
                text: '取消',
                scope: me,
                handler: me.close
            }
        ];
        me.callParent();
        me.deptTree.on({
            scope: me,
            itemclick: function (tree, record, item) {
                Ext.apply(me.sourceStore.proxy.extraParams, { Department: record.data.ID });
                me.sourceStore.currentPage = 1;
                me.sourceStore.load();
            },
        });
        me.sourceUserGrid.on({
            scope: me,
            itemdblclick: me.addSelected
        });
        me.targetUserGrid.on({
            scope: me,
            itemdblclick: me.removeSelected
        });
    },
    confirmSelectFn: Ext.emptyFn,
    addSelected: function () {
        var m = this.sourceUserGrid.getSelectionModel().getSelection();
        if (m.length > 0) {
            this.targetStore.add(m);
            //store添加数据时会忽略重复的数据，但是grid还是会显示重复的数据。
            //所以需要手动的对grid的视图的刷新，让grid显示的数据和store保持一致。
            this.targetUserGrid.view.refresh();
        } else {
            Ext.Msg.alert("提示", "请选择至少一个用户");
        }
    },
    addAll: function () {
        var me = this;
        Ext.Msg.confirm('提示', '是否选择当前所有待选用户？', function (optional) {
            if (optional == "yes") {
                var ds = me.sourceStore;
                var count = ds.getCount();
                for (var i = 0; i < count; i++) {
                    me.targetStore.add(ds.getAt(i));
                }
                //store添加数据时会忽略重复的数据，但是grid还是会显示重复的数据。
                //所以需要手动的对grid的视图的刷新，让grid显示的数据和store保持一致。
                me.targetUserGrid.view.refresh();
            }
        });
    },
    removeSelected: function () {
        var m = this.targetUserGrid.getSelectionModel().getSelection();
        if (m.length > 0) {
            this.targetStore.remove(m);
        } else {
            Ext.Msg.alert("提示", "请先选中需要移除的数据");
        }
    },
    removeAll: function () {
        var me = this;
        Ext.Msg.confirm('提示', '是否移除全部已选择用户？', function (optional) {
            if (optional == "yes") {
                me.targetStore.removeAll();
            }
        });
    }
});
