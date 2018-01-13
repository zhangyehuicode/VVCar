Ext.define('WX.view.WeChatFans.WeChatFans', {
    extend: 'Ext.container.Container',
    alias: 'widget.WeChatFans',
    title: '用户管理-粉丝',
    layout: 'hbox',
    align: 'stretch',
    loadMask: true,
    closable: true,
    initComponent: function () {
        var me = this;
        var fansTagTreeStore = Ext.create('WX.store.BaseData.WeChatFansTagTreeStore');
        var fansStore = Ext.create('WX.store.BaseData.WeChatFansStore');
        me.items = [{
            xtype: 'grid',
            name: 'gridWeChatFansTag',
            //title: '全部用户',
            width: 250,
            height: '100%',
            store: fansTagTreeStore,
            stripeRows: true,
            hideHeaders: true,
            columns: [
                {
                    header: '标签', dataIndex: 'Name', flex: 1, align: 'center',
                    //renderer: function (value) {
                    //    return '<span style="font-size:18px;">' + value + '</span>';
                    //}
                }
            ],
            bbar: ['->', {
                action: 'manageFansTag',
                xtype: 'button',
                text: '管理标签',
                margin: '1 0 1 0',
            }, '->']
        }, {
            xtype: 'splitter'
        }, {
            xtype: 'grid',
            name: 'gridWeChatFans',
            flex: 1,
            height: '100%',
            store: fansStore,
            stripeRows: true,
            selType: 'checkboxmodel',
            tbar: [{
                xtype: 'form',
                name: 'formSearch',
                layout: 'column',
                border: false,
                frame: false,
                labelAlign: 'left',
                buttonAlign: 'right',
                padding: 5,
                autoWidth: true,
                autoScroll: true,
                fieldDefaults: {
                    labelAlign: 'left',
                    labelWidth: 40,
                    width: 190,
                    margin: '0 0 0 10',
                },
                items: [{
                    xtype: 'textfield',
                    name: 'NickName',
                    fieldLabel: '昵称',
                }, {
                    xtype: 'textfield',
                    name: 'District',
                    fieldLabel: '地区',
                }, {
                    xtype: "datefield",
                    name: "SubscribeStartDate",
                    fieldLabel: "关注时间",
                    format: "Y-m-d",
                    labelWidth: 60,
                }, {
                    xtype: 'displayfield',
                    value: '-',
                    width: 10
                }, {
                    xtype: "datefield",
                    name: "SubscribeFinishDate",
                    fieldLabel: "关注时间",
                    hideLabel: true,
                    format: "Y-m-d",
                    width: 125,
                }, {
                    action: 'search',
                    xtype: 'button',
                    text: '搜 索',
                    iconCls: 'fa fa-search',
                    cls: 'submitBtn',
                    margin: '0 0 0 10',
                }, {
                    action: 'export',
                    xtype: 'button',
                    iconCls: 'fa fa-download',
                    text: '导 出',
                    margin: '0 0 0 10',
                }, {
                    action: 'setTag',
                    xtype: 'button',
                    iconCls: 'fa fa-star',
                    text: '打标签',
                    margin: '0 0 0 10',
                }]
            }],
            columns: [
                {
                    header: '头像', dataIndex: 'HeadImgUrl', width: 70,
                    renderer: function (value, meta, record) {
                        if (value == "" | value == null) {
                            return "";
                        } else {
                            return '<img src="' + value + '" style="width: 50px; height: 50px;" />';
                        }
                    }
                },
                { header: '昵称', dataIndex: 'NickName', width: 120 },
                { header: '关注时间', dataIndex: 'SubscribeTime', width: 180 },
                {
                    header: '地区', dataIndex: 'Region', width: 150,
                    //renderer: function (value, meta, record) {
                    //    if (value == '' | value == null) {
                    //        return '';
                    //    } else {
                    //        return record.data.Country + ' ' + record.data.Province + ' ' + record.data.City;
                    //    }
                    //}
                },
                { header: '标签', dataIndex: 'Tags', flex: 1 },
            ],
            bbar: {
                xtype: 'pagingtoolbar',
                store: fansStore,
                displayInfo: true
            }
        }];
        this.callParent();
    },
});
