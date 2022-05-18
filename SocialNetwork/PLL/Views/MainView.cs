using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views;
public class MainView
{
    public void Show()
    {
        Console.WriteLine("1. Войти в профиль");
        Console.WriteLine("2. Зарегистрироваться");

        var input = Console.ReadKey(true).Key;
        Console.WriteLine();

        switch (input)
        {
            case ConsoleKey.D1:
                {
                    Program.AuthentificationView.Show();
                    break;
                }
            case ConsoleKey.D2:
                {
                    Program.RegistrationView.Show();
                    break;
                }
        }
    }
}
