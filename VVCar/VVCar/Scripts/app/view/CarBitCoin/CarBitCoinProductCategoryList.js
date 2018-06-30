Ext.define('WX.view.CarBitCoin.CarBitCoinProductCategoryList', {
    extend: 'Ext.window.Window',
    alias: 'widget.CarBitCoinProductCategoryList',
    title: '分类管理',
    layout: 'fit',
    width: 400,
    height: 500,
    modal: true,
    initComponent: function() {
        var me = this;
        var treegridCarBitCoinProductCategoryStore = Ext.create('WX.store.BaseData.CarBitCoinProductCategoryTreeStore');
        me.tbar = [{
            action: 'addCarBitCoinProductCategory',
            xtype: 'button',
            text: '新增',
            scope: this,
            iconCls: 'add'
        }, {
            action: 'editCarBitCoinProductCategory',
            xtype: 'button',
            text: '修改',
            scope: this,
            iconCls: 'edit'
        }, {
            action: 'delCarBitCoinProductCategory',
            xtype: 'button',
            text: '删除',
            scope: this,
            iconCls: 'delete'
        }];
        me.items = [{
            xtype: 'treepanel',
            name: 'treegridCarBitCoinProductCategory',
            store: treegridCarBitCoinProductCategoryStore,
            useArrows: true,
            rootVisible: false,
            stripeRows: true,
            columns: [
                { xtype: 'treecolumn', header: '类别代码', dataIndex: 'Code', flex: 1, },
                { header: '类别名称', dataIndex: 'Name', flex: 1, },
                //}, {
                //    header: '会员产品', dataIndex: 'IsForMember', flex: 1,
                //    renderer: function (value) {
                //        if (value == 1) {
                //            return '<span style="color:green;">是</span>';
                //        } else {
                //            return '<span style="color:red;">否</span>';
                //        }
                //    }
                //}
            ],
        }];
        me.buttons = [{
            text: '关闭',
            action: 'close',
            scope: me,
            handler: me.close
        }];
        this.callParent();
    },
    afterRender: function() {
        this.callParent(arguments);
    }
});