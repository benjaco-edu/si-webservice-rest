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
Next build the docker container for the soap service
```
sudo docker build -t soapserver .
```
Now Run the container
```
sudo docker run -it --rm --link mysql01 -p 9090:80 minserver Newdb
```
Notice the 'Newdb' at the very end, this command tells the server to create a new database and auto-populate it, the run-command will also work without 'Newdb', in that case its up to you to insert data.

## Rest service
