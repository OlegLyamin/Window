using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparxSystems;
using EA;

namespace ConsoleApplication
{
    class Program
    {
        private EA.Repository Repository = null;
        private int m_ProcessID = 0;

		public void Trace( string msg)
		{
			if(Repository!=null)
			{
				// Displays the message in the 'Script' tab of Enterprise Architect System Output Window
				Repository.WriteOutput( "Script", msg, 0);
			}
			Console.WriteLine(msg);
		}
        public Program(int pid)
        {
            m_ProcessID = pid;
            Repository = SparxSystems.Services.GetRepository(m_ProcessID);
			Trace("Running C# Console Application AppPattern .NET 4.0");
        }
        private void PrintPackage(EA.Package package)
        {
            Trace(package.Name);
            EA.Collection packages = package.Packages;
            for (short ip = 0; ip < packages.Count; ip++)
            {
                EA.Package child = (EA.Package)packages.GetAt(ip);
                PrintPackage(child);
            }
        }
        public bool PrintModel()
        {
            if (Repository == null)
            {
				Trace( String.Format("Repository unavailable for pid {0}", m_ProcessID) );
                return false;
            }
			Trace( String.Format("Target repository process pid {0}", m_ProcessID) );
            EA.Collection packages = Repository.Models;
            for (short ip = 0; ip < packages.Count; ip++)
            {
                EA.Package child = (EA.Package)packages.GetAt(ip);
                PrintPackage(child);
            }
            return true;
        }

        static void Main(string[] args)
        {
            int pid = 0;
            if (args.Length > 0)
            {
                pid = Int32.Parse(args[0]);
            }
            if (pid > 0)
            {
                Program p = new Program(pid);
                p.PrintModel();
            }

        }
    }
}
