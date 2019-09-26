const Sqllite = require('./Sqlite');
const express = require('express')

const bodyParser = require('body-parser')


let app = express();

app.use(bodyParser.urlencoded({extended: false}));
app.use(bodyParser.json());

(async ()=>{
    const db = new Sqllite("sqllite"); // memory is the default in my

    try {
        await db.createDatabase("database.sqllite");

        await db.query`
    create table students
    (
    id      INTEGER PRIMARY KEY AUTOINCREMENT ,
    name char(32),
    age int
    );
    `;
        await db.query`insert into students (name, age) values ("bo", 22)`;
        await db.query`insert into students (name, age) values ("andreas", 28)`;
        await db.query`insert into students (name, age) values ("hans", 21)`;
    } catch (error) {}
    
    
    
    

    app.get('/students', async (req, res) => {
        res.json(await db.query`select * from students`)
    });
    
    app.post('/students', async (req, res) => {
        await db.query`insert into students (name, age) values (${req.body.name}, ${req.body.age})`;
        res.status(201).end();
    });
    
    app.get('/students/:id', async (req, res) => {
        let data = await db.query`select * from students where id = ${req.params.id}`
        if(data.length === 0)
            return res.status(404).end();
        res.json(data[0]);
    });
    
    app.put('/students/:id', async (req, res) => {
        let data = await db.query`select * from students where id = ${req.params.id}`
        if(data.length === 0)
            return res.status(404).end();
        await db.query`update students set name = ${req.body.name}, age = ${req.body.age} where id = ${req.params.id}`;
        res.status(202).end();
    });
    
    app.delete('/students/:id',async (req, res) => {
        let data = await db.query`select * from students where id = ${req.params.id}`
        if(data.length === 0)
            return res.status(404).end();
        await db.query`delete from students where id = ${req.params.id}`
        res.status(202).end();
    });
    
    
    
    app.listen(8080, () =>
        console.log(`Example app listening on port 8080`),
    );

    

})()
