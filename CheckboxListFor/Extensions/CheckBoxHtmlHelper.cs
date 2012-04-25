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

            Dictionary<TOption, string> listOfOptions = null;
            if (typeof(TOption).BaseType == typeof(Enum) && listOfOptionsExpression == null)
            {
                listOfOptions = Enum.GetValues(typeof(TOption)).Cast<TOption>().ToDictionary(t => (TOption)t, t => t.ToString());
            }
            else
            {
                ModelMetadata metadatalistOfOptions = ModelMetadata.FromLambdaExpression(listOfOptionsExpression, htmlHelper.ViewData);
                listOfOptions = metadatalistOfOptions.Model as Dictionary<TOption, string>;

                int i = 0;
                foreach (var item in listOfOptions)
                {
                    innerhtml = innerhtml + htmlHelper.Hidden(string.Format("{0}[{1}].Key", metadatalistOfOptions.PropertyName, i.ToString()), item.Key);
                    innerhtml = innerhtml + htmlHelper.Hidden(string.Format("{0}[{1}].Value", metadatalistOfOptions.PropertyName, i.ToString()), item.Value);
                    i ++;
                }

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

                liBuilder.InnerHtml = inputBuilder.ToString() + htmlHelper.Label(metadata.PropertyName + "." + item.Key.ToString(), item.Value.ToString());

                innerhtml = innerhtml + liBuilder;

            }

            html.InnerHtml = innerhtml;

            return new MvcHtmlString(html.ToString());

        }
    }
}