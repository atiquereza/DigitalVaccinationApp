var CenterManagementApp = angular.module('CenterManagementApp', [
    'ngRoute',
    'CenterManagementControllers'
]);



CenterManagementApp.config(['$routeProvider', function ($routeProvider) {

    $routeProvider.
        when('/createCenter', {
            templateUrl: '/PartialViews/Center/EditCenter.html',
            controller: 'EditCenterController'
        }).
        when('/editCenter/:ID', {
            templateUrl: '/PartialViews/Center/EditCenter.html',
            controller: 'EditCenterController'
        }).
    when('/deleteCenter/:id', {
        templateUrl: '/PartialViews/Center/DeleteCenter.html',
        controller: 'DeleteCenterController'
    }).
    otherwise({
        redirectTo: '/createCenter'
    });

}]);


angular.element(document).ready(function () {
    var myHtml = document.getElementById("Container");
    angular.bootstrap(myHtml, ["CenterManagementApp"]);

});

angular.element(document).ready(function () {
    var myHtml = document.getElementById("Container");
    angular.bootstrap(myHtml, ["CenterManagementApp"]);

});