namespace Autoglass.Domain.Core.Notifications
{
    public class DomainNotification : Notification
    {

        #region Propriedades

        public string Key { get; private set; }
        public string Value { get; private set; }

        #endregion

        #region Construtores

        public DomainNotification(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public DomainNotification()
        {
        }

        #endregion

    }
}
