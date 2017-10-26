[![Deploy](https://www.herokucdn.com/deploy/button.svg)](https://heroku.com/deploy?template=https://github.com/WilhelmKleuSage/heroku-rabbitmq)

Docker commands for local rabbitmq:
```
docker run -d --hostname my-rabbit --name local-rabbit -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```