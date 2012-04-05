using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("EWUSIS: Sensitive Information Scanner")]
[assembly: AssemblyDescription("The Eastern Washington University Sensitive Information Scanner is a utility that will scan a system for Social Security Numbers or Credit Card Numbers."+
                                "The application runs in two different modes, Administrative mode and Partial mode."+
                                "Administrative mode will attempt to scan the entire file system"+
                                "In this mode the application will scan all mounted drives found, including fixed drives and removable storage."+
                                "Partial mode scans only the user directory of the system that is logged in at the time of execution."+
                                "After the scan, there is an encrypted database that is generated for use by an outside party to securely scrub the system of any information found."+
                                "This application is NOT designed to remove data from the system, but simply report where potentially sensitive information is in the system.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("NamingException: Cyrus Mir, Gina Sprint, Mathew Doiron, Nathan Harden, Steven Kakoczky")]
[assembly: AssemblyProduct("EWU Sensitive Data Scanner")]
[assembly: AssemblyCopyright("Copyright EWU ©  2012")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("786cd7c9-d1b0-4755-86dd-f8aebdb3050d")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("2.11.3.0")]
[assembly: AssemblyFileVersion("2.11.3.0")]
