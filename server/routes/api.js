const express = require('express');
const router =express.Router();
const mongoose = require('mongoose');
const readline = require('readline');

const Todo = require('../models/todos')
const Employee = require('../models/employees')
const User = require('../models/users')
const Client = require('../models/client');
const Useraccount = require('../models/userAccount ');



const db = "mongodb://userso:123@ds042607.mlab.com:42607/tools";

mongoose.promise = global.Promise;
mongoose.connect(db, function(err){
    if(err){
        console.error("Error! "+err);
    }
})

router.get('/demo', function(req, res){
    res.send('api works');
    let arr=[1,2,3,4];

    let newArr=arr.map((v,i) => {
        return v+2;
    });
    //console.log(newArr);

    let employees= [
        {ID:"1", Name: "So Kim", Salary:"$1223", Country: "South Korea", City: "Seoul"},
        {ID:"2", Name: "Dave", Salary:"$1223123", Country: "U.S.A", City: "Richmond"},
        {ID:"3", Name: "Maria", Salary:"$5245234", Country: "Japan", City: "Tokyo"},
        {ID:"4", Name: "Sam", Salary:"$23123", Country: "France", City: "Paris"},
        {ID:"5", Name: "Kobe", Salary:"$25232", Country: "China", City: "Beijing"},

      ];

    let newemployees= [
        {ID:"6", Name: "Eddie", Salary:"$51341234", Country: "U.S.A", City: "L.A"},
        {ID:"7", Name: "Dara", Salary:"$12414", Country: "U.S.A", City: "Richmond"},
   
      ];


    let summit = employees.map(function(convertForm){
          let IDs = convertForm['ID'];
          let Names = convertForm['Name'];
          let Salarys = convertForm['Salary'];
          let Countrys = convertForm['Country'];
          let Citys = convertForm['City'];
        
        return [IDs,Names,Salarys,Countrys,Citys];
      });

      let newEmps = newemployees.map(function(convertForm){
        let IDs = convertForm['ID'];
        let Names = convertForm['Name'];
        let Salarys = convertForm['Salary'];
        let Countrys = convertForm['Country'];
        let Citys = convertForm['City'];
      
      return [IDs,Names,Salarys,Countrys,Citys];
    });
    

    let addedEmp=summit.concat(newEmps);

   // for (var i=0; i<newEmps.length;i++)
   //     summit.push(newEmps[i])
  


   console.log(addedEmp);
   var newEmployee={ID:"",Name:"",Salary:"",Country:"",City:""};
   var testEmp={
    ID: "99 TestAPI",
    Name: "Eddie TestAPI",
    Salary: "$100,000,000 TestAPI",
    Country: "U.S.A TestAPI",
    City: "Richmond TestAPI"
    };


/*
    newEmployee.save(function(err, testEmp){
        if(err){
            console.log('Error saving testEmp');
        }else{
            res.json(testEmp);
        }
    });*/
});


router.get('/test', function(req, res){
    res.send('api works');

   

    var endpoint={name:"CLIENT",URL:'18.204.138.78:0'}
    var Stg={name:"STG",URL:'18.204.138.78:1'};
    var Prod={name:"Prod",URL:'18.204.138.78:2'};

    let endpoints= [endpoint];
    endpoints= endpoints.concat(Stg);
    endpoints= endpoints.concat(Prod);
    
    var key="stg";
    key=key.toUpperCase();
   

    var result= endpoints.find(res => res.name === key);

   
    console.log(endpoints);
    console.log("length",endpoints.length);
    console.log(result);


    var k="key1,key2,key3";
    var keys=k.split(",");

    var v="111,222,333"
    var vals=v.split(",");

    var inputs=[];
    for(var i=0; i<=keys.length;i++){
        inputs[i]={key:keys[i],val:vals[i]}
    }



    console.log(endpoints);



});




router.get('/todos', function(req, res){
    console.log('Get request for all todos');
    Todo.find({})
    .exec(function(err, Todos){
        if(err){
            console.log("Error retrieving Todos");
        }else {
            res.json(Todos);
        }
    });
});

router.get('/employees', function(req, res){
    console.log('Get request for all employees');
    Employee.find({})
    .exec(function(err, Employees){
        if(err){
            console.log("Error retrieving employees");
        }else {
            res.json(Employees);
        }
    });
});



router.post('/employee', function(req, res){
    console.log('Post a Employee');
    var newEmployee= new Employee();
    newEmployee.ID = req.body.ID;
    newEmployee.Name = req.body.Name;
    newEmployee.Salary=req.body.Salary;
    newEmployee.Country=req.body.Country;
    newEmployee.City=req.body.City;
    newEmployee.save(function(err, insertedEmployee){
        if(err){
            console.log('Error saving newEmployee');
        }else{
            res.json(insertedEmployee);
        }
    });

});


router.delete('/employee/:id', function(req, res){
    console.log('Deleting a employee');
    Employee.findByIdAndRemove(req.params.id, function(err, deletedEmployee)
    {
        if(err){
            res.send("Error deleting Employee");
        }
        else {
            res.json(deletedEmployee);
        }
    });

});



router.get('/stg/message', function(req, res){
    res.send('STG api works');

});

router.get('/prod/message', function(req, res){
    res.send('PROD api works');

});


router.get('/users', function(req, res){
    console.log('Get request for all users');
    User.find({})
    .exec(function(err, User){
        if(err){
            console.log("Error retrieving Users");
        }else {
            res.json(User);
        }
    });
});


router.post('/user', function(req, res){
    console.log('Post a User');
    var newUser= new User();
    newUser.email = req.body.email;
    newUser.password = req.body.password;
    newUser.save(function(err, insertedUser){
        if(err){
            console.log('Error saving newUser');
        }else{
            res.json(insertedUser);
        }
    });

});

router.get('/clients', function(req, res){
    console.log('Get request for all clients');
    Client.find({})
    .exec(function(err, Client){
        if(err){
            console.log("Error retrieving Clients");
        }else {
            res.json(Client);
        }
    });
});

router.post('/client', function(req, res){
    console.log('Post a Client');
    var newClient= new Client();
    newClient.name = req.body.name;
    newClient.email = req.body.email;
    newClient.birthday = req.body.birthday;
    newClient.user = req.body.user;
    newClient.save(function(err, insertedClient){
        if(err){
            console.log('Error saving Client');
        }else{
            res.json(insertedClient);
        }
    });

});

router.get('/useraccounts', function(req, res){
    console.log('Get request for all Useraccount');
    Useraccount.find({})
    .exec(function(err, Useraccount){
        if(err){
            console.log("Error retrieving Useraccounts");
        }else {
            res.json(Useraccount);
        }
    });
});

router.post('/useraccount', function(req, res){
    console.log('Post a Useraccount');
    var newUseraccount= new Useraccount();
    newUseraccount.ID = req.body.ID;
    newUseraccount.Email = req.body.Email;
    newUseraccount.Username = req.body.Username;
    newUseraccount.Password = req.body.Password;
    newUseraccount.ConfirmPassword = req.body.ConfirmPassword;
    newUseraccount.save(function(err, insertedUseraccount){
        if(err){
            console.log('Error saving Useraccount');
        }else{
            res.json(insertedUseraccount);
        }
    });

});



module.exports=router;