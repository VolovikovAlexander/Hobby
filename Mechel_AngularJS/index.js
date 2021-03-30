var app = angular.module('HomeApp', []);
app.controller('HomeController', function ($scope, $http) {

        $http.get('https://localhost:44391/Home/GetServices?pointID=1').
            then(function (response) {
                console.log(response);
                $scope.servicesForPoint1 = response.data;
        }, function (error) { alert(error); });

        $http.get('https://localhost:44391/Home/GetServices?pointID=2').
            then(function (response) {
                console.log(response);
                $scope.servicesForPoint2 = response.data;
        });

        $http.get('https://localhost:44391/Home/GetServices?pointID=3').
            then(function (response) {
                console.log(response);
                $scope.servicesForPoint3 = response.data;
        });

});