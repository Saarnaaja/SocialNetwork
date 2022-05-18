using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views;
public class UserMenuView
{
    UserService _userService;
    public UserMenuView(UserService userService)
    { 
        _userService = userService;
    }

    public void Show(User user)
    {
        while (true)
        {
            Console.WriteLine($"Входящие сообщения: {user.IncomingMessages.Count()}");
            Console.WriteLine($"Исходящие сообщения: {user.OutgoingMessages.Count()}");
            Console.WriteLine($"1. Просмотреть информацию о моем профиле");
            Console.WriteLine($"2. Редактировать мой профиль");
            Console.WriteLine($"3. Добавить в друзья");
            Console.WriteLine($"4. Написать сообщение");
            Console.WriteLine($"5. Просмотреть входящие сообщения");
            Console.WriteLine($"6. Просмотреть исходящие сообщения");
            Console.WriteLine($"7. Просмотреть список моих друзей");
            Console.WriteLine($"8. Выйти из профиля");

            var input = Console.ReadKey(true).Key;
            Console.WriteLine();

            if (input == ConsoleKey.D8) break;

            switch (input)
            {
                case ConsoleKey.D1:
                    {
                        Program.UserInfoView.Show(user);
                        break;
                    }
                case ConsoleKey.D2:
                    {
                        Program.UserDataUpdateView.Show(user);
                        break;
                    }
                case ConsoleKey.D3:
                    {
                        Program.AddingFriendView.Show(user);
                        user = _userService.FindById(user.Id);
                        break;
                    }
                case ConsoleKey.D4:
                    {
                        Program.MessageSendingView.Show(user);
                        break;
                    }
                case ConsoleKey.D5:
                    {
                        Program.UserIncomingMessageView.Show(user.IncomingMessages);
                        break;
                    }
                case ConsoleKey.D6:
                    {
                        Program.UserOutcomingMessageView.Show(user.OutgoingMessages);
                        break;
                    }
                case ConsoleKey.D7:
                    {
                        Program.UserFriendView.Show(user.Friends);
                        break;
                    }
            }
        }
    }
}
