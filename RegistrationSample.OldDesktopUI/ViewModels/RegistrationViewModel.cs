using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;
using RegistrationSample.OldDesktopUI.Library.API;
using RegistrationSample.OldDesktopUI.Library.Models;
using RegistrationSample.OldDesktopUI.Models;
using RegistrationSample.OldDesktopUI.Utility;
using System.Collections.Generic;

namespace RegistrationSample.OldDesktopUI.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        private readonly IUserEndpoint _userEndpoint;
        private readonly UserDisplayModel _loggedInUser;

        public RegistrationViewModel(IUserEndpoint userEndpoint, UserDisplayModel loggedInUser, IMapper mapper) : base(mapper)
        {
            _userEndpoint = userEndpoint;
            _loggedInUser = loggedInUser;
            CreateUserCmd = new AsyncCommand(CreateUser);
        }

        public NewUserDisplayModel NewUser { get; set; } = new NewUserDisplayModel();

        public IAsyncCommand CreateUserCmd { get; }

        public async Task CreateUser()
        {
            if (IsNewUserValid())
            {
                var currentUser = await _userEndpoint.RegisterUser(_mapper.Map<NewUserModel>(NewUser));
                _loggedInUser.AssignUser(_mapper.Map<UserDisplayModel>(currentUser));
                Navigate<UserViewModel>();
            }
        }

        private bool IsNewUserValid()
        {
            return Validator.TryValidateObject(NewUser, new ValidationContext(NewUser), new List<ValidationResult>());
        }
    }
}