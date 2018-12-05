const expect  = require('chai').expect;
const request = require('request');


it('Main page content', function(done) {
    request('http://localhost:2000/api/demo' , function(error, response, body) {
        expect(body).to.equal('api works');
        done();
    });
});


it('Main page status', function(done) {
    request('http://localhost:2000/api/demo' , function(error, response, body) {
        expect(response.statusCode).to.equal(200);
        done();
    });
});

it('Main page status', function(done) {
    request('http://localhost:2000/' , function(error, response, body) {
        expect(response.statusCode).to.equal(404);
        done();
    });
});

it('Main page status', function(done) {
    request('http://localhost:2000/api/todos' , function(error, response, body) {
        expect(response.statusCode).to.equal(200);
        done();
    });
});

