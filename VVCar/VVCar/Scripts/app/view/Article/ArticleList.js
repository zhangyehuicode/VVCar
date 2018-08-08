Ext.define('WX.view.Article.ArticleList', {
	extend: 'Ext.container.Container',
	alias: 'widget.ArticleList',
	title: '图文消息',
	layout: 'hbox',
	align: 'stretch',
	loadMask: true,
	closable: true,
	initComponent: function () {
		var me = this;
		var articleStore = Ext.create('WX.store.BaseData.ArticleStore');
		articleStore.load();
		var articleItemStore = Ext.create('WX.store.BaseData.ArticleItemStore');
		me.items = [{
			xtype: 'grid',
			name: 'gridArticle',
			title: '公告',
			flex: 6,
			height: '100%',
			store: articleStore,
			stripeRow: true,
			selType: 'checkboxmodel',
			selModel: {
				selection: 'rowmodel',
				mode: 'single'
			},
			tbar: [{
				action: 'addArticle',
				xtype: 'button',
				text: '添加公告',
				iconCls: 'x-fa fa-plus-circle',
			}, {
				action: 'delArticle',
				xtype: 'button',
				text: '删除公告',
				iconCls: 'x-fa fa-close',
			}, {
				xtype: 'form',
				layout: 'column',
				border: false,
				frame: false,
				labelAlign: 'left',
				buttonAlign: 'right',
				labelWidth: 100,
				padding: 5,
				autoWidth: 1,
				items: [{
					xtype: 'textfield',
					name: 'Name',
					fieldLabel: '标题',
					width: 170,
					labelWidth: 30,
					margin: '0 0 0 5'
				}, {
					action: 'search',
					xtype: 'button',
					text: '搜索',
					iconCls: 'fa fa-search',
					cls: 'submitBtn',
					margin: '0 0 0 5'
				}]
			}],
			columns: [
				{ header: '标题', dataIndex: 'Name', flex: 1 },
				{ header: '创建人', dataIndex: 'CreatedUser', flex: 1 },
				{ header: '创建时间', dataIndex: 'CreatedDate', flex: 1 },
			],
			bbar: {
				xtype: 'pagingtoolbar',
				store: articleStore,
				dock: 'bottom',
				displayInfo: true
			}
		}, {
			xtype: 'splitter',
			}, {
				xtype: 'grid',
				name: 'gridArticleItem',
				flex: 4,
				stripeRows: true,
				store: articleItemStore,
				selType: 'checkboxmodel',
				title: '图文消息子项',
				tbar: [{
					action: 'addArticleItem',
					xtype: 'button',
					text: '添加图文子项',
					iconCls: 'x-fa fa-plus-circle',
					margin: '5 5 5 5',
				}, {
					action: 'delArticleItem',
					xtype: 'button',
					text: '删除图文子项',
					iconCls: 'x-fa fa-plus-circle',
					margin: '5 5 5 5',
				}],
				columns: [
					{ header: '文章标题', dataIndex: 'Title', flex: 1 },
					{ header: '作者', dataIndex: 'Author', flex: 1 },
					{ header: '图文消息', dataIndex: 'Disgest', flex: 1 },
					{ header: '是否显示封面', dataIndex: 'IsShowCoverPic', flex: 1 },
					{ header: '主体内容', dataIndex: 'Content', flex: 1 },
					{ header: '作者', dataIndex: 'CreatedUser' },
					{ header: '创建时间', dataIndex: 'CreatedDate' },
				],
				bbar: {
					xtype: 'pagingtoolbar',
					store: articleItemStore,
					dock: 'bottom',
					displayInfo: true,
				}
		}];
		this.callParent();
	}
})