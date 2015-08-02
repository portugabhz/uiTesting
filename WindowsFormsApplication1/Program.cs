using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestStack.White;
using TestStack.White.InputDevices;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.MenuItems;
using TestStack.White.UIItems.WindowItems;
using Xunit;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Tests tests = new Tests();
            tests.Notepad();
        }
    }

    class Tests
    {
        public void Notepad()
        {
            // Arrange
            Application app = Application.Launch("notepad.exe");
            Window window = app.GetWindow("Untitled - Notepad");

            // Act
            var box = window.Get(SearchCriteria.ByClassName("Edit"));
            Keyboard.Instance.Send("test", box);

            window.MenuBar.MenuItem("File", "Save As...").Click();
            var filename = window.Get(SearchCriteria.ByClassName("Edit"));
            var time = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            Keyboard.Instance.Send(time + "test.txt", filename);
            window.Get(SearchCriteria.ByText("Save")).Click();

            System.IO.StreamReader reader = new System.IO.StreamReader(@"c:\users\josh\desktop\" + time + "test.txt");
            var val = reader.ReadToEnd();
            System.Diagnostics.Debug.WriteLine(val);
            
            app.Kill();   

        }

    }

}
