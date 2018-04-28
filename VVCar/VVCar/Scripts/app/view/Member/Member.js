Ext.define('WX.view.Member.Member', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.Member',
    title: '会员信息',
    stripeRows: true,
    loadMask: true,
    closable: true,
    layout: 'hbox',
    initComponent: function () {
        var me = this;
        var statusStore = Ext.create("WX.store.DataDict.MemberCardStatusStore");
        var memberStore = Ext.create('WX.store.BaseData.MemberStore');
        //var memberGroupStore = Ext.create('WX.store.BaseData.MemberGroupStore');
        var memberCardTypeStore = Ext.create('WX.store.BaseData.MemberCardTypeStore');
        //memberGroupStore.load();
        var tbar = [{
            xtype: 'form',
            layout: 'column',
            border: false,
            frame: false,
            labelAlign: 'left',
            buttonAlign: 'right',
            labelWidth: 80,
            autoWidth: true,
            autoScroll: true,
            columnWidth: 1,
            items: [{
                xtype: "textfield",
                name: "Keyword",
                fieldLabel: "电话，姓名",
                width: 240,
                //labelWidth: 140,
                margin: "0 0 0 5"
            },
            //{
            //    xtype: "combobox",
            //    name: "Status",
            //    store: statusStore,
            //    fieldLabel: "会员状态",
            //    displayField: "DictName",
            //    valueField: "DictValue",
            //    width: 170,
            //    labelWidth: 60,
            //    forceSelection: true,
            //    margin: "0 0 0 5"
            //}, {
            //    xtype: "combobox",
            //    name: "CardTypeID",
            //    store: memberCardTypeStore,
            //    fieldLabel: "卡片类型",
            //    displayField: "Name",
            //    valueField: "ID",
            //    width: 170,
            //    labelWidth: 60,
            //    forceSelection: true,
            //    margin: "0 0 0 5"
            //    //}, {
            //    //    xtype: 'combobox',
            //    //    fieldLabel: '会员等级',
            //    //    name: "MemberGradeID",
            //    //    store: Ext.create('WX.store.BaseData.MemberGradeStore'),
            //    //    queryMode: 'remote',
            //    //    displayField: "Name",
            //    //    valueField: "ID",
            //    //    width: 170,
            //    //    labelWidth: 60,
            //    //    forceSelection: true,
            //    //    margin: '0 0 0 5',
            //},
            {
                action: "search",
                xtype: "button",
                text: "搜 索",
                iconCls: "fa fa-search",
                cls: "submitBtn",
                margin: "0 0 0 5"
                //}, {
                //    action: "export",
                //    xtype: "button",
                //    text: "导 出",
                //    cls: "submitBtn",
                //    margin: "0 0 0 5",
                //    permissionCode: 'Member.Member.ExportMember'
            }]
        }];
        var columns = [
            //{ header: "会员卡号", dataIndex: "CardNumber", flex: 1 },
            //{ header: "卡片类型", dataIndex: "CardTypeDesc", flex: 1 },
            //{
            //    header: "分组", dataIndex: "MemberGroupID", flex: 1,
            //    renderer: function (value, cellmeta, record) {
            //        if (value != null && value != '') {
            //            var record = memberGroupStore.findRecord('ID', value);
            //            if (record != null) {
            //                return record.data.Name;
            //            }
            //            return "普通会员";
            //        }
            //        return "普通会员";
            //    }
            //},
            { header: "姓名", dataIndex: "Name", flex: 1 },
            {
                header: "性别", dataIndex: "Sex", flex: 1,
                renderer: function (value) {
                    if (value == 1)
                        return "男";
                    else if (value == 2)
                        return "女";
                    else
                        return "未知";
                }
            },
            { header: "手机号码", dataIndex: "MobilePhoneNo", flex: 1 },
            { header: "归属地", dataIndex: "PhoneLocation", flex: 1 },
            { header: "剩余积分", dataIndex: "Point", flex: 1 },
            { header: "会员状态", dataIndex: "Status", flex: 1 },
            //{ header: "会员等级", dataIndex: "MemberGradeName", flex: 1 },
            //{ header: "所属门店", dataIndex: "OwnerDepartment", flex: 1 },
            { header: "注册时间", dataIndex: "CreatedDate", xtype: "datecolumn", format: "Y-m-d H:i:s", flex: 1 },
            //{ header: "余额（元）", dataIndex: "CardBalance", flex: 1, xtype: "numbercolumn" },
            //{ header: "OpenId", dataIndex: "WeChatOpenID", flex: 1, },
        ];
        var bbar = [{
            action: 'resetPassword',
            xtype: 'button',
            text: '重置核销密码',
            scope: me,
            iconCls: 'reset',
            permissionCode: 'Portal.BaseDataEdit'
        },
        //{
        //    action: 'reportLoss',
        //    xtype: 'button',
        //    text: '挂失',
        //    scope: me,
        //    iconCls: 'reportLoss',
        //    permissionCode: 'Portal.BaseDataEdit'
        //}, {
        //    action: 'cancelLoss',
        //    xtype: 'button',
        //    text: '解挂',
        //    scope: me,
        //    iconCls: 'cancelLoss',
        //    permissionCode: 'Portal.BaseDataEdit'
        //    //}, {
        //    //    action: 'changeCard',
        //    //    xtype: 'button',
        //    //    text: '换卡',
        //    //    scope: me,
        //    //    iconCls: 'switch',
        //    //    permissionCode: 'Portal.BaseDataEdit'
        //    },
        {
            action: 'editMember',
            xtype: 'button',
            text: '资料维护',
            scope: me,
            iconCls: 'edit',
            permissionCode: 'Portal.BaseDataEdit'
        },
        //{
        //    action: 'adjustBalance',
        //    xtype: 'button',
        //    text: '余额调整',
        //    scope: me,
        //    iconCls: "adjust",
        //    permissionCode: 'Member.Member.adjustBalance',
        //},
        {
            action: 'adjustMemberPoint',
            xtype: 'button',
            text: '积分调整',
            scope: me,
            //permissionCode: 'Member.Member.adjustMemberPoint',
            //}, {
            //    xtype: 'button',
            //    name: 'btnChangeMemberGroup',
            //    text: '移动分组',
            //    scope: me,
            //    permissionCode: 'Portal.BaseDataEdit',
            //    menu: Ext.create('Ext.menu.Menu')
        }];
        var dockedItems = [{
            xtype: "pagingtoolbar",
            store: memberStore,
            dock: "bottom",
            displayInfo: true
        }];

        me.memberGrid = Ext.create('Ext.grid.Panel', {
            name: 'gridMember',
            height: '100%',
            flex: 1,
            store: memberStore,
            selType: 'checkboxmodel',
            tbar: tbar,
            columns: columns,
            bbar: bbar,
            dockedItems: dockedItems
        });

        //me.memberGroup = Ext.create('WX.view.MemberGroup.MemberGroup', { width: '15%', height: '100%' });
        me.items = [me.memberGrid];
        //    [me.memberGroup, {
        //    xtype: 'splitter'
        //}, me.memberGrid],

        this.callParent(arguments);
    },
    afterRender: function () {
        this.callParent(arguments);
        this.down('grid').getStore().load();
    }
});
