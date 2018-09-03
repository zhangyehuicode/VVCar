Ext.define('WX.view.AgentDepartment.AgentDepartmentEdit', {
    extend: 'Ext.window.Window',
    alias: 'widget.AgentDepartmentEdit',
    title: '编辑代理商门店信息',
    //layout: 'fit',
    width: 1150,
    height: 700,
    bodyPadding: 5,
    modal: true,
    autoScoll: true,
    bodyStyle: 'overflow-y: auto; overflow-x: hidden;',
    initComponent: function () {
        var me = this;
        var yesNoDictStore = Ext.create('WX.store.DataDict.YesNoTypeStore');
        me.form = Ext.create('Ext.form.Panel', {
            border: false,
            trackResetOnLoad: true,
            width: 1100,
            fieldDefaults: {
                labelAlign: 'left',
                labelWidth: 80,
                anchor: '100%',
                flex: 1,
                margin: '5',
            },
            items: [
                {
                    xtype: 'textfield',
                    margin: '5 10 5 5',
                    fieldLabel: '分类ID',
                    name: 'AgentDepartmentCategoryID',
                    allowBlank: true,
                    hidden: true,
                }, {
                    xtype: 'form',
                    layout: 'hbox',
                    items: [{
                        xtype: 'textfield',
                        margin: '5 10 5 5',
                        name: 'Name',
                        fieldLabel: '名称',
                        maxLength: 100,
                        allowBlank: false,
                    }, {
                        xtype: 'form',
                        layout: 'hbox',
                        margin: '5 0 0 0',
                        items: [{
                            xtype: 'combobox',
                            margin: '5 5 5 10',
                            fieldLabel: '客户类型',
                            name: 'Type',
                            editable: false,
                            allowBlank: false,
                            store: [
                                [0, '开发客户'],
                                [1, '意向客户'],
                            ]
                        }, {
                            xtype: 'textfield',
                            margin: '5 5 5 10',
                            name: 'Email',
                            fieldLabel: '注册邮箱',
                            maxLength: 25,
                            allowBlank: true,
                        }]
                    }]
                }, {
                    xtype: 'form',
                    layout: 'hbox',
                    items: [{
                        xtype: 'textfield',
                        margin: '5 10 5 5',
                        name: 'LegalPerson',
                        fieldLabel: '法人(负责人)',
                        maxLength: 20,
                        allowBlank: false,

                    }, {
                        xtype: 'textfield',
                        margin: '5 5 5 10',
                        name: 'IDNumber',
                        fieldLabel: '身份证编号',
                        vtype: 'IDNumber',
                        maxLength: 18,
                        allowBlank: true,

                    }]
                }, {
                    xtype: 'form',
                    layout: 'hbox',
                    items: [{
                        xtype: 'textfield',
                        margin: '5 10 5 5',
                        name: 'MobilePhoneNo',
                        fieldLabel: '手机号码',
                        vtype: 'mobilephone',
                        maxLength: 11,
                        allowBlank: false,

                    }, {
                        xtype: 'textfield',
                        margin: '5 5 5 10',
                        name: 'CompanyAddress',
                        fieldLabel: '公司地址',
                        maxLength: 50,
                        allowBlank: false,

                    }]
                }, {
                    xtype: 'form',
                    layout: 'hbox',
                    items: [{
                        xtype: 'textfield',
                        margin: '5 10 5 5',
                        name: 'WeChatAppID',
                        fieldLabel: '公众号AppID',
                        maxLength: 50,
						allowBlank: true,
						hidden: true,
                    }, {
                        xtype: 'textfield',
                        margin: '5 5 5 10',
                        name: 'WeChatAppSecret',
                        fieldLabel: '公众号Secret',
                        maxLength: 50,
						allowBlank: true,
						hidden: true,
                    }]
                }, {
                    xtype: 'form',
                    layout: 'hbox',
                    items: [{
                        xtype: 'textfield',
                        margin: '5 10 5 5',
                        name: 'WeChatMchID',
                        fieldLabel: '微信商户号',
                        maxLength: 20,
						allowBlank: true,
						hidden: true,
                    }, {
                        xtype: 'textfield',
                        margin: '5 8 5 10',
                        name: 'WeChatMchKey',
                        fieldLabel: '微信商户Key',
                        maxLength: 50,
						allowBlank: true,
						hidden: true,
                    }]
                }, {
                    xtype: 'form',
                    layout: 'hbox',
                    items: [{
                        xtype: 'textfield',
                        margin: '5 10 5 5',
                        name: 'WeChatOAPassword',
                        fieldLabel: '微信公众平台登录密码',
                        emptyText: '微信公众平台登录密码',
						maxLength: 20,
						hidden: true,
                    }, {
                        xtype: 'textfield',
                        margin: '5 5 5 10',
                        name: 'MeChatMchPassword',
                        fieldLabel: '微信商户平台操作密码',
                        emptyText: '微信商户平台操作密码',
						maxLength: 20,
						hidden: true,
                    }]
                }, {
                    xtype: 'form',
                    layout: 'hbox',
                    items: [{
                        xtype: 'textfield',
                        margin: '5 10 5 5',
                        name: 'Bank',
                        fieldLabel: '开户行',
                        maxLength: 20,
                        allowBlank: true,
                    }, {
                        xtype: 'form',
                        layout: 'hbox',
                        items: [{
                            xtype: 'textfield',
                            margin: '5 12 5 10',
                            name: 'BankCard',
                            fieldLabel: '银行账号',
                            maxLength: 32,
                            allowBlank: true,
                        }, {
                            xtype: 'textfield',
                            name: 'UserName',
                            fieldLabel: '销售经理',
                            readOnly: true,
                            width: 190,
                            allowBlank: true,
                            permissionCode: 'AgentDepartment.SelectManager',
                        }, {
                            action: 'selectManageUser',
                            xtype: 'button',
                            text: '查找',
                            cls: 'submitBtn',
                            margin: '5 5 5 10',
                            permissionCode: 'AgentDepartment.SelectManager',
                        }, {
                            xtype: 'textfield',
                            name: 'UserID',
                            fieldLabel: '用户ID',
                            hidden: true,
                        }]
                    }]
                }, {
                    xtype: 'form',
                    layout: 'hbox',
                    items: [{
                        xtype: 'form',
                        border: false,
                        layout: 'hbox',
                        items: [{
                            xtype: 'filefield',
                            fieldLabel: '营业执照',
                            labelWidth: 30,
                            width: 200,
                            allowBlank: true,
                            buttonText: '选择图片',
                        }, {
                            xtype: 'button',
                            text: '上传',
                            margin: '10 10 0 0',
                            action: 'uploadLicensePic',
                        }]
                    }, {
                        xtype: 'form',
                        border: false,
                        layout: 'hbox',
                        items: [{
                            xtype: 'filefield',
                            fieldLabel: '门店照片',
                            labelWidth: 30,
                            width: 200,
                            allowBlank: true,
                            buttonText: '选择图片',
                        }, {
                            xtype: 'button',
                            text: '上传',
                            margin: '10 10 0 0',
                            action: 'uploadDepartmentPic',
                        }]
                    }, {
                        xtype: 'form',
                        border: false,
                        layout: 'hbox',
                        items: [{
                            xtype: 'filefield',
                            fieldLabel: '身份证正面',
                            labelWidth: 40,
                            width: 200,
                            allowBlank: true,
                            buttonText: '选择图片',
                        }, {
                            xtype: 'button',
                            text: '上传',
                            margin: '10 10 0 0',
                            action: 'uploadIDCardFrontPic',
                        }]
                    }, {
                        xtype: 'form',
                        border: false,
                        layout: 'hbox',
                        items: [{
                            xtype: 'filefield',
                            fieldLabel: '身份证反面',
                            labelWidth: 40,
                            width: 200,
                            allowBlank: true,
                            buttonText: '选择图片',
                        }, {
                            xtype: 'button',
                            text: '上传',
                            margin: '10 10 0 0',
                            action: 'uploadIDCardBehindPic',
                        }]
                    }]
                }, {
                    xtype: 'form',
                    layout: 'hbox',
                    items: [{
                        xtype: 'form',
                        layout: 'hbox',
                        items: [{
                            xtype: 'box',
                            name: 'ImgLicenseShow',
                            width: 200,
                            height: 290,
                            margin: '5 5 5 50',
                            autoEl: {
                                tag: 'img',
                                src: '',
                            },
                        }, {
                            xtype: 'box',
                            name: 'ImgDepartmentShow',
                            width: 200,
                            height: 290,
                            margin: '5 5 5 50',
                            autoEl: {
                                tag: 'img',
                                src: '',
                            },
                        }]
                    }, {
                        xtype: 'box',
                        name: 'ImgIDCardFrontShow',
                        width: 200,
                        height: 290,
                        margin: '5 5 5 80',
                        autoEl: {
                            tag: 'img',
                            src: '',
                        }
                    }, {
                        xtype: 'box',
                        name: 'ImgIDCardBehindShow',
                        width: 200,
                        height: 290,
                        margin: '5 5 5 50',
                        autoEl: {
                            tag: 'img',
                            src: '',
                        }
                    }]
                }, {
                    xtype: 'textfield',
                    name: 'BusinessLicenseImgUrl',
                    fieldLabel: '营业执照图片路径',
                    hidden: true,
                }, {
                    xtype: 'textfield',
                    name: 'DepartmentImgUrl',
                    fieldLabel: '门店执照图片路径',
                    hidden: true,
                }, {
                    xtype: 'textfield',
                    name: 'LegalPersonIDCardFrontImgUrl',
                    fieldLabel: '法人身份证正面图片路径',
                    hidden: true,
                }, {
                    xtype: 'textfield',
                    name: 'LegalPersonIDCardBehindImgUrl',
                    fieldLabel: '法人身份证背面图片路径',
                    hidden: true,
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