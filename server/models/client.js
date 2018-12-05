const mongoose = require('mongoose');
const Schema = mongoose.Schema;


const clientSchema = new Schema({
    name: String,
    email: String,
    birthday: Date,
    user: String
})


var Client = mongoose.model('clients',clientSchema,'clients');

module.exports =Client;