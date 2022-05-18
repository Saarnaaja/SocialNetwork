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
public class MessageSendingView
{
    MessageService _messageService;
    UserService _userService;
    public MessageSendingView(MessageService messageService, UserService userService)
    { 
        _messageService = messageService;
        _userService = userService;
    }

    public void Show(User user)
    {
        var messageSendingData = new MessageSendingData();
        Console.Write("Введите почтовый адрес получателя: ");
        messageSendingData.RecipientEmail = Console.ReadLine();

        Console.WriteLine("Введите сообщение (не более 5000 символов):");
        messageSendingData.Content = Console.ReadLine();

        messageSendingData.SenderId = user.Id;

        try
        {
            _messageService.SendMessage(messageSendingData);
            SuccessMessage.Show("Сообщение успешно отправлено!");
            user = _userService.FindById(user.Id);
        }
        catch (UserNotFoundException)
        {
            AlertMessage.Show("Пользователь не найден!");
        }
        catch (ArgumentNullException)
        {
            AlertMessage.Show("Введите корректное значение!");
        }
        catch (Exception)
        {
            AlertMessage.Show("Произошла ошибка при отправке сообщения!");
        }

    }
}
