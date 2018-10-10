using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SmartMenuLibrary
{
   
    public class SmartMenu
    {
        //Variables
        string menuName = string.Empty;
        string menuDescription = string.Empty;
        List<string> menuList = new List<string>();
        List<string> menuId = new List<string>();
        List<string> errorList = new List<string>();

        public string ChooseLanguage(out string errorFile, out string bindingsPath)
        {
            int userInput = 0;
            string menuChoice;

            Console.WriteLine("Press 1 for English");
            Console.WriteLine("Tryk 2 for dansk");

            while (!int.TryParse(Console.ReadLine(), out userInput) || userInput > 2 || userInput < 1)
            {
                Console.WriteLine("Please enter valid input.");
                Console.WriteLine("Indtast veligst en gyldig værdi.");
            }

            if (userInput == 1)
            {
                menuChoice = "MenuSpecEn.txt";
                errorFile = "ErrorlistEn.txt";
                bindingsPath = "BindingsEn.txt";
            }
            else
            {
                menuChoice = "MenuSpecDk.txt";
                errorFile = "ErrorlistDk.txt";
                bindingsPath = "BindingsDk.txt";
            }

            return menuChoice;

        }

        public bool LoadMenu(string path, string errorListpath)
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
                using (StreamReader sr = new StreamReader(errorListpath))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        errorList.Add(line);
                    }
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
        public void Activate(IBindings bindings)
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

                //Ensure input is a valid number.
                while (!int.TryParse(Console.ReadLine(), out userInput) || userInput > menuId.Count)
                {
                    Console.WriteLine(errorList[0]);
                }
                if (userInput > 0)
                {
                    bindings.Call(menuId[userInput - 1]);
                    Console.ReadKey();
                }
            }
        }
    }
}
