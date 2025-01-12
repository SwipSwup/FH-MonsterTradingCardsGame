using Server.Routing;

namespace Tests.Server.Routing;

[TestFixture]
public class RouterTests
{
    private Router _router;

    [SetUp]
    public void SetUp()
    {
        _router = new Router();
    }

    [Test]
    public void RegisterRoute_ShouldRegisterHandlerForGetMethod()
    {
        HttpMethod method = HttpMethod.Get;
        string path = "/test";
        RouteHandler handler = _ => Task.FromResult(new HttpResponseMessage());

        _router.RegisterRoute(method, path, handler);

        bool isRegistered = _router.TryGetHandler(method, path, out RouteHandler? retrievedHandler);
        Assert.IsTrue(isRegistered);
        Assert.That(retrievedHandler, Is.EqualTo(handler));
    }

    [Test]
    public void RegisterRoute_ShouldRegisterHandlerForPostMethod()
    {
        HttpMethod method = HttpMethod.Post;
        string path = "/create";
        RouteHandler handler = _ => Task.FromResult(new HttpResponseMessage());

        _router.RegisterRoute(method, path, handler);

        bool isRegistered = _router.TryGetHandler(method, path, out RouteHandler? retrievedHandler);
        Assert.IsTrue(isRegistered);
        Assert.That(retrievedHandler, Is.EqualTo(handler));
    }

    [Test]
    public void RegisterRoute_ShouldNotRegisterHandlerForDifferentHttpMethod()
    {
        HttpMethod getMethod = HttpMethod.Get;
        HttpMethod postMethod = HttpMethod.Post;
        string path = "/test";
        RouteHandler handler = _ => Task.FromResult(new HttpResponseMessage());

        _router.RegisterRoute(getMethod, path, handler);

        bool isRegistered = _router.TryGetHandler(postMethod, path, out RouteHandler? retrievedHandler);
        Assert.IsFalse(isRegistered);
        Assert.IsNull(retrievedHandler);
    }

    [Test]
    public void TryGetHandler_ShouldReturnFalseIfPathNotFound()
    {
        HttpMethod method = HttpMethod.Get;
        string path = "/nonexistent";

        bool isRegistered = _router.TryGetHandler(method, path, out RouteHandler? handler);

        Assert.IsFalse(isRegistered);
        Assert.IsNull(handler);
    }

    [Test]
    public void RegisterRoute_ShouldRegisterMultipleHandlersForSamePathWithDifferentMethods()
    {
        HttpMethod getMethod = HttpMethod.Get;
        HttpMethod postMethod = HttpMethod.Post;
        string path = "/resource";
        RouteHandler getHandler = _ => Task.FromResult(new HttpResponseMessage());
        RouteHandler postHandler = _ => Task.FromResult(new HttpResponseMessage());

        _router.RegisterRoute(getMethod, path, getHandler);
        _router.RegisterRoute(postMethod, path, postHandler);

        bool isGetRegistered = _router.TryGetHandler(getMethod, path, out RouteHandler? getHandlerResult);
        bool isPostRegistered = _router.TryGetHandler(postMethod, path, out RouteHandler? postHandlerResult);

        Assert.IsTrue(isGetRegistered);
        Assert.IsTrue(isPostRegistered);
        Assert.That(getHandlerResult, Is.EqualTo(getHandler));
        Assert.That(postHandlerResult, Is.EqualTo(postHandler));
    }
}