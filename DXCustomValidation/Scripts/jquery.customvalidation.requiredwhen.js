(function ($) {
	 "use strict";
	 $.validator.addMethod("requiredwhen",
				function (value, element, parameters) {
					 var depCtrl = parameters["dependingonproperty"];
					 var targetVal = parameters["targetvalue"];
					 if ((typeof targetVal === "undefined") || (targetVal === null)) {
					 	 targetVal = "";
					 }
					 //...
					 //check if we're dealing with DX controls :-)
					 var actualVal = null;
					 if ((typeof ASPxClientControl !== "undefined") && (ASPxClientControl.GetControlCollection().Get(depCtrl) !== "undefined")) {
					 	 var dxCtrl = ASPxClientControl.GetControlCollection().Get(depCtrl);
					 	 if (typeof dxCtrl !== "undefined") {
					 	 	 actualVal = (typeof dxCtrl.GetChecked !== "undefined") ? dxCtrl.GetChecked() : dxCtrl.GetValue();
					 	 }
					 }
					 else {
					 	 var control = $("#" + depCtrl);
					 	 var controlType = control.attr("type");
					 	 actualVal = control.val().toString();
					 	 if (controlType === "checkbox") {
					 	 	 actualVal = (typeof control.prop !== "undefined") ? control.prop("checked").toString() : control.attr("checked");
					 	 }
					 }
					 if ((typeof actualVal === "undefined") || (actualVal === null)) {
					 	 actualVal = "";
					 }
					 if (targetVal.toLowerCase() === actualVal.toString().toLowerCase()) {
					 	 return $.validator.methods["required"].call(this, value, element, parameters);
					 }
					 return true;
				});
	 $.validator.unobtrusive.adapters.add("requiredwhen", ["dependingonproperty", "targetvalue"],
			  function (options) {
			  	 options.rules["requiredwhen"] = {
			  	 	 dependingonproperty: options.params["dependingonproperty"],
			  	 	 targetvalue: options.params["targetvalue"]
			  	 };
			  	 options.messages["requiredwhen"] = options.message;
			  });
})(jQuery);
