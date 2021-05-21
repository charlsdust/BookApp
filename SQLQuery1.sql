select GenreName,Count(Distinct b.Title),COUNT(Distinct a.ID)
from Genre as G
join Book as B on G.ID=B.GenreID
join Author as A on B.AuthorID=A.ID
Group By GenreName