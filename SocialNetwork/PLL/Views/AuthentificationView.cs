using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views;
internal class AuthentificationView
{
    UserService _userService;
    public AuthentificationView(UserService userService)
    { 
        _userService = userService;
    }

    public void Show()
    {
        var authenticationData = new UserAuthenticationData();

        Console.WriteLine("Введите почтовый адрес:");
        authenticationData.Email = Console.ReadLine();

        Console.WriteLine("Введите пароль:");
        authenticationData.Password = Console.ReadLine();

        try
        { 
            var user = _userService.Authenticate(authenticationData);
            SuccessMessage.Show("Вы успешно вошли в социальную сеть!");
            SuccessMessage.Show($"Добро пожаловать {user.FirstName}");

            Program.UserMenuView.Show(user);
        }
        catch (WrongPasswordException)
        {
            AlertMessage.Show("Пароль не корректный!");
        }
        catch (UserNotFoundException)
        {
            AlertMessage.Show("Пользователь не найден!");
        }
    }
}
