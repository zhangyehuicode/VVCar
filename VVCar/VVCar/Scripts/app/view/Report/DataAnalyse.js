Ext.define('WX.view.Report.DataAnalyse', {
	extend: 'Ext.container.Container',
	alias: 'widget.DataAnalyse',
	title: '客户数据分析',
	layout: 'anchor',
	loadMask: true,
	closable: true,
	padding: 15,
	initComponent: function () {
		var me = this;
		var url = window.location.origin;
		var memberStore = Ext.create('WX.store.BaseData.MemberStore');
		memberStore.load();
		var count = memberStore.getCount();
		me.form = Ext.create('Ext.form.Panel', {
			name: 'datatype',
			border: false,
			trackResetOnLoad: true,
			width: '100%',
			fieldDefaults: {
				labelAlign: 'left',
				labelWidth: 80,
				anchor: '100%',
				readOnly: true,
				margin: '0 0 8 0',
				tabIndex: 0,
				fieldStyle: 'font-size: 16px; line-height: normal',
			},
			items: [{
				xtype: 'button',
				action: 'newmember',
				id: 'newmemberbtn',
				text: '新增客户',
				cls: 'submitBtn',
				margin: '0 0 0 5',
			}, {
				xtype: 'button',
				action: 'membertype',
				id: 'membertypebtn',
				text: '会员类型',
				cls: 'submitBtn',
				margin: '0 0 0 5',
			}, {
				xtype: 'button',
				action: 'bigmember',
				id: 'bigmemberbtn',
				text: '大客户',
				cls: 'submitBtn',
				margin: '0 0 0 5',
			}, {
				xtype: 'button',
				action: 'loyalmember',
				id: 'loyalmemberbtn',
				text: '客户忠诚度',
				cls: 'submitBtn',
				margin: '0 0 0 5',
			}, {
				xtype: 'button',
				action: 'losemember',
				id: 'losememberbtn',
				text: '流失客户',
				cls: 'submitBtn',
				margin: '0 0 0 5',
			}]
		});

		me.newmembergrid = Ext.create('Ext.container.Container', {
			layout: 'hbox',
			height: 410,
			name: 'newmembergrid',
			items: [{
				name: 'newmemberday',
				xtype: 'grid',
				store: Ext.create('WX.store.BaseData.DataAnalyseStore'),
				width: '50%',
				height: 410,
				columns: [
					{ header: '会员昵称', dataIndex: 'MemberName', flex: 1 },
					{ header: '手机号码', dataIndex: 'MemberMobilePhone', flex: 1 },
					{ header: '消费项目总数', dataIndex: 'TotalQuantity', flex: 1 },
					{ header: '消费总额', dataIndex: 'TotalMoney', flex: 1 },
					{ header:'注册时间', dataIndex: 'RegistDate', flex: 1 },
				],
				dockedItems: [{
					xtype: 'pagingtoolbar',
					store: this.store,
					dock: 'bottom',
					displayInfo: true
				}]
			}, {
				xtype: 'splitter'
			}, {
				name: 'newmembermonth',
				xtype: 'grid',
				store: Ext.create('WX.store.BaseData.DataAnalyseStore'),
				width: '50%',
				height: 410,
				columns: [
					{ header: '会员昵称', dataIndex: 'MemberName', flex: 1 },
					{ header: '手机号码', dataIndex: 'MemberMobilePhone', flex: 1 },
					{ header: '消费项目总数', dataIndex: 'TotalQuantity', flex: 1 },
					{ header: '消费总额', dataIndex: 'TotalMoney', flex: 1 },
					{ header: '注册时间', dataIndex: 'RegistDate', flex: 1 },
				],
				dockedItems: [{
					xtype: 'pagingtoolbar',
					store: this.store,
					dock: 'bottom',
					displayInfo: true
				}]
			}]
		});

		me.membertypegrid1 = Ext.create('Ext.container.Container', {
			layout: 'hbox',
			height: 410,
			name: 'membertypegrid1',
			items: [{
				name: 'membertype1',
				xtype: 'grid',
				store: Ext.create('WX.store.BaseData.DataAnalyseStore'),
				width: '50%',
				height: 410,
				columns: [
					{ header: '会员昵称', dataIndex: 'MemberName', flex: 1 },
					{ header: '手机号码', dataIndex: 'MemberMobilePhone', flex: 1 },
					{ header: '消费项目总数', dataIndex: 'TotalQuantity', flex: 1 },
					{ header: '消费总额', dataIndex: 'TotalMoney', flex: 1 },
				],
				dockedItems: [{
					xtype: 'pagingtoolbar',
					store: this.store,
					dock: 'bottom',
					displayInfo: true
				}]
			}, {
				xtype: 'splitter'
			}, {
				name: 'membertype2',
				xtype: 'grid',
				store: Ext.create('WX.store.BaseData.DataAnalyseStore'),
				width: '50%',
				height: 410,
				columns: [
					{ header: '会员昵称', dataIndex: 'MemberName', flex: 1 },
					{ header: '手机号码', dataIndex: 'MemberMobilePhone', flex: 1 },
					{ header: '消费项目总数', dataIndex: 'TotalQuantity', flex: 1 },
					{ header: '消费总额', dataIndex: 'TotalMoney', flex: 1 },
				],
				dockedItems: [{
					xtype: 'pagingtoolbar',
					store: this.store,
					dock: 'bottom',
					displayInfo: true
				}]
			}]
		});

		me.membertypegrid2 = Ext.create('Ext.container.Container', {
			layout: 'hbox',
			height: 410,
			name: 'membertypegrid2',
			items: [{
				name: 'membertype3',
				xtype: 'grid',
				store: Ext.create('WX.store.BaseData.DataAnalyseStore'),
				width: '50%',
				height: 410,
				columns: [
					{ header: '会员昵称', dataIndex: 'MemberName', flex: 1 },
					{ header: '手机号码', dataIndex: 'MemberMobilePhone', flex: 1 },
					{ header: '消费项目总数', dataIndex: 'TotalQuantity', flex: 1 },
					{ header: '消费总额', dataIndex: 'TotalMoney', flex: 1 },
				],
				dockedItems: [{
					xtype: 'pagingtoolbar',
					store: this.store,
					dock: 'bottom',
					displayInfo: true
				}]
			}, {
				xtype: 'splitter'
			}, {
				name: 'membertype4',
				xtype: 'grid',
				store: Ext.create('WX.store.BaseData.DataAnalyseStore'),
				width: '50%',
				height: 410,
				columns: [
					{ header: '会员昵称', dataIndex: 'MemberName', flex: 1 },
					{ header: '手机号码', dataIndex: 'MemberMobilePhone', flex: 1 },
					{ header: '消费项目总数', dataIndex: 'TotalQuantity', flex: 1 },
					{ header: '消费总额', dataIndex: 'TotalMoney', flex: 1 },
				],
				dockedItems: [{
					xtype: 'pagingtoolbar',
					store: this.store,
					dock: 'bottom',
					displayInfo: true
				}]
			}]
		});

		me.bigmembergrid = Ext.create('Ext.container.Container', {
			layout: 'hbox',
			height: 410,
			name: 'bigmembergrid',
			items: [{
				name: 'bigmember1',
				xtype: 'grid',
				store: Ext.create('WX.store.BaseData.DataAnalyseStore'),
				width: '50%',
				height: 410,
				columns: [
					{ header: '会员昵称', dataIndex: 'MemberName', flex: 1 },
					{ header: '手机号码', dataIndex: 'MemberMobilePhone', flex: 1 },
					{ header: '消费项目总数', dataIndex: 'TotalQuantity', flex: 1 },
					{ header: '消费总额', dataIndex: 'TotalMoney', flex: 1 },
				],
				dockedItems: [{
					xtype: 'pagingtoolbar',
					store: this.store,
					dock: 'bottom',
					displayInfo: true
				}]
			}, {
				xtype: 'splitter'
			}, {
				name: 'bigmember2',
				xtype: 'grid',
				store: Ext.create('WX.store.BaseData.DataAnalyseStore'),
				width: '50%',
				height: 410,
				columns: [
					{ header: '会员昵称', dataIndex: 'MemberName', flex: 1 },
					{ header: '手机号码', dataIndex: 'MemberMobilePhone', flex: 1 },
					{ header: '消费项目总数', dataIndex: 'TotalQuantity', flex: 1 },
					{ header: '消费总额', dataIndex: 'TotalMoney', flex: 1 },
				],
				dockedItems: [{
					xtype: 'pagingtoolbar',
					store: this.store,
					dock: 'bottom',
					displayInfo: true
				}]
			}]
		});

		me.loyalmembergrid = Ext.create('Ext.container.Container', {
			layout: 'hbox',
			height: 410,
			name: 'loyalmembergrid',
			items: [{
				name: 'loyalmember1',
				xtype: 'grid',
				store: Ext.create('WX.store.BaseData.DataAnalyseStore'),
				width: '33%',
				height: 410,
				columns: [
					{ header: '会员昵称', dataIndex: 'MemberName', flex: 1 },
					{ header: '手机号码', dataIndex: 'MemberMobilePhone', flex: 1 },
					{ header: '消费项目总数', dataIndex: 'TotalQuantity', flex: 1 },
					{ header: '消费总额', dataIndex: 'TotalMoney', flex: 1 },
				],
				dockedItems: [{
					xtype: 'pagingtoolbar',
					store: this.store,
					dock: 'bottom',
					displayInfo: true
				}]
			}, {
				xtype: 'splitter'
			}, {
				name: 'loyalmember2',
				xtype: 'grid',
				store: Ext.create('WX.store.BaseData.DataAnalyseStore'),
				width: '33%',
				height: 410,
				columns: [
					{ header: '会员昵称', dataIndex: 'MemberName', flex: 1 },
					{ header: '手机号码', dataIndex: 'MemberMobilePhone', flex: 1 },
					{ header: '消费项目总数', dataIndex: 'TotalQuantity', flex: 1 },
					{ header: '消费总额', dataIndex: 'TotalMoney', flex: 1 },
				],
				dockedItems: [{
					xtype: 'pagingtoolbar',
					store: this.store,
					dock: 'bottom',
					displayInfo: true
				}]
			}, {
				xtype: 'splitter'
			}, {
				name: 'loyalmember3',
				xtype: 'grid',
				store: Ext.create('WX.store.BaseData.DataAnalyseStore'),
				width: '33%',
				height: 410,
				columns: [
					{ header: '会员昵称', dataIndex: 'MemberName', flex: 1 },
					{ header: '手机号码', dataIndex: 'MemberMobilePhone', flex: 1 },
					{ header: '消费项目总数', dataIndex: 'TotalQuantity', flex: 1 },
					{ header: '消费总额', dataIndex: 'TotalMoney', flex: 1 },
				],
				dockedItems: [{
					xtype: 'pagingtoolbar',
					store: this.store,
					dock: 'bottom',
					displayInfo: true
				}]
			}]
		});

		me.losemembergrid = Ext.create('Ext.container.Container', {
			layout: 'hbox',
			height: 410,
			name: 'losemembergrid',
			items: [{
				name: 'losemember1',
				xtype: 'grid',
				store: Ext.create('WX.store.BaseData.DataAnalyseStore'),
				width: '33%',
				height: 410,
				columns: [
					{ header: '会员昵称', dataIndex: 'MemberName', flex: 1 },
					{ header: '手机号码', dataIndex: 'MemberMobilePhone', flex: 1 },
					{ header: '消费项目总数', dataIndex: 'TotalQuantity', flex: 1 },
					{ header: '消费总额', dataIndex: 'TotalMoney', flex: 1 },
				],
				dockedItems: [{
					xtype: 'pagingtoolbar',
					store: this.store,
					dock: 'bottom',
					displayInfo: true
				}]
			}, {
				xtype: 'splitter'
			}, {
				name: 'losemember2',
				xtype: 'grid',
				store: Ext.create('WX.store.BaseData.DataAnalyseStore'),
				width: '33%',
				height: 410,
				columns: [
					{ header: '会员昵称', dataIndex: 'MemberName', flex: 1 },
					{ header: '手机号码', dataIndex: 'MemberMobilePhone', flex: 1 },
					{ header: '消费项目总数', dataIndex: 'TotalQuantity', flex: 1 },
					{ header: '消费总额', dataIndex: 'TotalMoney', flex: 1 },
				],
				dockedItems: [{
					xtype: 'pagingtoolbar',
					store: this.store,
					dock: 'bottom',
					displayInfo: true
				}]
			}, {
				xtype: 'splitter'
			}, {
				name: 'losemember3',
				xtype: 'grid',
				store: Ext.create('WX.store.BaseData.DataAnalyseStore'),
				width: '33%',
				height: 410,
				columns: [
					{ header: '会员昵称', dataIndex: 'MemberName', flex: 1 },
					{ header: '手机号码', dataIndex: 'MemberMobilePhone', flex: 1 },
					{ header: '消费项目总数', dataIndex: 'TotalQuantity', flex: 1 },
					{ header: '消费总额', dataIndex: 'TotalMoney', flex: 1 },
				],
				dockedItems: [{
					xtype: 'pagingtoolbar',
					store: this.store,
					dock: 'bottom',
					displayInfo: true
				}]
			}]
		});

		me.newmembercharts = Ext.create('Ext.container.Container', {
			layout: 'hbox',
			height: 320,
			name: 'newmemberechart',
			items: [{
				xtype: 'fieldset',
				title: '当日新增会员',
				layout: 'hbox',
				margin: '10px',
				width: '50%',
				height: 300,
				items: [{
					border: false,
					width: '100%',
					html: '<iframe width="100%" height=260 frameborder=0 src="' + url + '/Reporting/AnalysePieChart?AnalyseType=1"></iframe>'
				}]
			}, {
				xtype: 'fieldset',
				title: '当月新增会员',
				layout: 'hbox',
				margin: '10px',
				width: '50%',
				height: 300,
				items: [{
					border: false,
					width: '100%',
					html: '<iframe width="100%" height=260 frameborder=0 src="' + url + '/Reporting/AnalysePieChart?AnalyseType=2"></iframe>'
				}]
			}]
		});

		me.membertypecharts1 = Ext.create('Ext.container.Container', {
			layout: 'hbox',
			height: 320,
			name: 'membertypechart1',
			items: [{
				xtype: 'fieldset',
				title: '普通会员（上月消费小于2千）',
				layout: 'hbox',
				margin: '10px',
				width: '50%',
				height: 300,
				items: [{
					border: false,
					width: '100%',
					html: '<iframe width="100%" height=260 frameborder=0 src="' + url + '/Reporting/AnalysePieChart?AnalyseType=3"></iframe>'
				}]
			}, {
				xtype: 'fieldset',
				title: '白银会员（上月消费2千~5千）',
				layout: 'hbox',
				margin: '10px',
				width: '50%',
				height: 300,
				items: [{
					border: false,
					width: '100%',
					html: '<iframe width="100%" height=260 frameborder=0 src="' + url + '/Reporting/AnalysePieChart?AnalyseType=4"></iframe>'
				}]
			}]
		});

		me.membertypecharts2 = Ext.create('Ext.container.Container', {
			layout: 'hbox',
			height: 320,
			name: 'membertypechart2',
			items: [{
				xtype: 'fieldset',
				title: '黄金会员（上月消费5千~1万）',
				layout: 'hbox',
				margin: '10px',
				width: '50%',
				height: 300,
				items: [{
					border: false,
					width: '100%',
					html: '<iframe width="100%" height=260 frameborder=0 src="' + url + '/Reporting/AnalysePieChart?AnalyseType=5"></iframe>'
				}]
			}, {
				xtype: 'fieldset',
				title: '铂金会员（上月消费大于1万）',
				layout: 'hbox',
				margin: '10px',
				width: '50%',
				height: 300,
				items: [{
					border: false,
					width: '100%',
					html: '<iframe width="100%" height=260 frameborder=0 src="' + url + '/Reporting/AnalysePieChart?AnalyseType=6"></iframe>'
				}]
			}]
		});

		me.bigmembercharts = Ext.create('Ext.container.Container', {
			layout: 'hbox',
			height: 320,
			name: 'bigmemberchart',
			items: [{
				xtype: 'fieldset',
				title: '普通大客户(去年消费大于1万)',
				layout: 'hbox',
				margin: '10px',
				width: '50%',
				height: 300,
				items: [{
					border: false,
					width: '100%',
					html: '<iframe width="100%" height=260 frameborder=0 src="' + url + '/Reporting/AnalysePieChart?AnalyseType=7"></iframe>'
				}]
			}, {
				xtype: 'fieldset',
				title: '土豪大客户(去年消费大于2万)',
				layout: 'hbox',
				margin: '10px',
				width: '50%',
				height: 300,
				items: [{
					border: false,
					width: '100%',
					html: '<iframe width="100%" height=260 frameborder=0 src="' + url + '/Reporting/AnalysePieChart?AnalyseType=8"></iframe>'
				}]
			}]
		});

		me.loyalmembercharts = Ext.create('Ext.container.Container', {
			layout: 'hbox',
			height: 320,
			name: 'loyalmemberchart',
			items: [{
				xtype: 'fieldset',
				title: '上月初至今消费1次（一般）',
				layout: 'hbox',
				margin: '10px',
				width: '33%',
				height: 300,
				items: [{
					border: false,
					width: '100%',
					html: '<iframe width="100%" height=260 frameborder=0 src="' + url + '/Reporting/AnalysePieChart?AnalyseType=9"></iframe>'
				}]
			}, {
				xtype: 'fieldset',
				title: '上月初至今消费2次（良好）',
				layout: 'hbox',
				margin: '10px',
				width: '33%',
				height: 300,
				items: [{
					border: false,
					width: '100%',
					html: '<iframe width="100%" height=260 frameborder=0 src="' + url + '/Reporting/AnalysePieChart?AnalyseType=10"></iframe>'
				}]
			}, {
				xtype: 'fieldset',
				title: '上月初至今消费3次以上（绝对忠诚）',
				layout: 'hbox',
				margin: '10px',
				width: '33%',
				height: 300,
				items: [{
					border: false,
					width: '100%',
					html: '<iframe width="100%" height=260 frameborder=0 src="' + url + '/Reporting/AnalysePieChart?AnalyseType=11"></iframe>'
				}]
			}]
		});

		me.losemembercharts = Ext.create('Ext.container.Container', {
			layout: 'hbox',
			height: 320,
			name: 'losememberchart',
			items: [{
				xtype: 'fieldset',
				title: '近三个月未消费',
				layout: 'hbox',
				margin: '10px',
				width: '33%',
				height: 300,
				items: [{
					border: false,
					width: '100%',
					html: '<iframe width="100%" height=260 frameborder=0 src="' + url + '/Reporting/AnalysePieChart?AnalyseType=12"></iframe>'
				}]
			}, {
				xtype: 'fieldset',
				title: '近六个月未消费',
				layout: 'hbox',
				margin: '10px',
				width: '33%',
				height: 300,
				items: [{
					border: false,
					width: '100%',
					html: '<iframe width="100%" height=260 frameborder=0 src="' + url + '/Reporting/AnalysePieChart?AnalyseType=13"></iframe>'
				}]
			}, {
				xtype: 'fieldset',
				title: '近十二个月未消费',
				layout: 'hbox',
				margin: '10px',
				width: '33%',
				height: 300,
				items: [{
					border: false,
					width: '100%',
					html: '<iframe width="100%" height=260 frameborder=0 src="' + url + '/Reporting/AnalysePieChart?AnalyseType=14"></iframe>'
				}]
			}]
		});

		me.items = [me.memberlist, me.form, me.newmembercharts, me.newmembergrid, me.membertypecharts1, me.membertypegrid1, me.membertypecharts2, me.membertypegrid2, me.bigmembercharts, me.bigmembergrid, me.loyalmembercharts, me.loyalmembergrid, me.losemembercharts, me.losemembergrid];
		me.callParent(arguments);
	},
});