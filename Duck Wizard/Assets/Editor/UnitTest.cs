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
        health.Initialize();
        health.Heal(999999);
        Debug.Log("Current health: " + health.GetCurrentHealth());
        Debug.Log("Max health: " + health.GetMaxHealth());
        
        // Assert
        Assert.AreEqual(health.GetMaxHealth(), health.GetCurrentHealth());
        
        Object.DestroyImmediate(testGo);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // // `yield return null;` to skip a frame.
    // [UnityTest]
    // public IEnumerator MaxHealthTest()
    // {
    //     // 1. Arrange
    //     GameObject testGo = new GameObject();
    //     Health health = testGo.AddComponent<Health>();
    //
    //     // 2. Act
    //     // Yield a frame so Unity executes Awake()
    //     yield return null;
    //
    //     health.Heal(999999);
    //     Debug.Log("Current health: " + health.GetCurrentHealth());
    //
    //     // 3. Assert
    //     Assert.AreEqual(health.GetMaxHealth(), health.GetCurrentHealth());
    //
    //     Object.DestroyImmediate(testGo);
    // }
}
