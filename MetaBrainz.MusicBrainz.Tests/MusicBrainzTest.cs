using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Moq;
using Moq.Protected;

namespace MetaBrainz.MusicBrainz.Tests
{

  public class MusicBrainzTest {

    protected WebClient CreateMockWebClient(string responseText) {
      var webClient = new Mock<WebClient> { CallBase = true };
      webClient
        .Protected()
        .Setup<WebRequest>("GetWebRequest", ItExpr.IsAny<Uri>())
        .Returns<Uri>(uri => MusicBrainzTest.CreateMockWebRequest(responseText));
      return webClient.Object;
    }

    private static WebRequest CreateMockWebRequest(string responseText) {
      var webResponse = new Mock<WebResponse>();

      webResponse
        .SetupGet(x => x.ContentLength)
        .Returns(responseText.Length);
      webResponse
        .Setup(x => x.GetResponseStream())
        .Returns(() => new MemoryStream(Encoding.UTF8.GetBytes(responseText)));


      var webRequest = new Mock<WebRequest>();
      webRequest
        .Setup(x => x.GetResponse())
        .Returns(() => webResponse.Object);

      webRequest
        .Setup(x => x.BeginGetResponse(It.IsAny<AsyncCallback>(), It.IsAny<object>()))
        .Returns((AsyncCallback callback, object state) => {
          TaskCompletionSource<WebResponse> tcs = new TaskCompletionSource<WebResponse>(state);
          Task.FromResult(webResponse.Object)
            .ContinueWith(completedTask => {
              if (tcs.TrySetResult(webResponse.Object))
                callback(tcs.Task);
              return webResponse.Object;
            }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.Default);
          return tcs.Task;
        });

      webRequest
        .Setup(x => x.EndGetResponse(It.IsAny<IAsyncResult>()))
        .Returns((IAsyncResult asyncResult) => webResponse.Object);

      return webRequest.Object;
    }
  }

}
