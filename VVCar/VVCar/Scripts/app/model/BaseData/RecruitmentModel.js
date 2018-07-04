Ext.define('WX.model.BaseData.RecruitmentModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'Recruiter' },
		{ name: 'Position' },
		{ name: 'StartDate' },
		{ name: 'EndDate' },
		{ name: 'HRName' },
		{ name: 'HRPhoneNo' },
		{ name: 'DegreeType' },
		{ name: 'Sex' },
		{ name: 'Address' },
		{ name: 'WorkTime' },
		{ name: 'RecruitNumber' },
		{ name: 'Requirement' },
	]
});