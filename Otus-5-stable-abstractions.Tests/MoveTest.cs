using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Otus_5_stable_abstractions.Actions;
using Otus_5_stable_abstractions.Adapters;
using Otus_5_stable_abstractions.TestObjects;

namespace Otus_5_stable_abstractions
{
    [TestClass]
    public class MoveTest
    {
        [TestMethod]
        public void MoveSimpleObject()
        {
            // Arrange
            var ship = new Ship(position: new[]{12, 5}, velocity: new[]{-7, 3}, direction: 0, angularVelocity: 0);
            
            // Act
            new Move(ship).Execute();

            // Assert
            CollectionAssert.AreEqual(new[]{5, 8}, ship.Position, "Incorrect position");
        }
        
        [TestMethod]
        public void MoveUniversalObject()
        {
            // Arrange
            var ship = new UShip(
                new Dictionary<string, object>
                {
                    {"Position", new[]{12, 5}},
                    {"Velocity", new[]{-7, 3}},
                    {"Direction", 0},
                    {"AngularVelocity", 0}
                });
            
            // Act
            new Move(new MovableAdapter(ship)).Execute();

            // Assert
            CollectionAssert.AreEqual(new[]{5, 8}, (int[])ship.GetProperty("Position"), "Incorrect position");
        }
        
        [TestMethod]
        public void MoveNonPositionableObject()
        {
            // Arrange
            var ship = new UShip(
                new Dictionary<string, object>
                {
                    {"Velocity", new[]{-7, 3}},
                    {"Direction", 0},
                    {"AngularVelocity", 0}
                });

            // Assert
            Assert.ThrowsException<ArgumentException>(() => new Move(new MovableAdapter(ship)).Execute());
        }
        
        [TestMethod]
        public void MoveNonVelocitibleObject()
        {
            // Arrange
            var ship = new UShip(
                new Dictionary<string, object>
                {
                    {"Position", new[]{12, 5}},
                    {"Direction", 0},
                    {"AngularVelocity", 0}
                });

            // Assert
            Assert.ThrowsException<ArgumentException>(() => new Move(new MovableAdapter(ship)).Execute());
        }
        
        [TestMethod]
        public void MoveNonMovableObject()
        {
            // Arrange
            var ship = new NonMovableShip(
                new Dictionary<string, object>
                {
                    {"Position", new[]{12, 5}},
                    {"Velocity", new[]{-7, 3}},
                    {"Direction", 0},
                    {"AngularVelocity", 0}
                });
            
            // Assert
            Assert.ThrowsException<ArgumentException>(() => new Move(new MovableAdapter(ship)).Execute());
        }
    }
}
