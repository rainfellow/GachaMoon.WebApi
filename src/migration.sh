if [ -z "$1" ]
  then
    echo "Pass Migration name"
    exit 1
fi
dotnet ef migrations add $1 -p GachaMoon.Database.Migrations -c ApplicationDbContext -s GachaMoon.WebApi
