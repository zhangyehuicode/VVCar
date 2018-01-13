Ext.define('WX.view.MemberCardType.MemberCardTypeList', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.MemberCardTypeList',
    title: '卡片类型管理',
    store: Ext.create('WX.store.BaseData.MemberCardTypeStore'),
    stripeRows: true,
    loadMask: true,
    closable: true,
    initComponent: function () {
        var yesNoTypes = Ext.getStore('DataDict.YesNoTypeStore');
        this.tbar = [{
            action: 'addMemberCardType',
            xtype: 'button',
            text: '添加',
            scope: this,
            iconCls: 'fa fa-plus-circle',
        }, {
            action: 'editMemberCardType',
            xtype: 'button',
            text: '编辑',
            scope: this,
            iconCls: 'x-fa fa-pencil',
        }, {
            xtype: 'form',
            layout: 'column',
            border: false,
            frame: false,
            labelAlign: 'left',
            buttonAlign: 'right',
            labelWidth: 100,
            padding: 5,
            autoWidth: true,
            autoScroll: true,
            columnWidth: 1,
            items: [{
                xtype: 'textfield',
                name: 'Name',
                fieldLabel: '卡片名称',
                width: 170,
                labelWidth: 60,
                margin: '0 0 0 5',
            }, {
                xtype: 'combobox',
                fieldLabel: '允许门店激活',
                name: 'AllowStoreActivate',
                width: 160,
                labelWidth: 90,
                margin: '0 0 0 5',
                store: yesNoTypes,
                displayField: 'DictName',
                valueField: 'DictValue',
            }, {
                xtype: 'combobox',
                fieldLabel: '允许折扣',
                name: 'AllowDiscount',
                width: 140,
                labelWidth: 60,
                margin: '0 0 0 5',
                store: yesNoTypes,
                displayField: 'DictName',
                valueField: 'DictValue',
            }, {
                xtype: 'combobox',
                fieldLabel: '允许储值',
                name: 'AllowRecharge',
                width: 140,
                labelWidth: 60,
                margin: '0 0 0 5',
                store: yesNoTypes,
                displayField: 'DictName',
                valueField: 'DictValue',
            }, {
                action: 'search',
                xtype: 'button',
                text: '搜 索',
                iconCls: 'fa fa-search',
                cls: 'submitBtn',
                margin: '0 0 0 5',
            }, {
                action: 'theme',
                xtype: 'button',
                text: '卡片主题',
                margin: '0 0 0 5',
            }]
        }];
        this.columns = [
            { header: '方案名称', dataIndex: 'Name', width: 120, },
            {
                header: '允许门店激活', dataIndex: 'AllowStoreActivate', width: 120,
                renderer: function (value, cellmeta, record) {
                    if (value == 1) {
                        return '是';
                    } else {
                        return '否';
                    }
                }
            },
            {
                header: '允许折扣', dataIndex: 'AllowDiscount', width: 80,
                renderer: function (value, cellmeta, record) {
                    if (value == 1) {
                        return '是';
                    } else {
                        return '否';
                    }
                }
            },
            {
                header: '允许储值', dataIndex: 'AllowRecharge', width: 80,
                renderer: function (value, cellmeta, record) {
                    if (value == 1) {
                        return '是';
                    } else {
                        return '否';
                    }
                }
            },
            { header: '储值余额上限', dataIndex: 'MaxRecharge', flex: 1, },
        ];
        this.dockedItems = [{
            xtype: 'pagingtoolbar',
            store: this.store,
            dock: 'bottom',
            displayInfo: true
        }];
        this.callParent();
    },

    afterRender: function () {
        this.callParent(arguments);
        this.getStore().load();
    }
});
