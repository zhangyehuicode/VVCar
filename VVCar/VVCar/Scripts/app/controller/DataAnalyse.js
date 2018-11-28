Ext.define('WX.controller.DataAnalyse', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.DataAnalyseStore'],
	models: ['BaseData.DataAnalyseModel'],
	views: ['Report.DataAnalyse'],
	refs: [{
		ref: 'dataAnalyse',
		selector: 'DataAnalyse',
	}, {
		ref: 'newMemberGrid',
		selector: 'DataAnalyse container[name=newmembergrid]',
	}, {
		ref: 'newMemberChart',
		selector: 'DataAnalyse container[name=newmemberechart]',
	}, {
		ref: 'memberTypeGrid1',
		selector: 'DataAnalyse container[name=membertypegrid1]',
	}, {
		ref: 'memberTypeGrid2',
		selector: 'DataAnalyse container[name=membertypegrid2]',
	}, {
		ref: 'memberTypeChart1',
		selector: 'DataAnalyse container[name=membertypechart1]',
	}, {
		ref: 'memberTypeChart2',
		selector: 'DataAnalyse container[name=membertypechart2]',
	}, {
		ref: 'bigMemberGrid',
		selector: 'DataAnalyse container[name=bigmembergrid]',
	}, {
		ref: 'bigMemberChart',
		selector: 'DataAnalyse container[name=bigmemberchart]',
	}, {
		ref: 'loyalMemberGrid',
		selector: 'DataAnalyse container[name=loyalmembergrid]',
	}, {
		ref: 'loyalMemberChart',
		selector: 'DataAnalyse container[name=loyalmemberchart]',
	}, {
		ref: 'loseMemberGrid',
		selector: 'DataAnalyse container[name=losemembergrid]',
	}, {
		ref: 'loseMemberChart',
		selector: 'DataAnalyse container[name=losememberchart]',
	}, {
		ref: 'newMemberDay',
		selector: 'DataAnalyse grid[name=newmemberday]',
	}, {
		ref: 'newMemberDay',
		selector: 'DataAnalyse grid[name=newmemberday]',
	}, {
		ref: 'newMemberMonth',
		selector: 'DataAnalyse grid[name=newmembermonth]',
	}, {
		ref: 'memberType1',
		selector: 'DataAnalyse grid[name=membertype1]',
	}, {
		ref: 'memberType2',
		selector: 'DataAnalyse grid[name=membertype2]',
	}, {
		ref: 'memberType3',
		selector: 'DataAnalyse grid[name=membertype3]',
	}, {
		ref: 'memberType4',
		selector: 'DataAnalyse grid[name=membertype4]',
	}, {
		ref: 'bigMember1',
		selector: 'DataAnalyse grid[name=bigmember1]',
	}, {
		ref: 'bigMember2',
		selector: 'DataAnalyse grid[name=bigmember2]',
	}, {
		ref: 'loyalMember1',
		selector: 'DataAnalyse grid[name=loyalmember1]',
	}, {
		ref: 'loyalMember2',
		selector: 'DataAnalyse grid[name=loyalmember2]',
	}, {
		ref: 'loyalMember3',
		selector: 'DataAnalyse grid[name=loyalmember3]',
	}, {
		ref: 'loseMember1',
		selector: 'DataAnalyse grid[name=losemember1]',
	}, {
		ref: 'loseMember2',
		selector: 'DataAnalyse grid[name=losemember2]',
	}, {
		ref: 'loseMember3',
		selector: 'DataAnalyse grid[name=losemember3]',
	}],
	init: function () {
		var me = this;
		me.control({
			'DataAnalyse': {
				afterrender: me.afterrender
			},
			'DataAnalyse grid[name=newmemberday]': {
				afterrender: me.newmemberday
			},
			'DataAnalyse grid[name=newmembermonth]': {
				afterrender: me.newmembermonth
			},
			'DataAnalyse grid[name=membertype1]': {
				afterrender: me.membertype1
			},
			'DataAnalyse grid[name=membertype2]': {
				afterrender: me.membertype2
			},
			'DataAnalyse grid[name=membertype3]': {
				afterrender: me.membertype3
			},
			'DataAnalyse grid[name=membertype4]': {
				afterrender: me.membertype4
			},
			'DataAnalyse grid[name=bigmember1]': {
				afterrender: me.bigmember1
			},
			'DataAnalyse grid[name=bigmember2]': {
				afterrender: me.bigmember2
			},
			'DataAnalyse grid[name=loyalmember1]': {
				afterrender: me.loyalmember1
			},
			'DataAnalyse grid[name=loyalmember2]': {
				afterrender: me.loyalmember2
			},
			'DataAnalyse grid[name=loyalmember3]': {
				afterrender: me.loyalmember3
			},
			'DataAnalyse grid[name=losemember1]': {
				afterrender: me.losemember1
			},
			'DataAnalyse grid[name=losemember2]': {
				afterrender: me.losemember2
			},
			'DataAnalyse grid[name=losemember3]': {
				afterrender: me.losemember3
			},
			'DataAnalyse button[action=newmember]': {
				click: me.newmember
			},
			'DataAnalyse button[action=membertype]': {
				click: me.membertype
			},
			'DataAnalyse button[action=bigmember]': {
				click: me.bigmember
			},
			'DataAnalyse button[action=loyalmember]': {
				click: me.loyalmember
			},
			'DataAnalyse button[action=losemember]': {
				click: me.losemember
			}
		});
	},
	afterrender: function () {
		Ext.getCmp('newmemberbtn').setStyle('background-color', '#5FA2DD');
		Ext.getCmp('membertypebtn').setStyle('background-color', '#2C3845');
		Ext.getCmp('bigmemberbtn').setStyle('background-color', '#2C3845');
		Ext.getCmp('loyalmemberbtn').setStyle('background-color', '#2C3845');
		Ext.getCmp('losememberbtn').setStyle('background-color', '#2C3845'); 
	},
	newmemberday: function () {
		var me = this;
		var store = me.getNewMemberDay().getStore();
		Ext.apply(store.proxy.extraParams, {
			All: false,
			TimeSelect: '1',
		});
		store.load();
	},
	newmembermonth: function () {
		var me = this;
		var store = me.getNewMemberMonth().getStore();
		Ext.apply(store.proxy.extraParams, {
			All: false,
			TimeSelect: '2',
		});
		store.load();
	},
	membertype1: function () {
		var me = this;
		var store = me.getMemberType1().getStore();
		Ext.apply(store.proxy.extraParams, {
			All: false,
			MemberType: '1',
		});
		store.load();
	},
	membertype2: function () {
		var me = this;
		var store = me.getMemberType2().getStore();
		Ext.apply(store.proxy.extraParams, {
			All: false,
			MemberType: '2',
		});
		store.load();
	},
	membertype3: function () {
		var me = this;
		var store = me.getMemberType3().getStore();
		Ext.apply(store.proxy.extraParams, {
			All: false,
			MemberType: '3',
		});
		store.load();
	},
	membertype4: function () {
		var me = this;
		var store = me.getMemberType4().getStore();
		Ext.apply(store.proxy.extraParams, {
			All: false,
			MemberType: '4',
		});
		store.load();
	},
	bigmember1: function () {
		var me = this;
		var store = me.getBigMember1().getStore();
		Ext.apply(store.proxy.extraParams, {
			All: false,
			BigMember: '1',
		});
		store.load();
	},
	bigmember2: function () {
		var me = this;
		var store = me.getBigMember2().getStore();
		Ext.apply(store.proxy.extraParams, {
			All: false,
			BigMember: '2',
		});
		store.load();
	},
	loyalmember1: function () {
		var me = this;
		var store = me.getLoyalMember1().getStore();
		Ext.apply(store.proxy.extraParams, {
			All: false,
			LoyalMember: '1',
		});
		store.load();
	},
	loyalmember2: function () {
		var me = this;
		var store = me.getLoyalMember2().getStore();
		Ext.apply(store.proxy.extraParams, {
			All: false,
			LoyalMember: '2',
		});
		store.load();
	},
	loyalmember3: function () {
		var me = this;
		var store = me.getLoyalMember3().getStore();
		Ext.apply(store.proxy.extraParams, {
			All: false,
			LoyalMember: '3',
		});
		store.load();
	},
	losemember1: function () {
		var me = this;
		var store = me.getLoseMember1().getStore();
		Ext.apply(store.proxy.extraParams, {
			All: false,
			LoseMember: '1',
		});
		store.load();
	},
	losemember2: function () {
		var me = this;
		var store = me.getLoseMember2().getStore();
		Ext.apply(store.proxy.extraParams, {
			All: false,
			LoseMember: '2',
		});
		store.load();
	},
	losemember3: function () {
		var me = this;
		var store = me.getLoseMember3().getStore();
		Ext.apply(store.proxy.extraParams, {
			All: false,
			LoseMember: '3',
		});
		store.load();
	},
	newmember: function () {
		var me = this;
		me.getNewMemberGrid().show();
		me.getNewMemberChart().show();
		me.getMemberTypeGrid1().hide();
		me.getMemberTypeChart1().hide();
		me.getMemberTypeGrid2().hide();
		me.getMemberTypeChart2().hide();
		me.getBigMemberGrid().hide();
		me.getBigMemberChart().hide();
		me.getLoyalMemberGrid().hide();
		me.getLoyalMemberChart().hide();
		me.getLoseMemberGrid().hide();
		me.getLoseMemberChart().hide();

		Ext.getCmp('newmemberbtn').setStyle('background-color', '#5FA2DD'); 
		Ext.getCmp('membertypebtn').setStyle('background-color', '#2C3845'); 
		Ext.getCmp('bigmemberbtn').setStyle('background-color', '#2C3845'); 
		Ext.getCmp('loyalmemberbtn').setStyle('background-color', '#2C3845'); 
		Ext.getCmp('losememberbtn').setStyle('background-color', '#2C3845'); 
	},
	membertype: function () {
		var me = this;
		me.getNewMemberGrid().hide();
		me.getNewMemberChart().hide();
		me.getMemberTypeGrid1().show();
		me.getMemberTypeChart1().show();
		me.getMemberTypeGrid2().show();
		me.getMemberTypeChart2().show();
		me.getBigMemberGrid().hide();
		me.getBigMemberChart().hide();
		me.getLoyalMemberGrid().hide();
		me.getLoyalMemberChart().hide();
		me.getLoseMemberGrid().hide();
		me.getLoseMemberChart().hide();

		Ext.getCmp('newmemberbtn').setStyle('background-color', '#2C3845');
		Ext.getCmp('membertypebtn').setStyle('background-color', '#5FA2DD');
		Ext.getCmp('bigmemberbtn').setStyle('background-color', '#2C3845');
		Ext.getCmp('loyalmemberbtn').setStyle('background-color', '#2C3845');
		Ext.getCmp('losememberbtn').setStyle('background-color', '#2C3845'); 
	},
	bigmember: function () {
		var me = this;
		me.getNewMemberGrid().hide();
		me.getNewMemberChart().hide();
		me.getMemberTypeGrid1().hide();
		me.getMemberTypeChart1().hide();
		me.getMemberTypeGrid2().hide();
		me.getMemberTypeChart2().hide();
		me.getBigMemberGrid().show();
		me.getBigMemberChart().show();
		me.getLoyalMemberGrid().hide();
		me.getLoyalMemberChart().hide();
		me.getLoseMemberGrid().hide();
		me.getLoseMemberChart().hide();

		Ext.getCmp('newmemberbtn').setStyle('background-color', '#2C3845');
		Ext.getCmp('membertypebtn').setStyle('background-color', '#2C3845');
		Ext.getCmp('bigmemberbtn').setStyle('background-color', '#5FA2DD');
		Ext.getCmp('loyalmemberbtn').setStyle('background-color', '#2C3845');
		Ext.getCmp('losememberbtn').setStyle('background-color', '#2C3845'); 
	},
	loyalmember: function () {
		var me = this;
		me.getNewMemberGrid().hide();
		me.getNewMemberChart().hide();
		me.getMemberTypeGrid1().hide();
		me.getMemberTypeChart1().hide();
		me.getMemberTypeGrid2().hide();
		me.getMemberTypeChart2().hide();
		me.getBigMemberGrid().hide();
		me.getBigMemberChart().hide();
		me.getLoyalMemberGrid().show();
		me.getLoyalMemberChart().show();
		me.getLoseMemberGrid().hide();
		me.getLoseMemberChart().hide();

		Ext.getCmp('newmemberbtn').setStyle('background-color', '#2C3845');
		Ext.getCmp('membertypebtn').setStyle('background-color', '#2C3845');
		Ext.getCmp('bigmemberbtn').setStyle('background-color', '#2C3845');
		Ext.getCmp('loyalmemberbtn').setStyle('background-color', '#5FA2DD');
		Ext.getCmp('losememberbtn').setStyle('background-color', '#2C3845'); 
	},
	losemember: function () {
		var me = this;
		me.getNewMemberGrid().hide();
		me.getNewMemberChart().hide();
		me.getMemberTypeGrid1().hide();
		me.getMemberTypeChart1().hide();
		me.getMemberTypeGrid2().hide();
		me.getMemberTypeChart2().hide();
		me.getBigMemberGrid().hide();
		me.getBigMemberChart().hide();
		me.getLoyalMemberGrid().hide();
		me.getLoyalMemberChart().hide();
		me.getLoseMemberGrid().show();
		me.getLoseMemberChart().show();

		Ext.getCmp('newmemberbtn').setStyle('background-color', '#2C3845');
		Ext.getCmp('membertypebtn').setStyle('background-color', '#2C3845');
		Ext.getCmp('bigmemberbtn').setStyle('background-color', '#2C3845');
		Ext.getCmp('loyalmemberbtn').setStyle('background-color', '#2C3845');
		Ext.getCmp('losememberbtn').setStyle('background-color', '#5FA2DD'); 
	},
})