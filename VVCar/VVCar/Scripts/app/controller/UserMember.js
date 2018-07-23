Ext.define('WX.controller.UserMember', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.UserStore', 'WX.store.BaseData.UserMemberStore'],
	models: ['BaseData.UserModel', 'BaseData.UserMemberModel'],
	views: ['UserMember.UserMember', 'UserMember.UserMemberSelector'],
	refs: [{
		ref: 'gridUser',
		selector: 'UserMember grid[name=gridUser]'
	}, {
		ref: 'gridUserMember',
		selector: 'UserMember grid[name=gridUserMember]'
	},
	{
		ref: 'gridUserMemberSelector',
		selector: 'UserMemberSelector grid[name=userMemberList]'
	}],
	init: function () {
		var me = this;
		me.control({
			'UserMember grid[name=gridUser]': {
				select: me.onGridUserSelect,
			},
			'UserMember button[action=addMember]': {
				click: me.addMember
			},
			'UserMember button[action=deleteMember]': {
				click: me.deleteMember
			},
			'UserMemberSelector button[action=save]': {
				click: me.save
			},
			'UserMemberSelector button[action=search]': {
				click: me.search
			}
		});
	},
	onGridUserSelect: function (grid, record, index, eOpts) {
		var store = this.getGridUserMember().getStore();
		Ext.apply(store.proxy.extraParams, {
			All: false,
			UserID: record.data.ID,
		});
		store.reload();
	},
	addMember: function (button) {
		var me = this;
		var selectedUsers = me.getGridUser().getSelectionModel().getSelection();
		if (selectedUsers.length < 1) {
			Ext.Msg.alert("提示", "请先选择用户");
			return;
		}
		var win = Ext.widget("UserMemberSelector");
		win.show();
	},
	deleteMember: function (button) {
		var me = this;
		var selectedItems = me.getGridUserMember().getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert("提示", "请选择需要删除关联的会员数据");
			return;
		}
		var userMemberIds = [];
		for (var i = 0; i < selectedItems.length; i++) {
			userMemberIds.push(selectedItems[i].data.ID);
		}
		var store = me.getGridUserMember().getStore();
		store.batchDelete(userMemberIds, function (response, opts) {
			var ajaxResult = JSON.parse(response.responseText);
			if (ajaxResult.Data == false) {
				Ext.Msg.alert("操作失败", ajaxResult.ErrorMessage);
				return;
			} else {
				store.reload();
			}
		}, function (response, opts) {
			var ajaxResult = JSON.parse(response.responseText);
			Ext.Msg.alert("操作失败", ajaxResult.ErrorMessage);
		});
	},
	save: function (btn) {
		var me = this;
		var win = me.getGridUserMember();

		var selectedMemberItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedMemberItems.length < 1) {
			Ext.Msg.alert('提示', '未选择会员');
			return;
		}

		var selectedUserItems = me.getGridUser().getSelectionModel().getSelection();
		if (selectedUserItems.length != 1) {
			Ext.Msg.alert('提示', '参数错误');
			return;
		}
		var userId = selectedUserItems[0].data.ID;

		var userMember = [];
		selectedMemberItems.forEach(function (item) {
			userMember.push({ UserID: userId, MemberID: item.data.ID });
		});
		var store = win.getStore();
		store.batchAdd(userMember, function (response, opts) {
			var ajaxResult = JSON.parse(response.responseText);
			if (ajaxResult.Data == true) {
				Ext.Msg.alert('提示', '新增成功');
				btn.up('grid').getStore().reload();
				win.close();
				store.reload();
				return;
			} else {
				Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
				return;
			}
			store.reload();
		}, function (response, opts) {
			var ajaxResult = JSON.parse(response.responseText);
			Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
		});
	},
	search: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			queryValues.ProductType = 0;
			var store = me.getGridUserMemberSelector().getStore();
			store.load({ params: queryValues });
		} else {
			Ext.Msg.alert('提示', '请输入过滤条件');
		}
	}
});
