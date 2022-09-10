# My test and practice repo

I'm using this repo to practice and study.

The code is a simples hexagonal oriented solution, where all the core interfaces is in the folder ports (In or Out).

The usecases are modelled CQRS oriented.

This repo has a Kafka adapter (message producer and backgroud consumer), a simple REST API, a sql adapter and a really cool cache interceptor.

Send me a e-mail if you have questions: gabrielnaveseng@gmail.com

#How to run?

* Install .Net 6 and docker (Or use IIS Express)

* Execute all docker composes inside the folder \Dependencies.

* Create a database named BFFDB (Why this name? No reason)

* Configure the server to receive requests from the container (or use IIS)

* Execute the sql in migration folder.