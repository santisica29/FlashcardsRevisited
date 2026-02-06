namespace FlashcardsRevisited.Views
{
    internal class MainMenu
    {
        private readonly StacksMenu _stacksMenu = new();
        internal void StartingMenu()
        {
            bool closeApp = false;

            while (!closeApp)
            {
                Console.WriteLine("Main Menu.");
                Console.WriteLine("-----------");
                Console.WriteLine("Manage Stacks (press s)");
                Console.WriteLine("Manage Flashcards (press f)");
                Console.WriteLine("Study Area (press x)");
                Console.WriteLine("Exit (press 0)");
                Console.WriteLine("-----------");
                Console.WriteLine("Choose one.");

                var commandInput = Console.ReadLine().Trim().ToLower();

                switch (commandInput)
                {
                    case "s":
                        _stacksMenu.MainMenu();
                        break;
                    //case "f":
                    //    FlashcardsMenu();
                    //    break;
                    //case "x":
                    //    StudyAreaMenu();
                    //    break;
                    case "0":
                        closeApp = true;
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid input.");
                        break;
                }
            }
        }
    }
}
