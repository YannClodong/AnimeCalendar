using AnimeCalendar.Work;

namespace AnimeCalendar.Services;

public class AniListService
{
    private const string AniListUrl = "https://graphql.anilist.co";
    private const string AniListQuery = "query($q: String) {  Page(perPage: 10) {    media(search: $q, type: ANIME) {      title {        romaji      },      startDate {        year        month        day      },      coverImage {        extraLarge      },      bannerImage,      episodes    }  }}";
    
    private readonly HttpClient _client;

    public AniListService(HttpClient client)
    {
        _client = client;
    }

    internal class RawAniListResponse
    {
        internal class RawAniListData
        {
            internal class RawAniListPage
            {
                internal class RawAniListServiceAnime
                {
                    internal class RawAniListDate
                    {
                        public int? Year { get; set; }
                        public int? Month { get; set; }
                        public int? Day { get; set; }

                        public DateTime? ToDate() => Year != null && Month != null && Day != null 
                            ? new(Year.Value, Month.Value, Day.Value) 
                            : null;
                    }
        
                    internal class RawAniListTitle
                    {
                        public string Romaji { get; set; } = null!;
                    }

                    internal class RawAniListCover
                    {
                        public string ExtraLarge { get; set; } = null!;
                    }

                    public RawAniListTitle Title { get; set; } = null!;
                    public RawAniListDate? StartDate { get; set; }
                    public RawAniListCover? CoverImage { get; set; }
                    public string? BannerImage { get; set; }
                    public int? Episodes { get; set; }

                    public AnimeSandbox ToAnimeSandbox()
                    {
                        return new AnimeSandbox(Title.Romaji, CoverImage?.ExtraLarge, BannerImage, StartDate?.ToDate(), Episodes);
                    }
                }
                
                public List<RawAniListServiceAnime> Media { get; set; } = null!;
            }

            public RawAniListPage Page { get; set; } = null!;   
        }

        public RawAniListData Data { get; set; } = null!;
    }
    
    

    public async Task<List<AnimeSandbox>> Search(string query)
    {
        var res = await _client.PostAsJsonAsync(
            AniListUrl, 
            new { query = AniListQuery, variables = new { q = query }}, 
            CancellationToken.None);
        var raw = await res.Content.ReadFromJsonAsync<RawAniListResponse>();
        
        
        return raw!.Data.Page.Media.Select(r => r.ToAnimeSandbox()).ToList();
    }
}