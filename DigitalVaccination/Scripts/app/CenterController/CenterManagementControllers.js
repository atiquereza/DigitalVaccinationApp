var CenterManagementControllers = angular.module("CenterManagementControllers", ['ui.bootstrap', 'ngResource']);



// this controller call the api method and display the record of selected employee
// in delete.html and provide an option for delete
CenterManagementControllers.controller("DeleteCenterController", ['$scope', '$http', '$routeParams', '$location',
        function ($scope, $http, $routeParams, $location) {
            console.log($routeParams.id);
            $scope.ID = $routeParams.id;
            $http.get('/api/Center/' + $routeParams.id).success(function (data) {
                $scope.ID = data.ID;
                $scope.centersName = data.CentersName;
                $scope.districtID = data.DistrictsID;
                $scope.address = data.CentersAddress;
                $scope.thanaID = data.ThanasID;
                $scope.DistrictsName = data.DistrictsName;
                $scope.ThanasName = data.ThanasName;
            });

            $scope.delete = function () {

                $http.delete('/api/Center/' + $scope.ID).success(function (data) {
                    
                    location.href = "/Center/Index";
                }).error(function (data) {
                    $scope.error = "An error has occured while deleting Center! " + data;
                });
            };
        }
]);

// this controller call the api method and display the record of selected employee
// in edit.html and provide an option for create and modify the employee and save the employee record
CenterManagementControllers.controller("EditCenterController", ['$scope', '$filter', '$http', '$routeParams', '$location',
    function ($scope, $filter, $http, $routeParams, $location) {

        $http.get('/api/address').success(function (data) {
            $scope.districts = data;
        });


        $scope.ID = 0;
        $scope.getThanas = function () {
            var districtId = $scope.districtID;
            if (districtId) {
                $http.get('/api/address/' + districtId).success(function (data) {
                    $scope.thanas = data;
                });
            }
            else {
                $scope.thanas = null;
            }
        }


        $scope.save = function () {


            var obj = {
                ID: $scope.ID,
                CentersName: $scope.centersName,
                ThanasID: $scope.thanaID,
                CentersAddress: $scope.address,
                DistrictsID: $scope.districtID,



            };

            if ($scope.ID == 0) {
                console.log(obj);
                $http.post('/api/Center/', obj).success(function (data) {
                    // $location.path('/listCenter');
                    var url = '@Url.Action("Index", "Center")';
                    location.href = "/Center/Index";
                }).error(function (data) {
                    $scope.error = "An error has occured while adding Center! " + data.ExceptionMessage;
                });
            }
            else {
                //alert($scope.ID);
                $http.put('/api/Center/', obj).success(function (data) {
                    //  $location.path('/listCenter');
                    var url = '@Url.Action("Index", "Center")';
                    location.href = "/Center/Index";
                }).error(function (data) {
                    console.log(data);
                    $scope.error = "An Error has occured while Saving Center Information! " + data.ExceptionMessage;
                });
            }
        }

        if ($routeParams.ID) {

            //alert($routeParams.ID);
            $scope.ID = $routeParams.ID;
            $scope.title = "Edit Center";
            $scope.vaccinName = "Hellooo";
            $http.get('/api/Center/' + $routeParams.ID).success(function (data) {
                $scope.ID = data.ID;
                $scope.centersName = data.CentersName;
                $scope.districtID = data.DistrictsID;
                $scope.address = data.CentersAddress;
                $scope.thanaID = data.ThanasID;



            });
            $scope.$watch('districtID', function (term) {
                $scope.getThanas();
            });

        }
        else {
            $scope.title = "Create Center";
        }
    }
]);