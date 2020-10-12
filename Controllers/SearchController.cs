using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using RestSharp.Serialization;
using RestSharp.Serializers.NewtonsoftJson;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Security.Cryptography;

namespace Notflix.Controllers
{
    public class Movie
    {   
        public string backdropPath { get; set; }
        public string overview { get; set; }
        public string releaseDate { get; set; }
        public string title { get; set; }
        public string popularity { get; set; }
        public string voteCount { get; set; }
        public string voteAverage { get; set; }
    }

    public class SearchController : Controller
    {
        public int getGenreId(string genre)
        {
            var genreId = 0;
            switch (genre)
            {
                case "Action":
                {
                    genreId = 28;
                    break;
                }
                case "Adventure":
                {
                    genreId = 12;
                    break;
                }
                case "Comedy":
                {
                    genreId = 35;
                    break;
                }
                case "Drama":
                {
                    genreId = 18;
                    break;
                }
                case "Fantasy":
                {
                    genreId = 14;
                    break;
                }
                case "Horror":
                {
                    genreId = 27;
                    break;
                }
                case "Mystery":
                {
                    genreId = 9648;
                    break;
                }
                case "Romance":
                {
                    genreId = 10749;
                    break;
                }
                case "Western":
                {
                    genreId = 37;
                    break;
                }
                case "Musical":
                {
                    genreId = 10402;
                    break;
                }
                case "Historical":
                {
                    genreId = 36;
                    break;
                }
                default:
                {
                    genreId = 0;
                    break;
                }
            }
            return genreId;
        }

        public string getGenreInverse(int genre)
        {
            string result = "";
            var genreIds = Enumerable.Range(0, 40).ToList();
            genreIds.Remove(genre);
            for (var i = 0; i < genreIds.Count; ++i)
            {
                if (i != genreIds.Count - 1)
                {
                    result += $"{genreIds[i]},";
                }
                else
                {
                    result += $"{genreIds[i]}";
                }
            }
            return result;
        }


        public IActionResult Index(string genre)
        {
            System.Random random = new System.Random();
            int randomPage = random.Next(1, 5);
            int randomYear = random.Next(1950, 2020);
            List<Movie> movies = new List<Movie>();
            var genreId = getGenreId(genre);
            string genreInverse = getGenreInverse(genreId);
            var client = new RestClient("https://api.themoviedb.org/3");
            client.UseNewtonsoftJson();
            var request = new RestRequest($"discover/movie?api_key=e120410b431979ca8d761440057cf329&with_genre={genre}&without_genres={genreInverse}&year={randomYear}&page={randomPage}", DataFormat.Json);
            var response = client.Get(request);

            JObject responseObject = JObject.Parse(response.Content);
            JArray results = JArray.Parse(responseObject.GetValue("results").ToString());
            foreach(JObject movie in results)
            {
                Movie current = new Movie();
                if (movie.GetValue("backdrop_path").ToString() != "")
                {
                    current.backdropPath = $"https://image.tmdb.org/t/p/original{movie.GetValue("backdrop_path").ToString()}";
                }
                else
                {
                    current.backdropPath = "https://image.tmdb.org/t/p/original/61dsn6ZsvI3BPNEHCQeDZeuhF7K.jpg";
                }
                current.overview = movie.GetValue("overview").ToString();
                current.releaseDate = movie.GetValue("release_date").ToString();
                current.title = movie.GetValue("title").ToString();
                current.popularity = movie.GetValue("popularity").ToString();
                current.voteCount = movie.GetValue("vote_count").ToString();
                current.voteAverage = movie.GetValue("vote_average").ToString();
                movies.Add(current);
            }
            ViewData["movies"] = movies;
            
            ViewData["genre"] = genre;
            ViewData["genreId"] = genreId;
            return View();
        }
    }
}
