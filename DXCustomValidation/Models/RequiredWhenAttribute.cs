using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace DXCustomValidation.Models
{
	 public class RequiredWhenAttribute : ValidationAttribute, IClientValidatable
	 {
		  public string DependingOnProperty { get; set; }
		  public object TargetValue { get; set; }

		  private readonly ValidationAttribute _attr = new RequiredAttribute();

		  public RequiredWhenAttribute(string dependingOnProperty, object targetValue)
		  {
				DependingOnProperty = dependingOnProperty;
				TargetValue = targetValue;
		  }

		  protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		  {
				Type containerType = validationContext.ObjectInstance.GetType();
				PropertyInfo propInfo = containerType.GetProperty(DependingOnProperty);
				if (propInfo != null)
				{
					 var dependentvalue = propInfo.GetValue(validationContext.ObjectInstance, null);
					 if ((dependentvalue == null && TargetValue == null) ||
						 (dependentvalue != null && dependentvalue.Equals(TargetValue)))
					 {
						  if (!_attr.IsValid(value))
								return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
					 }
				}
				return ValidationResult.Success;
		  }

		  public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
		  {
				ModelClientValidationRule rule = new ModelClientValidationRule()
				{
					 ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
					 ValidationType = "requiredwhen",
				};
				string propName = metadata.PropertyName + "_";
				string depProp = (context as ViewContext).ViewData.TemplateInfo.GetFullHtmlFieldId(DependingOnProperty);
				if (depProp.StartsWith(propName))
					 depProp = depProp.Substring(propName.Length);

				rule.ValidationParameters.Add("dependingonproperty", depProp);
				rule.ValidationParameters.Add("targetvalue", TargetValue);

				yield return rule;
		  }
	 }
}