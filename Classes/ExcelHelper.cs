using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace Dijekstra.Classes
{
    class ExcelHelper : IDisposable
    {
        private Excel.Application _excel;
        private Excel.Workbook _workbook;
        private Excel.Worksheet _worksheet;
        private object _missingObj = System.Reflection.Missing.Value;

        public ExcelHelper()
        {
            _excel = new Excel.Application();
        }

        internal bool Open(string pathFile)
        {
            try
            {
                if (File.Exists(pathFile))
                {
                    _workbook = _excel.Workbooks.Open(pathFile);
                    _worksheet = (Excel.Worksheet)_excel.Worksheets.get_Item(1);
                }
                else
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        internal int RowNumber()
        {
            return _excel.Sheets[1].Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Row;
        }

        internal int ColNumber()
        {
            return _excel.Sheets[1].Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Column;
        }

        internal string ReadVertex(int colIdx)
        {
            return _excel.Cells[colIdx][1].Text.ToString();
        }

        internal string ReadCell(int rowIdx, int colIdx)
        {
            return _excel.Cells[colIdx][rowIdx].Text.ToString();
        }

        internal bool Set(string column, int row, object data)
        {
            try
            {
                ((Excel.Worksheet)_excel.ActiveSheet).Cells[row, column] = data;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public void Dispose()
        {
            try
            {

                _workbook.Close(false, _missingObj, _missingObj);
                _excel.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(_excel);

                _excel = null;
                _workbook = null;
                _worksheet = null;

                System.GC.Collect();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
