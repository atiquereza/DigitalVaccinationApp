﻿'use strict';
UsersPagingApp.controller("UserPagingController", [
    '$scope', 'Restangular', 'ngTableParams',
    function ($scope, Restangular, ngTableParams) {

        /* jshint validthis:true */
        var vm = this;

        $scope.search = '';

        $scope.tableParams = new ngTableParams({
            page: 1,
            count: 7,
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
   
        UsersPagingApp.controller("EditUserController", ['$scope', '$filter', '$http', '$routeParams', '$location',
    function ($scope, $filter, $http, $routeParams, $location) {
       
        
        $scope.child = {}

        $scope.isDate = false;
        
        $scope.ID = 0;
    

        $scope.save = function () {
            var obj = {
                BirthCertificateID: $scope.BirthCertificateID,
                BirthDate: $scope.DOB,
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
                    location.href("/User/Index");
                }).error(function (data) {
                    $scope.error = "An error has occured while adding employee! " + data.ExceptionMessage;
                });
            }
            else {
                //alert($scope.ID);
                $http.put('/api/User/', obj).success(function (data) {
                  
                    location.href("/User/Index");
                }).error(function (data) {
                    console.log(data);
                    $scope.error = "An Error has occured while Saving customer! " + data.ExceptionMessage;
                });
            }
        }

        if ($routeParams.id) {
            $scope.form = {};
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
                $scope.DOB = moment(data.BirthDate).format('YYYY/MM/DD');
                $scope.CurrentAddress = data.CurrentAddress;
                $scope.PermanentAddress = data.PermanentAddress;
                $scope.BirthCertificateID = data.BirthCertificateID;
                $scope.UserId = data.UserId;

            });
        }
        else {
            $scope.title = "Create New User";
        }
    }
        ]);