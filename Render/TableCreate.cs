namespace SkyFlowManagement.Render
{
    /* Class that specifically created to implement any class with a header and row
        into a uniformed table to display information: E.g.: */
    /* +------------+----------+-------------+-----------+
       | Flight No  | Origin   | Destination | Status    |
       +------------+----------+-------------+-----------+
       | SF102      | CPT      | JHB         | Boarding  |
       +------------+----------+-------------+-----------+
    */

    public static class TableCreate
    {
       
        public static void Render(string[] headers, List<string[]> rows)
        {
            if (headers.Length == 0)
            {
                Console.WriteLine(" No headers provided.");
                return;
            }

            // Step 1: Estimates how wide the table columns need to be.
            // Starts with the heeader with and compares with row width to see which is wider.
            int[] columnWidths = getColumnWidths(headers, rows);

            // Step 2: Builds the lines in between the rows and columns
            string divider = buildDivider(columnWidths);

            // Step 3: Prints the table as a whole.
            Console.WriteLine();
            Console.WriteLine(divider);
            Console.WriteLine(buildRow(headers, columnWidths));
            Console.WriteLine(divider);

            if (rows.Count == 0)
            {
                // Print a single row the wide of of the table,
                // to display if a record is not found.
                string emptyMessage = " No records found.";
                Console.WriteLine("| " + emptyMessage.PadRight(divider.Length - 4) + " |");
            }
            else
            {
                foreach (string[] row in rows)
                {
                    Console.WriteLine(buildRow(row, columnWidths));
                }
            }

            Console.WriteLine(divider);
            Console.WriteLine($" {rows.Count} record(s) found.");
            Console.WriteLine();
        }


        // Works out the maximum width needed for each column.
        // Checks both the header and every data row.
        private static int[] getColumnWidths(string[] headers, List<string[]> rows)
        {
            // Start with the header widths.
            int[] widths = headers.Select(h => h.Length).ToArray();

            // Checks each row and if the cell is wider, then the width is updated.
            foreach (string[] row in rows)
            {
                for (int i = 0; i < row.Length && i < widths.Length; i++)
                {
                    int cellWidth = (row[i] ?? "").Length;
                    if (cellWidth > widths[i])
                    {
                        widths[i] = cellWidth;
                    }
                }
            }

            // Adds to every column for padding 
            return widths.Select(w => w + 2).ToArray();
        }


        // Builds a divider lines.
        private static string buildDivider(int[] columnWidths)
        {
            // Creates a line in the table for each column.
            // Then joins it with a separator.
            string[] segments = columnWidths.Select(w => new string('-', w)).ToArray();
            return "+" + string.Join("+", segments) + "+";
        }


        // Builds a single data row:
        private static string buildRow(string[] cells, int[] columnWidths)
        {
            string row = "|";

            for (int i = 0; i < columnWidths.Length; i++)
            {
                // Uses an empty string if the row is shorter than what was estimated.
                string cell = i < cells.Length ? (cells[i] ?? "") : "";

                // Fills the cell with enough spaces to reach the column width.
                // Reduce padding to leave room for the space on each side of the cell.
                string paddedCell = " " + cell.PadRight(columnWidths[i] - 1);

                row += paddedCell + "|";
            }

            return row;
        }
    }
}
