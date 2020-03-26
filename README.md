# pony

This is a simple Web API server that stores data in LiteDb.

## Purpose

Start quickly server to provide REST endpoints for your application.
It's useful for a demo or pre-sale projects.

## Usage

The server creates database collections automatically based on the provided URL path.

### Store data for your application

Request
```
PUT https://localhost:5001/users
Content-Type: application/json

{"name":"Felix","Role":"Developer"}
```
Response
```
HTTP/1.1 200 OK
Connection: close
Date: Thu, 26 Mar 2020 20:01:14 GMT
Content-Type: text/plain; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked

{
  "_id": "5e7d0a0b895eb101caf16c49",
  "name": "Felix",
  "Role": "Developer"
}
```

### Get your data

Request
```
GET https://localhost:5001/users
```
Response
```
HTTP/1.1 200 OK
Connection: close
Date: Thu, 26 Mar 2020 20:01:56 GMT
Content-Type: text/plain; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked

[
  {
    "_id": "5e7d0a0b895eb101caf16c49",
    "name": "Felix",
    "Role": "Developer"
  }
]
```

### Update your data

Request
```
POST https://localhost:5001/users
Content-Type: application/json

{
"_id": "5e7d0a0b895eb101caf16c49",
"name": "Felix",
"Role": "Manager"
}
```

Response
```
HTTP/1.1 200 OK
Connection: close
Date: Thu, 26 Mar 2020 20:04:39 GMT
Server: Kestrel
Content-Length: 0
```

### Delete your data

Request
```
DELETE https://localhost:5001/users
Content-Type: application/json

{
"_id": "5e7d0a0b895eb101caf16c49",
"name": "Felix",
"Role": "Manager"
}
```
Response
```
HTTP/1.1 200 OK
Connection: close
Date: Thu, 26 Mar 2020 20:05:59 GMT
Server: Kestrel
Content-Length: 0
```