Ext.define('WX.view.WeChatFans.WeChatFansSetTag', {
    extend: 'Ext.window.Window',
    alias: 'widget.WeChatFansSetTag',
    layout: 'fit',
    title: '设置标签',
    width: 350,
    minHeight: 200,
    bodyPadding: 5,
    modal: true,
    initComponent: function () {
        var me = this;
        me.items = [{
            xtype: 'checkboxgroup',
            name: 'FansTagGroup',
            columns: 3,
            items: []
        }];
        me.buttons = [{
            text: '保存',
            action: 'save',
            cls: 'submitBtn',
            scope: me,
        }, {
            text: '取消',
            scope: me,
            handler: me.close
        }];
        me.callParent(arguments);
    }
});