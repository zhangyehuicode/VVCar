Ext.define('WX.view.Recruitment.RecruitmentEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.RecruitmentEdit',
	title: '编辑人才需求信息',
	layout: 'fit',
	width: 800,
	bodyPadding: 5,
	modal: true,
	initComponent: function () {
		var me = this;
		var degreeTypeStore = Ext.create('WX.store.DataDict.DegreeTypeStore');
		var sexStore = Ext.create('WX.store.DataDict.RecruitmentSexStore');
		me.form = Ext.create('Ext.form.Panel', {
			border: false,
			trackResetOnLoad: true,
			fieldDefaults: {
				labelAlign: 'left',
				labelWidth: 60,
				anchor: '100%',
				flex: 1,
				margin: '5',
			},
			items: [
				{
					xtype: 'form',
					layout: 'hbox',
					items: [{
						xtype: 'textfield',
						margin: '5 10 5 5',
						name: 'Recruiter',
						fieldLabel: '招聘单位',
						maxLength: 50,
						allowBlank: false,
					}, {
						xtype: 'textfield',
						margin: '5 10 5 5',
						name: 'Position',
						fieldLabel: '招聘岗位',
						maxLength: 50,
						allowBlank: false,
					}]
				}, {
					xtype: 'form',
					layout: 'hbox',
					items: [{
						xtype: 'datefield',
						name: 'StartDate',
						fieldLabel: '开始时间',
						margin: '5 10 5 5',
						allowBlank: true,
						format: 'Y-m-d',
						value: new Date(),
					}, {
						xtype: 'datefield',
						name: 'EndDate',
						fieldLabel: '结束时间',
						margin: '5 10 5 5',
						allowBlank: true,
						format: 'Y-m-d',
						value: new Date(),
					}]
				}, {
					xtype: 'form',
					layout: 'hbox',
					items: [{
						xtype: 'textfield',
						margin: '5 10 5 5',
						name: 'HRName',
						fieldLabel: '联系人',
						allowBlank: true,
					}, {
						xtype: 'textfield',
						margin: '5 10 5 5',
						name: 'MobilePhoneNo',
						fieldLabel: '手机号码',
						vtype: 'mobilephone',
						maxLength: 11,
						allowBlank: true,
					}]
				}, {
					xtype: 'form',
					layout: 'hbox',
					items: [{
						xtype: 'combobox',
						name: 'DegreeType',
						store: degreeTypeStore,
						displayField: 'DictName',
						valueField: 'DictValue',
						fieldLabel: '学历要求',
						margin: '5 10 5 5',
						labelWidth: 60,
						editable: false,
						allowBlank: true,
					}, {
						xtype: 'combobox',
						name: 'Sex',
						store: sexStore,
						displayField: 'DictName',
						valueField: 'DictValue',
						fieldLabel: '性别要求',
						margin: '5 10 5 5',
						labelWidth: 60,
						editable: false,
						allowBlank: true,
					}]
				}, {
					xtype: 'form',
					layout: 'hbox',
					items: [{
						xtype: 'textfield',
						margin: '5 10 5 5',
						name: 'WorkTime',
						fieldLabel: '工作时间',
						maxLength: 50,
						allowBlank: true,
					}, {
						xtype: 'textfield',
						margin: '5 10 5 5',
						name: 'Address',
						fieldLabel: '工作地点',
						maxLength: 50,
						allowBlank: true,
					}]
				}, {
					xtype: 'form',
					layout: 'hbox',
					items: [{
						xtype: 'textareafield',
						margin: '5 10 5 5',
						name: 'Requirement',
						height: 150,
						fieldLabel: '职位要求',
						maxLength: 250,
						allowBlank: true,
					}]
				}
			]
		});
		me.items = [me.form];
		me.buttons = [
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
		]
		me.callParent(arguments);
	}
});