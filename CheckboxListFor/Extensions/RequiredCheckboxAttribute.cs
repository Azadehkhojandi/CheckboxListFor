using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CheckboxListFor.Extensions
{
	[AttributeUsage(AttributeTargets.Property, Inherited = true)]
	public class RequiredCheckboxAttribute : ValidationAttribute, IClientValidatable
		{
			public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
			{
				var rule = new ModelClientValidationRule
				{
					ErrorMessage = FormatErrorMessage(metadata.DisplayName),
					ValidationType = "requiredcheckbox"
				};

				rule.ValidationParameters.Add("requirednumber", RequiredNumber);

				yield return rule;
			}

			public int RequiredNumber { get; private set; }

			public RequiredCheckboxAttribute(int requiredNumber)
			{
				if (requiredNumber <= 0)
				{
					throw new ArgumentNullException("requiredNumber");
				}

				RequiredNumber = requiredNumber;
			}

			public override string FormatErrorMessage(string name)
			{
				return ErrorMessage ?? string.Format("Select at least {0} of provided checkboxes", RequiredNumber);
			}

			protected override ValidationResult IsValid(object value, ValidationContext validationContext)
			{
				if (value == null)
					return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

				var selected = value as IEnumerable;

				if (selected == null)
					return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

				var count = selected.Cast<object>().Count();
				return count >= RequiredNumber ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
			}
		
	}
}