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
public class AddingFriendView
{
    UserService _userService;
    public AddingFriendView(UserService userService)
    {
        _userService = userService;
    }
    public void Show(User user)
    {
        try
        {
            var userAddingFriendData = new UserAddingFriendData();

            Console.WriteLine("Введите почтовый адрес пользователя которого хотите добавить в друзья: ");

            userAddingFriendData.FriendEmail = Console.ReadLine();
            userAddingFriendData.UserId = user.Id;

            this._userService.AddFriend(userAddingFriendData);

            SuccessMessage.Show("Вы успешно добавили пользователя в друзья!");
        }

        catch (UserNotFoundException)
        {
            AlertMessage.Show("Пользователя с указанным почтовым адресом не существует!");
        }

        catch (Exception)
        {
            AlertMessage.Show("Произоша ошибка при добавлении пользотваеля в друзья!");
        }

    }
}
