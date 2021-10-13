using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace TssT.Core.Exceptions
{
    public class EntityNotFoundException:ProblemDetailsException
    {
        public EntityNotFoundException(int? entityId = null):base(new ProblemDetails()
        {
            Type = "https://example.com/entity-not-found",
            Instance = null,
            Title = "Объект не найден",
            Detail = "Нет объекта для продолжения операции",
            Status = 400
        })
        {
            if (entityId.HasValue)
                Details.Detail = $"Объект c идентификатором {entityId} не найден";
        }
    }
}