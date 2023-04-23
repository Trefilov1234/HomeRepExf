using GalaSoft.MvvmLight;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TestClient.Api;
using TestClient.Api.Extensions;
using TestClient.Api.Requests;

namespace TestClient.ViewModel
{
   
    public class MainViewModel : ViewModelBase
    {
        
        public MainViewModel()
        {
            
        }
        private const string BaseUri = "http://127.0.0.1:8888";
        private string login;
        public string Login
        {
            get => login;
            set=>Set(ref login, value);
        }
        private string password;
        public string Password
        {
            get => password;
            set => Set(ref password, value);
        }
        private ICommand registerCommand;
        public ICommand RegisterCommand => registerCommand ?? (registerCommand = new AsyncRelayCommand(Register));
        private readonly ITestApi testApi=new TestApi(BaseUri);
        public async Task Register()
        {
            await testApi.CreateUser(new UserRequest() { Login = Login, Password = Password, UserType = "student" });
            
        }
    }
}