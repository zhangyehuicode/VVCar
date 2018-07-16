Ext.define('WX.view.Shop.HtmlEditorImage', {
	extend: 'Ext.util.Observable',
	alias: 'HtmlEditorImage',
	langTitle: '插入图片',
	langIconCls: 'x-fa fa-file-picture-o',
	init: function (view) {
		var scope = this;
		view.on('render', function () {
			scope.onRender(view);
		});
	},
	onRender: function (view) {
		var scope = this;
		view.getToolbar().add(1, {
			iconCls: scope.langIconCls,
			tooltip: {
				title: '插入图片',

				text: '选择插图',
				cls: Ext.baseCSSPrefix + 'html-editor-tip'
			},
			handler: function () {
				scope.showImgWindow(view);
			}
		});
	},
	showImgWindow: function (view) {
		var scope = this;
		Ext.create('Ext.window.Window', {
			width: 300,
			height: 140,
			title: scope.langTitle,
			layout: 'fit',
			autoShow: true,
			modal: true,
			resizable: false,
			constrain: true,
			plain: true,
			enableTabScroll: true,
			border: false,
			items: [{
				xtype: 'form',
				layout: 'column',
				autoScroll: true,
				border: false,
				defaults: {
					columnWidth: 1,
					labelWidth: 70,
					labelAlign: 'right',
					padding: '5 5 5 5',
					allowBlank: false
				},
				items: [{
					xtype: 'fileuploadfield',
					fieldLabel: '选择文件',
					buttonText: '请选择...',
					name: 'upload',
					emptyText: '请选择图片',
					blankText: '图片不能为空',
					listeners: {
						change: function (view, value, eOpts) {
							scope.uploadImgCheck(view, value);
						}
					}
				}],
				buttons: [{
					text: '上传',
					action: 'btn_save',
					handler: function (btn) {
						scope.saveUploadImg(btn, view);
					}
				}, {
					text: '取消',
					handler: function (btn) {
						btn.up('window').close();
					}
				}]
			}]
		})
	},
	uploadImgCheck: function (fileObj, fileName) {
		var scope = this;
		var typestr = 'jpg,jpeg,png,gif';
		var isImg = false;
		var index = fileName.lastIndexOf('.');
		if (index != -1) {
			var tag = fileName.substr(index + 1).toLowerCase();
			var types = typestr.split(',');
			for (var i = 0; i < types.length; i++) {
				if (tag == types[i]) {
					isImg = true;
					return;
				}
			}
		}
		if (!isImg) {
			Ext.Msg.alert('提示', '上传的图片格式有错');
			return;
		}
	},
	saveUploadImg: function (btn, view) {
		var scope = this;
		var windowObj = btn.up('window');
		var formObj = btn.up('form');
		if (formObj.isValid()) {
			formObj.form.doAction('submit', {
				url: Ext.GlobalConfig.ApiDomainUrl + 'api/UploadFile/UploadGraphicIntroduction',
				method: 'POST',
				submitEmptyText: false,
				waitMsg: '正在上传图片,请稍候...',
				timeout: 60000,
				success: function (response, options) {
					var result = options.result;
					if (!result.success) {
						Ext.MessageBox.alert('温馨提示', result.errorMessage);
						return;
					}
					scope.insertImg(view, result);
					windowObj.close();
				},
				failure: function (response, options) {
					Ext.MessageBox.alert('温馨提示', options.result.errorMessage);
				}
			});
		}
	},
	insertImg: function (view, data) {
		var url = data.FileUrl;
		var str = '<img src="' + url + '" width="200" height="100" border="0"/>';
		view.insertAtCursor(str);
	},
	//uploadImage: function (id, url) {
	//	if (id == null || id == '' || url == null || url == '')
	//		return;
	//	$('#' + id).fileupload({
	//		dropZone: $('#' + id),
	//		pasteZone: $('#' + id),
	//		dataType: 'json',
	//		autoUpload: true,
	//		url: url,
	//		add: function (e, data) {
	//			var uploadErrors = [];
	//			var acceptFileTypes = /(gif|jpe?g|png|bmp)$/i;
	//			if (!acceptFileTypes.test(data.originalFiles[0]['name'])) {
	//				uploadErrors.push('不允许的文件类型');
	//			}
	//			if (uploadErrors.length > 0) {
	//				$.alert(uploadErrors.join(", "));
	//			} else {
	//				data.submit();
	//			}
	//		},
	//		send: function (e, data) {
	//			$.showLoading();
	//		},
	//		done: function (e, data) {
	//			$.hideLoading();
	//			if (data.result.success == true) {
	//				$('#' + id).find('.uploadimg').attr('src', data.result.FileUrl);
	//			} else {
	//				$.alert(data.result.errorMessage);
	//			}
	//		}
	//	});
	//}
})