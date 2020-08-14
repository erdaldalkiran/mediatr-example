using business.behaviours;
using MediatR;

namespace business.commands
{
    public class DoExceptionalThingsCommandAsync : IRequest, IRetriable
    {
    }
}