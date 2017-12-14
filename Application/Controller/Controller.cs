using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
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
                        UpdateSkiRun();
                        break;

                    case AppEnum.MenuOptions.DisplayFirearmInfo:
                        DisplayFirearmDetail();
                        break;

                    case AppEnum.MenuOptions.QueryBy:
                        QueryFirearmsById();
                        break;

                    case AppEnum.MenuOptions.Quit:
                        active = false;
                        break;

                    default:
                        break;
                };
            }
            ConsoleView.DisplayExitPrompt();
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

        private static void UpdateSkiRun()
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

        private static void QueryFirearmsById()
        {
            FirearmRepositorySQL firearmRepository = new FirearmRepositorySQL();
            IEnumerable<Firearm> matchingFirearms = new List<Firearm>();
            int lowerId;
            int higherId;

            ConsoleView.GetIdQueryLowHiValues(out lowerId, out higherId);

            using (firearmRepository)
            {
                matchingFirearms = firearmRepository.QueryById(lowerId, higherId);
            }

            ConsoleView.DisplayQueryResults(matchingFirearms);
            ConsoleView.DisplayContinuePrompt();
        }

        //private static void QueryFirearmsByManufacturer()
        //{
        //    FirearmRepositorySQL firearmRepository = new FirearmRepositorySQL();
        //    IEnumerable<Firearm> matchingFirearms = new List<Firearm>();
        //    string manufacturerValue;

        //    ConsoleView.GetManufacturerQueryValue(out manufacturerValue);

        //    using (firearmRepository)
        //    {
        //        matchingFirearms = firearmRepository.QueryByManufacturer(manufacturerValue);
        //    }

        //    ConsoleView.DisplayQueryResults(matchingFirearms);
        //    ConsoleView.DisplayContinuePrompt();
        //}
        #endregion
    }
}
