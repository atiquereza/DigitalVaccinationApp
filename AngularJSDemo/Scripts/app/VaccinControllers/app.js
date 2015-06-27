/// <reference path="D:\vs13_test\ApnarSishuKtikaDin-master\Angular\AngularJSDemo\AngularJSDemo\PartialViews/User/showUser.html" />
var TikaApp = angular.module('TikaApp', [
    'ngRoute',
    'TikaControllers'
]);
//angular.bootstrap(document.getElementById("Container"), ["TikaApp"]);


angular.element(document).ready(function () {
    var myHtml = document.getElementById("Container");
    angular.bootstrap(myHtml, ["TikaApp"]);
   
});

TikaApp.config(['$routeProvider', function ($routeProvider) {

        $routeProvider.when('/list', {
            templateUrl: '/PartialViews/Vaccine/ListTika.html',
                controller: 'ListController'
            }).
            when('/create', {
                templateUrl: '/PartialViews/Vaccine/EditTika.html',
                controller: 'EditController'
            }).
            when('/edit/:id', {
                
                templateUrl: '/PartialViews/Vaccine/EditTika.html',
            controller: 'EditController'
        }).
        when('/delete/:id', {
            templateUrl: '/PartialViews/Vaccine/DeleteTika.html',
            controller: 'DeleteController'
        }).
        otherwise({
        redirectTo: '/list'
    });

}]);

//var UserApp = angular.module('UserApp', [
//    'ngRoute',
//    'UserControllers'
//]);

//UserApp.config(['$routeProvider', function($routeProvider) {
//    $routeProvider.when('/showuser', {
//        templateUrl: '/PartialViews/User/showUser.html',
//        controller: 'UserShowController'
//    }).

//            when('/create', {
//                templateUrl: 'Employee/EditTika.html',
//                controller: 'EditUserController'
//            }).
//            when('/edit/:id', {

//                templateUrl: 'Employee/EditTika.html',
//                controller: 'EditUserController'
//            }).
//        when('/delete/:id', {
//            templateUrl: 'Employee/DeleteTika.html',
//            controller: 'DeleteController'
//        }).
//        otherwise({
//            redirectTo: '/showuser'
//        });

//}]);