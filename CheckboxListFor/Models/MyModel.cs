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
		Type1,
		Type2,
		Type3
	}
	
	
	public class MyModel
	{
		public MyModel()
		{
			ListofDisplayCaptionsOfMyType = new Dictionary<MyType, string>()
			                                	{
													{MyType.Type1, "C# .Net"},
			                                		{MyType.Type2, "VB .Net"},
			                                		{MyType.Type3, "JQurey"}
			                                	};
            ListofDisplayCaptionsOfInt = new Dictionary<int, string>()
			                                	{
													{1, "One"},
			                                		{2, "Two"},
			                                		{3, "Three"},
                                                    {4, "Four"},
                                                    {5, "Five"}
			                                	};
		}
        
        [Required(ErrorMessage="Please provide your name.")]
        public string Name { get; set; }

        [RequiredCheckboxAttribute(1)]
		public IList<MyType> ListofSelectedMyType { get; set; }
		public Dictionary<MyType, string> ListofDisplayCaptionsOfMyType { get; set; }

        [RequiredCheckboxAttribute(1)]
        public IList<int> ListofSelectedInt { get; set; }
        public Dictionary<int, string> ListofDisplayCaptionsOfInt { get; set; }
	}
}