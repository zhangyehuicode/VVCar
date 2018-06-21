Ext.define('WX.view.Shop.ServiceCategoryList', {
	extend: 'Ext.window.Window',
	alias: 'widget.ServiceCategoryList',
	title: '分类管理',
	layout: 'fit',
	width: 400,
	height: 500,
	modal: true,
	initComponent: function () {
		var me = this;
		var treegridServiceCategoryStore = Ext.create('WX.store.BaseData.ProductCategoryTreeStore');
		me.tbar = [{
			action: 'addProductCategory',
			xtype: 'button',
			text: '新增',
			scope: this,
			iconCls: 'add'
		}, {
			action: 'editProductCategory',
			xtype: 'button',
			text: '修改',
			scope: this,
			iconCls: 'edit'
		}, {
			action: 'delProductCategory',
			xtype: 'button',
			text: '删除',
			scope: this,
			iconCls: 'delete'
		}];
		me.items = [{
			xtype: 'treepanel',
			name: 'treegridServiceCategory',
			store: treegridServiceCategoryStore,
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
	afterRender: function () {
		this.callParent(arguments);
	}
});