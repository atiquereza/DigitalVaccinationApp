'use strict';

    //angular
    //    .module('UsersPagingApp')
    //    .controller('UsersController', UsersController);

    //UsersController.$inject = ['$scope', 'Restangular', 'ngTableParams'];

    UsersPagingApp.controller("UsersController", [
    '$scope', 'Restangular', 'ngTableParams',
    function ($scope, Restangular, ngTableParams) {

        /* jshint validthis:true */
        var vm = this;

        $scope.search = '';

        $scope.tableParams = new ngTableParams({
            page: 1,
            count: 7,
            //noCounts: true,
            //counts: [],
            sorting: {
                UserID: 'asc'
            }
        },
        {
            getData: function ($defer, params) {
                // Load the data from the API
                Restangular.all('UsersPaging').getList({
                    pageNo: params.page(),
                    pageSize: params.count(),
                    sort: params.orderBy(),
                    search: $scope.search
                }).then(function (customers) {
                    // console.log(customers);
                    $scope.RowCount = customers.paging.totalRecordCount;
                    // Tell ngTable how many records we have (so it can set up paging)
                    params.total(customers.paging.totalRecordCount);

                    // Return the customers to ngTable
                    $defer.resolve(customers);
                }, function (response) {
                    // Notify of error
                });
            }
        });

        // Watch for changes to the search text, so we can reload the table
        $scope.$watch(angular.bind(vm, function () {
            return $scope.search;
        }), function (value) {
            $scope.tableParams.reload();
        });
        
    }
]);

    //function UsersController($scope, Restangular, ngTableParams) {
    //    /* jshint validthis:true */
    //    var vm = this;

    //    $scope.search = '';

    //    $scope.tableParams = new ngTableParams({
    //        page: 1,
    //        count: 7,
    //        //noCounts: true,
    //        //counts: [],
    //        sorting: {
    //            UserID: 'asc'
    //        }
    //    },
    //    {
    //        getData: function ($defer, params) {
    //            // Load the data from the API
    //            Restangular.all('UsersPaging').getList({
    //                pageNo: params.page(),
    //                pageSize: params.count(),
    //                sort: params.orderBy(),
    //                search: $scope.search
    //            }).then(function (customers) {
    //              // console.log(customers);
    //                $scope.RowCount = customers.paging.totalRecordCount;
    //                // Tell ngTable how many records we have (so it can set up paging)
    //                params.total(customers.paging.totalRecordCount);

    //                // Return the customers to ngTable
    //                $defer.resolve(customers);
    //            }, function (response) {
    //                // Notify of error
    //            });
    //        }
    //    });

    //    // Watch for changes to the search text, so we can reload the table
    //    $scope.$watch(angular.bind(vm, function () {
    //        return $scope.search;
    //    }), function (value) {
    //        $scope.tableParams.reload();
    //    });
    //}




   
        UsersPagingApp.controller("EditUserController", ['$scope', '$filter', '$http', '$routeParams', '$location',
    function ($scope, $filter, $http, $routeParams, $location) {
        // $scope.parent = { BirthDate: '' };
        
        // $scope.BirthDate = new Date();
        $scope.MyDate1 = "20/03/2012";
        $scope.DatePublish = function () {
            console.log($scope.MyDate1);
        };

        //$scope.$watch('BirthDate', function (newValue, oldValue) {
        //    $scope.parent = { BirthDate: '' };
        //    console.log('BirthDate changed', oldValue, newValue);
        //}, true);

        // $scope.form.BirthDate = null;
        $scope.child = {}

        $scope.isDate = false;
        
        $scope.ID = 0;
    

        $scope.save = function () {

          
           
            var obj = {
                //ID: $scope.ID,
                //UserId: $scope.UserId,
                //StartDay: $scope.startTime,
                //EndDay: $scope.endTime,
                //Description: $scope.description,
                BirthCertificateID: $scope.BirthCertificateID,
                //BirthDate: $scope.BirthDate,

                BirthDate: $scope.MyDate1,
                CurrentAddress: $scope.CurrentAddress,
                FatherName: $scope.FatherName,
                FullName: $scope.FullName,
                ID: $scope.ID,
                MotherName: $scope.MotherName,
                PermanentAddress: $scope.PermanentAddress,
                PhoneNumber: $scope.PhoneNumber,
                UserId: $scope.UserId,
                UserName: $scope.UserName,


            };

            if ($scope.ID == 0) {

                $http.post('/api/User/', obj).success(function (data) {
                    $location.path('/showuser');
                }).error(function (data) {
                    $scope.error = "An error has occured while adding employee! " + data.ExceptionMessage;
                });
            }
            else {
                //alert($scope.ID);
                $http.put('/api/User/', obj).success(function (data) {
                    $location.path('/showuser');
                }).error(function (data) {
                    console.log(data);
                    $scope.error = "An Error has occured while Saving customer! " + data.ExceptionMessage;
                });
            }
        }

        if ($routeParams.id) {
            $scope.form = {};
            //$scope.User.BirthDate = [];
            //alert($routeParams.ID);
            $scope.ID = $routeParams.id;
            $scope.title = "Edit Vaccin";
            $scope.vaccinName = "Hellooo";
            $http.get('/api/User/' + $routeParams.id).success(function (data) {
                $scope.user = data;
                $scope.UserName = data.UserName;
                $scope.FullName = data.FullName;
                $scope.FatherName = data.FatherName;
                $scope.MotherName = data.MotherName;
                $scope.PhoneNumber = data.PhoneNumber;
                $scope.MyDate1 = moment(data.BirthDate).format('YYYY/MM/DD');
                $scope.CurrentAddress = data.CurrentAddress;
                $scope.PermanentAddress = data.PermanentAddress;
                $scope.BirthCertificateID = data.BirthCertificateID;
                $scope.UserId = data.UserId;
                


                //$scope.startTime = data.StartDay;
                //$scope.endTime = data.EndDay;
                //$scope.description = data.Description;


            });
        }
        else {
            $scope.title = "Create New User";
        }
    }
        ]);