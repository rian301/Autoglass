using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Autoglass.Domain.Core.Models;

namespace Autoglass.Domain.Core.Notifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        #region Propriedades
        private List<DomainNotification> _notifications;
        #endregion

        #region Construtores
        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }
        #endregion

        #region Métodos
        public virtual List<DomainNotification> GetNotifications()
        {
            return _notifications;
        }

        public void Handle(DomainNotification message)
        {
            _notifications.Add(message);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Erro: {message.MessageType} - {message.Value}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Handle(IList<ValidationFailure> erros)
        {
            foreach (var item in erros)
            {
                Handle(new DomainNotification(item.PropertyName, item.ErrorMessage));
            }
        }

        public void Handle(CommandResult errors)
        {
            Handle(new DomainNotification(null, errors.Message));
            foreach (var item in errors.Errors)
            {
                Handle(new DomainNotification(item.Property, item.Message));
            }
        }

        public virtual bool HasNotifications()
        {
            return _notifications.Any();
        }

        public void Dispose()
        {
            _notifications = new List<DomainNotification>();
        }

        public void Clear()
        {
            _notifications.Clear();
        }

        #endregion
    }
}
