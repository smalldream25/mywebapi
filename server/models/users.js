const mongoose = require('mongoose');
const Schema = mongoose.Schema;


const userSchema = new Schema({
    email: String,
    password: String
})


var User = mongoose.model('users',userSchema,'users');

module.exports =User;  