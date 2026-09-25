// <copyright file="FormulaSyntaxTests.cs" company="UofU-CS3500">
//   Copyright 2024 UofU-CS3500. All rights reserved.
// </copyright>
// <authors> [Insert Your Name] </authors>
// <date> [Insert the Date] </date>

namespace FormulaTests;

using Formula; // Change this using statement to use different formula implementations.

/// <summary>
///   <para>
///     The following class shows the basics of how to use the MSTest framework,
///     including:
///   </para>
///   <list type="number">
///     <item> How to catch exceptions. </item>
///     <item> How a test of valid code should look. </item>
///   </list>
/// </summary>
[TestClass]
public class FormulaSyntaxTests
{
    // --- Tests for One Token Rule ---

    /// <summary>
    ///   <para>
    ///     This test makes sure the right kind of exception is thrown
    ///     when trying to create a formula with no tokens.
    ///   </para>
    ///   <remarks>
    ///     <list type="bullet">
    ///       <item>
    ///         We use the _ (discard) notation because the formula object
    ///         is not used after that point in the method.  Note: you can also
    ///         use _ when a method must match an interface but does not use
    ///         some of the required arguments to that method.
    ///       </item>
    ///       <item>
    ///         string.Empty is often considered best practice (rather than using "") because it
    ///         is explicit in intent (e.g., perhaps the coder forgot to but something in "").
    ///       </item>
    ///       <item>
    ///         The name of a test method should follow the MS standard:
    ///         https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices
    ///       </item>
    ///       <item>
    ///         All methods should be documented, but perhaps not to the same extent
    ///         as this one.  The remarks here are for your educational
    ///         purposes (i.e., a developer would assume another developer would know these
    ///         items) and would be superfluous in your code.
    ///       </item>
    ///       <item>
    ///         Notice the use of the attribute tag [ExpectedException] which tells the test
    ///         that the code should throw an exception, and if it doesn't an error has occurred;
    ///         i.e., the correct implementation of the constructor should result
    ///         in this exception being thrown based on the given poorly formed formula.
    ///       </item>
    ///     </list>
    ///   </remarks>
    ///   <example>
    ///     <code>
    ///        // here is how we call the formula constructor with a string representing the formula
    ///        _ = new Formula( "5+5" );
    ///     </code>
    ///   </example>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestNoTokens_Invalid( )
    {
        Assert.Throws<FormulaFormatException>( () => _ = new Formula( "" ) );
        // note: it is arguable that you should replace "" with string.Empty for readability and clarity of intent (e.g., not a cut-and-paste error or a "I forgot to put something there" error).
    }
    
    
    /// <summary>
    /// Testing the one token rule. Ensures that with a minimum of one token, no FormulaFormatException is caused
    /// Expected outcome: Valid
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestOneToken_Valid( )
    {
        _ = new Formula( "1" );
        
    }

    // --- Tests for Valid Token Rule ---
    
    /// <summary>
    /// Testing that all valid tokens are in fact valid
    /// Expected outcome: valid
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestValidTokens_Valid( )
    {
        _ = new Formula( "(1 + a2 - 3 / 2 * 4)" );
    }
    
    /// <summary>
    /// Testing that non-valid tokens cause FormulaFormatException
    /// Expected outcome: FormulaFormatException
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestInvalidTokens_Invalid( )
    {
        Assert.Throws<FormulaFormatException>( () => _ = new Formula( "(1 + a2 # 3 / 2 * 4)" ));
    }
    
    /// <summary>
    /// Testing an invalid variable causes FormulaFormatException
    /// Expected outcome: FormulaFormatException
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestInvalidTokensVariables_Invalid( )
    {
        Assert.Throws<FormulaFormatException>( () => _ = new Formula( "a1a" ));
    }

    
    
    // --- Tests for Closing Parenthesis Rule
    
    /// <summary>
    /// Testing the closing parenthesis rule with a valid input
    /// Expected outcome: valid
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestClosingParenthesis_Valid( )
    {
        _ = new Formula( "((1+1)+(1+1))" );
    }
    
    /// <summary>
    ///Testing the closing parenthesis rule with one extra closing parenthesis before its open parenthesis
    /// Expected outcome: FormulaFormatException
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestClosingParenthesis_Invalid( )
    {
        Assert.Throws<FormulaFormatException>( () => _ = new Formula( "(1+1))+(1+1)(" ));
    }
    
    /// <summary>
    /// Testing the closing parenthesis rule with a closing parenthesis before an open parenthesis
    /// Expected outcome: FormulaFormatException
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestClosingParenthesisBeforeOpening_Invalid( )
    {
        Assert.Throws<FormulaFormatException>( () => _ = new Formula( "1) + ((1+1)" ));
    }

    // --- Tests for Balanced Parentheses Rule
    
    /// <summary>
    /// Testing that a valid arrangement of parenthesis passes
    /// Expected outcome: valid
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestBalancedParenthesis_Valid( )
    {
        _ = new Formula( "(((1+1))+(1+1))" );
    }
    
    /// <summary>
    /// Testing unbalanced parenthesis with an extra closing parenthesis
    /// Expected outcome: FormulaFormatException
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestUnbalancedParenthesisExtraClosing_Invalid( )
    {
        Assert.Throws<FormulaFormatException>( () => _ = new Formula( "(1+1)+(1+1))" ));
    }
    
    /// <summary>
    /// Testing unbalanced parenthesis with an extra opening parenthesis
    /// Expected outcome: FormulaFormatException
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestUnbalancedParenthesisExtraOpening_Invalid( )
    {
        Assert.Throws<FormulaFormatException>( () => _ = new Formula( "(1+1)+((1+1)" ));
    }

    // --- Tests for First Token Rule

    /// <summary>
    ///   <para>
    ///     Make sure a simple well-formed formula is accepted by the constructor (the constructor
    ///     should not throw an exception).
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is not expected to throw an exception, i.e., it succeeds.
    ///     In other words, the formula "1+1" is a valid formula which should not cause any errors.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestFirstTokenNumber_Valid( )
    {
        _ = new Formula( "1+1" );
    }
    
    /// <summary>
    /// Testing first token rule. This test is checking the first token can be a variable
    /// Expected outcome: valid
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestFirstTokenVariable_Valid( )
    {
        _ = new Formula( "a4" );
    }
    
    /// <summary>
    /// Testing first token rule. This tests that the first token can be an opening parenthesis
    /// Expected outcome: valid
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestFirstTokenOpenParenthesis_Valid( )
    {
        _ = new Formula( "(1)" );
    }
    
    
    /// <summary>
    /// Testing first token rule. This test checks for FormulaFormatException if the first token is an operator
    /// Expected outcome: FormulaFormatException
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestFirstTokenOperator_Invalid( )
    {
        Assert.Throws<FormulaFormatException>( () => _ = new Formula( "+ 1" ));
    }

    // --- Tests for  Last Token Rule ---
    
    /// <summary>
    /// Testing the last token rule, testing that numbers can be the last token.
    /// Expected outcome: Valid
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestLastTokenNumber_Valid( )
    {
        _ = new Formula( "a1 * 2" );
    }
    
    /// <summary>
    /// Testing the last token rule, testing that a variable can be the last token.
    /// Expected outcome: Valid
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestLastTokenVariable_Valid( )
    {
        _ = new Formula( "2 * a3" );
    }
    
    /// <summary>
    /// Testing the last token rule, testing that a closing parenthesis can be the last token.
    /// Expected outcome: Valid
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestLastTokenClosingParenthesis_Valid( )
    {
        _ = new Formula( "(a1 * 2)" );
    }
    
    /// <summary>
    /// Testing the last token rule, testing ending in an operator causes FormulaFormatException
    /// Expected outcome: FormulaFormatException
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestLastTokenOperator_Invalid( )
    {
        Assert.Throws<FormulaFormatException>( () => _ = new Formula( "a1 * 2 +" ));
    }

    // --- Tests for Parentheses/Operator Following Rule ---
    
    /// <summary>
    /// Testing the parenthesis / operator following rule on parenthesis. Testing that numbers do not cause FormulaFormatException
    /// 
    /// Expected outcome: Valid
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestParenthesisFollowingNumber_Valid( )
    {
        _ = new Formula( "(2)" );
    }
    
    /// <summary>
    /// Testing the parenthesis / operator following rule on parenthesis. Testing that variables do not cause FormulaFormatException
    /// Expected outcome: Valid
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestParenthesisFollowingVariable_Valid( )
    {
        _ = new Formula( "(a2)" );
    }
    
    /// <summary>
    /// Testing the parenthesis / operator following rule on parenthesis. Testing that open parenthesis do not cause FormulaFormatException
    /// Expected outcome: Valid
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestParenthesisFollowingOpenParenthesis_Valid( )
    {
        _ = new Formula( "((2))" );
    }
    
    /// <summary>
    /// Testing the parenthesis / operator following rule. Having a closing parenthesis right after
    /// an opening parenthesis should cause FormulaFormatException
    /// Expected outcome: FormulaFormatException
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestParenthesisFollowingClosingParenthesis_Invalid( )
    {
        Assert.Throws<FormulaFormatException>( () =>_ = new Formula( "()" ));
    }
    
    /// <summary>
    /// Testing the parenthesis / operator following rule. Having an operator right after
    /// an opening parenthesis should cause FormulaFormatException
    /// Expected outcome: FormulaFormatException
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestParenthesisFollowingOperator_Invalid( )
    {
        Assert.Throws<FormulaFormatException>( () =>_ = new Formula( "(+ 1)" ));
    }
    
    
    
    /// <summary>
    /// Testing the parenthesis / operator following rule on an operator. Testing that numbers do not cause FormulaFormatException
    /// Expected outcome: Valid
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestOperatorFollowingNumber_Valid( )
    {
        _ = new Formula( "2+2" );
    }
    
    /// <summary>
    /// Testing the parenthesis / operator following rule. Testing that variables do not cause FormulaFormatException
    /// Expected outcome: Valid
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestOperatorFollowingVariable_Valid( )
    {
        _ = new Formula( "2+b12" );
    }
    
    /// <summary>
    /// Testing the parenthesis / operator following rule. Testing that open parenthesis do not cause FormulaFormatException
    /// Expected outcome: Valid
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestOperatorFollowingOpenParenthesis_Valid( )
    {
        _ = new Formula( "2 + (3)" );
    }
    
    /// <summary>
    /// Testing the parenthesis / operator following rule. Having a closing parenthesis right after
    /// an operator should cause FormulaFormatException
    /// Expected outcome: FormulaFormatException
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestOperatorFollowingClosingParenthesis_Invalid( )
    {
        Assert.Throws<FormulaFormatException>( () =>_ = new Formula( "( 1 +)" ));
    }
    
    /// <summary>
    /// Testing the parenthesis / operator following rule. Having an operator right after
    /// an operator should cause FormulaFormatException
    /// Expected outcome: FormulaFormatException
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestOperatorFollowingOperator_Invalid( )
    {
        Assert.Throws<FormulaFormatException>( () =>_ = new Formula( "1 ++ 1" ));
    }
    

    // --- Tests for Extra Following Rule ---
    
    
    /// <summary>
    /// Testing the extra following rule. This test checks that a closing parenthesis can follow a number.
    /// Expected outcome: Valid
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestExtraFollowingNumberClosingParenthesis_Valid( )
    {
        _ = new Formula( "(a1 + 5)" );
    }
    
    /// <summary>
    /// Testing the extra following rule. This test checks that a closing parenthesis can follow a variable.
    /// Expected outcome: Valid
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestExtraFollowingVariableClosingParenthesis_Valid( )
    {
        _ = new Formula( "(5 + a45)" );
    }
    
    /// <summary>
    /// Testing the extra following rule. This test checks that a closing parenthesis can follow a closing parenthesis.
    /// Expected outcome: Valid
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestExtraFollowingClosingParenthesisClosingParenthesis_Valid( )
    {
        _ = new Formula( "(1 +(2))" );
    }
    
    
    
    
    /// <summary>
    /// Testing the extra following rule. This test checks that an operator can follow a number.
    /// Expected outcome: Valid
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestExtraFollowingNumberOperator_Valid( )
    {
        _ = new Formula( "10 + 1" );
    }
    
    /// <summary>
    /// Testing the extra following rule. This test checks that an operator can follow a variable.
    /// Expected outcome: Valid
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestExtraFollowingVariableOperator_Valid( )
    {
        _ = new Formula( "xyz12 / 2" );
    }
    
    /// <summary>
    /// Testing the extra following rule. This test checks that an operator can follow a closing parenthesis.
    /// Expected outcome: Valid
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestExtraFollowingClosingParenthesisOperator_Valid( )
    {
        _ = new Formula( "(1) + 12" );
    }
    
    
    
    /// <summary>
    /// Testing the extra following rule. This test checks following a number with a number causes FormulaFormatException
    /// Expected outcome: FormulaFormatException
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestExtraFollowingNumberWithNumber_Invalid( )
    {
        Assert.Throws<FormulaFormatException>( () =>_ = new Formula( " 5 5 + 12" ));
    }
    
    /// <summary>
    /// Testing the extra following rule. This test checks following a number with a variable causes FormulaFormatException
    /// Expected outcome: FormulaFormatException
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestExtraFollowingNumberWithVariable_Invalid( )
    {
        Assert.Throws<FormulaFormatException>( () =>_ = new Formula( "5 a1 + 6" ));
    }
    
    
    
    /// <summary>
    /// Testing the extra following rule. This test checks following a variable with a number causes FormulaFormatException
    /// Expected outcome: FormulaFormatException
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestExtraFollowingVariableWithNumber_Invalid( )
    {
        Assert.Throws<FormulaFormatException>( () =>_ = new Formula( "a1 5 + 6" ));
    }
    
    /// <summary>
    /// Testing the extra following rule. This test checks following a variable with a variable causes FormulaFormatException
    /// Expected outcome: FormulaFormatException
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestExtraFollowingVariableWithVariable_Invalid( )
    {
        Assert.Throws<FormulaFormatException>( () =>_ = new Formula( "a1 b2 + 5" ));
    }
    
    /// <summary>
    /// Testing the extra following rule. This test checks following a closing parenthesis with a number causes FormulaFormatException
    /// Expected outcome: FormulaFormatException
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestExtraFollowingClosingParenthesisWithNumber_Invalid( )
    {
        Assert.Throws<FormulaFormatException>( () =>_ = new Formula( "(1+1) 1 + 1" ));
    }
    
    /// <summary>
    /// Testing the extra following rule. This test checks following a closing parenthesis with a variable causes FormulaFormatException
    /// Expected outcome: FormulaFormatException
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestExtraFollowingClosingParenthesisWithVariable_Invalid( )
    {
        Assert.Throws<FormulaFormatException>( () =>_ = new Formula( "(1+1) d3 + 1" ));
    }
    
    
    //test methods for getVariables
    
    /// <summary>
    /// This test checks that having different cases for a variable, ie b2 and B2, will be represented as just B2
    /// Expected outcome: valid
    /// </summary>
    [TestMethod]
    public void FormulaGetVariables_TestCaseOnVariables_Valid( )
    {
        Formula var = new Formula("a1 + b2 + B2");
        Assert.HasCount(2, var.GetVariables());
    }
    
    
    /// <summary>
    /// This test checks that all variables are shown when using GetVariables() and that it is working properly
    /// Expected outcome: valid
    /// </summary>
    [TestMethod]
    public void FormulaGetVariables_TestGetVariables_Valid( )
    {
        Formula var = new Formula("a1 + b2 * (c3 / d4)");
        var varSet = var.GetVariables();
        string result = string.Join(", ", varSet);
        Assert.AreEqual("A1, B2, C3, D4", result);
    }
    
    
    /// <summary>
    /// This test checks that the toString() method is working how it is supposed to (checking variables for capitalization
    /// and doubles especially)
    /// Expected outcome: valid
    /// </summary>
    [TestMethod]
    public void FormulaToString_TestToString_Valid( )
    {
        Formula var = new Formula("a1 + 5.000 - B12 * (c3 / d4)");
        string result = var.ToString();
        Assert.AreEqual("A1+5-B12*(C3/D4)", result);
        
    }

    /// <summary>
    /// Tests that Evaluate correctly calculates basic addition
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void Evaluate_TestSimpleAddition_Valid()
    {
        Formula  var = new Formula("3 + 2");
        Assert.AreEqual(5.0, (double)var.Evaluate(o => 0));
    }
    
    /// <summary>
    /// Tests that Evaluate correctly calculates basic subtraction
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void Evaluate_TestSimpleSubtraction_Valid()
    {
        Formula  var = new Formula("3 - 2");
        Assert.AreEqual(1.0, (double)var.Evaluate(o => 0));
    }
    
    /// <summary>
    /// Tests that Evaluate correctly calculates basic multiplication
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void Evaluate_TestSimpleMultiplication_Valid()
    {
        Formula  var = new Formula("3 * 2");
        Assert.AreEqual(6.0, (double)var.Evaluate(o => 0));
    }
    
    /// <summary>
    /// Tests that Evaluate correctly calculates basic division
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void Evaluate_TestSimpleDivision_Valid()
    {
        Formula  var = new Formula("3 / 2");
        Assert.AreEqual(1.5, (double)var.Evaluate(o => 0));
    }
    /// <summary>
    /// Tests that Evaluate will cause FormulaError when divided by 0
    /// expected outcome: FormulaError
    /// </summary>
    [TestMethod]
    public void Evaluate_TestDivisionByZero_Invalid()
    {
        Formula var = new Formula("5/0");

        object result = var.Evaluate(s => 0);

        Assert.IsInstanceOfType(result, typeof(FormulaError));
        Assert.AreEqual("Division by zero", ((FormulaError)result).Reason);
    }
    
    /// <summary>
    /// Tests that Evaluate follows order of operations with multiplication
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void Evaluate_TestOrderOfOperationsMultiplication_Valid()
    {
        Formula  var = new Formula("3 + 2 * 4");
        Assert.AreEqual(11.0, (double)var.Evaluate(o => 0));
    }
    
    /// <summary>
    /// Tests that Evaluate follows order of operations with division
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void Evaluate_TestOrderOfOperationsDivision_Valid()
    {
        Formula  var = new Formula("2 + 4 / 2");
        Assert.AreEqual(4.0, (double)var.Evaluate(o => 0));
    }
    
    /// <summary>
    /// Tests that Evaluate correctly calculates multiple multiplications
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void Evaluate_TestMultipleMultiplications_Valid()
    {
        Formula  var = new Formula("3 * 2 * 9");
        Assert.AreEqual(54.0, (double)var.Evaluate(o => 0));
    }

    /// <summary>
    /// Tests that Evaluate correctly calculates multiple divisions
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void Evaluate_TestMultipleDivisions_Valid()
    {
        Formula var = new Formula("36 / 9 / 2");
        Assert.AreEqual(2.0, (double)var.Evaluate(o => 0));
    }
    
    /// <summary>
    /// Tests that Evaluate correctly gives precedence to operations in parentheses
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void Evaluate_TestParenthesisPrecedence_Valid()
    {
        Formula  var = new Formula("2*(3+3)");
        Assert.AreEqual(12.0, (double)var.Evaluate(o => 0));
    }
    
    /// <summary>
    /// Tests that Evaluate correctly gives precedence to operations in parentheses with order switched
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void Evaluate_TestParenthesisPrecedence2_Valid()
    {
        Formula  var = new Formula("(3+3)*2");
        Assert.AreEqual(12.0, (double)var.Evaluate(o => 0));
    }
    
    /// <summary>
    /// Tests that Evaluate correctly gives precedence to operations in parentheses
    /// a little bit more complex
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void Evaluate_TestParenthesisPrecedence3_Valid()
    {
        Formula  var = new Formula("4 + 2*(2 + 1)");
        Assert.AreEqual(10.0, (double)var.Evaluate(o => 0));
    }
    
    /// <summary>
    /// Tests that Evaluate correctly uses variables and returns correct value
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void Evaluate_TestVariables_Valid()
    {
        Formula  var = new Formula("A1 * 2");
        Assert.AreEqual(20.0, (double)var.Evaluate(o => 10));
    }
    
    /// <summary>
    /// Tests that Evaluate correctly uses multiple variables and returns correct value
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void Evaluate_TestMultipleVariables_Valid()
    {
        Formula  var = new Formula("A1 * B2");
        Assert.AreEqual(100.0, (double)var.Evaluate(o => 10));
    }
    
    /// <summary>
    /// Tests that division by a variable containing 0 causes FormulaError
    /// Expected outcome: FormulaError
    /// </summary>
    [TestMethod]
    public void Evaluate_TestVariableDivisionByZero_Invalid()
    {
        Formula var = new Formula("5/A1");

        object result = var.Evaluate(s => 0);

        Assert.IsInstanceOfType(result, typeof(FormulaError));
        Assert.AreEqual("Division by zero", ((FormulaError)result).Reason);
    }
    
    /// <summary>
    /// Tests that the value can be a negative number
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void Evaluate_TestNegativeOutput_Invalid()
    {
        Formula var = new Formula("5-10");
        Assert.AreEqual(-5.0, (double)var.Evaluate(o => 0));

        
    }


    /// <summary>
    /// Tests that == works
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void EqualsOperator_TestEqualsSameFormula_Valid()
    {
        Formula f1 = new Formula("2+3");
        Formula f2 = new Formula("2+3");
        Assert.IsTrue(f1 == f2);
    }
    
    /// <summary>
    /// Tests that == works and shows this is false
    /// expected outcome: false
    /// </summary>
    [TestMethod]
    public void EqualsOperator_TestEqualsDifferentFormula_False()
    {
        Formula f1 = new Formula("3+3");
        Formula f2 = new Formula("2+3");
        Assert.IsFalse(f1 == f2);
    }
    
    /// <summary>
    /// Tests that != works with different formulas
    /// expected outcome: true
    /// </summary>
    [TestMethod]
    public void NotEqualsOperator_TestNotEqualsDifferentFormula_Valid()
    {
        Formula f1 = new Formula("3+3");
        Formula f2 = new Formula("2+4");
        Assert.IsTrue(f1 != f2);
    }
    
    /// <summary>
    /// Tests that != works with same formulas
    /// expected outcome: false
    /// </summary>
    [TestMethod]
    public void NotEqualsOperator_TestNotEqualsSameFormula_False()
    {
        Formula f1 = new Formula("3+3");
        Formula f2 = new Formula("3+3");
        Assert.IsFalse(f1 != f2);
    }
    
    /// <summary>
    /// Tests that Equals method works with same formula
    /// </summary>
    [TestMethod]
    public void Equals_TestEqualsSameFormula_Valid()
    {
        Formula f1 = new Formula("2+3");
        Formula f2 = new Formula("2+3");
        Assert.IsTrue(f1.Equals(f2));
    }
    
    /// <summary>
    /// Tests that Equals method works with different formulas
    /// expected outcome: false
    /// </summary>
    [TestMethod]
    public void Equals_TestEqualsDifferentFormula_Valid()
    {
        Formula f1 = new Formula("2+3");
        Formula f2 = new Formula("3+3");
        Assert.IsFalse(f1.Equals(f2));
    }

    /// <summary>
    /// Tests that Equals returns false when one object is null
    /// expected outcome: false
    /// </summary>
    [TestMethod]
    public void Equals_Null_False()
    {
        Formula var = new Formula("2+2");
        
        Assert.IsFalse(var.Equals(null));
    }
    
    /// <summary>
    /// Tests that Equals returns false when one object is not a formula object
    /// expected outcome: false
    /// </summary>
    [TestMethod]
    public void Equals_NotFormulaObject_False()
    {
        Formula var = new Formula("2+2");
        
        Assert.IsFalse(var.Equals("2+2"));
    }
    
    /// <summary>
    /// Tests that 2 identical formulas have the same HashCode
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void GetHashCode_EqualFormulas_valid()
    {
        Formula f1 = new Formula("2+3");
        Formula f2 = new Formula("2+3");

        Assert.AreEqual(f1.GetHashCode(), f2.GetHashCode());
    }
    
    /// <summary>
    /// This tests that evaluate will work on only a variable
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void Evaluate_TestStandaloneVariable_Valid()
    {
        Formula var = new Formula("A1");

        Assert.AreEqual(10.0, (double)var.Evaluate(s => 10));
    }
    
    /// <summary>
    /// Tests variable division where the variable is not 0
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void Evaluate_TestVariableDivision_Valid()
    {
        Formula var = new Formula("10 / A1");

        Assert.AreEqual(5.0, (double)var.Evaluate(o => 2));
    }

    /// <summary>
    /// Tests what happens when a variable is undefined
    /// expected outcome: FormulaError
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    [TestMethod]
    public void Evaluate_TestUndefinedVariable_Invalid()
    {
        Formula var = new Formula("A1");
        
        object result = var.Evaluate(o =>
        {
            throw new ArgumentException();
        });
        
        Assert.IsInstanceOfType(result, typeof(FormulaError));
        Assert.AreEqual("Variable is undefined", ((FormulaError)result).Reason);
    }
    
    /// <summary>
    /// Tests what happens when you try to multiply with an undefined variable
    /// expected outcome: valid
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    [TestMethod]
    public void Evaluate_TestUndefinedVariableMultiplication_Invalid()
    {
        Formula var = new Formula("2*A1");

        object result = var.Evaluate(o =>
        {
            throw new ArgumentException();
        });

        Assert.IsInstanceOfType(result, typeof(FormulaError));
        Assert.AreEqual("Variable is undefined", ((FormulaError)result).Reason);
    }
    
    /// <summary>
    /// Tests what happens when you try to multiply with an undefined variable
    /// expected outcome: valid
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    [TestMethod]
    public void Evaluate_TestUndefinedVariableDivision_Invalid()
    {
        Formula var = new Formula("10 / A1");

        object result = var.Evaluate(o =>
        {
            throw new ArgumentException();
        });

        Assert.IsInstanceOfType(result, typeof(FormulaError));
        Assert.AreEqual("Variable is undefined", ((FormulaError)result).Reason);
    }
    
    /// <summary>
    /// Tests subtraction inside parenthesis
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void Evaluate_TestSubtractionInsideParentheses_Valid()
    {
        Formula var = new Formula("(5 - 2)");
        Assert.AreEqual(3.0, (double)var.Evaluate(o => 0));
    }
    
    /// <summary>
    /// tests division after parentheses
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void Evaluate_TestDivisionAfterParentheses_Valid()
    {
        Formula var = new Formula("(3+3)/2");
        Assert.AreEqual(3.0, (double)var.Evaluate(o => 0));
    }

    /// <summary>
    /// tests that when a number in parentheses that is being used to divide equals zero,
    /// it results in FormulaError
    /// expected outcome: FormulaError
    /// </summary>
    [TestMethod]
    public void Evaluate_TestDivisionWithZeroWithParenthesis_Invalid()
    {
        Formula var = new Formula("10 / (1 - 1)");
        object result = var.Evaluate(o => 0);

        Assert.IsInstanceOfType(result, typeof(FormulaError));
        Assert.AreEqual("Division by zero", ((FormulaError)result).Reason);
    }
    
    
    /// <summary>
    /// Tests what happens if you only have a value within parentheses
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void Evaluate_TestJustParenthesis_Valid()
    {
        Formula var = new Formula("(5)");
        Assert.AreEqual(5.0, (double)var.Evaluate(s => 0));
    }

    /// <summary>
    /// Tests when there is division before parentheses
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void Evaluate_TestDivisionBeforeParenthesis_Valid()
    {
        Formula var = new Formula("50/(8+2)");
        Assert.AreEqual(5.0, (double)var.Evaluate(s => 0));
    }
    
    /// <summary>
    /// Tests multiple additions
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void Evaluate_TestMultipleAdditions_Valid()
    {
        Formula var = new Formula("2+3+4+5");
        Assert.AreEqual(14.0, (double)var.Evaluate(s => 0));
    }
    
    /// <summary>
    /// Tests multiple subtractions
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void Evaluate_TestMultipleSubtractions_Valid()
    {
        Formula var = new Formula("10-2-3-4");
        Assert.AreEqual(1.0, (double)var.Evaluate(s => 0));
    }
    
    /// <summary>
    /// Formula Constructor test. Tests when there is more opening than closing parentheses
    /// expected outcome: FormulaFormatException
    /// </summary>
    [TestMethod]
    public void Formula_MoreClosingThanOpening_Invalid()
    {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("1 + 1)"));
    }
    
    
}