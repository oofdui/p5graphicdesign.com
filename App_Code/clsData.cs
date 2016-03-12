using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;
using System.Web;
using System.IO;
using System.Reflection;

public class clsData
{
    public enum JoinType
    {
        Inner = 0,
        Left = 1
    }
    /// <summary>
    /// ค้นหาข้อความใน List String
    /// </summary>
    /// <param name="lstSearch">List ที่ต้องการค้นหา</param>
    /// <param name="strWord">คำที่ต้องการค้นหา</param>
    /// <returns>True=พบ , False=ไม่พบ</returns>
    /// <remarks>2013-08-16</remarks>
    public bool Search(List<string> lstSearch, string strWord)
    {
        bool rtnBool = false;
        List<string> lstResult = new List<string>();

        #region LINQ
        lstResult = (from word in lstSearch
                     where word == strWord
                     select word).ToList();
        if (lstResult.Count > 0) rtnBool = true;
        #endregion

        return rtnBool;
    }
    /// <summary>
    /// ค้นหาข้อความใน DataTable
    /// </summary>
    /// <param name="dtSearch">DataTable ที่ต้องการค้นหา</param>
    /// <param name="strWord">คำที่ต้องการค้นหา</param>
    /// <param name="strField">Field ที่ต้องการค้นหา</param>
    /// <returns>True=พบ , False=ไม่พบ</returns>
    /// <remarks>2013-08-16</remarks>
    public bool Search(DataTable dtSearch, string strWord, string strField)
    {
        bool rtnBool = false;

        #region LINQ
        var varResult = (from word in dtSearch.AsEnumerable()
                         where word[strField].ToString() == strWord
                         select word);
        if (varResult != null && varResult.Count() > 0)
        {
            DataTable dtResult = new DataTable();
            dtResult = varResult.CopyToDataTable();
            rtnBool = true;
        }
        #endregion

        return rtnBool;
    }
    /// <summary>
    /// Joins 2 DataTables แบบเลือกวิธี Join ได้
    /// <para>Returns an appropriate DataTable with zero rows if the colToJoinOn does not exist in both tables.</para>
    /// </summary>
    /// <param name="dtblLeft"></param>
    /// <param name="dtblRight"></param>
    /// <param name="colToJoinOn"></param>
    /// <param name="joinType">clsData.JoinType.Left หรือ clsData.JoinType.Inner</param>
    /// <returns></returns>
    /// <example>
    /// dt = clsData.JoinTwoDataTable(dtHoldList, dtDepartment, "dept_id", clsData.JoinType.Left);
    /// </example>
    /// <remarks>
    /// <para>http://stackoverflow.com/questions/2379747/create-combined-datatable-from-two-datatables-joined-with-linq-c-sharp?rq=1</para>
    /// <para>http://msdn.microsoft.com/en-us/library/vstudio/bb397895.aspx</para>
    /// <para>http://www.codinghorror.com/blog/2007/10/a-visual-explanation-of-sql-joins.html</para>
    /// <para>http://stackoverflow.com/questions/406294/left-join-and-left-outer-join-in-sql-server</para>
    /// </remarks>
    public DataTable JoinTwoDataTable(DataTable dtblLeft, DataTable dtblRight, string colToJoinOn, JoinType joinType)
    {
        //Change column name to a temp name so the LINQ for getting row data will work properly.
        string strTempColName = colToJoinOn + "_2";
        if (dtblRight.Columns.Contains(colToJoinOn))
            dtblRight.Columns[colToJoinOn].ColumnName = strTempColName;

        //Get columns from dtblA
        DataTable dtblResult = dtblLeft.Clone();

        //Get columns from dtblB
        var dt2Columns = dtblRight.Columns.OfType<DataColumn>().Select(dc => new DataColumn(dc.ColumnName, dc.DataType, dc.Expression, dc.ColumnMapping));

        //Get columns from dtblB that are not in dtblA
        var dt2FinalColumns = from dc in dt2Columns.AsEnumerable()
                              where !dtblResult.Columns.Contains(dc.ColumnName)
                              select dc;

        //Add the rest of the columns to dtblResult
        dtblResult.Columns.AddRange(dt2FinalColumns.ToArray());

        //No reason to continue if the colToJoinOn does not exist in both DataTables.
        if (!dtblLeft.Columns.Contains(colToJoinOn) || (!dtblRight.Columns.Contains(colToJoinOn) && !dtblRight.Columns.Contains(strTempColName)))
        {
            if (!dtblResult.Columns.Contains(colToJoinOn))
                dtblResult.Columns.Add(colToJoinOn);
            return dtblResult;
        }

        switch (joinType)
        {

            default:
            case JoinType.Inner:
                #region Inner
                //get row data
                //To use the DataTable.AsEnumerable() extension method you need to add a reference to the System.Data.DataSetExtension assembly in your project. 
                var rowDataLeftInner = from rowLeft in dtblLeft.AsEnumerable()
                                       join rowRight in dtblRight.AsEnumerable() on rowLeft[colToJoinOn] equals rowRight[strTempColName]
                                       select rowLeft.ItemArray.Concat(rowRight.ItemArray).ToArray();


                //Add row data to dtblResult
                foreach (object[] values in rowDataLeftInner)
                    dtblResult.Rows.Add(values);

                #endregion
                break;
            case JoinType.Left:
                #region Left
                var rowDataLeftOuter = from rowLeft in dtblLeft.AsEnumerable()
                                       join rowRight in dtblRight.AsEnumerable() on rowLeft[colToJoinOn] equals rowRight[strTempColName] into gj
                                       from subRight in gj.DefaultIfEmpty()
                                       select rowLeft.ItemArray.Concat((subRight == null) ? (dtblRight.NewRow().ItemArray) : subRight.ItemArray).ToArray();


                //Add row data to dtblResult
                foreach (object[] values in rowDataLeftOuter)
                    dtblResult.Rows.Add(values);

                #endregion
                break;
        }

        //Change column name back to original
        dtblRight.Columns[strTempColName].ColumnName = colToJoinOn;

        //Remove extra column from result
        dtblResult.Columns.Remove(strTempColName);

        return dtblResult;
    }
	public void ExportToExcel(DataTable dt,string Title,Boolean EnableAlternateRowColor)
    {
        #region Build FileHeader
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Export.xls");
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        #endregion

        #region Build Table
        HttpContext.Current.Response.Write("<table border='1' bgColor='#fff' " +
          "borderColor='#DADADA' cellSpacing='0' cellPadding='0' " +
          "style='font-size:10pt; font-family:tahoma; background:#fff;'>");
        #region Build Title
        HttpContext.Current.Response.Write("<tr>");
        HttpContext.Current.Response.Write("<td colspan='" + dt.Columns.Count.ToString() + "' ");
        HttpContext.Current.Response.Write("style='text-align:center;background-color:#00B1F2;color:#fff;font-weight:bold;font-size:12pt;'");
        HttpContext.Current.Response.Write(">");
        if (string.IsNullOrEmpty(Title))
        {
            HttpContext.Current.Response.Write("Export");
        }
        else
        {
            HttpContext.Current.Response.Write(Title);
        }
        HttpContext.Current.Response.Write("</td>");
        HttpContext.Current.Response.Write("</tr>");
        #endregion
        #region Build ContentHeader
        HttpContext.Current.Response.Write("<tr>");
        for (int j = 0; j < dt.Columns.Count; j++)
        {
            HttpContext.Current.Response.Write("<td style='background-color:#56C4FD;text-align:center;color:#fff;font-weight:bold;padding:5px;'>");
            HttpContext.Current.Response.Write(dt.Columns[j].ColumnName.ToString());
            HttpContext.Current.Response.Write("</td>");
        }
        HttpContext.Current.Response.Write("</tr>");
        #endregion
        #region Build Content
        int loopCount = 0;
        foreach (DataRow row in dt.Rows)
        {
            loopCount += 1;
            HttpContext.Current.Response.Write("<tr>");
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                #region Build Row Style
                string alternateRowColor = "";
                if (EnableAlternateRowColor)
                {
                    if (loopCount % 2 == 0)
                    {
                        alternateRowColor = "background-color:#FFF;";
                    }
                    else
                    {
                        alternateRowColor = "background-color:#FAFAFA;";
                    }
                }
                else
                {
                    alternateRowColor = "background-color:#FFF;";
                }
                #endregion
                HttpContext.Current.Response.Write("<td style='text-align:center;"+alternateRowColor+"'>");
                HttpContext.Current.Response.Write(row[i].ToString());
                HttpContext.Current.Response.Write("</td>");
            }
            HttpContext.Current.Response.Write("</tr>");
        }
        #endregion
        HttpContext.Current.Response.Write("</table>");
        #endregion
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }
	public void ExportToExcel(GridView gv, string Title, Boolean EnableAlternateRowColor)
    {
        DataTable dt = new DataTable();
        dt = GridViewToDataTable(gv);

        if (dt != null && dt.Rows.Count > 0)
        {
            ExportToExcel(dt, Title, EnableAlternateRowColor);
        }
    }
    public DataTable GridViewToDataTable(GridView gv)
    {
        DataTable dt = new DataTable();

        if (gv.HeaderRow != null)
        {
            #region Build Header
            string headerBefore="";
            for (int i = 0; i < gv.HeaderRow.Cells.Count; i++)
            {
                //ตรวจสอบไม่ให้ชื่อ Header ซ้ำกัน
                if (gv.HeaderRow.Cells[i].Text == headerBefore)
                {
                    dt.Columns.Add(gv.HeaderRow.Cells[i].Text+" "+i.ToString());
                }
                else
                {
                    dt.Columns.Add(gv.HeaderRow.Cells[i].Text);
                }
                headerBefore = gv.HeaderRow.Cells[i].Text;
            }
            #endregion
            #region Build Row
            foreach (GridViewRow gvr in gv.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();
                #region Build Column
                for (int i = 0; i < gvr.Cells.Count; i++)
                {
                    dr[i] = gvr.Cells[i].Text.Trim();
                }
                #endregion
                dt.Rows.Add(dr);
            }
            #endregion
        }

        return dt;
    }
    /// <summary>
    /// แปลงไฟล์ CSV ที่ต้องการเป็น DataTable
    /// </summary>
    /// <param name="FilePath">Path ไฟล์ CSV พร้อมชื่อไฟล์</param>
    /// <returns>DataTable ที่แปลงได้</returns>
    /// <example>
    /// clsData.CSVToDataTable("DB\FileList.csv");
    /// </example>
    public DataTable CSVToDataTable(string FilePath)
    {
        DataTable dt = new DataTable();
        FileInfo fi = new FileInfo(@FilePath);

        #region File Exist
        if (fi.Exists)
        {
            #region CSV Extension Check
            if (fi.Extension.ToLower() == ".csv")
            {
                try
                {
                    string line;

                    using (StreamReader reader = new StreamReader(Path.GetFullPath(FilePath), Encoding.GetEncoding(874)))
                    {
                        line = reader.ReadLine();
                        #region Loop Line
                        do
                        {
                            if (!string.IsNullOrEmpty(line))
                            {
                                string[] arrLine = line.Trim().Split(',');
                                DataRow dr;

                                #region Build Column
                                if (dt.Columns.Count == 0)
                                {
                                    for (int i = 0; i < arrLine.Length; i++)
                                    {
                                        dt.Columns.Add(i.ToString());
                                    }
                                }
                                else if (dt.Columns.Count < arrLine.Length)
                                {
                                    for (int i = dt.Columns.Count; i < arrLine.Length; i++)
                                    {
                                        dt.Columns.Add(i.ToString());
                                    }
                                }
                                #endregion
                                #region Build Row
                                dr = dt.NewRow();
                                for (int i = 0; i < arrLine.Length; i++)
                                {
                                    dr[i] = arrLine[i].Trim();
                                }
                                dt.Rows.Add(dr);
                                #endregion
                            }
                            line = reader.ReadLine();
                        } while (!string.IsNullOrEmpty(line));
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    //Error : Read File
                }
            }
            #endregion
        }
        #endregion

        return dt;
    }
    /// <summary>
    /// เขียนไฟล์ CSV
    /// </summary>
    /// <param name="PathFile">Path ไฟล์ CSV พร้อมชื่อไฟล์</param>
    /// <param name="Values">Array ค่าที่ต้องการใส่</param>
    /// <param name="EnableAddDateTime">true=บันทึกเวลาที่ฟิลด์แรก , false=ไม่บันทึก</param>
    /// <returns>true=งานสำเร็จ , false=ไม่สำเร็จ</returns>
    /// <example>
    /// clsData.CSVWriter("Path\Log.csv", new string[] { "ทดสอบ 1", "Test 1", "Test 2" });
    /// </example>
    public bool CSVWriter(string PathFile,string[] Values,bool EnableAddDateTime=true,bool EnableDeleteOldFile=false)
    {
        FileInfo fi = new FileInfo(PathFile);
        bool rtnValue = false;

        #region Delete Old File
        if (EnableDeleteOldFile)
        {
            try
            {
                if (fi.Exists)
                {
                    fi.Delete();
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Write File
        try
        {
            StreamWriter sw = new StreamWriter(PathFile, true, System.Text.Encoding.UTF8);
            StringBuilder strValue = new StringBuilder();

            #region Build Data
            if (EnableAddDateTime)
            {
                strValue.Append(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                strValue.Append(",");
            }

            #region Build Data by Array
            for (int i = 0; i < Values.Length; i++)
            {
                strValue.Append(Values[i].Trim().Replace(",", "#"));

                if (i < Values.Length - 1)
                {
                    strValue.Append(",");
                }
            }
            #endregion
            #endregion

            sw.WriteLine(strValue.ToString());
            strValue.Length = 0; strValue.Capacity = 0;
            sw.Close();
            rtnValue = true;
        }
        catch (Exception ex)
        {

        }
        #endregion

        return rtnValue;
    }
	/// <summary>
    /// แทนที่คำด้วยชุดของ Array 2 มิติ
    /// </summary>
    /// <param name="Text">ข้อความที่ใช้ตั้งต้นในการแทนที่</param>
    /// <param name="Parameter">{ข้อความเก่า,ข้อความใหม่}</param>
    /// <returns>ข้อความที่ถูกแทนที่แล้ว</returns>
    public string Replacer(string Text,string[,] Parameter)
    {
        string rtnValue = Text;

        if (Parameter != null && Parameter.Length > 0)
        {
            for (int i = 0; i < Parameter.Length / Parameter.Rank; i++)
            {
                rtnValue = rtnValue.Replace(Parameter[i, 0], Parameter[i, 1]);
            }
        }
        
        return rtnValue;
    }
	/// <summary>
    /// แปลงไฟล์ Excel (xls,xlsx) เป็น DataTable โดยใช้ ExcelDataReader (Excel.dll)
    /// </summary>
    /// <param name="pathFile">File full path.</param>
    /// <returns>DataTable</returns>
    /// <example>
    /// clsData clsData = new clsData();
    /// dt = clsData.BindExcel("c:\test.xlsx");
    /// </example>
    /*
    public DataTable BindExcel(string pathFile)
    {
        DataTable dt = new DataTable();

        FileStream stream = File.Open(pathFile, FileMode.Open, FileAccess.Read);
        Excel.IExcelDataReader excelReader;

        if (pathFile.Contains("xlsx"))
        {
            excelReader = Excel.ExcelReaderFactory.CreateOpenXmlReader(stream);
        }
        else
        {
            excelReader = Excel.ExcelReaderFactory.CreateBinaryReader(stream);
        }

        excelReader.IsFirstRowAsColumnNames = true;
        DataSet result = excelReader.AsDataSet();
        excelReader.Close();

        return result.Tables[0];
    }
    */
    /// <summary>
    /// คืนค่า Distinct ของ DataTable ตามชื่อฟิลด์ที่เราส่งเข้าไป
    /// </summary>
    /// <param name="dataTable">DataTable ที่ต้องการ Distinct</param>
    /// <param name="distinctColumns">ชื่อ Columns ที่ต้องการคืนค่า และ ทำการ Distinct</param>
    /// <returns>DataTable ที่ผ่านการ Distinct แล้ว</returns>
    /// <example>
    /// dt = clsData.Distinct(dt, "ReportGroupUID","ReportGroupName");
    /// </example>
    public DataTable Distinct(DataTable dataTable, params string[] distinctColumns)
    {
        #region Variable
        var dt = new DataTable();
        var dv = new DataView(dataTable);
        #endregion
        #region Procedure
        dt = dv.ToTable(true, distinctColumns);
        #endregion
        return dt;
    }
    /// <summary>
    /// เพิ่ม แก้ไข ข้อมูลในไฟล์ INI
    /// </summary>
    /// <param name="PathFile">ที่อยู่ของไฟล์</param>
    /// <param name="Key">ชื่อ</param>
    /// <param name="Value">ค่า</param>
    /// <returns>true=สำเร็จ , false=ไม่สำเร็จ</returns>
    /// <example>
    /// INIUpdater(AppDomain.CurrentDomain.BaseDirectory + @"\Config.ini", "cs", "SQLServer");
    /// </example>
    public bool INIUpdater(string PathFile, string Key, string Value)
    {
        #region Variable
        var fi = new FileInfo(PathFile);
        var result = false;
        string[] readLines;
        var writeLines = new List<string>();
        var searchFound = false;
        #endregion
        #region Write File
        try
        {
            if (fi.Exists)
            {
                #region ReadFile
                const Int32 BufferSize = 128;
                using (var fileStream = File.OpenRead(PathFile))
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                {
                    String line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        readLines = line.Split('=');
                        if (readLines[0].Trim().ToLower() == Key.Trim().ToLower())
                        {
                            searchFound = true;
                            readLines[1] = Value.Trim();
                        }
                        writeLines.Add(readLines[0] + "=" + readLines[1]);
                    }
                    if (!searchFound)
                    {
                        writeLines.Add(Key + "=" + Value);
                    }
                }
                #endregion
            }
            else
            {
                writeLines.Add(Key + "=" + Value);
            }
            #region WriteFile
            using (var sw = new StreamWriter(PathFile, false, System.Text.Encoding.UTF8))
            {
                for (int i = 0; i < writeLines.Count(); i++)
                {
                    sw.WriteLine(writeLines[i]);
                }
                result = true;
            }
            #endregion
        }
        catch (Exception ex)
        {
            result = false;
            //ErrorLogBuilder("INIUpdater()", ex.Message);
        }
        #endregion

        return result;
    }
    /// <summary>
    /// ดึงค่าจาก Key ที่ต้องการในไฟล์ INI
    /// </summary>
    /// <param name="PathFile">ที่เก็บไฟล์</param>
    /// <param name="Key">ชื่อที่ต้องการ</param>
    /// <returns></returns>
    /// <example>
    /// MessageBox.Show(INISelecter(AppDomain.CurrentDomain.BaseDirectory+@"\Config.ini","cs"));
    /// </example>
    private string INISelecter(string PathFile, string Key)
    {
        #region Variable
        var result = "";
        var fi = new FileInfo(PathFile);
        string[] readLines;
        #endregion
        #region Procedure
        if (fi.Exists)
        {
            #region ReadFile
            const Int32 BufferSize = 128;
            using (var fileStream = File.OpenRead(PathFile))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    readLines = line.Split('=');
                    if (readLines[0].Trim().ToLower() == Key.Trim().ToLower())
                    {
                        result = readLines[1];
                        break;
                    }
                }
            }
            #endregion
        }
        #endregion
        return result;
    }
}

public static class clsDataExtension
{
    public static DataTable ToDataTable<T>(this List<T> items)
    {
        var tb = new DataTable(typeof(T).Name);

        PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var prop in props)
        {
            tb.Columns.Add(prop.Name, prop.PropertyType);
        }

        foreach (var item in items)
        {
            var values = new object[props.Length];
            for (var i = 0; i < props.Length; i++)
            {
                values[i] = props[i].GetValue(item, null);
            }

            tb.Rows.Add(values);
        }

        return tb;
    }
    /// <summary>
    /// Export DataTable to Excel(xlsx) with EPPlus.dll
    /// </summary>
    /// <param name="dataToExcel">DataTable ที่ต้องการ</param>
    /// <param name="outResult">คืนชื่อพาร์ธพร้อมไฟล์ กรณีที่แปลงได้ , คืนค่า Error กรณีแปลงไม่ได้</param>
    /// <param name="pathName">ที่เก็บไฟล์หลังจาก Export (ไม่ต้องระบุชื่อไฟล์ และ เครื่องใหม่ / ปิดท้าย) เช่น C:\Export</param>
    /// <param name="fileName">ชื่อไฟล์ (ไม่ต้องระบุนามสกุล) เช่น Employee</param>
    /// <param name="sheetName">ชื่อ Sheet</param>
    /// <returns>true=สำเร็จ , false=ไม่สำเร็จ</returns>
    /// <example>
    /// clsData clsData = new clsData();
    /// dt = clsData.BindExcel(txtFile.Text.Trim());
    /// string outResult;
    /// if (dt.ToExcel(out outResult, @"C:\Test", "FileName", "SheetName"))
    /// {
    ///     MessageBox.Show("Export to Excel : Success!! <path:"+outResult+">");
    /// }
    /// else
    /// {
    ///     MessageBox.Show("Export to Excel : Fail!! <message:"+outResult+">");
    /// }
    /// </example>
    /*
    public static bool ToExcel(this DataTable dataToExcel, out string outResult, string pathName = "", string fileName = "", string sheetName = "")
    {
        #region Variable
        bool result = false;
        string currentDirectorypath = string.Empty;
        string finalFileNameWithPath = string.Empty;
        #endregion
        #region ValueAssign
        if (string.IsNullOrEmpty(pathName))
        {
            currentDirectorypath = Environment.CurrentDirectory;
        }
        else
        {
            currentDirectorypath = pathName;
        }
        if (string.IsNullOrEmpty(fileName))
        {
            fileName = string.Format("{0}_{1}", "ExportToExcel", DateTime.Now.ToString("dd-MM-yyyy"));
        }
        else
        {
            fileName = string.Format("{0}", fileName);
        }
        if (string.IsNullOrEmpty(sheetName))
        {
            sheetName = "Sheet1";
        }
        finalFileNameWithPath = string.Format("{0}\\{1}.xlsx", currentDirectorypath, fileName);
        #endregion

        try
        {
            //Delete existing file with same file name.
            if (File.Exists(finalFileNameWithPath))
                File.Delete(finalFileNameWithPath);

            var newFile = new FileInfo(finalFileNameWithPath);

            //Step 1 : Create object of ExcelPackage class and pass file path to constructor.
            using (var package = new OfficeOpenXml.ExcelPackage(newFile))
            {
                //Step 2 : Add a new worksheet to ExcelPackage object and give a suitable name
                OfficeOpenXml.ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(sheetName);

                //Step 3 : Start loading datatable form A1 cell of worksheet.
                worksheet.Cells["A1"].LoadFromDataTable(dataToExcel, true, OfficeOpenXml.Table.TableStyles.None);

                //Step 4 : (Optional) Set the file properties like title, author and subject
                package.Workbook.Properties.Title = sheetName;
                package.Workbook.Properties.Author = "Nithi.re";

                //Step 5 : Save all changes to ExcelPackage object which will create Excel 2007 file.
                package.Save();

                outResult = finalFileNameWithPath;
                result = true;
            }
        }
        catch (Exception ex)
        {
            outResult = ex.Message;
        }

        return result;
    }
    */
}
