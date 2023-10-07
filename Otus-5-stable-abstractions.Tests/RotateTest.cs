using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Otus_5_stable_abstractions.Actions;
using Otus_5_stable_abstractions.Adapters;
using Otus_5_stable_abstractions.TestObjects;

namespace Otus_5_stable_abstractions
{
    [TestClass]
    public class RotateTest
    {
        [TestMethod]
        public void RotateSimpleObject()
        {
            // Arrange
            var ship = new Ship(position: new[]{0, 0}, velocity: new[]{0, 0}, direction: 45, angularVelocity: -90);
            
            // Act
            new Rotate(ship).Execute();

            // Assert
            Assert.AreEqual(-45, ship.Direction, "Incorrect direction");
        }
        
        [TestMethod]
        public void RotateUniversalObject()
        {
            // Arrange
            var ship = new UShip(
                new Dictionary<string, object>
                {
                    {"Position", new[]{0, 0}},
                    {"Velocity", new[]{0, 0}},
                    {"Direction", 45},
                    {"AngularVelocity", -(90+360)}
                });
            
            // Act
            new Rotate(new RotatableAdapter(ship)).Execute();

            // Assert
            Assert.AreEqual(-45, ship.GetProperty("Direction"), "Incorrect direction");
        }
        
        [TestMethod]
        public void RotateNonDirectionableObject()
        {
            // Arrange
            var ship = new UShip(
                new Dictionary<string, object>
                {
                    {"Position", new[]{0, 0}},
                    {"Velocity", new[]{0, 0}},
                    {"AngularVelocity", -(90+360)}
                });

            // Assert
            Assert.ThrowsException<ArgumentException>(() => new Rotate(new RotatableAdapter(ship)).Execute());
        }
        
        [TestMethod]
        public void RotateNonAngularVelocitibleObject()
        {
            // Arrange
            var ship = new UShip(
                new Dictionary<string, object>
                {
                    {"Position", new[]{0, 0}},
                    {"Velocity", new[]{0, 0}},
                    {"Direction", 45},
                });

            // Assert
            Assert.ThrowsException<ArgumentException>(() => new Rotate(new RotatableAdapter(ship)).Execute());
        }
        
        [TestMethod]
        public void RotateNonRotatableObject()
        {
            // Arrange
            var ship = new NonRotatableShip(new Dictionary<string, object>
                {
                    {"Position", new[]{0, 0}},
                    {"Velocity", new[]{0, 0}},
                    {"Direction", 45},
                    {"AngularVelocity", -(90+360)}
                });
            
            // Assert
            Assert.ThrowsException<ArgumentException>(() => new Rotate(new RotatableAdapter(ship)).Execute());
        }
    }
}
