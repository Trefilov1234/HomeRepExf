using EditTestClient.Api.Responses;
using System.Collections.Generic;

namespace EditTestClient.Services.Tests
{
    public interface ITestService
    {
		// todo(v): вместо этого списка можно использовать список во VM в ObservableCollection
		// todo(v): хранить данные лучше не в TestResponse, а в TestModel
		public List<TestResponse> TestBank { get; set; }
    }
}
