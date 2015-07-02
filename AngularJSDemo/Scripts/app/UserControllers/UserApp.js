var UserApp = angular.module('UserApp', [
    'ngRoute',
    'UserControllers'
]);

angular.element(document).ready(function () {
    var myHtml = document.getElementById("Container");
    angular.bootstrap(myHtml, ["UserApp"]);

});

UserApp.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/showuser', {
        templateUrl: '/PartialViews/User/showUser.html',
        controller: 'UserShowController'
    }).

            when('/createUser', {
                templateUrl: '/PartialViews/User/EditUser.html',
                controller: 'EditUserController',
                 message: 'Create User'
            }).
            when('/editUser/:id', {

                templateUrl: '/PartialViews/User/EditUser.html',
                controller: 'EditUserController',
                message: 'Edit User'
            }).
        when('/deleteUser/:id', {
            templateUrl: '/PartialViews/User/DeleteUser.html',
            controller: 'DeleteUserController',
            message: 'Delete User'
        }).
        otherwise({
            redirectTo: '/ShowUserInv'
        });

}]);