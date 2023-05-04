using EditTestClient.Api;
using EditTestClient.Api.Extensions;
using EditTestClient.Api.Helpers;
using EditTestClient.Api.Questions;
using EditTestClient.Api.Requests;
using EditTestClient.Api.Results;
using EditTestClient.Api.Tests;
using EditTestClient.Api.Users;
using EditTestClient.Services.CalculateResults;
using EditTestClient.Services.Questions;
using EditTestClient.Services.Tests;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EditTestClient.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private const string BaseUri = "http://127.0.0.1:8888";

        private string login;
        private string password;
        private string regStatus;
        private string token;
        private string clientUserRole;

        private int? testViewIndex;
        private int? questionIndex;
        private int currQuestionIndex;

        private ObservableCollection<string> testsData;

        private string addingTestName;
        private string addingTestAttempts;

        private ObservableCollection<string> addingQuestions;
        private ImageSource questionImage;
        private string questionTaskText;
        private string questionAnswers;
        private string questionRightAnswer;
        private string questionValueAnswer;

        private string solutionPartTestQuestion;
        private string solutionPartTestAnswerVariants;
        private string solutionPartUserAnswer;

        private string userResult;
        private string userAttempts;

        private ObservableCollection<string> historySource;

        private bool authFormVanish;
        private bool chooseTestFormVanish;
        private bool addTestFormVanish;
        private bool addOrEditQuestionFormVanish;
        private bool studentPartSelectTestFormVanish;
        private bool studentPartSolveTestFormVanish;
        private bool studentPartResultFormVanish;
        private bool teacherSelectModeFormVanish;
        private bool historyFormVanish;

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
        private ICommand backToStudentPartSelectTestFormCommand;

        private ICommand studentPartSelectTestCommand;
        private ICommand solutionPartPrevQuestionCommand;
        private ICommand solutionPartNextQuestionCommand;

        private ICommand studentModeActivateCommand;
        private ICommand buildTestModeActivateCommand;

        private ICommand backToSelectModeFormCommand;

        private ICommand showHistoryFormCommand;
        private ICommand backToResultFormCommand;

        private bool editQuestionButtonWasClicked;
        private bool addImageButtonWasClicked;
        private bool editTestButtonWasClicked;

        private bool publishButtonClicked;
        private bool publishTestButtonClickedForDisable;

        private string addOrUpdateQuestionButtonContent;
        private string addOrUpdateTestContent;
        private string nextQuestionButtonContent;

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
        public int? QuestionIndex
        {
            get => questionIndex;
            set => Set(ref questionIndex, value);
        }

        public ObservableCollection<string> TestsData
        {
            get => testsData;
            set => Set(ref testsData, value);
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
        public ObservableCollection<string> AddingQuestions
        {
            get => addingQuestions;
            set => Set(ref addingQuestions, value);
        }

        public ImageSource QuestionImage
        {
            get => questionImage;
            set => Set(ref questionImage, value);
        }
        public string QuestionTaskText
        {
            get => questionTaskText;
            set => Set(ref questionTaskText, value);
        }
        public string QuestionAnswers
        {
            get => questionAnswers;
            set => Set(ref questionAnswers, value);
        }
        public string QuestionRightAnswer
        {
            get => questionRightAnswer;
            set => Set(ref questionRightAnswer, value);
        }
        public string QuestionValueAnswer
        {
            get => questionValueAnswer;
            set => Set(ref questionValueAnswer, value);
        }

        public string SolutionPartTestQuestion
        {
            get => solutionPartTestQuestion;
            set => Set(ref solutionPartTestQuestion, value);
        }
        public string SolutionPartTestAnswerVariants
        {
            get => solutionPartTestAnswerVariants;
            set => Set(ref solutionPartTestAnswerVariants, value);
        }
        public string SolutionPartUserAnswer
        {
            get => solutionPartUserAnswer;
            set => Set(ref solutionPartUserAnswer, value);
        }

        public ObservableCollection<string> HistorySource
        {
            get => historySource;
            set => Set(ref historySource, value);
        }

        public string UserResult
        {
            get => userResult;
            set => Set(ref userResult, value);
        }
        public string UserAttempts
        {
            get => userAttempts;
            set => Set(ref userAttempts, value);
        }

        public bool AuthFormVanish
        {
            get => authFormVanish;
            set => Set(ref authFormVanish, value);
        }
        public bool ChooseTestFormVanish
        {
            get => chooseTestFormVanish;
            set => Set(ref chooseTestFormVanish, value);
        }
        public bool AddTestFormVanish
        {
            get => addTestFormVanish;
            set => Set(ref addTestFormVanish, value);
        }
        public bool AddOrEditQuestionFormVanish
        {
            get => addOrEditQuestionFormVanish;
            set => Set(ref addOrEditQuestionFormVanish, value);
        }
        public bool StudentPartSelectTestFormVanish
        {
            get => studentPartSelectTestFormVanish;
            set => Set(ref studentPartSelectTestFormVanish, value);
        }
        public bool StudentPartSolveTestFormVanish
        {
            get => studentPartSolveTestFormVanish;
            set => Set(ref studentPartSolveTestFormVanish, value);
        }
        public bool StudentPartResultFormVanish
        {
            get => studentPartResultFormVanish;
            set => Set(ref studentPartResultFormVanish, value);
        }
        public bool TeacherSelectModeFormVanish
        {
            get => teacherSelectModeFormVanish;
            set => Set(ref teacherSelectModeFormVanish, value);
        }
        public bool HistoryFormVanish
        {
            get => historyFormVanish;
            set => Set(ref historyFormVanish, value);
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
        public ICommand BackToStudentPartSelectTestFormCommand => backToStudentPartSelectTestFormCommand ?? (backToStudentPartSelectTestFormCommand = new AsyncRelayCommand(BackToStudentPartSelectTestForm));

        public ICommand StudentPartSelectTestCommand => studentPartSelectTestCommand ?? (studentPartSelectTestCommand = new AsyncRelayCommand(StudPartSelectTest));
        public ICommand SolutionPartPrevQuestionCommand => solutionPartPrevQuestionCommand ?? (solutionPartPrevQuestionCommand = new AsyncRelayCommand(StudPartPrevQuestion));
        public ICommand SolutionPartNextQuestionCommand => solutionPartNextQuestionCommand ?? (solutionPartNextQuestionCommand = new AsyncRelayCommand(StudPartNextQuestion));

        public ICommand BuildTestModeActivateCommand => buildTestModeActivateCommand ?? (buildTestModeActivateCommand = new AsyncRelayCommand(ActivateBuildTestMode));
        public ICommand StudentModeActivateCommand => studentModeActivateCommand ?? (studentModeActivateCommand = new AsyncRelayCommand(ActivateStudentMode));

        public ICommand BackToSelectModeFormCommand => backToSelectModeFormCommand ?? (backToSelectModeFormCommand = new RelayCommand(BackToSelectModeForm));

        public ICommand ShowHistoryFormCommand => showHistoryFormCommand ?? (showHistoryFormCommand = new AsyncRelayCommand(ShowHistory));
        public ICommand BackToResultFormCommand => backToResultFormCommand ?? (backToResultFormCommand = new RelayCommand(BackToResultForm));

        public bool PublishButtonClicked
        {
            get => publishButtonClicked;
            set => Set(ref publishButtonClicked, value);
        }
        public bool PublishTestButtonClickedForDisable
        {
            get => publishTestButtonClickedForDisable;
            set => Set(ref publishTestButtonClickedForDisable, value);
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
        public string NextQuestionButtonContent
        {
            get => nextQuestionButtonContent;
            set => Set(ref nextQuestionButtonContent, value);
        }

        private readonly ITestApi testApi = new TestApi(BaseUri);
        private readonly IQuestionApi questionApi = new QuestionApi(BaseUri);
        private readonly IUserApi userApi = new UserApi(BaseUri);
        private readonly IResultApi resultApi = new ResultApi(BaseUri);

        private readonly IQuestionService questionService = new QuestionService();
        private readonly ITestService testService = new TestService();
        private readonly ICalculateResultService resultService = new CalculateResultService();

        public MainViewModel()
        {
            AuthFormVanish = true;
            ChooseTestFormVanish = false;
            AddTestFormVanish = false;
            AddOrEditQuestionFormVanish = false;
            StudentPartSelectTestFormVanish = false;
            StudentPartSolveTestFormVanish = false;
            StudentPartResultFormVanish = false;
            TeacherSelectModeFormVanish = false;
            HistoryFormVanish = false;
            PublishButtonClicked = false;
            PublishTestButtonClickedForDisable = true;
            AddOrUpdateQuestionButtonContent = "add question";
            AddOrUpdateTestContent = "publish test";
            NextQuestionButtonContent = "next question";
        }

        private async Task MakeRegistration()
        {
            var statusCode = await userApi.CreateUser(new UserRequest() { Login = Login, Password = Password, UserType = "student" });
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
            var statusCode = await userApi.LoginUser(new UserRequest() { Login = Login, Password = Password });
            token = statusCode.user.JWT.ToString();
            clientUserRole = statusCode.user.UserType.ToString();
            if (!statusCode.statusCode.Equals(HttpStatusCode.OK))
            {
                MessageBox.Show("something went wrong");
                return;
            }
            var user = statusCode.user;
            if (user.UserType.Equals(UserRoles.Student))
            {
                var pair = await testApi.GetTests(token);
                if (pair.statusCode == HttpStatusCode.Unauthorized)
                {
                    MessageBox.Show("incorrect authorization");
                }
                else if (pair.statusCode == HttpStatusCode.Forbidden)
                {
                    MessageBox.Show("unavailable functionality");
                }
                else
                {
                    testService.TestBank = pair.tests;
                    TestsData = new ObservableCollection<string>(ViewHelper.GetTests(pair.tests));
                    AuthFormVanish = false;
                    StudentPartSelectTestFormVanish = true;
                }
            }
            if (user.UserType.Equals(UserRoles.Teacher))
            {
                AuthFormVanish = false;
                TeacherSelectModeFormVanish = true;
            }

        }

        private void AddTest()
        {
            ChooseTestFormVanish = false;
            AddTestFormVanish = true;
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
            if (editTestButtonWasClicked)
            {
                var tests = await testApi.GetTests(token);
                var localTests = ViewHelper.GetTests(tests.tests);
                var localTestName = localTests[(int)TestViewIndex].Substring(0, localTests[(int)TestViewIndex].IndexOf(' '));
                int testId = tests.tests.FirstOrDefault(x => x.Name == localTestName).Id;
                var updatedTest = await testApi.UpdateTest(new TestRequest()
                {
                    Name = AddingTestName,
                    AttemptsCount = int.Parse(AddingTestAttempts)
                }, testId, token);
                if (updatedTest.Equals(HttpStatusCode.OK)) MessageBox.Show("the test container has been updated");
                else MessageBox.Show("something went wrong");
            }
            else
            {
                var testAdded = await testApi.AddTest(new TestRequest() { Name = AddingTestName, AttemptsCount = int.Parse(AddingTestAttempts) }, token);
                if (!testAdded.Equals(HttpStatusCode.OK))
                {
                    MessageBox.Show("test with this name already exists");
                    return;
                }
                var pair = await testApi.GetTests(token);
                if (pair.statusCode == HttpStatusCode.Unauthorized)
                {
                    MessageBox.Show("incorrect authorization");
                    return;
                }
                if (pair.statusCode == HttpStatusCode.Forbidden)
                {
                    MessageBox.Show("unavailable functionality");
                    return;
                }
                if (pair.statusCode == HttpStatusCode.Conflict)
                {
                    MessageBox.Show("something went wrong");
                    return;
                }
                MessageBox.Show($"you have added a test container {AddingTestName}\nnow fill it with questions");
                testService.TestBank = pair.tests;
                TestsData = new ObservableCollection<string>(ViewHelper.GetTests(pair.tests));

                PublishTestButtonClickedForDisable = false;
            }
            PublishButtonClicked = true;
        }

        private async Task EditTest()
        {
            if (TestsData == null || TestsData.Count == 0)
            {
                MessageBox.Show("There are no tests to edit");
                return;
            }
            if (TestViewIndex == null)
            {
                MessageBox.Show("select some test");
                return;
            }
            var tests = await testApi.GetTests(token);
            int testId = tests.tests[(int)TestViewIndex].Id;
            var testName = tests.tests[(int)TestViewIndex].Name;
            var testAttempts = tests.tests[(int)TestViewIndex].AttemptsCount;
            var questions = await questionApi.GetQuestions(testId, token);
            AddingTestName = testName;
            AddingTestAttempts = testAttempts.ToString();
            questionService.QuestionBank = questions.questions;
            AddingQuestions = new ObservableCollection<string>(ViewHelper.GetQuestions(questions.questions));
            PublishButtonClicked = true;
            ChooseTestFormVanish = false;
            AddTestFormVanish = true;
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
            var tests = await testApi.GetTests(token);
            int testId = tests.tests[(int)TestViewIndex].Id;
            var deletedTest = await testApi.DeleteTest(testId, token);
            if (deletedTest.Equals(HttpStatusCode.OK)) MessageBox.Show("test was deleted");
            else MessageBox.Show("something went wrong");
            testService.TestBank.RemoveAt((int)TestViewIndex);
            TestsData = new ObservableCollection<string>(ViewHelper.GetTests(testService.TestBank));
        }

        private void AddQuestion()
        {
            AddTestFormVanish = false;
            AddOrEditQuestionFormVanish = true;
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
            if (questionService.QuestionBank.Count == 0)
            {
                MessageBox.Show("There is no question to select");
                return;
            }
            if (QuestionIndex != null)
            {
                var tests = await testApi.GetTests(token);
                int testId = tests.tests.FirstOrDefault(x => x.Name.Equals(AddingTestName)).Id;
                var questions = await questionApi.GetQuestions(testId, token);
                var questionId = questions.questions[(int)QuestionIndex].Id;
                var concreteQuestion = await questionApi.GetQuestion(testId, questionId, token);
                QuestionImage = ImageHelper.BitmapToBitmapSource(ImageHelper.ByteToBitMap(concreteQuestion.Image));
                QuestionTaskText = concreteQuestion.Text;
                QuestionAnswers = concreteQuestion.Answers;
                QuestionRightAnswer = concreteQuestion.RightAnswer;
                QuestionValueAnswer = concreteQuestion.AnswerValue.ToString();
                editQuestionButtonWasClicked = true;
                AddOrUpdateQuestionButtonContent = "update question";
                AddTestFormVanish = false;
                AddOrEditQuestionFormVanish = true;
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
                var tests = await testApi.GetTests(token);
                int testId = tests.tests.FirstOrDefault(x => x.Name.Equals(AddingTestName)).Id;
                var questions = await questionApi.GetQuestions(testId, token);
                var questionId = questions.questions[(int)QuestionIndex].Id;
                var deletedQuestion = await questionApi.DeleteQuestion(testId, questionId, token);
                if (deletedQuestion.Equals(HttpStatusCode.OK)) MessageBox.Show("question was deleted");
                else MessageBox.Show("something went wrong");
                questionService.QuestionBank.RemoveAt((int)QuestionIndex);
                AddingQuestions = new ObservableCollection<string>(ViewHelper.GetQuestions(questionService.QuestionBank));
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
            if (string.IsNullOrEmpty(QuestionValueAnswer) || string.IsNullOrEmpty(QuestionTaskText)
                    || string.IsNullOrEmpty(QuestionAnswers) || string.IsNullOrEmpty(QuestionRightAnswer))
            {
                MessageBox.Show("incorrect data entered");
                return;
            }
            if (!int.TryParse(QuestionValueAnswer, out int answer))
            {
                MessageBox.Show("incorrect data entered");
                return;
            }
            var tests = await testApi.GetTests(token);
            int testId = tests.tests.FirstOrDefault(x => x.Name.Equals(AddingTestName)).Id;
            if (editQuestionButtonWasClicked)
            {
                var questions = await questionApi.GetQuestions(testId, token);
                var questionId = questions.questions[(int)QuestionIndex].Id;
                var concreteQuestion = await questionApi.GetQuestion(testId, questionId, token);
                HttpStatusCode updatedQuestion;
                if (addImageButtonWasClicked)
                {
                    updatedQuestion = await questionApi.UpdateQuestion(new QuestionRequest()
                    {
                        Text = QuestionTaskText,
                        Answers = QuestionAnswers,
                        RightAnswer = QuestionRightAnswer,
                        AnswerValue = int.Parse(QuestionValueAnswer),
                        Image = ImageHelper.ImageToByte(questionService.ImagePath)
                    }, testId, questionId, token);
                }
                else
                {
                    updatedQuestion = await questionApi.UpdateQuestion(new QuestionRequest()
                    {
                        Text = QuestionTaskText,
                        Answers = QuestionAnswers,
                        RightAnswer = QuestionRightAnswer,
                        AnswerValue = int.Parse(QuestionValueAnswer),
                        Image = concreteQuestion.Image
                    }, testId, questionId, token);
                }
                if (updatedQuestion.Equals(HttpStatusCode.OK))
                {
                    var questionsToView = await questionApi.GetQuestions(testId, token);
                    questionService.QuestionBank = questionsToView.questions;
                    AddingQuestions = new ObservableCollection<string>(ViewHelper.GetQuestions(questionService.QuestionBank));
                }
                AddOrUpdateQuestionButtonContent = "add question";
            }
            else
            {
                var addedQuestion = await questionApi.AddQuestion(new QuestionRequest()
                {
                    Text = QuestionTaskText,
                    Answers = QuestionAnswers,
                    RightAnswer = QuestionRightAnswer,
                    AnswerValue = int.Parse(QuestionValueAnswer),
                    Image = ImageHelper.ImageToByte(questionService.ImagePath)
                }, testId, token);
                if (addedQuestion.Equals(HttpStatusCode.OK))
                {
                    var questionsToView = await questionApi.GetQuestions(testId, token);
                    questionService.QuestionBank = questionsToView.questions;
                    AddingQuestions = new ObservableCollection<string>(ViewHelper.GetQuestions(questionService.QuestionBank));
                }
            }
            QuestionImage = null;
            QuestionTaskText = "";
            QuestionAnswers = "";
            QuestionRightAnswer = "";
            QuestionValueAnswer = "";
            AddTestFormVanish = true;
            AddOrEditQuestionFormVanish = false;
            addImageButtonWasClicked = false;
            QuestionIndex = null;
        }

        private void BackToAddTestForm()
        {
            AddTestFormVanish = true;
            AddOrEditQuestionFormVanish = false;
            editQuestionButtonWasClicked = false;
        }

        private async void BackToChooseTestForm()
        {
            ChooseTestFormVanish = true;
            AddTestFormVanish = false;
            AddingQuestions = null;
            AddingTestName = "";
            AddingTestAttempts = "";
            PublishButtonClicked = false;
            var tests = await testApi.GetTests(token);
            TestsData = new ObservableCollection<string>(ViewHelper.GetTests(tests.tests));
            AddOrUpdateTestContent = "publish test";
            editTestButtonWasClicked = false;
            PublishTestButtonClickedForDisable = true;
        }

        private void BackToAuthForm()
        {
            AuthFormVanish = true;
            ChooseTestFormVanish = false;
            StudentPartSelectTestFormVanish = false;
            TeacherSelectModeFormVanish = false;
            TestsData = null;
        }

        private async Task StudPartSelectTest()
        {
            currQuestionIndex = 0;
            if (TestViewIndex == null)
            {
                MessageBox.Show("select a test");
                return;
            }
            var tests = await testApi.GetTests(token);
            var test = tests.tests[(int)TestViewIndex];
            int testId = test.Id;
            var resultHistory = await resultApi.GetResults(testId, token);
            if (test.AttemptsCount - resultHistory.results.Count() == 0)
            {
                MessageBox.Show("you have run out of attempts to pass");
                return;
            }
            var questions = await questionApi.GetQuestions(testId, token);
            questionService.QuestionBank = questions.questions;
            resultService.InitializeRightAnswers(questionService.QuestionBank);
            var localQuestion = questionService.QuestionBank[0];
            int questionId = questions.questions.FirstOrDefault(x => x.Text.Equals(localQuestion.Text)).Id;
            var concreteQuestion = await questionApi.GetQuestion(testId, questionId, token);
            QuestionImage = ImageHelper.BitmapToBitmapSource(ImageHelper.ByteToBitMap(concreteQuestion.Image));
            SolutionPartTestQuestion = concreteQuestion.Text;
            SolutionPartTestAnswerVariants = ViewHelper.ListToString(ViewHelper.GetAnswers(concreteQuestion.Answers));
            StudentPartSolveTestFormVanish = true;
            StudentPartSelectTestFormVanish = false;
        }

        private async Task BackToStudentPartSelectTestForm()
        {
            var tests = await testApi.GetTests(token);
            UserResult = "";
            UserAttempts = "";
            SolutionPartUserAnswer = "";
            TestsData = new ObservableCollection<string>(ViewHelper.GetTests(tests.tests));
            StudentPartSolveTestFormVanish = false;
            StudentPartResultFormVanish = false;
            StudentPartSelectTestFormVanish = true;
            resultService.UserAnswers.Clear();
            resultService.RightAnswers.Clear();
            currQuestionIndex = 0;
        }

        private async Task StudPartPrevQuestion()
        {
            if (currQuestionIndex == 0)
            {
                MessageBox.Show("this is the first question of the test\nit is not possible to go to the previous question");
                return;
            }
            currQuestionIndex--;
            var currQuestion = questionService.QuestionBank[currQuestionIndex];
            var tests = await testApi.GetTests(token);
            int testId = tests.tests.FirstOrDefault(x => x.Name == currQuestion.TestName).Id;
            var concreteQuestion = await questionApi.GetQuestion(testId, currQuestion.Id, token);
            QuestionImage = ImageHelper.BitmapToBitmapSource(ImageHelper.ByteToBitMap(concreteQuestion.Image));
            SolutionPartTestQuestion = concreteQuestion.Text;
            SolutionPartTestAnswerVariants = ViewHelper.ListToString(ViewHelper.GetAnswers(concreteQuestion.Answers));
            if (currQuestionIndex < resultService.UserAnswers.Count())
                SolutionPartUserAnswer = ViewHelper.ListToString(resultService.UserAnswers[currQuestionIndex]);
            else SolutionPartUserAnswer = "";
        }

        private async Task StudPartNextQuestion()
        {
            resultService.AddOrUpdateUserAnswer(currQuestionIndex, SolutionPartUserAnswer);
            if (currQuestionIndex.Equals(questionService.QuestionBank.Count - 2))
            {
                NextQuestionButtonContent = "complete the test";
            }
            else NextQuestionButtonContent = "next question";
            if (currQuestionIndex.Equals(questionService.QuestionBank.Count - 1))
            {
                UserResult = resultService.CalculateResult().ToString();
                var question = questionService.QuestionBank[0];
                var resTests = await testApi.GetTests(token);
                int resTestId = resTests.tests.FirstOrDefault(x => x.Name == question.TestName).Id;
                await resultApi.AddResult(resTestId, int.Parse(UserResult), token);
                int testAttempts = resTests.tests.FirstOrDefault(x => x.Name == question.TestName).AttemptsCount;
                var resultHistory = await resultApi.GetResults(resTestId, token);
                UserAttempts = (testAttempts - resultHistory.results.Count()).ToString();
                StudentPartSolveTestFormVanish = false;
                StudentPartResultFormVanish = true;
                return;
            }
            currQuestionIndex++;
            var currQuestion = questionService.QuestionBank[currQuestionIndex];
            var tests = await testApi.GetTests(token);
            int testId = tests.tests.FirstOrDefault(x => x.Name == currQuestion.TestName).Id;
            var concreteQuestion = await questionApi.GetQuestion(testId, currQuestion.Id, token);
            QuestionImage = ImageHelper.BitmapToBitmapSource(ImageHelper.ByteToBitMap(concreteQuestion.Image));
            SolutionPartTestQuestion = concreteQuestion.Text;
            SolutionPartTestAnswerVariants = ViewHelper.ListToString(ViewHelper.GetAnswers(concreteQuestion.Answers));
            if (currQuestionIndex < resultService.UserAnswers.Count())
                SolutionPartUserAnswer = ViewHelper.ListToString(resultService.UserAnswers[currQuestionIndex]);
            else SolutionPartUserAnswer = "";
        }

        private async Task ActivateBuildTestMode()
        {
            TeacherSelectModeFormVanish = false;
            var pair = await testApi.GetTests(token);
            if (pair.statusCode == HttpStatusCode.Unauthorized)
            {
                MessageBox.Show("incorrect authorization");
                return;
            }
            if (pair.statusCode == HttpStatusCode.Forbidden)
            {
                MessageBox.Show("unavailable functionality");
                return;
            }
            testService.TestBank = pair.tests;
            TestsData = new ObservableCollection<string>(ViewHelper.GetTests(pair.tests));
            AuthFormVanish = false;
            ChooseTestFormVanish = true;
        }

        private async Task ActivateStudentMode()
        {
            TeacherSelectModeFormVanish = false;
            var pair = await testApi.GetTests(token);
            if (pair.statusCode == HttpStatusCode.Unauthorized)
            {
                MessageBox.Show("incorrect authorization");
                return;
            }
            if (pair.Item1 == HttpStatusCode.Forbidden)
            {
                MessageBox.Show("unavailable functionality");
                return;
            }
            testService.TestBank = pair.tests;
            TestsData = new ObservableCollection<string>(ViewHelper.GetTests(pair.tests));
            AuthFormVanish = false;
            StudentPartSelectTestFormVanish = true;
        }

        private void BackToSelectModeForm()
        {
            if (clientUserRole == UserRoles.Teacher)
            {
                StudentPartSelectTestFormVanish = false;
                ChooseTestFormVanish = false;
                TeacherSelectModeFormVanish = true;
                return;
            }
            StudentPartSelectTestFormVanish = false;
            AuthFormVanish = true;
        }

        private async Task ShowHistory()
        {
            var question = questionService.QuestionBank[0];
            var resTests = await testApi.GetTests(token);
            int resTestId = resTests.tests.FirstOrDefault(x => x.Name == question.TestName).Id;
            await resultApi.AddResult(resTestId, int.Parse(UserResult), token);
            int testAttempts = resTests.tests.FirstOrDefault(x => x.Name == question.TestName).AttemptsCount;
            var resultHistory = await resultApi.GetResults(resTestId, token);
            HistorySource = new ObservableCollection<string>(ViewHelper.GetResults(resultHistory.results));
            HistoryFormVanish = true;
            StudentPartResultFormVanish = false;
        }

        private void BackToResultForm()
        {
            HistoryFormVanish = false;
            StudentPartResultFormVanish = true;
        }
    }
}