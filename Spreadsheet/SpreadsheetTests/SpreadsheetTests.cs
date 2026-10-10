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
        sheet.SetContentsOfCell("A1", "1.0");
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
        Assert.Throws<InvalidNameException>( () => sheet.SetContentsOfCell("1A1", "1.0"));
        

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
        sheet.SetContentsOfCell("A1", "1 + 1");
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
        Assert.Throws<InvalidNameException>( () => sheet.SetContentsOfCell("1A1", "1"));
    }
    
    /// <summary>
    /// Tests putting an invalid variable name into the string SetCellContents method
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void SetCellContents_TestSetCellContentsStringEmptyText_valid()
    {
        Spreadsheet sheet = new Spreadsheet();
        sheet.SetContentsOfCell("A1", "1 + 1");
        sheet.SetContentsOfCell("A1", "");
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
        sheet.SetContentsOfCell("A1", "=1 + 2");
        sheet.SetContentsOfCell("B1", "=A1 + 2");
        
        Assert.AreEqual(new Formula.Formula("1 + 2"), sheet.GetCellContents("A1"));
    }
    
    /// <summary>
    /// Tests when an invalid variable name is put into the formula SetCellContents method
    /// expected outcome: InvalidNameException
    /// </summary>
    [TestMethod]
    public void SetCellContentsFormula_TestInvalidVariableName_valid()
    {
        Spreadsheet sheet = new Spreadsheet();
        Assert.Throws<InvalidNameException>( () => sheet.SetContentsOfCell("A1A", "=1+2"));
        
    }
    
    /// <summary>
    /// Tests the formula SetCellContents method when it causes a CircularException
    /// expected outcome: CircularException
    /// </summary>
    [TestMethod]
    public void SetCellContentsFormula_TestCircularException_CircularException()
    {
        Spreadsheet sheet = new Spreadsheet();
        sheet.SetContentsOfCell("A1", "=B1+2");
        
        Assert.Throws<CircularException>( () => sheet.SetContentsOfCell("B1", "=A1 + 2"));
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
        sheet.SetContentsOfCell("A1", "1 + 1");
        sheet.SetContentsOfCell("B1", "2");
        sheet.SetContentsOfCell("C2", "3");

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
    
    /// <summary>
    /// Tests GetCellValue when the content is a Formula
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void GetCellValue_TestGetCellValueFormula_valid()
    {
        Spreadsheet sheet = new Spreadsheet();
        sheet.SetContentsOfCell("A1", "=1 + 1");
        
        Assert.AreEqual(2.0, sheet.GetCellValue("A1"));
    }
    
    /// <summary>
    /// Tests GetCellValue when the content is a string
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void GetCellValue_TestGetCellValueString_valid()
    {
        Spreadsheet sheet = new Spreadsheet();
        sheet.SetContentsOfCell("A1", "1 + 1");
        
        Assert.AreEqual("1 + 1", sheet.GetCellValue("A1"));
    }
    
    /// <summary>
    /// Tests GetCellValue when the content is a double
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void GetCellValue_TestGetCellValueDouble_valid()
    {
        Spreadsheet sheet = new Spreadsheet();
        sheet.SetContentsOfCell("A1", "5.0");
        
        Assert.AreEqual(5.0, sheet.GetCellValue("A1"));
    }
    
    /// <summary>
    /// Tests GetCellValue when the variable was never given a value
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void GetCellValue_TestGetCellValueNotGivenValue_valid()
    {
        Spreadsheet sheet = new Spreadsheet();
        Assert.AreEqual("", sheet.GetCellValue("A1"));
    }
    
    
    /// <summary>
    /// Tests GetCellValue when the variable given does not have a valid name
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void GetCellValue_TestGetCellValueInvalidVariable_valid()
    {
        Spreadsheet sheet = new Spreadsheet();
        sheet.SetContentsOfCell("A1", "5.0");
        
        Assert.Throws<InvalidNameException>( () => sheet.GetCellValue("A1A"));
    }
    
    /// <summary>
    /// Tests GetCellValue when the value it is trying to find cannot exist
    /// due to a reliant variable not having a numeric value
    /// expected outcome: FormulaError
    /// </summary>
    [TestMethod]
    public void GetCellValue_TestGetCellValueArgumentException_FormulaError()
    {
        Spreadsheet sheet = new Spreadsheet();

        sheet.SetContentsOfCell("A1", "hello");
        sheet.SetContentsOfCell("B1", "=A1 + 2");

        Assert.IsInstanceOfType<Formula.FormulaError>(sheet.GetCellValue("B1"));
    }
    
    /// <summary>
    /// tests that the non default spreadsheet constructor works as intended
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void Spreadsheet_TestSpreadsheetConstructor_valid()
    {
        
        string filename = "testSpreadsheet1.json";

        string json = """
                      {
                          "Cells": {
                              "A1": {
                                  "StringForm": "Hi"
                              },
                              "B1": {
                                  "StringForm": "12"
                              },
                              "C1": {
                                  "StringForm": "=A1 + 1"
                              }
                          }
                      }
                      """;

        File.WriteAllText(filename, json);

        try
        {
            Spreadsheet sheet = new Spreadsheet(filename);
            
            Assert.AreEqual("Hi", sheet.GetCellContents("A1"));
            Assert.AreEqual(12.0, sheet.GetCellContents("B1"));
            Assert.AreEqual(new Formula.Formula("A1+1"), sheet.GetCellContents("C1"));
            Assert.HasCount(3, sheet.GetNamesOfAllNonemptyCells());
            Assert.IsFalse(sheet.Changed);
        }
        finally
        {
            File.Delete(filename);
        }
    }
    
    
    /// <summary>
    /// tests that the non default spreadsheet constructor throws when JSON file is not setup correctly
    /// expected outcome: SpreadsheetReadWriteException
    /// </summary>
    [TestMethod]
    public void Spreadsheet_TestSpreadsheetCellNotObject_SpreadsheetReadWriteException()
    {
        
        string filename = "testSpreadsheet2.json";

        string json = """
                      {
                          "Cells": "String"
                      }
                      """;

        File.WriteAllText(filename, json);

        try
        {
            Assert.Throws<SpreadsheetReadWriteException>( () => new Spreadsheet(filename));
            
        }
        finally
        {
            File.Delete(filename);
        }
    }
    
    /// <summary>
    /// tests that the non default spreadsheet constructor will throw when the variable values are null
    /// expected outcome: SpreadsheetReadWriteException
    /// </summary>
    [TestMethod]
    public void Spreadsheet_TestSpreadsheetCellValueNull_SpreadSheetReadWriteException()
    {
        
        string filename = "testSpreadsheet3.json";

        string json = """
                      {
                          "Cells": {
                              "A1": {
                                  "StringForm": null
                              }
                          }
                      }
                      """;

        File.WriteAllText(filename, json);

        try
        {
            Assert.Throws<SpreadsheetReadWriteException>( () => new Spreadsheet(filename));
            
        }
        finally
        {
            File.Delete(filename);
        }
    }
    
    
    /// <summary>
    /// Tests that Save method works as intended
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void Save_TestSave_Valid()
    {
        string filename = "testSpreadsheet4.json";

        
        Spreadsheet sheet = new Spreadsheet();
        sheet.SetContentsOfCell("A1", "Hi");
        sheet.SetContentsOfCell("B1", "12");
        sheet.SetContentsOfCell("C1", "=A1+1");

        try
        {
            sheet.Save(filename);
            
            Assert.IsFalse(sheet.Changed);
            
            Spreadsheet loadedSheet = new Spreadsheet(filename);
            
            Assert.AreEqual("Hi", loadedSheet.GetCellContents("A1"));
            Assert.AreEqual(12.0, loadedSheet.GetCellContents("B1"));
            Assert.AreEqual(new Formula.Formula("A1+1"), loadedSheet.GetCellContents("C1"));

            Assert.HasCount(3, loadedSheet.GetNamesOfAllNonemptyCells());

            Assert.IsFalse(loadedSheet.Changed);
        }
        finally
        {
                File.Delete(filename);
        }
    }
    
    /// <summary>
    /// Testing what happens if we try to save to a non existent path
    /// expected outcome: SpreadsheetReadWriteException
    /// </summary>
    [TestMethod]
    public void Save_TestSaveInvalidDirectory_SpreadsheetReadWriteException()
    {
        string filename = "C:/blahblah/notrealdirectory/nonsenseyay";

        
        Spreadsheet sheet = new Spreadsheet();
        
        Assert.Throws<SpreadsheetReadWriteException>(() => sheet.Save(filename));
    }

    
    /// <summary>
    /// tests what value Cell class returns when contents is set to something other than
    /// a string, double, or Formula
    /// expected outcome: returns empty string
    /// </summary>
    [TestMethod]
    public void Cell_StringFormUnknownType_ReturnsEmptyString()
    {
        Cell cell = new Cell(true);
        
        string result = cell.StringForm;
        
        Assert.AreEqual("", result);
    }
    
    /// <summary>
    /// Tests that the indexer returns the correct value
    /// expected outcome: valid 
    /// </summary>
    [TestMethod]
    public void Indexer_TestIndexer_valid()
    {
        Spreadsheet sheet = new Spreadsheet();
        sheet.SetContentsOfCell("A1", "Hi");
        sheet.SetContentsOfCell("B1", "12");
        sheet.SetContentsOfCell("C1", "=1+1");
        
        Assert.AreEqual("Hi", sheet["A1"]);
        Assert.AreEqual(12.0, sheet["B1"]);
        Assert.AreEqual(2.0, sheet["C1"]);
        Assert.AreEqual("", sheet["D1"]);
    }

    
    /// <summary>
    /// Stress test for Spreadsheet class testing a long chain of dependencies and that
    /// the values are updated correctly when a dependee value is changed
    /// </summary>
    [TestMethod]
    public void Spreadsheet_StressTest()
    {
        
        Spreadsheet sheet = new Spreadsheet();
        int numberOfCells = 1000;
        
        sheet.SetContentsOfCell("A1", "1");
        
        //creates 1000 cells with each additional cell being dependent on the previous cell
        for (int i = 2; i <= numberOfCells; i++)
        {
            sheet.SetContentsOfCell("A" + i, "=A" + (i - 1) + "+1");
        }
        
        Assert.AreEqual(1.0, sheet.GetCellValue("A1"));
        Assert.AreEqual(500.0, sheet.GetCellValue("A500"));
        Assert.AreEqual(1000.0, sheet.GetCellValue("A1000"));
        
        //changes value of A1, causing all dependent variables to also change
        sheet.SetContentsOfCell("A1", "10");
        
        Assert.AreEqual(10.0, sheet.GetCellValue("A1"));
        Assert.AreEqual(209.0, sheet.GetCellValue("A200"));
        Assert.AreEqual(509.0, sheet.GetCellValue("A500"));
        Assert.AreEqual(1009.0, sheet.GetCellValue("A1000"));
        
        
        Assert.HasCount(numberOfCells, sheet.GetNamesOfAllNonemptyCells());

        //removes all values
        for (int i = 1; i <= numberOfCells; i++)
        {
            sheet.SetContentsOfCell("A" + i, "");
        }
        Assert.HasCount(0,  sheet.GetNamesOfAllNonemptyCells());
    }

    
}