(function () {
    app.controller('TestMy', function ($scope, $http) {
        $scope.TestMethod = function () {
            alert("Now Angular Is Working!");
        }
    });
}).call(angular);