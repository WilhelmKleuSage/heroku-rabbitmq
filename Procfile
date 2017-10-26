web: cd $HOME/heroku_output && ASPNETCORE_URLS='http://+:$PORT' dotnet "./HerokuRabbitMq.dll" --server.urls http://+:$PORT
worker: cd $HOME/heroku_output && dotnet "./Worker.dll"