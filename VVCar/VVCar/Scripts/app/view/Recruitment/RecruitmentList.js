Ext.define('WX.view.Recruitment.RecruitmentList', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.RecruitmentList',
	title: '商户管理',
	name: 'gridRecruitment',
	store: Ext.create('WX.store.BaseData.RecruitmentStore'),
	stripeRows: true,
	loadMask: true,
	closable: true,
	viewConfig: { enableTextSelection: true },
	selType: 'checkboxmodel',
	initComponent: function () {
		var me = this;
		me.tbar = [
			{
				action: 'addRecruitment',
				xtype: 'button',
				text: '添加需求',
				scope: this,
				iconCls: 'fa fa-plus-circle',
			}, {
				action: 'deleteRecruitment',
				xtype: 'button',
				text: '删除需求',
				scope: this,
				iconCls: 'fa fa-close',
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
				autoScroll: false,
				columnWidth: 1,
				items: [{
					xtype: 'textfield',
					name: 'Recruiter',
					fieldLabel: '招聘单位',
					width: 170,
					labelWidth: 60,
					margin: '0 0 0 5',
				}, {
					action: 'search',
					xtype: 'button',
					text: '搜索',
					iconCls: 'fa fa-search',
					cls: 'submitBtn',
					margin: '0 0 0 5',
				}, {
					action: 'export',
					xtype: 'button',
					text: '导出',
					iconCls: '',
					margin: '0 0 0 5',
				}]
			},
		];
		me.columns = [
			{ header: '招聘单位', dataIndex: 'Recruiter', flex: 1 },
			{ header: '招聘岗位', dataIndex: 'Position', flex: 1 },
			{ header: '招聘人数', dataIndex: 'RecruitNumber', flex: 1 },
			{
				header: '开始时间', dataIndex: 'StartDate', flex: 1,
				renderer: Ext.util.Format.dateRenderer('Y-m-d'),
			},
			{
				header: '结束时间', dataIndex: 'EndDate', flex: 1,
				renderer: Ext.util.Format.dateRenderer('Y-m-d'),
			},
			{ header: '联系人', dataIndex: 'HRName', flex: 1 },
			{ header: '联系人电话', dataIndex: 'HRPhone', flex: 1 },
			{
				header: '学历要求', dataIndex: 'DegreeType', flex: 1,
				renderer: function (value) {
					if (value == -1)
						return '<span>不限</span>';
					if (value == 0)
						return '<span>大专</span>';
					if (value == 1)
						return '<span>本科</span>';
					if (value == 2)
						return '<span>硕士</span>';
					if (value == 3)
						return '<span>博士</span>';
				}
			},
			{
				header: '性别要求', dataIndex: 'Sex', flex: 1,
				renderer: function (value) {
					if (value == 0)
						return '<span>不限</span>';
					if (value == 1)
						return '<span>男</span>';
					if (value == 2)
						return '<span>女</span>';
				}
			},
			{ header: '上班时间', dataIndex: 'WorkTime', flex: 1 },
			{ header: '职位职责要求', dataIndex: 'Requirement', flex: 1 },
		];
		me.dockedItems = [{
			xtype: 'pagingtoolbar',
			store: me.store,
			dock: 'bottom',
			displayInfo: true
		}];
		me.callParent();
	},
	afterRender: function () {
		var me = this;
		me.callParent(arguments);
		me.getStore().load();
	}
});