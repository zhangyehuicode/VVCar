Ext.define('WX.view.RechargePlan.RechargePlanList', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.RechargePlanList',
    title: '储值方案管理',
    store: Ext.create('WX.store.BaseData.RechargePlanStore'),
    stripeRows: true,
    loadMask: true,
    closable: true,
    initComponent: function () {
        var rechargePlanTypes = Ext.getStore('DataDict.RechargePlanTypeStore');
        var enableDisableTypes = Ext.getStore('DataDict.EnableDisableTypeStore');
        this.tbar = [{
            action: 'addRechargePlan',
            xtype: 'button',
            text: '添加',
            scope: this,
            iconCls: 'x-fa fa-plus-circle',
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
                name: 'Code',
                fieldLabel: '方案代码',
                width: 170,
                labelWidth: 60,
                margin: '0 0 0 5',
            }, {
                xtype: 'textfield',
                name: 'Name',
                fieldLabel: '方案名称',
                width: 170,
                labelWidth: 60,
                margin: '0 0 0 5',
            }, {
                xtype: 'combobox',
                fieldLabel: '优惠类型',
                name: 'PlanType',
                width: 170,
                labelWidth: 60,
                margin: '0 0 0 5',
                store: Ext.create("WX.store.DataDict.RechargePlanTypeStore"),
                displayField: 'DictName',
                valueField: 'DictValue',
            }, {
                xtype: 'combobox',
                fieldLabel: '方案状态',
                name: 'Status',
                width: 170,
                labelWidth: 60,
                margin: '0 0 0 5',
                store: enableDisableTypes,
                displayField: 'DictName',
                valueField: 'DictValue',
            }, {
                action: 'search',
                xtype: 'button',
                text: '搜 索',
                iconCls: 'fa fa-search',
                cls: 'submitBtn',
                margin: '0 0 0 5',
            }]
        }];
        this.columns = [
            { header: '方案编号', dataIndex: 'Code', flex: 1, },         
            { header: '方案名称', dataIndex: 'Name', flex: 1, },
            { header: '储值金额', dataIndex: 'RechargeAmount', flex: 1, },
            {
                header: '状态', dataIndex: 'IsAvailable', flex: 1,
                renderer: function (value, cellmeta, record) {
                    if (value == 1) {
                        return '<span>启用</span>';
                    } else {
                        return '<span style="color:red;">禁用</span>';
                    }
                }
            },
            {
                header: '优惠类型', dataIndex: 'PlanType', flex: 1,
                renderer: function (value, cellmeta, record) {
                    var record = rechargePlanTypes.findRecord('DictValue', value);
                    if (record == null) return value;
                    else return record.data.DictName;
                }
            },
            { header: '生效日期', dataIndex: 'EffectiveDate', xtype: 'datecolumn', format: 'Y-m-d H:i:s', flex: 1 },
            { header: '截止日期', dataIndex: 'ExpiredDate', xtype: 'datecolumn', format: 'Y-m-d H:i:s', flex: 1 },
            {
                text: '操作功能',
                xtype: 'actioncolumn',
                width: 100,
                sortable: false,
                menuDisabled: true,
                height: 30,
                align: 'center',
                items: [{
                    action: 'editItem',
                    iconCls: 'x-fa fa-pencil',
                    tooltip: '编辑',
                    scope: this,
                    margin: '10 10 10 10',
                    handler: function (grid, rowIndex, colIndex) {
                        var record = grid.getStore().getAt(rowIndex);
                        this.fireEvent('editActionClick', grid, record);
                    },
                }, { scope: this }, {
                    action: 'enableItem',
                    iconCls: 'x-fa fa-check-circle',
                    tooltip: '启用',
                    scope: this,
                    margin: '10 10 10 10',
                    handler: function (grid, rowIndex, colIndex) {
                        var record = grid.getStore().getAt(rowIndex);
                        this.fireEvent('enableActionClick', grid, record);
                    },
                }, { scope: this }, {
                    action: 'disableItem',
                    iconCls: 'x-fa fa-ban',
                    tooltip: '禁用',
                    scope: this,
                    margin: '10 10 10 10',
                    handler: function (grid, rowIndex, colIndex) {
                        var record = grid.getStore().getAt(rowIndex);
                        this.fireEvent('disableActionClick', grid, record);
                    },
                }]
            }
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
