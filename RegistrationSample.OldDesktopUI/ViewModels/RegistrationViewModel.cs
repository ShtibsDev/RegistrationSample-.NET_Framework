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

        public RegistrationViewModel(IUserEndpoint userEndpoint, UserDisplayModel loggedInUser, IMapper mapper, IEventAggregator eventAggregator) : base(mapper, eventAggregator)
        {
            _userEndpoint = userEndpoint;
            _loggedInUser = loggedInUser;
            CreateUserCmd = new AsyncCommand(CreateUser);
        }

        public NewUserDisplayModel NewUser { get; set; } = new NewUserDisplayModel();

        public IAsyncCommand CreateUserCmd { get; }

        public async Task CreateUser()
        {
            NewUser.Validate();
            if (!NewUser.HasErrors)
            {

                try
                {
                    await _userEndpoint.RegisterUser(_mapper.Map<NewUserModel>(NewUser));
                    var authentication = await _userEndpoint.Authenticate(NewUser.EmailAddress, NewUser.Password);
                    var loggedInUser = await _userEndpoint.GetLogedInUserInfo();
                    loggedInUser.Token = authentication.Access_token;
                    _loggedInUser.AssignUser(_mapper.Map<UserDisplayModel>(loggedInUser));
                    Navigate<UserViewModel>();
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
            
        }

        private bool IsNewUserValid()
        {
            return Validator.TryValidateObject(NewUser, new ValidationContext(NewUser), new List<ValidationResult>());
        }
    }
}