﻿using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestServer.Extensions;
using TestServer.Common.Extensions;

namespace TestServer
{
    public class Server : IServer
    {
        private readonly ICommand[] _commands;

        private readonly object _lockObject = new();

        private HttpListener _listener;

        public Server(ICommand[] commands)
        {
            _commands = commands;
        }

        public async Task StartAsync(string uri)
        {
            lock (_lockObject)
            {
                if (_listener != null)
                    throw new InvalidOperationException("Server is started");

                _listener = new HttpListener();

                _listener.Prefixes.Add(uri);
                _listener.Start();
            }

            Console.WriteLine("Server started");

            while (_listener.IsListening)
                await HandleNextRequestAsync().ConfigureAwait(false);
        }

        private async Task HandleNextRequestAsync()
        {
            var context = await _listener.GetContextAsync().ConfigureAwait(false);

            Task.Run(async () =>
            {
                try
                {
                    Console.WriteLine($"Request {context.Request.HttpMethod} {context.Request.Url}");

                    var method = context.Request.HttpMethod;
                    var path = context.Request.Url.AbsolutePath.TrimEnd('/');

                    var commandsWithRegex = _commands
                    .Where(command => command.Method.ToString() == method)
                    .Select(command => new
                    {
                        Command = command,
                        Path = Regex.Match(path, $"^{command.Path}$", RegexOptions.IgnoreCase)
                    })
                    .Where(group => group.Path.Success)
                    .ToArray();

                    if (!commandsWithRegex.Any())
                    {
                        await context.WriteResponseAsync(501, $"Not found command for path {path} with method {method}").ConfigureAwait(false);
                        return;
                    }

                    if (commandsWithRegex.Length > 1)
                    {
                        await context.WriteResponseAsync(501, $"Multiple binding command for path {path} with method {method}").ConfigureAwait(false);
                        return;
                    }

                    var single = commandsWithRegex.Single();
                    await single.Command.HandleRequestAsync(context, single.Path).ConfigureAwait(false);

                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    await context.WriteResponseAsync(500, exception.Message).ConfigureAwait(false);
                }
            }).FireAndForgetSafeAsync();
        }

        public void Dispose()
        {
            _listener?.Stop();
        }
    }
}
