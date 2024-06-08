using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autoglass.Domain.Core.Models
{
    public class CommandResult
    {
        #region Properties
        public bool Success { get; set; }
        public string Message { get; set; }
        public IList<CommandResultError> Errors { get; set; }
        #endregion

        #region Methods
        public CommandResult(bool success)
        {
            Success = success;
            WriteLog();
        }
        public CommandResult(bool success, string message)
        {
            Success = success;
            Message = message;
            WriteLog();
        }

        public CommandResult(bool success, string message, IList<CommandResultError> errors)
        {
            Success = success;
            Message = message;
            Errors = new List<CommandResultError>();
            if (errors != null) Errors = errors.ToList();
            WriteLog();
        }

        public CommandResult(bool success, string message, IList<ValidationFailure> errors)
        {
            Success = success;
            Message = message;
            Errors = new List<CommandResultError>();
            foreach (var erro in errors)
                this.Errors.Add(new CommandResultError(erro.PropertyName, erro.ErrorMessage));
            WriteLog();
        }

        private void WriteLog()
        {
            Console.ForegroundColor = Success ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"Command: {Message}");
            if (Errors != null && Errors.Count() > 0) Errors.ToList().ForEach(x => Console.WriteLine($"Command error detail: Property: {x.Property} => Message: {x.Message}"));
            Console.ForegroundColor = ConsoleColor.White;
        }

        #endregion
    }

    public class CommandResult<T> : CommandResult
    {
        #region Properties
        public T Data { get; set; }

        #endregion

        #region Constructors

        public CommandResult(bool success, string message, T data) : base(success, message)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public CommandResult(bool success, string message, T data, IList<CommandResultError> errors) : base(success, message, errors)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public CommandResult(bool success, string message, T data, IList<ValidationFailure> errors) : base(success, message, errors)
        {
            Success = success;
            Message = message;
            Data = data;
        }
        #endregion
    }
}
