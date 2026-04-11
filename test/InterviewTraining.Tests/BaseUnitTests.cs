using System.Threading;

namespace InterviewTraining.Tests;

public abstract class BaseUnitTests
{
    protected CancellationToken Token;

    protected BaseUnitTests()
    {
        Token = new CancellationTokenSource().Token;
    }
}
