using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIT255FinalApplication
{
    public class Controller
    {

        #region Fields

        bool active = true;

        #endregion

        #region Properties



        #endregion

        #region Constructors

        public Controller()
        {
            ApplicationLoop();
        }

        #endregion

        #region Methods
        private void ApplicationLoop()
        {
            AppEnum.MenuOptions userMenuChoice;

            ConsoleView.DisplayTitleScreen();

            while (active)
            {

                userMenuChoice = ConsoleView.GetUserMenuOption();

                switch (userMenuChoice)
                {
                    case AppEnum.MenuOptions.ViewAllFirearms:
                        ListAllFirearms();
                        break;

                    case AppEnum.MenuOptions.AddFirearm:
                        AddFirearm();
                        break;

                    case AppEnum.MenuOptions.DeleteFirearm:
                        DeleteFirearm();
                        break;

                    case AppEnum.MenuOptions.UpdateFirearm:
                        UpdateFirearm();
                        break;

                    case AppEnum.MenuOptions.DisplayFirearmInfo:
                        DisplayFirearmDetail();
                        break;

                    case AppEnum.MenuOptions.Quit:
                        active = false;
                        break;

                    default:
                        break;
                }
            }


        }
        private static void ListAllFirearms()
        {
            FirearmRepositorySQL firearmRepository = new FirearmRepositorySQL();
            List<Firearm> firearm;

            using (firearmRepository)
            {
                firearm = firearmRepository.SelectAll();
                ConsoleView.DisplayAllFirearms(firearm);
                ConsoleView.DisplayContinuePrompt();
            }
        }

        private static void DisplayFirearmDetail()
        {
            FirearmRepositorySQL firearmRepository = new FirearmRepositorySQL();
            List<Firearm> firearms;
            Firearm firearm = new Firearm();
            int firearmID;

            using (firearmRepository)
            {
                firearms = firearmRepository.SelectAll();
                firearmID = ConsoleView.GetFirearmID(firearms);
                firearm = firearmRepository.SelectById(firearmID);
            }

            ConsoleView.DisplayFirearm(firearm);
            ConsoleView.DisplayContinuePrompt();
        }

        private static void AddFirearm()
        {
            FirearmRepositorySQL firearmRepository = new FirearmRepositorySQL();
            Firearm firearm = new Firearm();

            firearm = ConsoleView.AddFirearm();
            using (firearmRepository)
            {
                firearmRepository.Insert(firearm);
            }

            ConsoleView.DisplayContinuePrompt();
        }

        private static void DeleteFirearm()
        {
            FirearmRepositorySQL firearmRepository = new FirearmRepositorySQL();
            List<Firearm> firearms = firearmRepository.SelectAll();
            Firearm firearm = new Firearm();
            int firearmID;
            string message;

            firearmID = ConsoleView.GetFirearmID(firearms);

            using (firearmRepository)
            {
                firearmRepository.Delete(firearmID);
            }

            ConsoleView.DisplayReset();
            
            message = String.Format("Firearm ID: {0} had been deleted.", firearmID);

            ConsoleView.DisplayMessage(message);
            ConsoleView.DisplayContinuePrompt();
        }

        private static void UpdateFirearm()
        {
            FirearmRepositorySQL firearmRepository = new FirearmRepositorySQL();
            List<Firearm> firearms = firearmRepository.SelectAll();
            Firearm firearm = new Firearm();
            int firearmID;

            using (firearmRepository)
            {
                firearms = firearmRepository.SelectAll();
                firearmID = ConsoleView.GetFirearmID(firearms);
                firearm = firearmRepository.SelectById(firearmID);
                firearm = ConsoleView.UpdateFirearm(firearm);
                firearmRepository.Update(firearm);
            }
        }

        #endregion
    }
}
