using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class UnitTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void MaxHealthTest()
    {
        // Arrange
        GameObject testGo = new GameObject();
        Health health = testGo.AddComponent<Health>();
        
        // Act
        health.Heal(health.MaxHealth);
        
        // Assert
        Assert.AreEqual(health.MaxHealth, health.CurrentHealth);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    // [UnityTest]
    // public IEnumerator MaxHealthTest()
    // { 
    //     // Arrange
    //     GameObject testGo = new GameObject();
    //     Health health = testGo.AddComponent<Health>();
    //     // Use the Assert class to test conditions.
    //     // Use yield to skip a frame.
    //     yield return null;
    // }
}
