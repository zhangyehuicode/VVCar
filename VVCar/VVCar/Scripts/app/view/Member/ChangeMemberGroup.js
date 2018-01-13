Ext.define('WX.view.Member.ChangeMemberGroup', {
    extend: 'Ext.window.Window',
    alias: 'widget.ChangeMemberGroup',
    title: '移动会员分组',
    layout: 'fit',
    width: 350,
    bodyPadding: 5,
    modal: true,
    initComponent: function () {
        var me = this;
        var memberGroupStore = Ext.create('WX.store.BaseData.MemberGroupStore');
        memberGroupStore.load({ params: { All: true } });
        me.form = Ext.create('Ext.form.Panel', {
            border: false,
            trackResetOnLoad: true,
            fieldDefaults: {
                labelAlign: 'left',
                labelWidth: 90,
                anchor: '100%'
            },
            items: [{
                xtype: 'textfield',
                name: 'ID',
                fieldLabel: 'ID',
                readOnly: true,
                hidden: true,
                hideLabel: true
            }, {
                xtype: 'textfield',
                name: 'CardNumber',
                fieldLabel: '会员卡号',
                readOnly: true,
            }, {
                xtype: 'textfield',
                name: 'MobilePhoneNo',
                fieldLabel: '手机号码',
                readOnly: true,
            }, {
                xtype: 'combo',
                name: 'CurrMemberGroup',
                fieldLabel: '当前分组',
                store: memberGroupStore,
                queryMode: 'local',
                displayField: 'Name',
                valueField: 'ID',
                readOnly: true,
            }, {
                xtype: 'combo',
                name: 'DestMemberGroup',
                fieldLabel: '目标分组',
                store: memberGroupStore,
                queryMode: 'local',
                displayField: 'Name',
                valueField: 'ID',
            }]
        });
        me.items = [me.form];
        me.buttons = [{
            text: '保存',
            action: 'save'
        }, {
            text: '取消',
            scope: me,
            handler: me.close
        }];
        me.callParent(arguments);
    }
});