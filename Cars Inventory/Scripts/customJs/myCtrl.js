var app = angular.module('myApp', []);

app.controller('myCtrl', function ($scope, $http) {
    $scope.user = {};

    $scope.signup = function () {
        $http.post('/signup', $scope.user)
            .then(function (response) {
                console.log(response.data);
            }, function (error) {
                console.log(error);
            });
    };
});
