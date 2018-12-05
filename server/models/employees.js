const mongoose = require('mongoose');
const Schema = mongoose.Schema;


const employeeSchema = new Schema({
    ID: String,
    Name: String,
    Salary: String,
    Country: String,
    City: String
})
  

var Employee = mongoose.model('employees',employeeSchema,'tahzooEmployees');

module.exports =Employee;