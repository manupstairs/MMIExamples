1.Before we use Microsoft.Management.Infrastructure.
  Need to start Window Service "Windows Remote Management (WS-Management)".
2.Microsoft.Management.Infrastructure should be refered by .NET Standard 2.0 library.
  Then we reference our .NET Standard library to our .NET Framework 4.6.2 project.






MMILibrary:
	I create two classes MMIWrapper and MMIObserver to use those MMI classes from Microsoft.Management.Infrastucture namespace.
MMIExample:
	A console app which reference MMILibrary.
SystemManagementApp:
	A console app which I still use System.Management dll. I want to compare these two way on performance. 
