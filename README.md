CommentAPI
==========


Overview
--------

Most basic implementation with in memory storage. Confirms to the specs requested, yet has dubious value on itself. 


Endpoints
---------

- GET /api/comments (gets all comments or an empty list)
- GET /api/comments/{id} (get an order by id)
- DELETE api/comments/{id} (deleted a comment with a given id)
- POST api/api/comments (adds a new comment)


TODO
----

Decide on a backend storage first, this would dictate a storage/retrial mechanism. Decide on a caching and batching requirements, this would change the design dramatically one way or another.
