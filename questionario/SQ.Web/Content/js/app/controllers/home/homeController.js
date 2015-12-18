angular.module('SQ.controllers', []).
controller('homeController', ['Phone', function($scope, Phone) {
    $scope.phones = {};//Phone.query();
    $scope.driversList = [
      {
          Driver: {
              givenName: 'Sebastian',
              familyName: 'Vettel'
          },
          points: 322,
          nationality: "German",
          Constructors: [
              { name: "Red Bull" }
          ]
      },
      {
          Driver: {
              givenName: 'Fernando',
              familyName: 'Alonso'
          },
          points: 207,
          nationality: "Spanish",
          Constructors: [
              { name: "Ferrari" }
          ]
      }
    ];
}]);