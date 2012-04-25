using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CheckboxListFor.Extensions;

namespace CheckboxListFor.Models
{
	public enum MyType
	{
		CSharp,
		VB,
		Java,
        Ruby,
        CPlus
	}
	
	
	public class MyModel
	{
		      
        [Required(ErrorMessage="Please provide your name.")]
        public string Name { get; set; }

        [RequiredCheckboxAttribute(2)]
		public IList<MyType> SelectedLanguages { get; set; }
		public Dictionary<MyType, string> LanguageOptions { get; set; }

        [RequiredCheckboxAttribute(1)]
        public IList<int> SelectedNumbers { get; set; }
        public Dictionary<int, string> NumberOptions { get; set; }
	}
}