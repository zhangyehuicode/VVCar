Ext.define('WX.view.Announcement.AnnouncementList', {
	extend: 'Ext.container.Container',
	alias: 'widget.AnnouncementList',
	title: '公告',
	layout: 'hbox',
	align: 'stretch',
	loadMask: true,
	closable: true,
	initComponent: function () {
		var me = this;
		var announcementStore = Ext.create('WX.store.BaseData.AnnouncementStore');
		announcementStore.load();
		announcementPushMemberStore = Ext.create('WX.store.BaseData.AnnouncementPushMemberStore');
		me.items = [{
			xtype: 'grid',
			name: 'gridAnnouncement',
			title: '公告',
			flex: 6,
			height: '100%',
			store: announcementStore,
			stripeRow: true,
			selType: 'checkboxmodel',
			selModel: {
				selection: 'rowmodel',
				model: 'single',
			},
			tbar: [{
				action: 'addAnnouncement',
				xtype: 'button',
				text: '添加公告',
				iconCls: 'x-fa fa-plus-circle',
			}, {
				action: 'delAnnouncement',
				xtype: 'button',
				text: '删除公告',
				iconCls: 'x-fa fa-close',
			}, {
				action: 'batchHandPush',
				xtype: 'button',
				text: '手动推送',
				iconCls: 'fa fa-arrow-up',
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
					name: 'Title',
					fieldLabel: '标题',
					width: 170,
					labelWidth: 30,
					margin: '0 0 0 5',
				}, {
					xtype: 'textfield',
					name: 'Name',
					fieldLabel: '项目名称',
					width: 170,
					labelWidth: 60,
					margin: '0 0 0 5',
				}, {
					xtype: 'combobox',
					fieldLabel: '推送状态',
					name: 'Status',
					width: 175,
					labelWidth: 60,
					margin: '0 0 0 5',
					store: [
						[0, '未推送'],
						[1, '已推送'],
						[-1, '终止推送'],
					],
					queryMode: 'local',
					displayField: 'DictName',
					valueField: 'DictValue',
				}, {
					action: 'search',
					xtype: 'button',
					text: '搜索',
					iconCls: 'fa fa-search',
					cls: 'submitBtn',
					margin: '0 0 0 5',
				}]
			}],
			columns: [
				{ header: '标题', dataIndex: 'Title', flex: 1 },
				{ header: '项目名称', dataIndex: 'Name', flex: 1 },
				{
					header: '推送时间', dataIndex: 'PushDate', width: 100,
					renderer: Ext.util.Format.dateRenderer('Y-m-d'),
				},
				{
					header: '推送状态', dataIndex: 'Status', flex: 1,
					renderer: function (value) {
						if (value == 0)
							return '<span><font>未推送</font></span>';
						if (value == 1)
							return '<span><font color="green">已推送</font></span>';
						if (value == -1)
							return '<span><font color="red">终止推送</font></span>';
					}
				},
				{
					header: '推送所有会员', dataIndex: 'PushAllMembers', flex: 1,
					renderer: function (value) {
						if (value == 0)
							return '<span><font color="red">否</font></span>';
						else
							return '<span><font color="green">是</font></span>';
					}
				},
				{ header: '项目进展', dataIndex: 'Process', flex: 1 },
				{ header: '详细内容', dataIndex: 'Content', flex: 1 },
				{ header: '项目备注', dataIndex: 'Remark', flex: 1 },
				{
					header: '创建时间', dataIndex: 'CreatedDate', flex: 1,
					renderer: Ext.util.Format.dateRenderer('Y-m-d'),
				},
				{
					text: '操作功能',
					xtype: 'actioncolumn',
					width: 80,
					sortable: false,
					menuDisabled: true,
					height: 30,
					align: 'center',
					items: [{
						action: 'updateItem',
						iconCls: 'x-fa fa-pencil',
						tooltip: '编辑',
						scope: this,
						margin: '10 10 10 10',
						handler: function (grid, rowIndex, colIndex) {
							var record = grid.getStore().getAt(rowIndex);
							this.fireEvent('updateActionClick', grid, record);
						}
					}]
				}
			],
			bbar: {
				xtype: 'pagingtoolbar',
				store: announcementStore,
				dock: 'bottom',
				displayInfo: true,
			}
		}, {
			xtype: 'splitter',
		}, {
			xtype: 'grid',
			name: 'gridAnnouncementPushMember',
			title: '会员',
			flex: 4,
			stripeRows: true,
			store: announcementPushMemberStore,
			selType: 'checkboxmodel',
			tbar: [{
				action: 'addAnnouncementPushMember',
				xtype: 'button',
				text: '添加推送会员',
				iconCls: 'x-fa fa-plus-circle',
				margin: '5 5 5 5',
			}, {
				action: 'delAnnouncementPushMember',
				xtype: 'button',
				text: '删除推送会员',
				iconCls: 'x-fa fa-close',
				margin: '5 5 5 5',
			}],
			columns: [
				{ header: '会员名称', dataIndex: 'Name', flex: 1 },
				{ header: '手机号', dataIndex: 'MobilePhoneNo', flex: 1 },
				{ header: '车牌号', dataIndex: 'PlateList', flex: 1 },
			],
			dockedItems: {
				xtype: 'pagingtoolbar',
				store: announcementPushMemberStore,
				dock: 'bottom',
				displayInfo: true,
			}
		}];
		me.callParent();
	}
})