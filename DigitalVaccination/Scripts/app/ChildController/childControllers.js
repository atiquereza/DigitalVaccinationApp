var childControllers = angular.module("ChildModule", ['ui.bootstrap', 'ng-bs3-datepicker','ngResource']);


childControllers.controller("UserChildController", [
    '$scope', '$resource','filterFilter',
    function ($scope, $resource, filterFilter) {
        $scope.isThingsCollapsed = false;

        var IssueChild = $resource('/api/Child');
        $scope.childObjects = IssueChild.query();

        $scope.childObjects.$promise.then(function () {
            $scope.ChildInfo = $scope.childObjects;

        });


        $scope.filtered = [];
        var Issue = $resource('/api/Vaccination');
        $scope.objects = Issue.query();

        $scope.objects.$promise.then(function () {
            $scope.totalItems = $scope.objects.length;
            $scope.filtered = $scope.objects;
            console.log($scope.objects);
        });
        $scope.currentPage = 1;
        $scope.numPerPage = 10;
        $scope.noOfPages = Math.ceil($scope.filtered.length / $scope.numPerPage);

        $scope.sortType = 'Name'; // set the default sort type
        $scope.sortReverse = false; // set the default sort order

        $scope.paginate = function (value) {
            var begin, end, index;
            begin = ($scope.currentPage - 1) * $scope.numPerPage;
            end = begin + $scope.numPerPage;

            index = $scope.filtered.indexOf(value);
            console.log(index);
            return (begin <= index && index < end);
        };

        $scope.$watch('search', function (term) {
            if ($scope.objects.length > 0) {
                if (term.length > 0) {
                    $scope.tempArray = [];
                    angular.forEach($scope.objects, function (row) {
                        var t1 = (angular.lowercase((row.Name).toString()).indexOf(angular.lowercase($scope.search) || '') !== -1 || angular.lowercase((row.Description)).toString().indexOf(angular.lowercase($scope.search) || '') !== -1);
                        if (t1) {

                            $scope.tempArray.push(row);
                            console.log($scope.filtered);
                        }
                    });
                    $scope.filtered = $scope.tempArray;


                } else {
                    $scope.filtered = $scope.objects;
                }

                $scope.noOfPages = Math.ceil($scope.filtered.length / $scope.numPerPage);
                $scope.currentPage = 1;
                $scope.paginate();
            }
        });
        
    }
]);

childControllers.controller("ViewChildInfo", [
    '$scope', '$resource', 'filterFilter', '$routeParams',
    function ($scope, $resource, filterFilter,$routeParams) {
        $scope.isThingsCollapsed = false;



       

        var IssueChild = $resource('/api/Child/' + $routeParams.id );
        $scope.childObjects = IssueChild.query();

        $scope.childObjects.$promise.then(function () {
            $scope.ChildInfo = $scope.childObjects;

        });


        $scope.filtered = [];
        var Issue = $resource('/api/ChildVaccin/' + $routeParams.id);
        $scope.objects = Issue.query();

        $scope.objects.$promise.then(function () {
            $scope.totalItems = $scope.objects.length;
            $scope.filtered = $scope.objects;
            console.log($scope.objects);
        });
        $scope.currentPage = 1;
        $scope.numPerPage = 10;
        $scope.noOfPages = Math.ceil($scope.filtered.length / $scope.numPerPage);

        $scope.sortType = 'Name'; // set the default sort type
        $scope.sortReverse = false; // set the default sort order

        $scope.paginate = function (value) {
            var begin, end, index;
            begin = ($scope.currentPage - 1) * $scope.numPerPage;
            end = begin + $scope.numPerPage;

            index = $scope.filtered.indexOf(value);
            return (begin <= index && index < end);
        };

        $scope.$watch('search', function (term) {
            if ($scope.objects.length > 0) {
                if (term.length > 0) {
                    $scope.tempArray = [];
                    angular.forEach($scope.objects, function (row) {
                        var t1 = (angular.lowercase((row.VaccinName).toString()).indexOf(angular.lowercase($scope.search) || '') !== -1);
                        if (t1) {

                            $scope.tempArray.push(row);
                                    }
                    });
                    $scope.filtered = $scope.tempArray;


                } else {
                    $scope.filtered = $scope.objects;
                }

                $scope.noOfPages = Math.ceil($scope.filtered.length / $scope.numPerPage);
                $scope.currentPage = 1;
                $scope.paginate();
            }
        });

    }
]);

// this controller call the api method and display the record of selected employee
// in edit.html and provide an option for create and modify the employee and save the employee record
childControllers.controller("EditAddChildController", ['$scope', '$filter', '$http', '$routeParams', '$location',
    function ($scope, $filter, $http, $routeParams, $location) {
        $scope.DOB = $filter('date')(new Date(), 'yyyy/MM/dd');
      
        function errorHandle(data, exception) {
            var errorMessage = "";
            if (data.status === 0) {
                errorMessage = 'Not connect.\n Verify Network.';
            } else if (data.status == 404) {
                errorMessage = 'Requested page not found. [404]';
            } else if (data.status == 500) {
                errorMessage = 'Internal Server Error [500].';
            } else if (exception === 'parsererror') {
                errorMessage = 'Requested JSON parse failed.';
            } else if (exception === 'timeout') {
                errorMessage = 'Time out error.';
            } else if (exception === 'abort') {
                errorMessage = 'Ajax request aborted.';
            } else if (exception == 403) {
                errorMessage = "UnAuthorized Access!!!";

            }
            else {
                errorMessage = 'Uncaught Error.\n' + jqXHR.responseText;
            }
            $scope.error = "An error has occured while Managing user!  " + errorMessage + " " + exception;
        }
        $scope.isDate = false;
        $scope.ID = 0;
        $scope.save = function () {

            var obj = {
                Name: $scope.ChildName,
                BirthCertificateID: $scope.BirthCertificateID,
                BirthDate: $scope.DOB,
                FatherName: $scope.FatherName,
                MotherName: $scope.MotherName
            };
            if ($scope.ID == 0) {

                $http.post('/api/Child/', obj).success(function (data) {
                    location.href = "/Child/Index#/dashboard";
                }).error(function (data, xhr) {
                    $scope.error = "An error has occured while Managing user! " + data.status + " " + xhr;
                });
            }
            else {
                $http.put('/api/Child/', obj).success(function (data) {
                    location.href = "/Child/Index#/dashboard";
                }).error(function (data, xhr) {
                    $scope.error = "An Error has occured while Managing user! " + data.status + " " + xhr;
                });
            }
        }

        if ($routeParams.id) {
            $scope.form = {};
            $http.get('/api/Child/' + $routeParams.id).success(function (data) {
                $scope.user = data;
                $scope.ID = data.Id;
                $scope.FatherName = data.FatherName;
                $scope.MotherName = data.MotherName;
                $scope.PhoneNumber = data.PhoneNumber;
                $scope.DOB = moment(data.BirthDate).format('YYYY/MM/DD');
                $scope.CurrentAddress = data.CurrentAddress;
                $scope.PermanentAddress = data.PermanentAddress;
                $scope.BirthCertificateID = data.BirthCertificateID;
                if ($scope.ID) {
                    $scope.title = "Edit Child info";
                    $scope.message = "Edit Child info";
                } else {
                    $scope.title = "Add Child info";
                    $scope.message = "Add Child info";
                }
            }).error(function (data, exception) {
                errorHandle(data, exception);
            });

        }
        else {
            $scope.title = "Add Child info";
            $scope.message = "Add Child info";
        }
    }
]);


childControllers.controller("UpdateTikaStatus", [
    '$scope', '$resource', 'filterFilter', '$routeParams',
    function ($scope, $resource, filterFilter, $routeParams) {
        $scope.isThingsCollapsed = false;

        var obj = {
            TikaId: $routeParams.tid,
            ChildId: $routeParams.cid
        }

        var IssusSave = $resource('/api/ChildVaccin/');
        $scope.objectSave = IssusSave.save(obj);
        //window.location.href = "/Child/ViewChild";
        window.location.href = "/Child/ViewChild#/ViewChild/" + $routeParams.cid;

    }]);


