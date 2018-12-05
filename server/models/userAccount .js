const mongoose = require('mongoose');
const Schema = mongoose.Schema;


const userAccountSchema = new Schema({
    ID: Number,
    Email: String,
    Username: String,
    Password: String,
    ConfirmPassword: String
})


var Useraccount = mongoose.model('useraccounts',userAccountSchema,'useraccounts');

module.exports =Useraccount;  
