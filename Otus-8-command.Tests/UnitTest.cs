using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Otus_8_command.Adapters;
using Otus_8_command.Commands;
using Otus_8_command.Interfaces;

namespace Otus_8_command.Tests;

[TestClass]
public class UnitTest
{
    [TestMethod]
    public void CheckFuelCommand_Positive()
    {
        // Arrange
        var ship = new UShip(
            new Dictionary<string, object>
            {
                {"FuelAmount", 10},
                {"FuelConsumptionRate", 5}
            });
        
        // Act
        new CheckFuelCommand(new FuelPoweredAdapter(ship)).Execute();
    }

    [TestMethod]
    public void CheckFuelCommand_Negative()
    {
        // Arrange
        var ship = new UShip(
            new Dictionary<string, object>
            {
                {"FuelAmount", 5}, // Fuel amount is not enough
                {"FuelConsumptionRate", 10}
            });
        
        // Act
        Assert.ThrowsException<CommandException>(() => 
            new CheckFuelCommand(new FuelPoweredAdapter(ship)).Execute());
    }

    [TestMethod]
    public void BurnFuelCommand()
    {
        // Arrange
        var ship = new UShip(
            new Dictionary<string, object>
            {
                {"FuelAmount", 10},
                {"FuelConsumptionRate", 2}
            });
        
        // Act
        new BurnFuelCommand(new FuelPoweredAdapter(ship)).Execute();

        // Assert
        Assert.AreEqual(8, (int)ship.GetProperty("FuelAmount"), "Incorrect fuel amount");
    }

    [TestMethod]
    public void MacroCommand_Move_Positive()
    {
        // Arrange
        // Зададим скорость так, чтобы проекции скорости были равны 10, 10 для упрощения проверки результата
        var ship = new UShip(
            new Dictionary<string, object>
            {
                {"FuelAmount", 10},
                {"FuelConsumptionRate", 2},
                {"Position", new[]{12, 5}},
                {"Velocity", (int)Math.Round(10*Math.Sqrt(2))}, 
                {"Direction", 45}
            });

        // Act
        new MacroCommand(new ICommand[] {
            new CheckFuelCommand(new FuelPoweredAdapter(ship)),
            new MoveCommand(new MovableAdapter(ship)),
            new BurnFuelCommand(new FuelPoweredAdapter(ship))
        }).Execute();

        // Assert
        Assert.AreEqual(8, (int)ship.GetProperty("FuelAmount"), "Incorrect fuel amount");
        CollectionAssert.AreEqual(new[]{22, 15}, (int[])ship.GetProperty("Position"), "Incorrect position");
    }

    [TestMethod]
    public void MacroCommand_Move_Negative()
    {
        // Arrange
        var ship = new UShip(
            new Dictionary<string, object>
            {
                {"FuelAmount", 5}, // Fuel amount is not enough
                {"FuelConsumptionRate", 10},
                {"Position", new[]{12, 5}},
                {"Velocity", 10}, 
                {"Direction", -45}
            });

        // Act
        Assert.ThrowsException<CommandException>(() => 
            new MacroCommand(new ICommand[] {
                new CheckFuelCommand(new FuelPoweredAdapter(ship)),
                new MoveCommand(new MovableAdapter(ship)),
                new BurnFuelCommand(new FuelPoweredAdapter(ship))
            }).Execute());
    }

    [TestMethod]
    public void ChangeVelocityCommand()
    {
        // Arrange
        // Зададим скорость так, чтобы проекции скорости были равны 10, -10 для упрощения проверки результата
        var ship = new UShip(
            new Dictionary<string, object>
            {
                {"Velocity", (int)Math.Round(10*Math.Sqrt(2))}, 
                {"Direction", -45},
                {"AngularVelocity", 90}
            });
        
        // Act
        new RotateCommand(new RotatableAdapter(ship)).Execute();
        new ChangeVelocityCommand(new MovableAdapter(ship), new RotatableAdapter(ship)).Execute();

        // Assert
        Assert.AreEqual(45, (int)ship.GetProperty("Direction"), "Incorrect direction");
        CollectionAssert.AreEqual(new[]{10, 10}, (int[])ship.GetProperty("VelocityProjections"), "Incorrect velocity projections");
    }

    [TestMethod]
    public void ChangeVelocityCommand_doesnot_move()
    {
        // Arrange
        // Зададим скорость так, чтобы проекции скорости были равны 10, -10 для упрощения проверки результата
        var ship = new UShip(
            new Dictionary<string, object>
            {
                {"Velocity", 0}, 
                {"Direction", 45},
                {"AngularVelocity", -55}
            });
        
        // Act
        new RotateCommand(new RotatableAdapter(ship)).Execute();
        new ChangeVelocityCommand(new MovableAdapter(ship), new RotatableAdapter(ship)).Execute();

        // Assert
        Assert.AreEqual(-10, (int)ship.GetProperty("Direction"), "Incorrect direction");
        CollectionAssert.AreEqual(new[]{0, 0}, (int[])ship.GetProperty("VelocityProjections"), "Incorrect velocity projections");
    }
    
    [TestMethod]
    public void MacroCommand_Rotate_ChangeVelocity()
    {
        // Arrange
        // Зададим скорость так, чтобы проекции скорости были равны -10, 10 для упрощения проверки результата
        var ship = new UShip(
            new Dictionary<string, object>
            {
                {"Velocity", (int)Math.Round(10*Math.Sqrt(2))}, 
                {"Direction", 135},
                {"AngularVelocity", -90}
            });
        
        // Act
        new MacroCommand(new ICommand[] {
            new RotateCommand(new RotatableAdapter(ship)),
            new ChangeVelocityCommand(new MovableAdapter(ship), new RotatableAdapter(ship))
        }).Execute();

        // Assert
        Assert.AreEqual(45, (int)ship.GetProperty("Direction"), "Incorrect direction");
        CollectionAssert.AreEqual(new[]{10, 10}, (int[])ship.GetProperty("VelocityProjections"), "Incorrect velocity projections");
    }

    [TestMethod]
    public void RotateWithChangeVelocityCommand()
    {
        // Arrange
        // Зададим скорость так, чтобы проекции скорости были равны -10, -10 для упрощения проверки результата
        var ship = new UShip(
            new Dictionary<string, object>
            {
                {"Velocity", (int)Math.Round(10*Math.Sqrt(2))}, 
                {"Direction", -135},
                {"AngularVelocity", 180}
            });
        
        // Act
        new RotateWithChangeVelocityCommand(new MovableAdapter(ship), new RotatableAdapter(ship)).Execute();

        // Assert
        CollectionAssert.AreEqual(new[]{10, 10}, (int[])ship.GetProperty("VelocityProjections"), "Incorrect velocity projections");
    }
}