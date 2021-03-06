﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIT255FinalApplication
{
    public static class ConsoleView
    {
        #region ENUMERABLES


        #endregion

        #region FIELDS

        //
        // window size
        //
        private const int WINDOW_WIDTH = ViewSettings.WINDOW_WIDTH;
        private const int WINDOW_HEIGHT = ViewSettings.WINDOW_HEIGHT;

        //
        // horizontal and vertical margins in console window for display
        //
        private const int DISPLAY_HORIZONTAL_MARGIN = ViewSettings.DISPLAY_HORIZONTAL_MARGIN;
        private const int DISPALY_VERITCAL_MARGIN = ViewSettings.DISPALY_VERITCAL_MARGIN;

        #endregion

        #region CONSTRUCTORS

        #endregion

        #region METHODS
        public static void DisplayTitleScreen()
        {
            Console.SetWindowSize(WINDOW_WIDTH, WINDOW_HEIGHT);

            Console.WriteLine(ConsoleUtil.Center(@" _____                  ______   _   _", WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.Center(@"|  __ \                 | ___ \ | | | |", WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.Center(@"   | |  \/_   _ _ __  ___  | |_/ / | | | |___", WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.Center(@"    | | __| | | | '_ \/ __| |    /  | | | / __|", WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.Center(@"    | |_\ \ |_| | | | \__ \ | |\ \  | |_| \__ \", WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.Center(@"     \____/\__,_|_| |_|___/ \_| \_|  \___/|___/", WINDOW_WIDTH));
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            DisplayContinuePrompt();
        }


        /// <summary>
        /// display the Continue prompt
        /// </summary>
        public static void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;

            Console.WriteLine();

            Console.WriteLine(ConsoleUtil.Center("Press any key to continue.", WINDOW_WIDTH));
            ConsoleKeyInfo response = Console.ReadKey();

            Console.WriteLine();

            Console.CursorVisible = true;
        }

        public static AppEnum.MenuOptions GetUserMenuOption()
        {
            DisplayReset();

            AppEnum.MenuOptions userMenuOption = AppEnum.MenuOptions.None;

            string leftTab = ConsoleUtil.FillStringWithSpaces(DISPLAY_HORIZONTAL_MARGIN);

            DisplayMessage("");
            Console.WriteLine(ConsoleUtil.Center("Main Menu", WINDOW_WIDTH));
            DisplayMessage("");

            Console.WriteLine(
                leftTab + "1. Display All Firearms" + Environment.NewLine +
                leftTab + "2. Display Firearm Info" + Environment.NewLine +
                leftTab + "3. Add a Firearm" + Environment.NewLine +
                leftTab + "4. Delete a Firearm" + Environment.NewLine +
                leftTab + "5. Update a Firearm" + Environment.NewLine +
                leftTab + "6. Query By ID" + Environment.NewLine +
                leftTab + "E. Exit" + Environment.NewLine);

            Console.WriteLine("");
            Console.WriteLine(ConsoleUtil.Center("Enter a menu option.", WINDOW_WIDTH));
            ConsoleKeyInfo userResponse = Console.ReadKey(true);

            switch (userResponse.KeyChar)
            {
                case '1':
                    userMenuOption = AppEnum.MenuOptions.ViewAllFirearms;
                    break;
                case '2':
                    userMenuOption = AppEnum.MenuOptions.DisplayFirearmInfo;
                    break;
                case '3':
                    userMenuOption = AppEnum.MenuOptions.AddFirearm;
                    break;
                case '4':
                    userMenuOption = AppEnum.MenuOptions.DeleteFirearm;
                    break;
                case '5':
                    userMenuOption = AppEnum.MenuOptions.UpdateFirearm;
                    break;
                case '6':
                    userMenuOption = AppEnum.MenuOptions.QueryBy;
                    break;
                case 'e':
                case 'E':
                    userMenuOption = AppEnum.MenuOptions.Quit;
                    break;
                default:
                    DisplayMessage("");
                    DisplayMessage("");
                    DisplayMessage("It appears you have selected an incorrect choice.");
                    DisplayMessage("");
                    DisplayMessage("Press any key to try again or the ESC key to exit.");

                    userResponse = Console.ReadKey(true);
                    if (userResponse.Key == ConsoleKey.Escape)
                    {
                        userMenuOption = AppEnum.MenuOptions.Quit;
                    }
                    break;
            }
            return userMenuOption;
        }

        /// <summary>
        /// method to display all firearm info
        /// </summary>
        public static void DisplayAllFirearms(List<Firearm> firearms)
        {
            DisplayReset();

            StringBuilder columnHeader = new StringBuilder();

            columnHeader.Append("ID".PadRight(8));
            columnHeader.Append("Firearm".PadRight(25));

            DisplayMessage(columnHeader.ToString());
            Console.WriteLine();

            foreach (Firearm firearm in firearms)
            {
                StringBuilder firearmInfo = new StringBuilder();

                firearmInfo.Append(firearm.ID.ToString().PadRight(8));
                firearmInfo.Append(firearm.Name.PadRight(25));

                DisplayMessage(firearmInfo.ToString());
            }
        }

        /// <summary>
        /// method to display a firearm's info
        /// </summary>
        public static void DisplayFirearm(Firearm firearm)
        {
            DisplayReset();

            DisplayMessage("");
            Console.WriteLine(ConsoleUtil.Center("Firearm Detail", WINDOW_WIDTH));
            DisplayMessage("");

            DisplayMessage(String.Format("Firearm: {0}", firearm.Name));
            DisplayMessage("");

            DisplayMessage(String.Format("ID: {0}", firearm.ID.ToString()));
            DisplayMessage(String.Format("Manufacturer: {0}", firearm.Manufacturer.ToString()));
            DisplayMessage(String.Format("Type: {0}", firearm.FirearmType.ToString()));
            DisplayMessage(String.Format("Caliber/Guage: {0}", firearm.AmmoType.ToString()));

            DisplayMessage("");
        }

        /// <summary>
        /// method to add a firearm's info
        /// </summary>
        public static Firearm AddFirearm()
        {
            Firearm firearm = new Firearm();

            DisplayReset();

            DisplayMessage("");
            Console.WriteLine(ConsoleUtil.Center("Add A Firearm", WINDOW_WIDTH));
            DisplayMessage("");

            DisplayPromptMessage("Enter the firearm ID: "); 
            firearm.ID = ConsoleUtil.ValidateIntegerResponse("Please enter the firearm ID: ", Console.ReadLine());
            Console.WriteLine("");

            DisplayPromptMessage("Enter the firearm name: "); 
            firearm.Name = Console.ReadLine();
            Console.WriteLine("");

            DisplayPromptMessage("Enter the manufacturer: ");
            firearm.Manufacturer = Console.ReadLine();
            Console.WriteLine("");

            DisplayPromptMessage("Enter the firearm type: ");
            firearm.FirearmType = Console.ReadLine();
            Console.WriteLine("");

            DisplayPromptMessage("Enter the caliber/guage: ");
            firearm.AmmoType = Console.ReadLine();
            Console.WriteLine("");

            DisplayPromptMessage("Enter the barrel length: ");
            firearm.BarrelLength = ConsoleUtil.ValidateIntegerResponse("Please enter the barrel length: ", Console.ReadLine());
            Console.WriteLine("");

            return firearm;
        }

        public static Firearm UpdateFirearm(Firearm firearm)
        {
            string userResponse = "";

            DisplayReset();

            DisplayMessage("");
            Console.WriteLine(ConsoleUtil.Center("Edit A Firearm", WINDOW_WIDTH));
            DisplayMessage("");

            DisplayMessage(String.Format("Current Name: {0}", firearm.Name));
            DisplayPromptMessage("Enter a new name or just press Enter to keep the current name: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                firearm.Name = userResponse;
            }

            DisplayMessage("");

            DisplayMessage(String.Format("Current Manufacturer: {0}", firearm.Manufacturer));
            DisplayPromptMessage("Enter a new manufacturer or just press Enter to keep the current manufacturer: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                firearm.Manufacturer = userResponse;
            }

            DisplayMessage("");

            DisplayMessage(String.Format("Current Type: {0}", firearm.FirearmType));
            DisplayPromptMessage("Enter a new type or just press Enter to keep the current type: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                firearm.FirearmType = userResponse;
            }

            DisplayMessage("");

            DisplayMessage(String.Format("Current Caliber/Guage: {0}", firearm.AmmoType));
            DisplayPromptMessage("Enter a new caliber/guage or just press Enter to keep the current caliber/guage: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                firearm.AmmoType = userResponse;
            }

            DisplayMessage("");

            DisplayMessage(String.Format("Current Barrel Length in cm: {0}", firearm.BarrelLength));
            DisplayPromptMessage("Enter a new barrel length or just press Enter to keep the current barrel length: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                firearm.BarrelLength = ConsoleUtil.ValidateIntegerResponse("Please enter the barrel length in cm.", userResponse);
            }

            DisplayMessage("");

            DisplayContinuePrompt();

            return firearm;
        }

        /// <summary>
        /// method gets the lower and higher values for the ID query
        /// </summary>
        /// <param name="lowerId">minimum vertical</param>
        /// <param name="higherId">maximum vertical</param>
        public static void GetIdQueryLowHiValues(out int lowerId, out int higherId)
        {
            lowerId = 0;
            higherId = 0;
            string userResponse = "";

            DisplayReset();

            DisplayMessage("");
            Console.WriteLine(ConsoleUtil.Center("Query Firearms by Id", WINDOW_WIDTH));
            DisplayMessage("");

            DisplayPromptMessage("Enter the lower ID: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                lowerId = ConsoleUtil.ValidateIntegerResponse("Please enter the lower ID.", userResponse);
            }

            DisplayMessage("");

            DisplayPromptMessage("Enter the higher ID: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                higherId = ConsoleUtil.ValidateIntegerResponse("Please enter the higher ID.", userResponse);
            }

            DisplayMessage("");

            DisplayMessage(String.Format("You have entered {0} as the lower ID and {1} as the higher ID.", lowerId, higherId));

            DisplayMessage("");

            DisplayContinuePrompt();
        }

        public static void DisplayQueryResults(IEnumerable<Firearm> matchingFirearms)
        {
            DisplayReset();

            DisplayMessage("");
            Console.WriteLine(ConsoleUtil.Center("Display Firearm Query Results", WINDOW_WIDTH));
            DisplayMessage("");

            DisplayMessage("All of the matching firearms are displayed below;");
            DisplayMessage("");

            StringBuilder columnHeader = new StringBuilder();

            columnHeader.Append("ID".PadRight(8));
            columnHeader.Append("Firearm".PadRight(25));

            DisplayMessage(columnHeader.ToString());

            foreach (Firearm firearm in matchingFirearms)
            {
                StringBuilder firearmInfo = new StringBuilder();

                firearmInfo.Append(firearm.ID.ToString().PadRight(8));
                firearmInfo.Append(firearm.Name.PadRight(25));

                DisplayMessage(firearmInfo.ToString());
            }
        }

        /// <summary>
        /// reset display to default size and colors including the header
        /// </summary>
        public static void DisplayReset()
        {
            Console.SetWindowSize(WINDOW_WIDTH, WINDOW_HEIGHT);

            Console.Clear();
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;

            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.Center("Guns R Us", WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));

            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// method to get the user's choice of firearm id
        /// </summary>
        public static int GetFirearmID(List<Firearm> firearms)
        {
            int firearmID = -1;

            DisplayAllFirearms(firearms);

            DisplayMessage("");
            DisplayPromptMessage("Enter the firearm ID: ");

            firearmID = ConsoleUtil.ValidateIntegerResponse("Please enter the firearm ID: ", Console.ReadLine());

            return firearmID;
        }

        /// <summary>
        /// display a message in the message area
        /// </summary>
        /// <param name="message">string to display</param>
        public static void DisplayMessage(string message)
        {
            //
            // calculate the message area location on the console window
            //
            const int MESSAGE_BOX_TEXT_LENGTH = WINDOW_WIDTH - (2 * DISPLAY_HORIZONTAL_MARGIN);
            const int MESSAGE_BOX_HORIZONTAL_MARGIN = DISPLAY_HORIZONTAL_MARGIN;

            // message is not an empty line, display text
            if (message != "")
            {
                //
                // create a list of strings to hold the wrapped text message
                //
                List<string> messageLines;

                //
                // call utility method to wrap text and loop through list of strings to display
                //
                messageLines = ConsoleUtil.Wrap(message, MESSAGE_BOX_TEXT_LENGTH, MESSAGE_BOX_HORIZONTAL_MARGIN);
                foreach (var messageLine in messageLines)
                {
                    Console.WriteLine(messageLine);
                }
            }
            // display an empty line
            else
            {
                Console.WriteLine();
            }
        }

        /// <summary>
        /// display the Exit prompt
        /// </summary>
        public static void DisplayExitPrompt()
        {
            DisplayReset();

            Console.CursorVisible = false;

            Console.WriteLine();
            DisplayMessage("Thank you for using our application. Press any key to Exit.");

            Console.ReadKey();

            System.Environment.Exit(1);
        }

        /// <summary>
        /// display a message in the message area without a new line for the prompt
        /// </summary>
        /// <param name="message">string to display</param>
        public static void DisplayPromptMessage(string message)
        {
            //
            // calculate the message area location on the console window
            //
            const int MESSAGE_BOX_TEXT_LENGTH = WINDOW_WIDTH - (2 * DISPLAY_HORIZONTAL_MARGIN);
            const int MESSAGE_BOX_HORIZONTAL_MARGIN = DISPLAY_HORIZONTAL_MARGIN;

            //
            // create a list of strings to hold the wrapped text message
            //
            List<string> messageLines;

            //
            // call utility method to wrap text and loop through list of strings to display
            //
            messageLines = ConsoleUtil.Wrap(message, MESSAGE_BOX_TEXT_LENGTH, MESSAGE_BOX_HORIZONTAL_MARGIN);

            for (int lineNumber = 0; lineNumber < messageLines.Count() - 1; lineNumber++)
            {
                Console.WriteLine(messageLines[lineNumber]);
            }

            Console.Write(messageLines[messageLines.Count() - 1]);
        }
        #endregion
    }
}
