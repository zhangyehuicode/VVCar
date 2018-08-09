Ext.define('WX.view.Member.MemberGroupEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.MemberGroupEdit',
	title: '编辑类别',
	layout: 'fit',
	width: 300,
	bodyPadding: 5,
	modal: true,
	initComponent: function () {
		var me = this;
		var parentMemberGroups = Ext.create('WX.store.BaseData.MemberGroupLiteStore');
		parentMemberGroups.load({ All: true });
		me.form = Ext.create('Ext.form.Panel', {
			border: false,
			trackResetOnLoad: true,
			fieldDefaults: {
				labelAlign: 'left',
				labelWidth: 90,
				anchor: '100%',
			},
			items: [{
				xtype: 'combo',
				name: 'ParentId',
				fieldLabel: '上级分类',
				store: parentMemberGroups,
				queryMode: 'local',
				displayField: 'Name',
				valueField: 'ID',
				editable: false,
				emptyText: '请选择...',
				blankText: '请选择所属类型',
				tpl: Ext.create('Ext.XTemplate', '<tpl for=".">', '<div class="x-boundlist-item">{Code} - {Name}</div>', '</tpl>'),
				displayTpl: Ext.create('Ext.XTemplate', '<tpl for=".">', '{Code} - {Name}', '</tpl>'),
			}, {
				xtype: 'textfield',
				name: 'ID',
				hidden: true,
				hideLabel: true,
			}, {
				xtype: 'textfield',
				name: 'Code',
				fieldLabel: '编号',
				maxLength: 20,
				allowBlank: false,
			}, {
				xtype: 'textfield',
				name: 'Name',
				fieldLabel: '分组名称',
				maxLength: 16,
				allowBlank: false,
			}, {
				xtype: 'numberfield',
				name: 'Index',
				fieldLabel: '排序',
				minValue: 1
			}, {
				xtype: 'checkboxfield',
				name: 'IsWholesalePrice',
				fieldLabel: '批发价',
				checked: false,
				inputValue: true,
			}]
		});

		me.items = [me.form];
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