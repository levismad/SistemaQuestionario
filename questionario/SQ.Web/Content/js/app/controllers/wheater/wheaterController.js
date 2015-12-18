angular.module('SQ.controllers').controller('WeatherController', function ($scope, weatherService) {
    $scope.getWeather = function () {
        $scope.weatherDescription = "Fetching . . .";
        weatherService.getWeather($scope.city, $scope.country).then(function (data) {
            $scope.weatherDescription = data;
        }, function () {
            $scope.weatherDescription = "Could not obtain data";
        });
    }
});