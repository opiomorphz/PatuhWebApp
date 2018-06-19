using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace System.Web.Mvc.Html
{
    public static class DatePickerExtension
    {
        public static string DatePicker(this HtmlHelper htmlHelper, string name, string value)
        {
            return "<script type=\"text/javascript\">"
                + "$(function() {" + "$(\"#" + name + "\").datepicker({changeMonth: true, changeYear: true, showButtonPanel: false, dateFormat: 'mm-yy', "

                + " onClose: function(dateText, inst) { "
                + " var month = $(\"#ui-datepicker-div .ui-datepicker-month :selected\").val();"
                + " var year = $(\"#ui-datepicker-div .ui-datepicker-year :selected\").val();"
                + " $(this).datepicker('setDate', new Date(year, month, 1));"
                + " }"

                //+ ","
                //+ " beforeShow : function(input, inst) {"
                //+ "    if ((datestr = $(this).val()).length > 0) {"
                //+ "        year = datestr.substring(datestr.length-4, datestr.length);"
                //+ "        month = jQuery.inArray(datestr.substring(0, datestr.length-5), $(this).datepicker('option', 'monthNames'));"
                ////+ "        month = datestr.substring(0, datestr.length-5);"
                ////+ "        month = '05';"
                //+ "        $(this).datepicker('option', 'defaultDate', new Date(year, month, 1));"
                //+ "        $(this).datepicker('setDate', new Date(year, month, 1));"
                //+ "    }"
                //+ " }"

                + "});"
                + "});"

                //+ "$(function() {" + "$(\"#" + name + "\").focus(function(){"
                //+ " $(\".ui-datepicker-calendar\").hide();"
                //+ " $(\"#ui-datepicker-div\").position({"
                //+ " my: \"center top\", at: \"center bottom\", of: $(this) }); });"

                + "</script>" + "<input type=\"text\" size=\"10\" value=\"" + value + "\" id=\"" + name + "\" name=\"" + name + "\" maxlength = 10  style = \"width:70px;\"  />";
        }
    }
}