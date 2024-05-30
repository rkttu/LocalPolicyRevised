using LocalPolicyRevised.Interop;
using LocalPolicyRevised.Resources;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace LocalPolicyRevised
{
    /// <summary>
    /// Provides helper methods for working with Group Policy.
    /// </summary>
    internal static class Helpers
    {
        /// <summary>
        /// Gets an instance of the <see cref="IGroupPolicyObject"/> interface.
        /// </summary>
        /// <returns></returns>
        public static IGroupPolicyObject GetGroupPolicyObjectInstance()
            => new Func<IGroupPolicyObject>(() => (IGroupPolicyObject)new GPClass()).WithSingleThreadedApartmentCheck();

        public static T WithSingleThreadedApartmentCheck<T>(this Func<T> operation)
        {
            try
            {
                if (operation == null)
                    throw new ArgumentNullException(nameof(operation));

                return operation.Invoke();
            }
            catch (InvalidCastException e)
            {
                if (Thread.CurrentThread.GetApartmentState() != 0)
                    throw new RequiresSingleThreadedApartmentException(e);

                throw e;
            }
        }

        public static void TryCatch(Func<uint> operation, string messageTemplate, params object[] messageArgs)
        {
            if (operation == null)
                throw new ArgumentNullException(nameof(operation));

            var result = operation.Invoke();

            if (result == 0)
                return;

            throw new GroupPolicyException(
                string.Format(MessageResources.GroupPolicyExceptionMessage, string.Format(messageTemplate, messageArgs), result),
                new Win32Exception((int)result));
        }

        public static string GetString(this Func<StringBuilder, int, uint> func, string errorMessage)
        {
            var buffer = new StringBuilder();
            TryCatch(() => func(buffer, 1024), errorMessage);
            return buffer.ToString();
        }

        public static GroupPolicyValue GetPolicySettingInternal(GroupPolicySection section, string registryKeyPath, string registryValueName, Guid? thisGuid = default)
        {
            var gpo = new ComputerGroupPolicyObject(thisGuid: thisGuid);

            object registryValue = default;
            RegistryValueKind registryValueKind = default;

            using (var rootRegistryKey = gpo.GetRootRegistryKey(section))
            {
                // Data can't be null so we can use this value to indicate key must be delete
                using (var subKey = rootRegistryKey.OpenSubKey(registryKeyPath, true))
                {
                    if (subKey != null)
                    {
                        registryValue = subKey.GetValue(registryValueName);

                        if (subKey.GetValueNames().Contains(registryValueName, StringComparer.OrdinalIgnoreCase))
                            registryValueKind = subKey.GetValueKind(registryValueName);
                    }
                }
            }

            return new GroupPolicyValue(
                section, registryKeyPath, registryValueName,
                registryValue, registryValueKind);
        }

        public static void SetPolicySettingInternal(GroupPolicySection section, string registryKeyPath, string registryValueName, object newRegistryValue, RegistryValueKind newRegistryValueKind = default, Guid? thisGuid = default)
        {
            var gpo = new ComputerGroupPolicyObject(thisGuid: thisGuid);

            using (var rootRegistryKey = gpo.GetRootRegistryKey(section))
            {
                // Data can't be null so we can use this value to indicate key must be delete
                if (newRegistryValue == null)
                {
                    using (var subKey = rootRegistryKey.OpenSubKey(registryKeyPath, true))
                    {
                        if (subKey != null && subKey.GetValueNames().Contains(registryValueName, StringComparer.OrdinalIgnoreCase))
                            subKey.DeleteValue(registryValueName);
                    }
                }
                else
                {
                    using (var subKey = rootRegistryKey.CreateSubKey(registryKeyPath))
                    {
                        subKey.SetValue(registryValueName, newRegistryValue, newRegistryValueKind);
                    }
                }
            }

            gpo.Save();
        }

        public static T GetAssemblyAttribute<T>() where T : Attribute
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            return GetAssemblyAttribute<T>(assembly);
        }

        public static T GetAssemblyAttribute<T>(Assembly assembly) where T : Attribute
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(T), inherit: true);

            if (attributes == null || attributes.Length == 0)
                return null;

            return (T)attributes.First();
        }

        public static Guid GetAssemblyGuid()
        {
            var guidAttr = GetAssemblyAttribute<GuidAttribute>();

            if (guidAttr == null)
                throw new ApplicationException(MessageResources.NoGuidAttribInExecutingAssembly);

            return new Guid(guidAttr.Value);
        }
    }
}
