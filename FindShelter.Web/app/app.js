var findShelterApp = angular.module('findShelterApp', ['ngRoute', 'ngSanitize']);

findShelterApp.config(function ($routeProvider) {
    $routeProvider
        .when('/facilities',
            {
                controller: 'facilityListController',
                templateUrl: '/views/FacilityList.html'
            })
        .when('/facilities/:facilityId',
            {
                controller: 'facilityDetailController',
                templateUrl: '/views/FacilityDetail.html'
            })
        .otherwise({ redirectTo: '/facilities' });
});

findShelterApp.controller('facilityListController',
    ['$scope', '$http', '$sce',
    function ($scope, $http, $sce) {
        $http.get('api/facilities').success(function (data) {
            $scope.facilities = data;
        });
    }]);

findShelterApp.controller('facilityDetailController',
    ['$scope', '$routeParams', '$http', '$sce',
    function ($scope, $routeParams, $http, $sce) {
        $scope.facilityId = $routeParams.facilityId;
        $http.get('api/facilities/' + $scope.facilityId).success(function (data) {
            $scope.facility = data;
        });

    }]);