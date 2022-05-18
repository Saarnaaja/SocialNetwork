using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Services;

public class MessageService
{
    IMessageRepository _messageRepository;
    IUserRepository _userRepository;

    public MessageService()
    {
        _messageRepository = new MessageRepository();
        _userRepository = new UserRepository();
    }

    public IEnumerable<Message> GetIncomingMessagesByUserId(int recipientId)
    {
        var messages = new List<Message>();
        _messageRepository.FindByRecipientId(recipientId).ToList().ForEach(m =>
        {
            var senderUserEntity = _userRepository.FindById(m.sender_id);
            var recipientUserEntity = _userRepository.FindById(m.recipient_id);
            messages.Add(new Message(m.id, m.content, senderUserEntity.email, recipientUserEntity.email));
        });
        return messages;
    }

    public IEnumerable<Message> GetOutcomingMessagesByUserId(int recipientId)
    {
        var messages = new List<Message>();
        _messageRepository.FindBySenderId(recipientId).ToList().ForEach(m =>
        {
            var senderUserEntity = _userRepository.FindById(m.sender_id);
            var recipientUserEntity = _userRepository.FindById(m.recipient_id);
            messages.Add(new Message(m.id, m.content, senderUserEntity.email, recipientUserEntity.email));
        });
        return messages;
    }

    public void SendMessage(MessageSendingData messageSendingData)
    {
        if (string.IsNullOrEmpty(messageSendingData.Content))
            throw new ArgumentNullException();
        if (messageSendingData.Content.Length > 5000)
            throw new ArgumentOutOfRangeException();
        var findUserEntity = _userRepository.FindByEmail(messageSendingData.RecipientEmail);
        if (findUserEntity == null)
            throw new UserNotFoundException();
        var messageEntity = new MessageEntity()
        {
            content = messageSendingData.Content,
            sender_id = messageSendingData.SenderId,
            recipient_id = findUserEntity.id
        };

        if (_messageRepository.Create(messageEntity) == 0)
            throw new Exception();
    }
}
