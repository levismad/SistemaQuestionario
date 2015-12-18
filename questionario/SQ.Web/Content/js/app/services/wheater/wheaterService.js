angular.module('SQ.services').factory('weatherService', function ($http) {
    return {
        getWeather: function (city, country) {
            var query = city + ',' + country;
            return $http.get('http://localhost:23695/api/hello/' + query + '?format=json', {
                
            }).then(function (response) { //then() returns a promise whichis resolved with return value of success callback
                return response.data.result; ///extractweather data
            });
        }
    }
});