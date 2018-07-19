Ext.define('WX.view.UserMember.UserMemberSelector', {
	extend: 'Ext.window.Window',
	alias: 'widget.UserMemberSelector',
	title: '选择会员',
	name: 'userMemberSelector',
	layout: 'fit',
	width: 600,
	height: 500,
	bodyPadding: 5,
	autoShow: false,
	modal: true,
	buttonAlign: 'center',
	initComponent: function () {
		var me = this;
		var memberStore = Ext.create('WX.store.BaseData.MemberStore');
		var params = {
			IsFromUserMember: true,
		}
		Ext.apply(memberStore.proxy.extraParams, params);
		memberStore.pageSize = 10;
		memberStore.load();
		me.items = [{
			xtype: 'grid',
			name: 'memberList',
			stripeRows: true,
			loadMask: true,
			store: memberStore,
			selType: 'checkboxmodel',
			tbar: {
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
					name: 'Code',
					xtype: 'textfield',
					fieldLabel: '服务编码',
					width: 170,
					labelWidth: 60,
					margin: '0 0 0 5',
				}, {
					name: 'Name',
					xtype: 'textfield',
					fieldLabel: '服务名称',
					width: 170,
					labelWidth: 60,
					margin: '0 0 0 5',
				}, {
					action: 'search',
					xtype: 'button',
					text: '搜索',
					iconCls: 'submitBtn',
					margin: '0 0 0 5',
				}]
			},
			columns: [
				{ header: '会员姓名', dataIndex: 'Name', flex: 1 },
				{ header: '会员性别', dataIndex: 'Sex', flex: 1 },
				{ header: '手机号码', dataIndex: 'MobilePhoneNo', flex: 1 },
				{ header: '车牌号', dataIndex: 'PlateList', flex: 1 },
			],
			bbar: {
				xtype: 'pagingtoolbar',
				displayInfo: true
			},
			buttons: [
				{
					text: '保存',
					action: 'save',
					cls: 'submitBtn',
					scope: me
				},
				{
					text: '取消',
					scope: me,
					handler: me.close
				}
			],
		}]
		me.callParent(arguments);
	}
});