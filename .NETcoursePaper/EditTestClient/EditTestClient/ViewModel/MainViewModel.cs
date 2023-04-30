using EditTestClient.Api;
using EditTestClient.Api.Extensions;
using EditTestClient.Api.Helpers;
using EditTestClient.Api.Requests;
using EditTestClient.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace EditTestClient.ViewModel
{
   
    public class MainViewModel : ViewModelBase
    {
        private string login;
        private string password;
        private string regStatus;

        private int? testViewIndex;
        
        private List<string> testsData;

        private string addingTestName;
        private string addingTestAttempts;

        private List<string> addingQuestions;
        private int? questionIndex;
        private ImageSource questionImage;
        private string questionTaskText;
        private string questionAnswers;
        private string questionRightAnswer;
        private string questionValueAnswer;

        private Visibility authFormVanish;
        private Visibility chooseTestFormVanish;
        private Visibility addTestFormVanish;
        private Visibility addOrEditQuestionFormVanish;

        private ICommand registerCommand;
        private ICommand loginCommand;

        private ICommand addTestCommand;
        private ICommand editTestCommand;
        private ICommand deleteTestCommand;
        private ICommand publishTestCommand;

        private ICommand addQuestionCommand;
        private ICommand editQuestionCommand;
        private ICommand deleteQuestionCommand;
        private ICommand addQuestionImageCommand;
        private ICommand addQuestionFromQFCommand;

        private ICommand backToAddTestFormCommand;
        private ICommand backToChooseTestFormCommand;
        private ICommand backToAuthFormCommand;

        private const string BaseUri = "http://127.0.0.1:8888";

        private bool editQuestionButtonWasClicked = false;
        private bool addImageButtonWasClicked = false;
        private bool editTestButtonWasClicked = false;

        private bool publishButtonClicked;

        private string addOrUpdateQuestionButtonContent;
        private string addOrUpdateTestContent;

        public string Login
        {
            get => login;
            set => Set(ref login, value);
        } 
        public string Password
        {
            get => password;
            set => Set(ref password, value);
        }
        public string RegStatus
        {
            get => regStatus;
            set => Set(ref regStatus, value);
        }

        public int? TestViewIndex
        {
            get => testViewIndex;
            set => Set(ref testViewIndex, value);
        }
        
        public List<string> TestsData
        {
            get => testsData;
            set=> Set(ref testsData, value);
        }

        public string AddingTestName
        {
            get => addingTestName;
            set => Set(ref addingTestName, value);
        }
        public string AddingTestAttempts
        {
            get => addingTestAttempts;
            set => Set(ref addingTestAttempts, value);
        }

        public List<string> AddingQuestions
        {
            get => addingQuestions;
            set => Set(ref addingQuestions, value);
        }
        public int? QuestionIndex
        {
            get => questionIndex;
            set=> Set(ref questionIndex, value);
        }
        public ImageSource QuestionImage
        {
            get => questionImage;
            set=> Set(ref questionImage, value);
        }
        public string QuestionTaskText
        {
            get => questionTaskText;
            set=>Set(ref questionTaskText, value);
        }
        public string QuestionAnswers
        {
            get => questionAnswers;
            set=> Set(ref questionAnswers, value);
        }
        public string QuestionRightAnswer
        {
            get => questionRightAnswer; 
            set => Set(ref questionRightAnswer, value);
        }
        public string QuestionValueAnswer
        {
            get=> questionValueAnswer;
            set=> Set(ref questionValueAnswer, value);
        }

        public Visibility AuthFormVanish
        {
            get => authFormVanish;
            set => Set(ref authFormVanish, value);
        }
        public Visibility ChooseTestFormVanish
        {
            get => chooseTestFormVanish;
            set => Set(ref chooseTestFormVanish, value);
        }
        public Visibility AddTestFormVanish
        {
            get => addTestFormVanish;
            set => Set(ref addTestFormVanish, value);
        }
        public Visibility AddOrEditQuestionFormVanish
        {
            get => addOrEditQuestionFormVanish;
            set => Set(ref addOrEditQuestionFormVanish, value);
        }

        public ICommand RegisterCommand => registerCommand ?? (registerCommand = new AsyncRelayCommand(MakeRegistration));
        public ICommand LoginCommand => loginCommand ?? (loginCommand = new AsyncRelayCommand(MakeLogin));

        public ICommand AddTestCommand => addTestCommand ?? (addTestCommand = new RelayCommand(AddTest));
        public ICommand EditTestCommand => editTestCommand ?? (editTestCommand = new AsyncRelayCommand(EditTest));
        public ICommand DeleteTestCommand => deleteTestCommand ?? (deleteTestCommand = new AsyncRelayCommand(DeleteTest));
        public ICommand PublishTestCommand => publishTestCommand ?? (publishTestCommand = new AsyncRelayCommand(PublishOrUpdateTest));

        public ICommand AddQuestionCommand => addQuestionCommand ?? (addQuestionCommand = new RelayCommand(AddQuestion));
        public ICommand EditQuestionCommand => editQuestionCommand ?? (editQuestionCommand = new AsyncRelayCommand(EditQuestion));
        public ICommand DeleteQuestionCommand => deleteQuestionCommand ?? (deleteQuestionCommand = new AsyncRelayCommand(DeleteQuestion));
        public ICommand AddQuestionImageCommand => addQuestionImageCommand ?? (addQuestionImageCommand = new RelayCommand(AddQuestionImage));
        public ICommand AddQuestionFromQFCommand => addQuestionFromQFCommand ?? (addQuestionFromQFCommand = new AsyncRelayCommand(AddOrUpdateQuestionFromQF));

        public ICommand BackToAddTestFormCommand => backToAddTestFormCommand ?? (backToAddTestFormCommand = new RelayCommand(BackToAddTestForm));
        public ICommand BackToChooseTestFormCommand => backToChooseTestFormCommand ?? (backToChooseTestFormCommand = new RelayCommand(BackToChooseTestForm));
        public ICommand BackToAuthFormCommand => backToAuthFormCommand ?? (backToAuthFormCommand = new RelayCommand(BackToAuthForm));

        public bool PublishButtonClicked
        {
            get => publishButtonClicked;
            set => Set(ref publishButtonClicked, value);
        }
        public string AddOrUpdateQuestionButtonContent
        {
            get => addOrUpdateQuestionButtonContent;
            set => Set(ref addOrUpdateQuestionButtonContent, value);
        }
        public string AddOrUpdateTestContent
        {
            get => addOrUpdateTestContent;
            set => Set(ref addOrUpdateTestContent, value);
        }

        private readonly ITestApi testApi = new TestApi(BaseUri);
        private readonly IQuestionService questionService=new QuestionService();
        private readonly ITestService testService=new TestService();

        public MainViewModel()
        {
            //AuthFormVanish = Visibility.Collapsed;
            ChooseTestFormVanish=Visibility.Collapsed;
            AddTestFormVanish = Visibility.Collapsed;
            AddOrEditQuestionFormVanish=Visibility.Collapsed;
            PublishButtonClicked = false;
            AddOrUpdateQuestionButtonContent = "add question";
            AddOrUpdateTestContent = "publish test";
        }
        
        private async Task MakeRegistration()
        {
            var statusCode = await testApi.CreateUser(new UserRequest() { Login = Login, Password = Password, UserType = "student" });
            if (statusCode.StatusCode.Equals(HttpStatusCode.Conflict))
            {
                RegStatus = "such a user already exists";
            }
            else if (statusCode.StatusCode.Equals(HttpStatusCode.Created))
            {
                RegStatus = "you have been successfully registered";
            }

        }
        private async Task MakeLogin()
        {
            var statusCode = await testApi.LoginUser(new UserRequest() { Login = Login, Password = Password});
            if (statusCode.Equals(HttpStatusCode.Conflict))
            {
                MessageBox.Show("something went wrong");
            }
            else if (statusCode.Equals(HttpStatusCode.OK))
            {
                var pair = await testApi.GetTests();
                if (pair.Key == HttpStatusCode.Unauthorized)
                {
                    MessageBox.Show("incorrect authorization");
                }
                else if(pair.Key == HttpStatusCode.Forbidden)
                {
                    MessageBox.Show("unavailable functionality");
                }
                else
                {
                    testService.TestBank = pair.Value;
                    TestsData = ViewHelper.GetTests(pair.Value);
                    AuthFormVanish = Visibility.Collapsed;
                    ChooseTestFormVanish = Visibility.Visible;
                }
            }
        }


        private  void AddTest()
        {
            ChooseTestFormVanish = Visibility.Collapsed;
            AddTestFormVanish = Visibility.Visible;
            AddOrUpdateTestContent = "publish test";
            editQuestionButtonWasClicked = false;
            editTestButtonWasClicked = false;
        }
        private async Task PublishOrUpdateTest()
        {
            if (string.IsNullOrEmpty(AddingTestName) || string.IsNullOrEmpty(AddingTestAttempts))
            {
                MessageBox.Show("incorrect data entered");
                return;
            }
            else
            {
                if (editTestButtonWasClicked)
                {
                    var tests = await testApi.GetTests();
                    var localTests = ViewHelper.GetTests(tests.Value);
                    var localTestName = localTests[(int)TestViewIndex].Substring(0, localTests[(int)TestViewIndex].IndexOf(' '));
                    int testId = tests.Value.FirstOrDefault(x => x.Name == localTestName).Id;
                    var updatedTest = await testApi.UpdateTest(new TestRequest()
                    {
                        Name = AddingTestName,
                        AttemptsCount = int.Parse(AddingTestAttempts)
                    }, testId);
                    if (updatedTest.Equals(HttpStatusCode.OK)) MessageBox.Show("the test container has been updated");
                    else MessageBox.Show("something went wrong");
                }
                else
                {
                    var testAdded = await testApi.AddTest(new TestRequest() { Name = AddingTestName, AttemptsCount = int.Parse(AddingTestAttempts) });
                    if (testAdded.Equals(HttpStatusCode.OK))
                    {

                        var pair = await testApi.GetTests();

                        if (pair.Key == HttpStatusCode.Unauthorized)
                        {
                            MessageBox.Show("incorrect authorization");
                            return;
                        }
                        else if (pair.Key == HttpStatusCode.Forbidden)
                        {
                            MessageBox.Show("unavailable functionality");
                            return;
                        }
                        else if (pair.Key == HttpStatusCode.Conflict)
                        {
                            MessageBox.Show("something went wrong");
                            return;
                        }
                        else
                        {
                            MessageBox.Show($"you have added a test container {AddingTestName}\nnow fill it with questions");
                            testService.TestBank = pair.Value;
                            TestsData = ViewHelper.GetTests(pair.Value);
                        }
                    }

                }
            }
            PublishButtonClicked = true;
        }
        private async Task EditTest()
        {
            if(TestsData==null||TestsData.Count==0)
            {
                MessageBox.Show("There are no tests to edit");
                return;
            }
            if(TestViewIndex==null)
            {
                MessageBox.Show("select some test");
                return;
            }
            var tests = await testApi.GetTests();
            var localTests = ViewHelper.GetTests(tests.Value);
            var localTestName = localTests[(int)TestViewIndex].Substring(0, localTests[(int)TestViewIndex].IndexOf(' '));
            int testId = tests.Value.FirstOrDefault(x => x.Name == localTestName).Id;
            var testName= tests.Value.FirstOrDefault(x => x.Name == localTestName).Name;
            var testAttempts = tests.Value.FirstOrDefault(x => x.Name == localTestName).AttemptsCount;
            var questions = await testApi.GetQuestions(testId);
            AddingTestName = testName;
            AddingTestAttempts = testAttempts.ToString();
            questionService.QuestionBank = questions.Value;
            AddingQuestions = ViewHelper.GetQuestions(questions.Value);
            PublishButtonClicked = true;
            ChooseTestFormVanish = Visibility.Collapsed;
            AddTestFormVanish = Visibility.Visible;
            AddOrUpdateTestContent = "update test";
            editTestButtonWasClicked = true;
        }
        private async Task DeleteTest()
        {
            if (TestsData == null || TestsData.Count == 0)
            {
                MessageBox.Show("There are no tests to edit");
                return;
            }
            var tests = await testApi.GetTests();
            var localTests = ViewHelper.GetTests(tests.Value);
            var testName = localTests[(int)TestViewIndex].Substring(0, localTests[(int)TestViewIndex].IndexOf(' '));
            int testId = tests.Value.FirstOrDefault(x => x.Name == testName).Id;
            var deletedTest=await testApi.DeleteTest(testId);
            if (deletedTest.Equals(HttpStatusCode.OK)) MessageBox.Show("test was deleted");
            else MessageBox.Show("something went wrong");
            testService.TestBank.RemoveAt((int)TestViewIndex);
            TestsData = ViewHelper.GetTests(testService.TestBank);
        }
        private  void AddQuestion()
        {
            AddTestFormVanish = Visibility.Collapsed;
            AddOrEditQuestionFormVanish = Visibility.Visible;
            questionService.ImagePath = null;
            QuestionImage = null;
            QuestionTaskText = "";
            QuestionAnswers = "";
            QuestionRightAnswer = "";
            QuestionValueAnswer = "";
            AddOrUpdateQuestionButtonContent = "add question";
        }
        
        private async Task EditQuestion()
        {
            if(questionService.QuestionBank.Count == 0)
            {
                MessageBox.Show("There is no question to select");
                return;
            }
            if (QuestionIndex != null)
            {
                var tests = await testApi.GetTests();
                int testId = tests.Value.FirstOrDefault(x => x.Name.Equals(AddingTestName)).Id;
                var questions = await testApi.GetQuestions(testId);
                var localQuestion = questionService.QuestionBank[(int)QuestionIndex];
                int questionId = questions.Value.FirstOrDefault(x => x.Text.Equals(localQuestion.Text)).Id;
                var concreteQuestion = await testApi.GetConcreteQuestion(testId, questionId);
                QuestionImage = ImageHelper.BitmapToBitmapSource(ImageHelper.ByteToBitMap(concreteQuestion.Image));
                QuestionTaskText = concreteQuestion.Text;
                QuestionAnswers = concreteQuestion.Answers;
                QuestionRightAnswer = concreteQuestion.RightAnswer;
                QuestionValueAnswer = concreteQuestion.AnswerValue.ToString();
                editQuestionButtonWasClicked = true;
                AddOrUpdateQuestionButtonContent = "update question";
                AddTestFormVanish = Visibility.Collapsed;
                AddOrEditQuestionFormVanish = Visibility.Visible;
                
            }
            else MessageBox.Show("select a question");
        }
        private async Task DeleteQuestion()
        {
            if (questionService.QuestionBank.Count == 0)
            {
                MessageBox.Show("There is no question to select");
                return;
            }
            if (QuestionIndex != null)
            {
                var tests = await testApi.GetTests();
                int testId = tests.Value.FirstOrDefault(x => x.Name.Equals(AddingTestName)).Id;
                var questions = await testApi.GetQuestions(testId);
                var localQuestion = questionService.QuestionBank[(int)QuestionIndex];
                int questionId = questions.Value.FirstOrDefault(x => x.Text.Equals(localQuestion.Text)).Id;
                var deletedQuestion = await testApi.DeleteQuestion(testId, questionId);
                if (deletedQuestion.Equals(HttpStatusCode.OK)) MessageBox.Show("question was deleted");
                else MessageBox.Show("something went wrong");
                questionService.QuestionBank.RemoveAt((int)QuestionIndex);
                AddingQuestions = ViewHelper.GetQuestions(questionService.QuestionBank);
            }
            
        }
        
        private void AddQuestionImage()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            if (!ofd.FileName.Equals(string.Empty))
            {
                var uri = new Uri(ofd.FileName);
                var bitmap = new BitmapImage(uri);
                QuestionImage = bitmap;
                questionService.ImagePath = ofd.FileName;
            }
            addImageButtonWasClicked = true;
        }
        private async Task AddOrUpdateQuestionFromQF()
        {
            if(editQuestionButtonWasClicked)
            {
                if (QuestionIndex != null)
                {
                    int index = (int)QuestionIndex;
                    if (string.IsNullOrEmpty(QuestionValueAnswer) || string.IsNullOrEmpty(QuestionTaskText)
                        || string.IsNullOrEmpty(QuestionAnswers) || string.IsNullOrEmpty(QuestionRightAnswer))
                    {
                        MessageBox.Show("incorrect data entered");
                    }
                    else
                    {
                        var tests = await testApi.GetTests();
                        int testId = tests.Value.FirstOrDefault(x => x.Name.Equals(AddingTestName)).Id;
                        var questions=await testApi.GetQuestions(testId);
                        var localQuestion = questionService.QuestionBank[(int)QuestionIndex];
                        int questionId = questions.Value.FirstOrDefault(x => x.Text.Equals(localQuestion.Text)).Id;
                        var concreteQuestion = await testApi.GetConcreteQuestion(testId, questionId);
                        if (int.TryParse(QuestionValueAnswer, out int answer))
                        {
                            HttpStatusCode updatedQuestion;
                            if (addImageButtonWasClicked)
                            {
                                updatedQuestion = await testApi.UpdateQuestion(new QuestionRequest()
                                {
                                    Text = QuestionTaskText,
                                    Answers = QuestionAnswers,
                                    RightAnswer=QuestionRightAnswer,
                                    AnswerValue=int.Parse(QuestionValueAnswer),
                                    Image= ImageHelper.ImageToByte(questionService.ImagePath)
                                }, testId, questionId);
                            }
                            else
                            {
                                updatedQuestion = await testApi.UpdateQuestion(new QuestionRequest()
                                {
                                    Text = QuestionTaskText,
                                    Answers = QuestionAnswers,
                                    RightAnswer = QuestionRightAnswer,
                                    AnswerValue = int.Parse(QuestionValueAnswer),
                                    Image = concreteQuestion.Image
                                }, testId, questionId);
                            }
                            if(updatedQuestion.Equals(HttpStatusCode.OK))
                            {
                                var questionsToView= await testApi.GetQuestions(testId);
                                questionService.QuestionBank = questionsToView.Value;
                                AddingQuestions = ViewHelper.GetQuestions(questionService.QuestionBank);
                            }
                            QuestionImage = null;
                            QuestionTaskText = "";
                            QuestionAnswers = "";
                            QuestionRightAnswer = "";
                            QuestionValueAnswer = "";
                            AddTestFormVanish = Visibility.Visible;
                            AddOrEditQuestionFormVanish = Visibility.Collapsed;
                        }
                        else
                        {
                            MessageBox.Show("incorrect data entered");
                            return;
                        }
                    }
                }
                editQuestionButtonWasClicked = false;
                addImageButtonWasClicked = false;
                AddOrUpdateQuestionButtonContent = "add question";
                QuestionIndex = null;
                return;
            }

            if(string.IsNullOrEmpty(QuestionValueAnswer) || string.IsNullOrEmpty(QuestionTaskText) 
                || string.IsNullOrEmpty(QuestionAnswers) || string.IsNullOrEmpty(QuestionRightAnswer))
            {
                MessageBox.Show("incorrect data entered");
                return;
            }
            else
            {
                if (int.TryParse(QuestionValueAnswer, out int answer))
                {
                    var tests = await testApi.GetTests();
                    int id = tests.Value.FirstOrDefault(x=>x.Name.Equals(AddingTestName)).Id;
                    var test = await testApi.GetConcreteTest(id);
                    var addedQuestion = await testApi.AddQuestion(new QuestionRequest()
                    {
                        Text = QuestionTaskText,
                        Answers = QuestionAnswers,
                        RightAnswer = QuestionRightAnswer,
                        AnswerValue = int.Parse(QuestionValueAnswer),
                        Image = ImageHelper.ImageToByte(questionService.ImagePath)
                    },test.Value.Id);
                    if (addedQuestion.Equals(HttpStatusCode.OK))
                    {
                        questionService.AddQuestion(ImageHelper.ImageToByte(questionService.ImagePath), QuestionTaskText, QuestionAnswers,
                            QuestionRightAnswer, int.Parse(QuestionValueAnswer));
                    }
                    AddingQuestions = ViewHelper.GetQuestions(questionService.QuestionBank);
                    QuestionImage = null;
                    QuestionTaskText = "";
                    QuestionAnswers = "";
                    QuestionRightAnswer = "";
                    QuestionValueAnswer = "";
                    AddTestFormVanish = Visibility.Visible;
                    AddOrEditQuestionFormVanish = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("incorrect data entered");
                    return;
                }
            }
            addImageButtonWasClicked = false;
            QuestionIndex = null;
        }
        private void BackToAddTestForm()
        {
            AddTestFormVanish = Visibility.Visible;
            AddOrEditQuestionFormVanish = Visibility.Collapsed;
            editQuestionButtonWasClicked = false;
        }
        private async void BackToChooseTestForm()
        {
            ChooseTestFormVanish= Visibility.Visible;
            AddTestFormVanish = Visibility.Collapsed;
            AddingQuestions = null;
            AddingTestName = "";
            AddingTestAttempts = "";
            PublishButtonClicked = false;
            var tests=await testApi.GetTests();
            TestsData = ViewHelper.GetTests(tests.Value);
            AddOrUpdateTestContent = "publish test";
            editTestButtonWasClicked = false; 
        }
        private void BackToAuthForm()
        {
            AuthFormVanish=Visibility.Visible;
            ChooseTestFormVanish = Visibility.Collapsed;
            TestsData = null;
        }
    }
}