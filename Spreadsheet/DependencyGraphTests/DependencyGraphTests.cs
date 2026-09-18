
namespace DependencyGraphTests;

using DependencyGraph;

/// <summary>
///   This is a test class for DependencyGraphTest and is intended
///   to contain all DependencyGraphTest Unit Tests
/// </summary>
[TestClass]
public class DependencyGraphTests
{
    /// <summary>
    /// Tests DependencyGraph by adding and removing a bunch of Dependencies. Tests for
    /// correctness as well as being completed in a timely manner
    /// </summary>
    [TestMethod]
    [Timeout( 2000, CooperativeCancellation = true)]  // 2 second run time limit
    public void StressTest()
    {
        DependencyGraph dg = new();

        // A bunch of sample strings to be added to DependencyGraph
        const int size = 200;
        string[] letters = new string[size];
        for ( int i = 0; i < size; i++ )
        {
            letters[i] = string.Empty + ( (char) ( 'a' + i ) );
        }

        // HashSet of the correct dependents and dependees to test with
        HashSet<string>[] dependents = new HashSet<string>[size];
        HashSet<string>[] dependees = new HashSet<string>[size];
        for ( int i = 0; i < size; i++ )
        {
            dependents[i] = [];
            dependees[i] = [];
        }

        // Adds Dependencies to DependencyGraph
        for ( int i = 0; i < size; i++ )
        {
            for ( int j = i + 1; j < size; j++ )
            {
                dg.AddDependency( letters[i], letters[j] );
                dependents[i].Add( letters[j] );
                dependees[j].Add( letters[i] );
            }
        }

        // Removes all Dependencies
        for ( int i = 0; i < size; i++ )
        {
            for ( int j = i + 4; j < size; j += 4 )
            {
                dg.RemoveDependency( letters[i], letters[j] );
                dependents[i].Remove( letters[j] );
                dependees[j].Remove( letters[i] );
            }
        }

        // Adds back all of the dependencies previously added then removed
        for ( int i = 0; i < size; i++ )
        {
            for ( int j = i + 1; j < size; j += 2 )
            {
                dg.AddDependency( letters[i], letters[j] );
                dependents[i].Add( letters[j] );
                dependees[j].Add( letters[i] );
            }
        }

        // Again removes dependencies
        for ( int i = 0; i < size; i += 2 )
        {
            for ( int j = i + 3; j < size; j += 3 )
            {
                dg.RemoveDependency( letters[i], letters[j] );
                dependents[i].Remove( letters[j] );
                dependees[j].Remove( letters[i] );
            }
        }

        // Checks for correctness in DependencyGraph and backing HashSets
        for ( int i = 0; i < size; i++ )
        {
            Assert.IsTrue( dependents[i].SetEquals( new HashSet<string>( dg.GetDependents( letters[i] ) ) ) );
            Assert.IsTrue( dependees[i].SetEquals( new HashSet<string>( dg.GetDependees( letters[i] ) ) ) );
        }
    }

    /// <summary>
    /// Test that with no dependencies, the depency size is 0
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void DependencyGraph_GraphSizeZero_Valid()
    {
        DependencyGraph dg = new DependencyGraph();
        Assert.AreEqual(0, dg.Size);
    }
    
    /// <summary>
    /// tests when adding one dependency, that size is updated
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void DependencyGraph_GraphSizeOne_Valid()
    {
        DependencyGraph dg = new DependencyGraph();
        dg.AddDependency("a1", "a2");
        Assert.AreEqual(1, dg.Size);
    }
    
    /// <summary>
    /// Tests when adding a duplicate dependency, size is NOT
    /// updated
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void DependencyGraph_GraphSizeDuplicateOne_Valid()
    {
        DependencyGraph dg = new DependencyGraph();
        dg.AddDependency("a1", "a2");
        dg.AddDependency("a1", "a2");
        Assert.AreEqual(1, dg.Size);
    }
    
    /// <summary>
    /// Tests that getDependees returns the correct variables
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void DependencyGraph_TestGetDependees_Valid()
    {
        DependencyGraph dg = new DependencyGraph();
        dg.AddDependency("a1", "a2");
        dg.AddDependency("b1", "a2");
        IEnumerable<string> result = dg.GetDependees("a2");
        var enumerable = result.ToList();
        Assert.AreEqual("a1", enumerable.First());
        Assert.AreEqual("b1", enumerable.Last());
    }
    
    /// <summary>
    /// Tests that getDependents returns the correct variables
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void DependencyGraph_TestGetDependents_Valid()
    {
        DependencyGraph dg = new DependencyGraph();
        dg.AddDependency("a1", "a2");
        dg.AddDependency("a1", "b2");
        IEnumerable<string> result = dg.GetDependents("a1");
        var enumerable = result.ToList();
        Assert.AreEqual("a2", enumerable.First());
        Assert.AreEqual("b2", enumerable.Last());
    }
    
    /// <summary>
    /// Tests that removing a depency decreases size
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void DependencyGraph_TestRemoveDependentsSize_Valid()
    {
        DependencyGraph dg = new DependencyGraph();
        dg.AddDependency("a1", "a2");
        dg.AddDependency("a1", "b2");
        dg.RemoveDependency("a1", "a2");
        Assert.AreEqual(1, dg.Size);
    }
    
    /// <summary>
    /// Tests that removing a non-existent dependent does nothing
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void DependencyGraph_TestRemoveNonDependents_Valid()
    {
        DependencyGraph dg = new DependencyGraph();
        dg.AddDependency("a1", "a2");
        dg.RemoveDependency("a1", "b2");
        Assert.AreEqual(1, dg.Size);
        IEnumerable<string> result = dg.GetDependents("a1");
        var enumerable = result.ToList();
        Assert.AreEqual(1, enumerable.Count());
        Assert.AreEqual("a2", enumerable.First());
        
    }
    
    /// <summary>
    /// Tests replaceDependents works and that both HashSets are updated
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void DependencyGraph_TestReplaceDependents_Valid()
    {
        DependencyGraph dg = new DependencyGraph();
        dg.AddDependency("a1", "a2");
        dg.AddDependency("a1", "a3");

        List<string> newDependents = new List<string>();
        newDependents.Add("b1");
        newDependents.Add("b2");
        dg.ReplaceDependents("a1", newDependents);
        IEnumerable<string> result = dg.GetDependents("a1");
        var enumerable = result.ToList();
        Assert.AreEqual("b2", enumerable.First());
        Assert.AreEqual("b1", enumerable.Last());
        
        Assert.DoesNotContain("a2", enumerable);
        Assert.DoesNotContain("a3", enumerable);
        
        Assert.IsTrue(dg.GetDependees("b1").Contains("a1"));
        Assert.IsTrue(dg.GetDependees("b2").Contains("a1"));
    }
    
    /// <summary>
    /// Tests replaceDependees works and that both HashSets are updated
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void DependencyGraph_TestReplaceDependees_Valid()
    {
        DependencyGraph dg = new DependencyGraph();
        dg.AddDependency("a2", "a1");
        dg.AddDependency("a3", "a1");

        List<string> newDependees = new List<string>();
        newDependees.Add("b1");
        newDependees.Add("b2");
        dg.ReplaceDependees("a1", newDependees);
        IEnumerable<string> result = dg.GetDependees("a1");
        var enumerable = result.ToList();
        Assert.AreEqual("b2", enumerable.First());
        Assert.AreEqual("b1", enumerable.Last());
        
        Assert.DoesNotContain("a2", enumerable);
        Assert.DoesNotContain("a3", enumerable);
        
        Assert.IsTrue(dg.GetDependents("b1").Contains("a1"));
        Assert.IsTrue(dg.GetDependents("b2").Contains("a1"));

    }
    
    /// <summary>
    /// Tests that the dependee and dependent can be the same value
    /// and everything works as intended
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void DependencyGraph_TestDependeeDependentAreSame_Valid()
    {
        DependencyGraph dg = new DependencyGraph();
        dg.AddDependency("a1", "a1");
        
        Assert.AreEqual(1, dg.Size);
        Assert.IsTrue(dg.HasDependees("a1"));
        Assert.IsTrue(dg.HasDependents("a1"));

        IEnumerable<string> resultDependents = dg.GetDependents("a1");
        var enumerableDependents = resultDependents.ToList();
        Assert.AreEqual("a1", enumerableDependents.First());
        
        IEnumerable<string> resultDependees = dg.GetDependees("a1");
        var enumerableDependees = resultDependees.ToList();
        Assert.AreEqual("a1", enumerableDependees.First());
        

    }
    
    /// <summary>
    /// this test checks that an empty dependent that was never added
    /// returns false 
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void DependencyGraph_TestDependentIsEmpty_Valid()
    {
        DependencyGraph dg = new DependencyGraph();
        
        Assert.IsFalse(dg.HasDependents("a1"));

    }
    
    /// <summary>
    /// this test checks that an empty dependee that was never added
    /// returns false 
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void DependencyGraph_TestDependeeIsEmpty_Valid()
    {
        DependencyGraph dg = new DependencyGraph();
        
        Assert.IsFalse(dg.HasDependees("a1"));

    }
    
    /// <summary>
    /// this test checks that a dependent that was added then removed
    /// properly returns with no dependents
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void DependencyGraph_TestDependentWhenEmpty_Valid()
    {
        DependencyGraph dg = new DependencyGraph();
        dg.AddDependency("a1", "b1");
        dg.RemoveDependency("a1", "b1");
        
        Assert.IsFalse(dg.HasDependents("a1"));

    }
    
    /// <summary>
    /// this test checks that a dependee that was added then removed
    /// properly returns with no dependees
    /// expected outcome: valid
    /// </summary>
    [TestMethod]
    public void DependencyGraph_TestDependeeWhenEmpty_Valid()
    {
        DependencyGraph dg = new DependencyGraph();
        dg.AddDependency("a1", "b1");
        dg.RemoveDependency("a1", "b1");
        
        Assert.IsFalse(dg.HasDependees("b1"));

    }
}
