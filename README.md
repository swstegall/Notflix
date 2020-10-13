# Notflix

## Description

In this application, I implemented a pseudo IMDB clone.

## Starting the Project

Open this project in Visual Studio 2019 with the latest version of ASP.NET installed. Building and running the project will launch the application with IIS Express.

### Pages

The application supports the following pages:

- Home
- Search

#### Home

You are able to select from 1 of 11 genres on the homepage, including:

- Action
- Adventure
- Comedy
- Drama
- Fantasy
- Horror
- Mystery
- Romance
- Western
- Musical
- Historical

Clicking on one of these buttons will open up the Search page.

#### Search

The Search page will present the user with 20 random films from a given genre. Each film is rendered in its own card, with A title, release date, the film's popularity, its rating, and the number of votes that led to the film's popularity rating.

Additionally, each film card will present you with a purchase button, with a breakdown of the price of the film. Since I used an API for getting films, this price was based on a normal distribution I calculated wherein films will be between the years 1950 and 2020, and films released in 1950 are worth $1, and films in 2020 are worth $30. All films' subtotal prices fall in this range. The state sales tax of 6.25% is applied to this, and there is a 20% chance that a given film will be given a lucky 10% discount (off of the subtotal price, of course). Unfortunately, I had issues getting my C# callback to properly hit the global state price figures, so when you click the purchase button, the page simply refreshes, rather than deducting the film from your total balance, and adding the film to your film number found in the navbar.

Additionally, if the API I am using has a trailer for the film, this is presented with a View Trailer button, which externally navigates you to the film's trailer. I attempted to embed an iframe for the trailers, but this wasn't possible because of the way YouTube and Vimeo will block rapid successions of requests on iframes. I could not find a way around this limitation, hence the external navigation. If the film does not have a trailer as given by my API, no button is rendered.

## Challenges

I decided to utilize freely available APIs for this project because I figured it would be more flexible for overall application usage. This, as well as C# and ASP.NET presented several challenges that I was unable to solve in the timeframe for this given project.

### C# JSON Support

C#'s JSON support is subpar at best and handled most elegantly with external packages. A lot of the boilerplate I wrote for my Search controller was because of boilerplate regarding consuming JSON and handling values on JSON Objects and JSON Arrays. Luckily enough, I have some JavaScript experience and am a quick learner, so I didn't have too much trouble developing the required boilerplate for this, but it bloated the controller considerably. With JavaScript, there is native JSON support, which makes handling these types of things very simple with conventional native JavaScript features. Some of the code here could probably be refactored to be made more simple or extracted into functions for cleanliness, but this took away from my controller understanding. Since this was mostly a learning experience for ASP.NET, this is probably fine.

### TheMovieDB API Issues

I had many, many issues interacting with TheMovieDB's API.

#### Deterministic Discovery

TheMovieDB's API provides an endpoint that allows you to discover films of a given genre, but its method for providing this includes no randomization, and only provides a sequential list films in order of what was most recently added to the database. This made all search pages static. To handle this, I provided extra query information to inject randomness into the discovery feature, including year of release, page number, and so on. Additionally, pruning genres was a pain because the discover endpoint includes all films of all genres by default. You can filter by genre ID, but you have to be explicit with listed genres by utilizing the `with_genre` and `without_genre` parameter lists. Creating queries that utilized this properly was a major challenge, and there are still some issues with the way I did it, like genre IDs being hard-coded, so this won't work over a period of time as genre IDs change. Additionally, I was unable to filter all genres based on their genre ID in all cases, so you get some weirdly-tagged films that seem to be in the wrong genre on the search page, but this is a limitation on how a given film was entered into the database rather than anything I can really control on my end with this approach. Perhaps over time, support for this discovery feature will improve.

#### Video Link Issues

The discovery endpoint does not include links for videos for a given film. Instead, you must query for the film independently in order to get a link for its associated videos. Because of this, I have to make 21 queries on the search page: one for a list of 20 films, and one for each film to check to see if it has a trailer associated to it. This singlehandedly is the slowest part of the application, with no solution in sight without a refactor of the discover endpoint or some cacheing mechanism.

### ASP.NET C# Callbacks to Globals

I wasn't able to figure out how to map a button to trigger a C# function in regards to handling globals. I believe this is the fault of me trying to make these mutations on globals from client-side rather than coming up with an effective way to do this server-side. I wasn't entirely sure how to mock some of this client-side without a database for my data. Perhaps this means that the biggest issue with my implementation of this is that it doesn't utilize a database in its current state for server-based state mutations rather than client-side ones. I had trouble finding ways to do this, but I assume implementing something that utilizes LocalDB would be an effective way to do this. As it stands right now, there is a purchase function in `Program.cs` that was supposed to handle this, but it does not work. I would fix this in further revisions if given more time.

### Databases

Since I chose to do this with an API, databases were something I wanted to tack on at the end of the project, rather than incorporate it into the beginning design phase of this application. Because of this, I had issues configuring my project to utilize any identity frameworks and implement a true login. This also hampered my ability to render money changes and acquired films within the application. This ties up with the previous point that I mentioned.

## Closing Thoughts

This was a fun application to get to create. I think doing this from an API approach gave me a bit more insight into some complexities of C#, ASP.NET MVC, and other challanges that dynamic projects may face. I wish I would have been able to finish all features for this application but I started, but overall, I am proud of what I produced.