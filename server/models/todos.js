const mongoose = require('mongoose');
const Schema = mongoose.Schema;


const todoSchema = new Schema({
    userID: Number,
    id: Number,
    title: String,
    completed: Boolean
})


var Todo = mongoose.model('todos',todoSchema,'todos');

module.exports =Todo;  