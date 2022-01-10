using Mimirorg.Common.Models;

namespace Mimirorg.Common.Exceptions
{
    [Serializable]
    public class ModelBuilderBadRequestException : Exception
    {
        private Validation _validation;

        public ModelBuilderBadRequestException(string message) : base(message)
        {
            _validation = new Validation
            {
                Message = message
            };
        }

        public ModelBuilderBadRequestException(string message, Validation validation) : base(message)
        {
            _validation = validation;
            _validation.Message = message;
        }

        public IEnumerable<ModelBuilderBadRequest> Errors()
        {
            if (_validation.IsValid)
                yield break;

            if(!_validation.Result.Any())
                yield break;

            foreach (var result in _validation.Result)
            {
                if(string.IsNullOrEmpty(result.ErrorMessage) || !result.MemberNames.Any())
                    continue;

                foreach (var name in result.MemberNames)
                {
                    if(string.IsNullOrEmpty(name))
                        continue;

                    yield return new ModelBuilderBadRequest
                    {
                        Key = name,
                        Error = result.ErrorMessage ?? string.Empty
                    };
                }
            }
        }
    }

    public class ModelBuilderBadRequest
    {
        public string Key { get; set; }
        public string Error { get; set; }
    }
}
