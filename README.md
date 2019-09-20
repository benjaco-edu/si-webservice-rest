## Handin for mini-project:web services


# What is it
<br>
This is a repo containing 3 projects.<br>
A server offering a REST endpoint (located in the rest folder),<br>
A server offering a SOAP endpoint (located in the soap folder),<br>
A client for contacting both of the endpoints and retreiving data from them<br>

# Up and running

## Soap service
The soap-service make use of a mysqldatabase for data storage, start it inside a docker container by executing:
```
sudo docker run -d --name mysql01 -p 3306:3306 -e MYSQL_ROOT_PASSWORD=test1234 mysql
```
(you might have to wait for 10'ish seconds for the mysql database to spin up inside the container you just ran befre executing the soap service)<br>
Now Run the container with the soap service 
```
sudo docker run -it --rm --link mysql01 -p 9090:80 cphjs284/siminiws:1.0 Newdb
```
Notice the 'Newdb' at the very end, this command tells the server to create a new database and auto-populate it, the run-command will also work without 'Newdb', in that case its up to you to insert data.
### Is it running?
Open a browser and navigate to 
```
http://localhost:9090/service.asmx?wsdl
```
If it's running correctly you will a WSDL document.
<br>
<br>
## Rest service

run the container by
```
docker run -p 8080:8080 bslcphbussiness/si-mp-ws
```
you can bind "sqllite" file down to the container to persist the data

the path in the container is /usr/src/app/sqllite

see the documentation in postman by loading in the si.postman_collection.json file# si-webservice-rest
