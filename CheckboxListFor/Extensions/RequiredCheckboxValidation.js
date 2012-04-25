
//RequiredCheckboxAttribute
$.validator.unobtrusive.adapters.add("requiredcheckbox", ["requirednumber"], function (options) {
	options.rules["requiredcheckbox"] = options.params;
	options.messages["requiredcheckbox"] = options.message;
});

jQuery.validator.addMethod("requiredcheckbox", function (value, element, params) {

	var requirednumber = params.requirednumber;

	if ($('input[name="' + element.name + '"]:checked').length >= requirednumber)
		return true;

	return false;


});