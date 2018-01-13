Ext.define('WX.view.MemberGrade.MemberGradeList', {
    extend: 'Ext.container.Container',
    alias: 'widget.MemberGradeList',
    title: '会员等级',
    layout: 'anchor',
    closable: true,
    loadMask: true,
    initComponent: function () {
        var me = this;
        var memberGradeStore = Ext.create('WX.store.BaseData.MemberGradeStore');
        var gradeStatusStore = Ext.getStore('DataDict.MemberGradeStatusStore');
        me.mainGrid = Ext.create('Ext.grid.Panel', {
            name: 'gridMemberGrade',
            store: memberGradeStore,
            stripeRows: true,
            anchor: '100%, 100%',
            tbar: [{
                action: 'addMemberGrade',
                xtype: 'button',
                text: '添加',
                scope: me,
                iconCls: 'fa fa-plus-circle',
            }, {
                action: 'editMemberGrade',
                xtype: 'button',
                text: '编辑',
                scope: me,
                iconCls: 'x-fa fa-pencil'
            }, {
                action: 'deleteMemberGrade',
                xtype: 'button',
                text: '删除',
                scope: me,
                iconCls: 'x-fa fa-close'
            }, {
                action: 'enableMemberGrade',
                xtype: 'button',
                text: '启用',
                scope: me,
            }, {
                action: 'disableMemberGrade',
                xtype: 'button',
                text: '禁用',
                scope: me,
            }, {
                action: 'openMemberGrade',
                xtype: 'button',
                text: '开放',
                scope: me,
            }, {
                action: 'closeMemberGrade',
                xtype: 'button',
                text: '关闭',
                scope: me,
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
                    xtype: 'combobox',
                    fieldLabel: '状态',
                    name: 'Status',
                    width: 160,
                    labelWidth: 50,
                    margin: '0 0 0 5',
                    store: gradeStatusStore,
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
            }],
            columns: [
                { header: '等级排序', dataIndex: 'Level', width: 100, },
                { header: '等级名称', dataIndex: 'Name', width: 200, },
                { header: '是否默认等级', xtype: "booleancolumn", dataIndex: 'IsDefault', width: 120, falseText: '否', trueText: '是' },
                {
                    header: '状态', dataIndex: 'Status', width: 80,
                    renderer: function (value, meta, record) {
                        var dictValue = gradeStatusStore.findRecord('DictValue', value);
                        if (dictValue == null) {
                            return value;
                        }
                        var color = value == 1 ? 'green' : 'red';
                        return '<span style="color:' + color + ';">' + dictValue.data.DictName + '</span>';
                    }
                },
                {
                    header: '是否开放', dataIndex: 'IsNotOpen', width: 80,
                    renderer: function (value, meta, record) {
                        if (value == 1) {
                            return '<span style="color:red;">关闭</span>';
                        } else {
                            return '<span style="color:green;">开放</span>';
                        }
                    }
                },
                { header: '发布时间', dataIndex: 'CreatedDate', width: 200, },
                { header: '备注', dataIndex: 'Remark', flex: 1, },
            ],
            dockedItems: [{
                xtype: 'pagingtoolbar',
                store: memberGradeStore,
                dock: 'bottom',
                displayInfo: true
            }]
        });
        me.items = [me.mainGrid];
        this.callParent();
    },

    afterRender: function () {
        this.callParent(arguments);
        this.mainGrid.getStore().load();
    }
});
