/// <reference path="D:\vs13_test\ApnarSishuKtikaDin-master\Angular\DigitalVaccination\DigitalVaccination\PartialViews/User/showUser.html" />
var TikaApp = angular.module('TikaApp', [
    'ngRoute',
    'TikaControllers'
]);

angular.element(document).ready(function () {
    var myHtml = document.getElementById("Container");
    angular.bootstrap(myHtml, ["TikaApp"]);
   
});

TikaApp.config(['$routeProvider', function ($routeProvider) {

    $routeProvider.when('/list', {
            templateUrl: 'PartialViews/Vaccine/ListTika.html',
            controller: 'ListController'
        }).
        when('/create', {
            templateUrl: 'PartialViews/Vaccine/EditTika.html',
            controller: 'EditController'
        }).
        when('/edit/:id', {
            templateUrl: 'PartialViews/Vaccine/EditTika.html',
            controller: 'EditController'
        }).
        when('/delete/:id', {
            templateUrl: 'PartialViews/Vaccine/DeleteTika.html',
            controller: 'DeleteController'
        }).
        otherwise({
            redirectTo: '/list'
        });

}]);

