web: printenv && cd $HOME/heroku_output/HerokuRabbitMq && ASPNETCORE_URLS='http://+:$PORT' dotnet "./HerokuRabbitMq.dll" --server.urls http://+:$PORT
worker: cd $HOME/heroku_output/Worker && dotnet "./Worker.dll"
