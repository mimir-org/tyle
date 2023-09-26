using Mimirorg.Common.Models;

namespace Mimirorg.Common.Exceptions;

[Serializable]
public class MimirorgBadRequestException : Exception
{
    private Validation _validation;

    public MimirorgBadRequestException(string message) : base(message)
    {
        _validation = new Validation
        {
            Message = message
        };
    }

    public MimirorgBadRequestException(string message, Validation validation) : base(message)
    {
        _validation = validation;
        _validation.Message = message;
    }

    //TODO override MimirorgBadRequestException

    public IEnumerable<MimirorgBadRequest> Errors()
    {
        if (_validation.IsValid)
            yield break;

        if (!_validation.Result.Any())
            yield break;

        foreach (var result in _validation.Result)
        {
            if (string.IsNullOrEmpty(result.ErrorMessage) || !result.MemberNames.Any())
                continue;

            foreach (var name in result.MemberNames)
            {
                if (string.IsNullOrEmpty(name))
                    continue;

                yield return new MimirorgBadRequest
                {
                    Key = name,
                    Error = result.ErrorMessage ?? string.Empty
                };
            }
        }
    }
}

public class MimirorgBadRequest
{
    public string Key { get; set; }
    public string Error { get; set; }
}