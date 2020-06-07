using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloGameSundayPicker
{
    /// <summary>
    /// Format names into table
    /// </summary>
    public static class TableFormater
    {
        /// <summary>
        /// Format names into a table
        /// </summary>
        /// <param name="pTable"></param>
        /// <returns></returns>
        public static string FormatTable(Dictionary<string,string> pTable)
        {
            List<string> tableLines = new List<string>();

            
            int maxNameLength = GetMaxColumnWidth(pTable);
            maxNameLength += GetLength(" | ");

            string header = addRow("Picker", "Pickee", maxNameLength);

            tableLines.Add(new string('=', header.Length));
            tableLines.Add(header);
            tableLines.Add(new string('=', header.Length));

            foreach (KeyValuePair<string,string> pair in pTable)
            {
                tableLines.Add(addRow(pair.Key, pair.Value, maxNameLength));

            }

            tableLines.Add(new string('=', header.Length));

            return string.Join(Environment.NewLine, tableLines);
        }//END FormatTable()

        /// <summary>
        /// Get column width
        /// </summary>
        /// <param name="pTable"></param>
        /// <returns></returns>
        private static int GetColumnWidth (Dictionary<string, string> pTable)
        {
            int width = 0;

            foreach (KeyValuePair<string, string> pair in pTable)
            {
                if(pair.Key.Length > width)
                {
                    width = pair.Key.Length;
                }
            }

            return width;
        }//END GetColumnWidth()

        private static int GetMaxColumnWidth(Dictionary<string, string> pTable)
        {
            int width = 0;
            Font measurementFont = new Font("Arial", 14);
            Graphics graphics = Graphics.FromImage(new Bitmap(1, 1));

            foreach (KeyValuePair<string, string> pair in pTable)
            {
                float widthNum = GetLength(pair.Key);
                if (widthNum > width)
                {
                    width = (int)widthNum;
                }
            }


            return width;
        }//END GetColumnWidth()

        private static int GetLength(string pStr)
        {
            Font measurementFont = new Font("Arial", 14);
            Graphics graphics = Graphics.FromImage(new Bitmap(1, 1));
            return (int)graphics.MeasureString(pStr, measurementFont).Width;
        }

        private static string addRows(string name, string value, int maxWidth)
        {
            string teststring = name + "";
            int spacesAdded = 0;
            while (maxWidth > GetLength(teststring + "x"))
            {
                spacesAdded++;
                teststring += " ";
            }

            return (name + " ".PadRight(spacesAdded) + " | " + value + "");
        }

        static string addRow(string name, string value, int maxWidth)
        {
            Font measurementFont = new Font("Arial", 14);
            Graphics graphics = Graphics.FromImage(new Bitmap(1, 1));
            float width = GetLength(name);
            int amountOfSpacesNeeded = Convert.ToInt32((maxWidth - width) / 5);
            return (name + " ".PadRight(amountOfSpacesNeeded) + "  " + value); // The console font is Arial
        }

    }//END class TableFormater
}//END Namespace
