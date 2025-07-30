using System.Net;

namespace Cubos.Finance.Core
{
    public class MessageHttp
    {
        private string _menssage;
        public string Menssage
        {
            get { return _menssage; }
            set { _menssage = value; }
        }
        public HttpStatusCode StatusCode { get; set; }
        private string[] _errors;
        public string[] errors
        {
            get { return _errors; }
            set
            {
                _errors = value;
                AddMenssageError();
            }
        }

        private void AddMenssageError()
        {
            if (_errors != null && _errors.Length > 0)
            {
                Menssage += string.Join("\n", _errors);
            }
        }
    }

}
