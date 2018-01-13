Ext.define('WX.view.MemberCardTheme.MemberCardTheme', {
    extend: 'Ext.window.Window',
    alias: 'widget.MemberCardTheme',
    title: '卡片主题管理',
    layout: 'hbox',
    height: 550,
    width: 1050,
    modal: true,
    initComponent: function () {
        var me = this;
        var memberCardThemeGroupStore = Ext.create('WX.store.BaseData.MemberCardThemeGroupStore');

        memberCardThemeGroupStore.proxy.extraParams = { IsFromPortal: true };
        memberCardThemeGroupStore.load();

        me.cardCategoryTree = Ext.create('Ext.list.Tree', {
            name: 'cardcategorytree',
            width: '100%',
            border: false,
            expanderOnly: false,
            singleExpand: false,
            ui: 'navigation',
            store: Ext.create("WX.store.BaseData.CardThemeCategoryMenuStore"),
            micro: false,
        });

        me.form = Ext.create('Ext.form.Panel', {
            border: false,
            width: '100%',
            hidden: true,
            fieldDefaults: {
                labelAlign: 'left',
                labelWidth: 90,
                anchor: '100%',
            },
            items: [{
                xtype: 'textfield',
                name: 'ID',
                fieldLabel: '卡片类型ID',
            }]
        });


        me.grid = Ext.create('Ext.grid.Panel', {
            width: '100%',
            height: '100%',
            store: memberCardThemeGroupStore,
            tbar: [
                {
                    xtype: 'button',
                    text: '添加',
                    action: 'add',
                }, {
                    xtype: 'button',
                    text: '编辑',
                    action: 'edit',
                }, {
                    xtype: 'button',
                    text: '删除',
                    action: 'delete',
                    iconCls: 'x-fa fa-close'
                }
            ],
            columns: [
                { text: '排序', dataIndex: 'Index', flex: 1 },
                { text: '主题名称', dataIndex: 'Name', flex: 1 },
                {
                    header: '主题图片', dataIndex: 'ImgUrlDto', width: 70, flex: 3,
                    renderer: function (value, meta, record) {
                        if (value == "" | value == null) {
                            return "";
                        }
                        else {
                            var html = "";
                            var valueList = value.split(";")

                            var Numlength = 0;
                            if (valueList.length < 3) {
                                Numlength = valueList.length;
                            }
                            else {
                                Numlength = 3;
                            }

                            for (var i = 0; i < Numlength; i++) {
                                html += '<img src="' + valueList[i] + '" style="width: 80px; height: 50px;margin-left:10px;" />';
                            }
                            return html
                        }
                    }
                },
                { text: '所属分组', dataIndex: 'CategoryName', flex: 1 },
                { text: '是否启用', dataIndex: 'IsAvailableShow', flex: 1 },
                {
                    text: '操作',
                    xtype: 'actioncolumn',
                    width: 150,
                    sortable: false,
                    menuDisabled: true,
                    height: 30,
                    align: 'center',
                    items: [{
                        action: 'esc',
                        iconCls: 'x-fa fa-arrow-up',
                        tooltip: '升序',
                        scope: this,
                        margin: '10 10 10 10',
                        handler: function (grid, rowIndex, colIndex) {
                            var record = grid.getStore().getAt(rowIndex);
                            this.fireEvent('escActionClick', grid, record);
                        },
                    },
                    { scope: this },
                    {
                        action: 'desc',
                        iconCls: 'x-fa fa-arrow-down',
                        tooltip: '降序',
                        scope: this,
                        margin: '10 10 10 10',
                        handler: function (grid, rowIndex, colIndex) {
                            var record = grid.getStore().getAt(rowIndex);
                            this.fireEvent('descActionClick', grid, record);
                        },
                    }, { scope: this }, {
                        iconCls: 'x-fa fa-check-circle',
                        tooltip: '启用',
                        scope: this,
                        margin: '10 10 10 10',
                        handler: function (grid, rowIndex, colIndex) {
                            var record = grid.getStore().getAt(rowIndex);
                            this.fireEvent('enableThemeActionClick', grid, record);
                        },
                    }, { scope: this }, {
                        iconCls: 'x-fa fa-ban',
                        tooltip: '禁用',
                        scope: this,
                        margin: '10 10 10 10',
                        handler: function (grid, rowIndex, colIndex) {
                            var record = grid.getStore().getAt(rowIndex);
                            this.fireEvent('disableThemeActionClick', grid, record);
                        }
                    }]
                },

            ],
            bbar: {
                xtype: 'pagingtoolbar',
                store: memberCardThemeGroupStore,
                displayInfo: true
            }
        });
        me.items = [{
            xtype: 'container',
            width: 200,
            height: '100%',
            bodyStyle: 'background:#32404e;',
            layout: 'vbox',
            style: {
                background: '#32404e',
            },
            items: [{
                xtype: 'container',
                width: '100%',
                flex: 1,
                scrollable: 'y',
                items: [me.cardCategoryTree]
            },
                //{
                //    xtype: 'container',
                //    width: '100%',
                //    layout: {
                //        type: 'vbox',
                //        align: 'center',
                //    },
                //    items: [{
                //        xtype: 'button',
                //        action: 'cardthemegroupmanager',
                //        text: '推荐分组管理',
                //        margin: 10,
                //    }]
                //    }
            ]
        }, {
            xtype: 'container',
            layout: 'vbox',
            width: 850,
            height: '100%',
            items: [me.form, me.grid]
        }];
        me.callParent(arguments);
    }
});