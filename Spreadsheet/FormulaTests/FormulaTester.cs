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
    public void FormulaGetVariables_TestCaseOnVariables_valid( )
    {
        Formula var = new Formula("a1 + b2 + B2");
        Assert.HasCount(2, var.GetVariables());
    }
    
    
    /// <summary>
    /// This test checks that all variables are shown when using GetVariables() and that it is working properly
    /// Expected outcome: valid
    /// </summary>
    [TestMethod]
    public void FormulaGetVariables_TestGetVariables_valid( )
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
    public void FormulaToString_TestToString_valid( )
    {
        Formula var = new Formula("a1 + 5.000 - B12 * (c3 / d4)");
        string result = var.ToString();
        Assert.AreEqual("A1+5-B12*(C3/D4)", result);
        
    }
    
    
}