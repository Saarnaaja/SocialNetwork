using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Services;
public class UserService
{

    private IUserRepository _userRepository;
    MessageService _messageService;
    IFriendRepository _friendRepository;
    public UserService()
    { 
        _userRepository = new UserRepository();
        _messageService = new MessageService();
        _friendRepository = new FriendRepository();
    }
    public void Register(UserRegistrationData data)
    {
        if (string.IsNullOrEmpty(data.FirstName))
            throw new ArgumentNullException();

        if (string.IsNullOrEmpty(data.LastName))
            throw new ArgumentNullException();

        if (string.IsNullOrEmpty(data.Password))
            throw new ArgumentNullException();

        if (string.IsNullOrEmpty(data.Email))
            throw new ArgumentNullException();

        if (data.Password.Length < 8)
            throw new ArgumentNullException();

        if (!new EmailAddressAttribute().IsValid(data.Email))
            throw new ArgumentNullException();

        if (_userRepository.FindByEmail(data.Email) is not null)
            throw new ArgumentNullException();

        var userEntity = new UserEntity()
        {
            firstname = data.FirstName,
            lastname = data.LastName,
            email = data.Email,
            password = data.Password,
        };

        if (_userRepository.Create(userEntity) == 0)
        {
            throw new Exception();
        }

    }
    public User Authenticate(UserAuthenticationData userAuthenticationData)
    {
        var findUserEntity = _userRepository.FindByEmail(userAuthenticationData.Email);
        if (findUserEntity is null) throw new UserNotFoundException();

        if (findUserEntity.password != userAuthenticationData.Password)
            throw new WrongPasswordException();

        return ConstructUserModel(findUserEntity);
    }

    public User FindByEmail(string email)
    {
        var findUserEntity = _userRepository.FindByEmail(email);
        if (findUserEntity is null) throw new UserNotFoundException();

        return ConstructUserModel(findUserEntity);
    }
    public User FindById(int id)
    {
        var findUserEntity = _userRepository.FindById(id);
        if (findUserEntity is null) throw new UserNotFoundException();

        return ConstructUserModel(findUserEntity);
    }

    public void Update(User user)
    {
        var updatableUserEntity = new UserEntity()
        {
            id = user.Id,
            firstname = user.FirstName,
            lastname = user.LastName,
            password = user.Password,
            email = user.Email,
            photo = user.Photo,
            favorite_movie = user.FavoriteMovie,
            favorite_book = user.FavoriteBook
        };

        if (_userRepository.Update(updatableUserEntity) == 0)
            throw new Exception();
    }

    public IEnumerable<User> GetFriendsByUserId(int userId)
    {
        return _friendRepository.FindAllByUserId(userId)
                .Select(friendsEntity => FindById(friendsEntity.friend_id));
    }

    public void AddFriend(UserAddingFriendData userAddingFriendData)
    {
        var findUserEntity = _userRepository.FindByEmail(userAddingFriendData.FriendEmail);
        if (findUserEntity is null) throw new UserNotFoundException();

        var friendEntity = new FriendEntity()
        {
            user_id = userAddingFriendData.UserId,
            friend_id = findUserEntity.id
        };

        if (_friendRepository.Create(friendEntity) == 0)
            throw new Exception();
    }

    private User ConstructUserModel(UserEntity userEntity)
    {
        var incomingMessages = _messageService.GetIncomingMessagesByUserId(userEntity.id);

        var outgoingMessages = _messageService.GetOutcomingMessagesByUserId(userEntity.id);
        var friends = GetFriendsByUserId(userEntity.id);

        return new User(userEntity.id,
                      userEntity.firstname,
                      userEntity.lastname,
                      userEntity.password,
                      userEntity.email,
                      userEntity.photo,
                      userEntity.favorite_movie,
                      userEntity.favorite_book,
                      incomingMessages,
                      outgoingMessages,
                      friends
                      );
    }

}
