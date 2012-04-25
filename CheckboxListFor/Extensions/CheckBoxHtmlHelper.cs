using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace CheckboxListFor.Extensions
{
    public static class CheckBoxHtmlHelper
    {

		
		public static MvcHtmlString CheckBoxListFor<TModel, TOption>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, IEnumerable<TOption>>> expression, Expression<Func<TModel, Dictionary<TOption, string>>> listOfOptionsExpression = null, IDictionary<string, object> htmlAttributes = null)

        {

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            var unobtrusiveValidationAttributes = htmlHelper.GetUnobtrusiveValidationAttributes(metadata.PropertyName, metadata);

            var html = new TagBuilder("ul");

			html.MergeAttributes(htmlAttributes);
            string innerhtml = "";

            var model = metadata.Model as IEnumerable<TOption>;
                        
            Dictionary<TOption, string> listOfOptions=null;
            if (typeof(TOption).BaseType == typeof(Enum) && listOfOptionsExpression == null)
            {
                listOfOptions = Enum.GetValues(typeof(TOption)).Cast<TOption>().ToDictionary(t => (TOption)t, t => t.ToString());
            }
            else
            {
                ModelMetadata metadatalistOfOptions = ModelMetadata.FromLambdaExpression(listOfOptionsExpression, htmlHelper.ViewData);
                listOfOptions = metadatalistOfOptions.Model as Dictionary<TOption, string>;
            }

            if (!listOfOptions.Any())
                throw new Exception("Your option type can be Enum or you need to pass dictionary of list of options");

            foreach (var item in listOfOptions)
            {

                bool ischecked = (model == null) ? false : model.Any(x => x.ToString() == item.Key.ToString());

                var liBuilder = new TagBuilder("li");

                var inputBuilder = new TagBuilder("input");

                inputBuilder.MergeAttribute("type", "checkbox");

                inputBuilder.MergeAttribute("name", metadata.PropertyName, true);

                inputBuilder.MergeAttribute("id", item.Key.ToString(), true);

                inputBuilder.MergeAttribute("value", item.Key.ToString(), true);

                inputBuilder.MergeAttributes(unobtrusiveValidationAttributes);

                if (ischecked)

                {

                    inputBuilder.MergeAttribute("checked", "'checked'");

                }

                liBuilder.InnerHtml = inputBuilder.ToString() + htmlHelper.Label(metadata.PropertyName + "." + item.Key.ToString(),item.Value.ToString() );

                innerhtml = innerhtml + liBuilder;

            }

            html.InnerHtml = innerhtml;

            return new MvcHtmlString(html.ToString());

        }



//        //public static MvcHtmlString CheckBoxListFor<TModel, T>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, T>> expression, IEnumerable<T> itemCollection = null, object htmlAttributes = null)
//        //{
//        //    return CheckBoxListFor(htmlHelper, expression, itemCollection, htmlAttributes != null ? HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes) : null);
//        //}
//        //private static MvcHtmlString CheckBoxListFor<TModel, T>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, T>> expression, IEnumerable<T> itemCollection = null, IDictionary<string, object> htmlAttributes = null)
//        //{
//        //    ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
//        //    var unobtrusiveValidationAttributes = htmlHelper.GetUnobtrusiveValidationAttributes(metadata.PropertyName, metadata);

//        //    var html = new TagBuilder("ul");
//        //    html.MergeAttributes(htmlAttributes);

//        //    string innerhtml = "";
//        //    if (itemCollection == null)
//        //    {
//        //        if (typeof(T) == typeof(bool))
//        //        {
//        //            itemCollection = (IEnumerable<T>)new List<bool> { true, false };
//        //        }
//        //        else if (typeof(T).BaseType == typeof(Enum))
//        //        {
//        //            itemCollection = Enum.GetValues(typeof(T)).Cast<T>();

//        //        }
//        //        else
//        //        {
//        //            throw new Exception("Please set itemCollection");
//        //        }

//        //    }
//        //    var name = ExpressionHelper.GetExpressionText(expression);
//        //    string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
//        //    var model = metadata.Model as IEnumerable<T>;
//        //    T item;
//        //    foreach (T tempitem in itemCollection)
//        //    {
//        //        item = tempitem;
//        //        bool ischecked = (model == null) ? false : model.Any(x => x.ToString() == item.ToString());
//        //        var itemId = fullName + "_" + item;
//        //        var liBuilder = new TagBuilder("li") { InnerHtml = htmlHelper.Label(fullName, item.ToString()).ToString() + htmlHelper.CheckBox(fullName, ischecked, new { @id = itemId, @checked = ischecked }).ToString() };

//        //        innerhtml = innerhtml + liBuilder;

//        //    }
//        //    html.InnerHtml = innerhtml;
//        //    return new MvcHtmlString(html.ToString());


//        //}

//        public static MvcHtmlString RadioButtonListFor<TModel, T>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, T>> expression, string ulClass = null)
//        {

//            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);



//            var name = ExpressionHelper.GetExpressionText(expression);

//            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

//            var html = new TagBuilder("ul");

//            if (!String.IsNullOrEmpty(ulClass))

//                html.MergeAttribute("class", ulClass);

//            string innerhtml = "";

//            Dictionary<string, T> myEnumDic = null;

//            Dictionary<string, bool> myBoolDic = null;

//            //

//            if (typeof(T).BaseType == typeof(Enum))
//            {

//                myEnumDic = Enum.GetValues(typeof(T)).Cast<T>().ToDictionary(currentItem => Enum.GetName(typeof(T), currentItem));

//                innerhtml = RadioRow<TModel, T>(htmlHelper, fullName, innerhtml, myEnumDic);

//            }

//            else if (typeof(T) == typeof(bool))
//            {

//                myBoolDic = new Dictionary<string, bool>();

//                myBoolDic.Add("Yes", true);

//                myBoolDic.Add("No", false);

//                innerhtml = RadioRow<TModel, bool>(htmlHelper, fullName, innerhtml, myBoolDic);

//            }

//            html.InnerHtml = htmlHelper.Label(fullName).ToString() + innerhtml;

//            return new MvcHtmlString(html.ToString());

//        }

//        private static string RadioRow<TModel, T>(HtmlHelper<TModel> htmlHelper, string fullName, string innerhtml, Dictionary<string, T> myDic)
//        {

//            foreach (var item in myDic)
//            {

//                var liBuilder = new TagBuilder("li");

//                liBuilder.InnerHtml = item.Key + " " + htmlHelper.RadioButton(fullName, item.Value).ToString();

//                innerhtml = innerhtml + liBuilder;

//            }

//            return innerhtml;

//        }

//        //public static MvcHtmlString CheckBoxListFor<TModel, T>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, T>> selectedValueExpression, Expression<Func<TModel, Dictionary<string, T>>> listOfOptionsExpression = null, string ulClass = null)
//        //{


//        //    return CheckBoxListFor(htmlHelper, selectedValueExpression, listOfOptionsExpression, ulClass);
//        //}
		
//        //public static MvcHtmlString CheckBoxListFor<TModel, T>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, IEnumerable<T>>> selectedValuesExpression, Expression<Func<TModel, Dictionary<string, T>>> listOfOptionsExpression = null, string ulClass = null)
//        //{

//        //    var metadataselectedValues = ModelMetadata.FromLambdaExpression(selectedValuesExpression, htmlHelper.ViewData);
//        //    var unobtrusiveValidationAttributes = htmlHelper.GetUnobtrusiveValidationAttributes(metadataselectedValues.PropertyName, metadataselectedValues);
//        //    var selectedValues = metadataselectedValues.Model as IEnumerable<T>;

//        //    return DDDD(htmlHelper, metadataselectedValues, ulClass, listOfOptionsExpression, selectedValues, unobtrusiveValidationAttributes);
//        //}
//        public static MvcHtmlString CheckBoxListFor<TModel, T>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, IEnumerable<T>>> selectedValueExpression, Expression<Func<TModel, Dictionary<string, T>>> listOfOptionsExpression = null, string ulClass = null)
//        {
//            var metadataselectedValue = ModelMetadata.FromLambdaExpression(selectedValueExpression, htmlHelper.ViewData);
//            var unobtrusiveValidationAttributes = htmlHelper.GetUnobtrusiveValidationAttributes(metadataselectedValue.PropertyName, metadataselectedValue);

//            //if ((typeof(T)).GetInterfaces().Any(x => x.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
//            //{
//            //    Type myType = (typeof(T)).GetGenericArguments()[0];
//            //    Type genericListType = typeof(IEnumerable<>).MakeGenericType(myType);
//            //    var selectedlist = (IList)Activator.CreateInstance(genericListType);

//            //    var enumerator = ((IEnumerable)metadataselectedValue.Model).GetEnumerator();
//            //    while (enumerator.MoveNext())
//            //    {
//            //        selectedlist.Add(enumerator.Current);
//            //    }

//            //    var selectedValues = selectedlist;
//            //    return DDDD(htmlHelper, metadataselectedValue, ulClass, null, selectedValues, unobtrusiveValidationAttributes);
//            //}
//            //else
//            //{
//                var selectedValues = new List<T> { (T)metadataselectedValue.Model };
//                return DDDD(htmlHelper, metadataselectedValue, ulClass, listOfOptionsExpression, selectedValues, unobtrusiveValidationAttributes);
//            //}



//        }

//        private static MvcHtmlString DDDD<TModel, T>(HtmlHelper<TModel> htmlHelper, ModelMetadata metadataselectedValues, string ulClass, Expression<Func<TModel, Dictionary<string, T>>> listOfOptionsExpression, IEnumerable<T> selectedValues, IDictionary<string, object> unobtrusiveValidationAttributes)
//        {
//            var html = new TagBuilder("ul");

//            if (!String.IsNullOrEmpty(ulClass))
//                html.MergeAttribute("class", ulClass);

//            string innerhtml = "";
			
//            Dictionary<string, T> listOfOptions = null;

//            if (typeof(T).BaseType != typeof(Enum) && listOfOptionsExpression == null)
//            {

//                throw new Exception("please provide list of possible checkboxes");

//            }

//            //check if is enum and we don't have any list

//            if (typeof(T).BaseType == typeof(Enum))
//            {
//                listOfOptions = Enum.GetValues(typeof(T)).Cast<T>().ToDictionary(currentItem => Enum.GetName(typeof(T), currentItem));
//            }

//            else
//            {
//                var metadatalistOfOptions = ModelMetadata.FromLambdaExpression(listOfOptionsExpression, htmlHelper.ViewData);
//                listOfOptions = metadatalistOfOptions.Model as Dictionary<string, T>;

//            }

//            foreach (var item in listOfOptions)
//            {

//                var ischecked = (selectedValues == null) ? false : selectedValues.Any(x => x.ToString() == item.Value.ToString());

//                var itemId = metadataselectedValues.PropertyName + "_" + item.Value;

//                var liBuilder = new TagBuilder("li");

//                var inputBuilder = new TagBuilder("input");

//                inputBuilder.MergeAttribute("type", "checkbox");

//                inputBuilder.MergeAttribute("name", metadataselectedValues.PropertyName, true);

//                inputBuilder.MergeAttribute("id", itemId, true);

//                inputBuilder.MergeAttribute("value", item.Value.ToString(), true);

//                inputBuilder.MergeAttributes(unobtrusiveValidationAttributes);

//                if (ischecked)
//                {

//                    inputBuilder.MergeAttribute("checked", "'checked'");

//                }

//                liBuilder.InnerHtml = inputBuilder.ToString() + htmlHelper.Label(itemId, item.Key);

//                innerhtml = innerhtml + liBuilder;

//            }

//            html.InnerHtml = innerhtml;

//            return new MvcHtmlString(html.ToString());
//        }
	}
}