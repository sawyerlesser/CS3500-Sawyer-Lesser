using Spreadsheets;

namespace SpreadsheetTests;

[TestClass]
public sealed class SpreadsheetTests
{
    
    /// <summary>
    /// This method tests that SetCellContents will return the contents of the cell
    /// when the contents are a double.
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void SetCellContents_TestSetCellContentsDouble_valid()
    {
        Spreadsheet sheet = new Spreadsheet();
        sheet.SetCellContents("A1", 1.0);
        Assert.AreEqual(1.0, sheet.GetCellContents("A1"));

    }
    
    /// <summary>
    /// Tests putting an invalid variable name into the double SetCellContents method
    /// expected outcome: InvalidNameException
    /// </summary>
    [TestMethod]
    public void SetCellContents_TestSetCellContentsDoubleInvalidName_invalidNameException()
    {
        Spreadsheet sheet = new Spreadsheet();
        Assert.Throws<InvalidNameException>( () => sheet.SetCellContents("1A1", 1.0));
        

    }
    
    
    /// <summary>
    /// This method tests that SetCellContents will return the contents of the cell
    /// when the contents are a string
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void SetCellContents_TestSetCellContentsString_valid()
    {
        Spreadsheet sheet = new Spreadsheet();
        sheet.SetCellContents("A1", "1 + 1");
        Assert.AreEqual("1 + 1", sheet.GetCellContents("A1"));

    }
    
    /// <summary>
    /// Tests putting an invalid variable name into the string SetCellContents method
    /// expected outcome: InvalidNameException
    /// </summary>
    [TestMethod]
    public void SetCellContents_TestSetCellContentsStringInvalidName_invalidNameException()
    {
        Spreadsheet sheet = new Spreadsheet();
        Assert.Throws<InvalidNameException>( () => sheet.SetCellContents("1A1", "1"));
    }
    
    /// <summary>
    /// Tests putting an invalid variable name into the string SetCellContents method
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void SetCellContents_TestSetCellContentsStringEmptyText_valid()
    {
        Spreadsheet sheet = new Spreadsheet();
        sheet.SetCellContents("A1", "1 + 1");
        sheet.SetCellContents("A1", "");
        Assert.IsEmpty(sheet.GetNamesOfAllNonemptyCells());
        Assert.AreEqual("", sheet.GetCellContents("A1"));
    }
    
    /// <summary>
    /// This method tests that SetCellContents will return the contents of the cell
    /// when the contents are a Formula
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void SetCellContentsFormula_TestSetCellContentsFormula_valid()
    {
        Spreadsheet sheet = new Spreadsheet();
        Formula.Formula formula1 = new Formula.Formula("1 + 2");
        sheet.SetCellContents("A1", formula1);
        
        Formula.Formula formula2 = new Formula.Formula("A1 + 2");
        sheet.SetCellContents("B1", formula2);
        
        Assert.AreEqual(formula1, sheet.GetCellContents("A1"));
    }
    
    /// <summary>
    /// Tests when an invalid variable name is put into the formula SetCellContents method
    /// expected outcome: InvalidNameException
    /// </summary>
    [TestMethod]
    public void SetCellContentsFormula_TestInvalidVariableName_valid()
    {
        Spreadsheet sheet = new Spreadsheet();
        Formula.Formula formula1 = new Formula.Formula("1 + 2");
        Assert.Throws<InvalidNameException>( () => sheet.SetCellContents("A1A", formula1));
        
    }
    
    /// <summary>
    /// Tests the formula SetCellContents method when it causes a CircularException
    /// expected outcome: CircularException
    /// </summary>
    [TestMethod]
    public void SetCellContentsFormula_TestCircularException_CircularException()
    {
        Spreadsheet sheet = new Spreadsheet();
        Formula.Formula formula1 = new Formula.Formula("B1 + 2");
        sheet.SetCellContents("A1", formula1);
        
        Formula.Formula formula2 = new Formula.Formula("A1 + 2");
        Assert.Throws<CircularException>( () => sheet.SetCellContents("B1", formula2));
    }
    
    /// <summary>
    /// This tests GetCellContents when the cell is empty
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void GetCellContents_TestGetCellContentsWhenCellEmpty_valid()
    {
        Spreadsheet sheet = new Spreadsheet();
        Assert.AreEqual("", sheet.GetCellContents("A1"));
    }

    /// <summary>
    /// Tests GetCellContents when the variable name is invalid
    /// expected outcome: InvalidNameException
    /// </summary>
    [TestMethod]
    public void GetCellContents_TestInvalidCellName_InvalidNameException()
    {
        Spreadsheet sheet = new Spreadsheet();
        Assert.Throws<InvalidNameException>( () => sheet.GetCellContents("1A1"));
    }

    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public void GetNamesOfAllNonemptyCells_TestGetNamesOfAllNonemptyCells_valid()
    {
        Spreadsheet sheet = new Spreadsheet();
        sheet.SetCellContents("A1", "1 + 1");
        sheet.SetCellContents("B1", 2);
        sheet.SetCellContents("C2", 3);

        ISet<string> nonEmptyVariables = sheet.GetNamesOfAllNonemptyCells();
        Assert.HasCount(3, nonEmptyVariables);
        Assert.Contains("A1", nonEmptyVariables);
        nonEmptyVariables.Remove("A1");
        Assert.Contains("B1", nonEmptyVariables);
        nonEmptyVariables.Remove("B1");
        Assert.Contains("C2", nonEmptyVariables);
        nonEmptyVariables.Remove("C2");
        Assert.HasCount(0, nonEmptyVariables);
    }
}