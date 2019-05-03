var mysql = require('mysql');
var express = require('express');
var connection = mysql.createConnection({
    host     : 'localhost',
    user : "root",
    port : 3306, 
    database : 'orcus'
  });
   
connection.connect(function(err){
    if(err) throw err;
    console.log("Connected!");
});
var app = express();

app.use(express.json());

app.get('/login', function (req, res) {
    
    //res.send(req.query.name);
    connection.query("SELECT * FROM users WHERE name = ? AND password = ?;", [req.query.name, req.query.password] , function(err, results){
        if(err) throw err;
        if(!results.length){
            res.send("-1");
        }
        else if(results[0].is_admin === 1){
            res.send("1");
        }
        else{
            res.send("0");
        }
    });
});

app.get('/tables', function(req, res) {
    connection.query("SHOW TABLES;", function(err, dat){
        if(err) throw err;
        var tables = [];
        for(var i in dat){
            tables.push(dat[i]["Tables_in_" + connection.config.database]);
        }
        res.send(tables); 
    })
});

app.get('/gettable', function(req, res){
    connection.query("SELECT * FROM " + req.query.table, function(err, results){
        if(err) throw err;
        res.send(results);
    })
});

app.post('/add', function(req, res){
    var keys = [];
    for(var k in req.body[0]){
        keys.push(k);
    }
    connection.query("SELECT AUTO_INCREMENT FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'orcus' AND TABLE_NAME = " + connection.escape(req.query.table), function(err, results){
        if(err) throw err;
        var curID = results[0].AUTO_INCREMENT;
        var cmd = "INSERT INTO " + req.query.table + "\n     (";
        for(var i = 1; i < keys.length; i++){
            cmd += keys[i];
            if(i < keys.length - 1){
                cmd += ", ";
            }
        }
        cmd += ")\nVALUES \n     (";
        for(var i = 0; i < req.body.length; i++){
            for(var j = 1; j < keys.length; j++){
                cmd += connection.escape(req.body[i][keys[j]]);
                if(j < keys.length - 1){
                    cmd += ", ";
                }
            }
            if(i < req.body.length - 1){
                cmd += "),\n     (";
            }
            else{
                cmd += ");";
            }
        }
        connection.query(cmd, function(err){
            if(err){
                res.send("FAILED");
                throw err;
            }
            res.send(curID + "");
        });
    });
});

app.post('/change', function(req, res){
    var keys = [];
    for(var k in req.body[0]){
        keys.push(k);
    }
    for(var i = 0; i < req.body.length; i++){
        var cmd = "UPDATE " + req.query.table + " SET ";
        for(var j = 1; j < keys.length; j++){
            cmd += keys[j] + " = " + connection.escape(req.body[i][keys[j]]);
            if(j < keys.length - 1){
                cmd += ", ";
            }
        }
        cmd += " WHERE " + keys[0] + " = " + req.body[i][keys[0]];
        //console.log(cmd);
        connection.query(cmd, function(err){
            if(err){
                res.send("FAILED");
                throw err;
            }
            res.send("OK");
        });
    }

});
 
app.post('/delete', function(req, res){
    var cmd = "DELETE FROM " + req.query.table + " WHERE id IN (" + req.body[0].id;
    for(var i = 1; i < req.body.length; i++){
        cmd += ", " + req.body[i].id;
    }
    cmd += ");";
    connection.query(cmd, function(err){
        if(err){
            res.send("FAILED");
            //throw err;
        }
        res.send("OK");
    })
});

app.listen(3000)