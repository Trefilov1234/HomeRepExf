﻿using EditTestClient.Api.Responses;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using EditTestClient.Api.Helpers;

namespace EditTestClient.Api.Results
{
    public class ResultApi : ApiBase, IResultApi
    {
        public ResultApi(string baseUri) : base(baseUri) { }

        public async Task<HttpStatusCode> AddResult(int testId, int result, string token)
        {
            var response = await SendAsync(HttpMethod.Post, $"/tests/{testId}/results", token, body: result);
            return response.StatusCode;
        }

        public async Task<(HttpStatusCode StatusCode, List<ResultResponse> Results)> GetResults(int testId, string token)
        {
            var response = await SendAsync(HttpMethod.Get, $"/tests/{testId}/results", token);
            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var resultResp = JsonSerializeHelper.Deserialize<List<ResultResponse>>(responseBody);
            return (response.StatusCode, resultResp);
        }
    }
}
