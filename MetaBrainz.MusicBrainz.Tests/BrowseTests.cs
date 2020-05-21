using System;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using Xunit;

namespace MetaBrainz.MusicBrainz.Tests {
  public class BrowseTests : MusicBrainzTest {

    private const string ReleaseTestData = @"{
   ""releases"":[
      {
         ""packaging"":null,
         ""title"":""Flying in a Blue Dream"",
         ""asin"":null,
         ""country"":""US"",
         ""status"":""Official"",
         ""barcode"":""088561101541"",
         ""id"":""296c4baa-4a13-3623-b526-7e7b35c8dfac"",
         ""packaging-id"":null,
         ""cover-art-archive"":{
            ""darkened"":false,
            ""artwork"":false,
            ""back"":false,
            ""count"":0,
            ""front"":false
         },
         ""disambiguation"":"""",
         ""release-events"":[
            {
               ""date"":""1989-10-30"",
               ""area"":{
                  ""iso-3166-1-codes"":[
                     ""US""
                  ],
                  ""id"":""489ce91b-6658-3307-9877-795b68554c98"",
                  ""name"":""United States"",
                  ""disambiguation"":"""",
                  ""sort-name"":""United States"",
                  ""type-id"":null,
                  ""type"":null
               }
            }
         ],
         ""date"":""1989-10-30"",
         ""quality"":""normal"",
         ""status-id"":""4e304316-386d-3409-af2e-78857eec5cfe"",
         ""text-representation"":{
            ""language"":""eng"",
            ""script"":""Latn""
         }
      }
   ],
   ""release-count"":1,
   ""release-offset"":0
}";


    [Fact]
    public async Task CanBrowseArtistReleases() {
      
      var query = new Query(this.CreateMockWebClient(BrowseTests.ReleaseTestData));
      var releases = await query.BrowseArtistReleasesAsync(Guid.Empty).ConfigureAwait(false);
      releases.Results.Should().HaveCount(1);
      releases.TotalResults.Should().Be(1);
      releases.UnhandledProperties.Should().BeNullOrEmpty();
      var release = releases.Results.Single();
      release.Should().NotBeNull();
      release.Title.Should().Be("Flying in a Blue Dream");
    }

  }

}
