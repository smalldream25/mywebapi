const mongoose = require('mongoose');
const Schema = mongoose.Schema;


const messageSchema = new Schema({
    ID: String,
    message: String,
   
})


var Message = mongoose.model('messages',employeeSchema,'messages');

module.exports =Employee;  