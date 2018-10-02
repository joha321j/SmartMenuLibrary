using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SmartMenuLibrary
{
    interface IBindings
    {
        void Call(string callId);
    }
    public class SmartMenu
    {
        //Variables
        string menuName = string.Empty;
        string menuDescription = string.Empty;
        List<string> menuList = new List<string>();
        List<string> menuId = new List<string>();

        public bool LoadMenu(string path)
        {
            int lineCounter = 0;
            string line;
            bool readFile = false;

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        menuList.Add(line);
                        if (menuList[lineCounter].Contains(";"))
                        {
                            string[] tempStringArray = menuList[lineCounter].Split(';');
                            menuList[lineCounter] = tempStringArray[0];
                            menuId.Add(tempStringArray[1]);
                        }
                        lineCounter++;
                    }
                    readFile = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read.");
                Console.WriteLine(e.Message);
            }



            //Moving the first 2 items in menuList to ensure that menuId and menuList should have same dimensions.
            menuName = menuList[0];
            menuDescription = menuList[1];
            menuList.RemoveAt(0);
            menuList.RemoveAt(0);
            return readFile;
        }
        public void Activate()
        {
            int userInput = 1;

            while (userInput != 0)
            {
                //Print the loaded menu
                Console.Clear();
                Console.WriteLine(menuName);
                Console.WriteLine(menuDescription);
                for (int lineCount = 0; lineCount < menuList.Count; lineCount++)
                {
                    Console.WriteLine("  {0}.  {1}", lineCount + 1, menuList[lineCount]);
                }

                Console.WriteLine("Tast linie nummeret for at tilgå den undermenu.");
                Console.WriteLine("Tast 0 for at afslutte.");


                //Ensure input is a valid number.
                while (!int.TryParse(Console.ReadLine(), out userInput) || userInput > menuId.Count)
                {
                    Console.WriteLine("Indtast venligst et gyldigt tal.");
                }
                if (userInput > 0)
                {
                        
                   IBindings bindings = bindings;
                   bindings.Call(menuId[userInput - 1]);
                    Console.ReadKey();
                }
            }
        }
    }
}
