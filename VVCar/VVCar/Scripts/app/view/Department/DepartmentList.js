Ext.define('WX.view.Department.DepartmentList', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.DepartmentList',
    title: '基地管理',
    store: Ext.create('WX.store.BaseData.DepartmentStore'),
    stripeRows: true,
    loadMask: true,
    closable: true,
    initComponent: function () {
        var me = this;
        var districtRegions = Ext.create('WX.store.DataDict.DataDictStore', { storeId: 'DistrictRegionDictStore' });
        districtRegions.load({ params: { dictType: 'DistrictRegion' } });
        var adminRegions = Ext.create('WX.store.DataDict.DataDictStore', { storeId: 'AdministrationRegionDictStore' });
        adminRegions.load({ params: { dictType: 'AdministrationRegion' } });
        me.tbar = [{
            action: 'AddDepartment',
            xtype: 'button',
            text: '添加',
            scope: me,
            iconCls: 'fa fa-plus-circle'
        }, {
            action: 'EditDepartment',
            xtype: 'button',
            text: '编辑',
            scope: me,
            iconCls: 'x-fa fa-pencil'
        }, {
            action: 'DeleteDepartment',
            xtype: 'button',
            text: '删除',
            scope: me,
            iconCls: 'x-fa fa-close'
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
                fieldLabel: '基地代码',
                width: 170,
                labelWidth: 60,
                margin: '0 0 0 5',
            }, {
                xtype: 'textfield',
                name: 'Name',
                fieldLabel: '基地名称',
                width: 190,
                labelWidth: 60,
                margin: '0 0 0 5',
            }, {
                xtype: 'combobox',
                fieldLabel: '地区分区',
                name: 'DistrictRegion',
                width: 175,
                labelWidth: 60,
                margin: '0 0 0 5',
                store: districtRegions,
                queryMode: 'local',
                displayField: 'DictName',
                valueField: 'DictValue',
                emptyText: '请选择...',
            }, {
                xtype: 'combobox',
                fieldLabel: '管理分区',
                name: 'AdministrationRegion',
                width: 175,
                labelWidth: 60,
                margin: '0 0 0 5',
                store: adminRegions,
                queryMode: 'local',
                displayField: 'DictName',
                valueField: 'DictValue',
                emptyText: '请选择...',
            }, {
                action: 'search',
                xtype: 'button',
                text: '搜 索',
                iconCls: 'fa fa-search',
                cls: 'submitBtn',
                margin: '0 0 0 5',
            }]
        }];
        me.columns = [
            { header: '基地代码', dataIndex: 'Code', flex: 1, },
            { header: '基地名称', dataIndex: 'Name', flex: 1, },
            {
                header: '地区分区', dataIndex: 'DistrictRegion', flex: 1,
                renderer: function (value, cellmeta, record) {
                    var record = districtRegions.findRecord('DictValue', value);
                    if (record == null) return value;
                    else return record.data.DictName;
                }
            },
            {
                header: '管理分区', dataIndex: 'AdministrationRegion', flex: 1,
                renderer: function (value, cellmeta, record) {
                    var record = adminRegions.findRecord('DictValue', value);
                    if (record == null) return value;
                    else return record.data.DictName;
                }
            },
            { header: '创建人', dataIndex: 'CreatedUser', flex: 1 },
            { header: '创建时间', dataIndex: 'CreatedDate', flex: 1 },
            { header: '最后更新时间', dataIndex: 'LastUpdateDate', flex: 1 },
        ];
        me.dockedItems = [{
            xtype: 'pagingtoolbar',
            store: me.store,
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
