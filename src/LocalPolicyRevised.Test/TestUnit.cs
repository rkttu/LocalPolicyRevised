using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LocalPolicyRevised.Test
{
    [TestClass]
    public class TestUnit
    {
        [TestMethod]
        public void TestMethod()
        {
            // Arrange
            var section = GroupPolicySection.Machine;
            var registryKeyPath = @"Software\Policies\Microsoft\Windows\HomeGroup";
            var registryValueName = "DisableHomeGroup";

            // This GUID has no special meaning; It's a random value.
            var thisGuid = Guid.Parse("{544D5306-4738-4F2F-AC09-0D4A12FF9244}");

            // Act - Lookup the policy
            var original = ComputerGroupPolicyObject.GetPolicyValue(
                section, registryKeyPath, registryValueName, thisGuid);

            // Act - Set a value for the polilcy
            // If settingValue is null, the policy will be deleted
            ComputerGroupPolicyObject.SetPolicySetting(
                section, registryKeyPath, registryValueName,
                0, thisGuid: thisGuid);
            ComputerGroupPolicyObject.SetPolicySetting(
                section, registryKeyPath, registryValueName,
                1, thisGuid: thisGuid);

            // Act - Deleting the policy
            ComputerGroupPolicyObject.DeletePolicySetting(
                section, registryKeyPath, registryValueName,
                thisGuid: thisGuid);

            var current = ComputerGroupPolicyObject.GetPolicyValue(
                section, registryKeyPath, registryValueName,
                thisGuid);

            // Act - Set the policy back to its original value
            ComputerGroupPolicyObject.SetPolicySetting(
                section, registryKeyPath, registryValueName,
                original, thisGuid: thisGuid);

            // Assert
            Assert.AreEqual(default, current);
        }
    }
}
