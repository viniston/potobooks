using System.Collections.Generic;
using System.Text;
using Photo.Business.Utilities.Formatting;

namespace Helper
{
    /// <summary>
    /// Helper class to construct CSV files
    /// </summary>
    public static class CSVHelper
    {
        #region Public Methods

        public static string GetCSVContent(List<List<string>> data, bool cleanUpHtmlFromHeader)
        {
            return GetCSVContent(data, ",", true, cleanUpHtmlFromHeader);
        }

        public static string GetCSVContent(List<List<string>> data, string delimiter, bool quoteAll, bool cleanUpHtmlFromHeader)
        {
            StringBuilder output = new StringBuilder();

            string quote = quoteAll ? "\"" : string.Empty;
            int rowIndex = 0;
            foreach (List<string> dataRow in data)
            {
                int count = dataRow.Count;
                for (int i = 0; i < count; i++)
                {
                    string dataValue = cleanUpHtmlFromHeader && rowIndex == 0 ? FormatHelper.CleanUpHtmlTags(dataRow[i]) : dataRow[i];
                    output.Append(
                        quote +
                        (string.IsNullOrEmpty(dataValue) ? string.Empty : dataValue.Replace("\"", "\"\"")) +
                        quote +
                        (i < count - 1 ? delimiter : "\r\n"));
                }
                rowIndex++;
            }

            return output.ToString();
        }

        #endregion
    }
}