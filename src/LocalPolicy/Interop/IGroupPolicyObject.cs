using System;
using System.Runtime.InteropServices;
using System.Text;

namespace LocalPolicy.Interop
{
	/// <summary>
	/// Represents a Group Policy Object.
	/// </summary>
	[ComImport]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("EA502723-A23D-11d1-A7D3-0000F87571E3")]
	public interface IGroupPolicyObject
	{
		/// <summary>
		/// Opens the specified Group Policy Object.
		/// </summary>
		/// <param name="domainName">
		/// The name of the domain where the GPO is located.
		/// </param>
		/// <param name="displayName">
		/// The display name of the GPO.
		/// </param>
		/// <param name="flags">
		/// The flags to use when opening the GPO.
		/// </param>
		/// <returns></returns>
		uint New([MarshalAs(UnmanagedType.LPWStr)] string domainName, [MarshalAs(UnmanagedType.LPWStr)] string displayName, uint flags);

		/// <summary>
		/// Opens the specified Group Policy Object.
		/// </summary>
		/// <param name="path">
		/// The path to the GPO.
		/// </param>
		/// <param name="flags">
		/// The flags to use when opening the GPO.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is S_OK.
		/// </returns>
		uint OpenDSGPO([MarshalAs(UnmanagedType.LPWStr)] string path, uint flags);

		/// <summary>
		/// Opens the local machine Group Policy Object.
		/// </summary>
		/// <param name="flags">
		/// The flags to use when opening the GPO.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is S_OK.
		/// </returns>
		uint OpenLocalMachineGPO(uint flags);

		/// <summary>
		/// Opens the Group Policy Object on a remote machine.
		/// </summary>
		/// <param name="computerName">
		/// The name of the remote computer.
		/// </param>
		/// <param name="flags">
		/// The flags to use when opening the GPO.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is S_OK.
		/// </returns>
		uint OpenRemoteMachineGPO([MarshalAs(UnmanagedType.LPWStr)] string computerName, uint flags);

		/// <summary>
		/// Saves the specified Group Policy Object.
		/// </summary>
		/// <param name="machine">
		/// If true, the machine settings are saved.
		/// </param>
		/// <param name="add">
		/// If true, the GPO is added to the list of GPOs.
		/// </param>
		/// <param name="extension">
		/// The GUID of the extension.
		/// </param>
		/// <param name="app">
		/// The GUID of the application.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is S_OK.
		/// </returns>
		uint Save([MarshalAs(UnmanagedType.Bool)] bool machine, [MarshalAs(UnmanagedType.Bool)] bool add, [MarshalAs(UnmanagedType.LPStruct)] Guid extension, [MarshalAs(UnmanagedType.LPStruct)] Guid app);

		/// <summary>
		/// Deletes the Group Policy Object.
		/// </summary>
		/// <returns></returns>
		uint Delete();

		/// <summary>
		/// Closes the Group Policy Object.
		/// </summary>
		/// <param name="name">
		/// The name of the GPO.
		/// </param>
		/// <param name="maxLength">
		/// The maximum length of the name.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is S_OK.
		/// </returns>
		uint GetName([MarshalAs(UnmanagedType.LPWStr)] StringBuilder name, int maxLength);

		/// <summary>
		/// Gets the display name of the Group Policy Object.
		/// </summary>
		/// <param name="name">
		/// The display name of the GPO.
		/// </param>
		/// <param name="maxLength">
		/// The maximum length of the display name.
		/// </param>
		/// <returns></returns>
		uint GetDisplayName([MarshalAs(UnmanagedType.LPWStr)] StringBuilder name, int maxLength);

		/// <summary>
		/// Sets the display name of the Group Policy Object.
		/// </summary>
		/// <param name="name">
		/// The display name of the GPO.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is S_OK.
		/// </returns>
		uint SetDisplayName([MarshalAs(UnmanagedType.LPWStr)] string name);

		/// <summary>
		/// Gets the path to the Group Policy Object.
		/// </summary>
		/// <param name="path">
		/// The path to the GPO.
		/// </param>
		/// <param name="maxPath">
		/// The maximum length of the path.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is S_OK.
		/// </returns>
		uint GetPath([MarshalAs(UnmanagedType.LPWStr)] StringBuilder path, int maxPath);

		/// <summary>
		/// Gets the path to the Group Policy Object.
		/// </summary>
		/// <param name="section">
		/// The section of the GPO.
		/// </param>
		/// <param name="path">
		/// The path to the GPO.
		/// </param>
		/// <param name="maxPath">
		/// The maximum length of the path.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is S_OK.
		/// </returns>
		uint GetDSPath(uint section, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder path, int maxPath);

		/// <summary>
		/// Gets the path to the Group Policy Object.
		/// </summary>
		/// <param name="section">
		/// The section of the GPO.
		/// </param>
		/// <param name="path">
		/// The path to the GPO.
		/// </param>
		/// <param name="maxPath">
		/// The maximum length of the path.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is S_OK.
		/// </returns>
		uint GetFileSysPath(uint section, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder path, int maxPath);

		/// <summary>
		/// Gets the registry key for the Group Policy Object.
		/// </summary>
		/// <param name="section">
		/// The section of the GPO.
		/// </param>
		/// <param name="key">
		/// The registry key for the GPO.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is S_OK.
		/// </returns>
		uint GetRegistryKey(uint section, out IntPtr key);

		/// <summary>
		/// Gets the options for the Group Policy Object.
		/// </summary>
		/// <param name="options">
		/// The options for the GPO.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is S_OK.
		/// </returns>
		uint GetOptions(out uint options);

		/// <summary>
		/// Sets the options for the Group Policy Object.
		/// </summary>
		/// <param name="options">
		/// The options for the GPO.
		/// </param>
		/// <param name="mask">
		/// The mask to use when setting the options.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is S_OK.
		/// </returns>
		uint SetOptions(uint options, uint mask);

		/// <summary>
		/// Gets the type of the Group Policy Object.
		/// </summary>
		/// <param name="gpoType">
		/// The type of the GPO.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is S_OK.
		/// </returns>
		uint GetType(out IntPtr gpoType);

		/// <summary>
		/// Gets the machine name.
		/// </summary>
		/// <param name="name">
		/// The name of the machine.
		/// </param>
		/// <param name="maxLength">
		/// The maximum length of the name.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is S_OK.
		/// </returns>
		uint GetMachineName([MarshalAs(UnmanagedType.LPWStr)] StringBuilder name, int maxLength);

		/// <summary>
		/// Gets the property sheet pages for the Group Policy Object.
		/// </summary>
		/// <param name="pages">
		/// The property sheet pages for the GPO.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is S_OK.
		/// </returns>
		uint GetPropertySheetPages(out IntPtr pages);
	}
}
