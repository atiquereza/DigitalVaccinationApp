var UserControllers = angular.module("UserControllers", ['ui.bootstrap', 'ng-bs3-datepicker']);


// this controller call the api method and display the list of employees
// in list.html
UserControllers.controller("UserShowController", ['$scope', '$http',
    function ($scope, $http) {
        $scope.isThingsCollapsed = false;
        $http.get('/api/User').success(function (data) {
            $scope.UserInfo = data;
            $scope.ChildsList = data.Childs;

        });

    }]
);

// this controller call the api method and display the record of selected employee
// in delete.html and provide an option for delete
UserControllers.controller("DeleteUserController", ['$scope', '$http', '$routeParams', '$location',
        function ($scope, $http, $routeParams, $location) {
            $scope.user = {};
           // $scope.id = $routeParams.id;
            $http.get('/api/User/' + $routeParams.id).success(function (data) {
                $scope.user = data;
                $scope.Id = data.Id;
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
            });
         
            $scope.ValidUser = function () {

                if ($scope.Id) { // your question said "more than one element"
                    $scope.title = "Delete User";
                    return false;
                  
                }
                else {
                    $scope.title = "Invalid User";
                    return true;
                }
            };

            $scope.delete = function () {
                $http.delete('/api/User/' + $scope.Id).success(function (data) {
                    var url = '@Url.Action("Index", "Home")';
                   // $location.url('/User/Index');
                    location.href = "/User/Index";
                   // location.href = "/Center/Index";
                }).error(function (data) {
                    $scope.error = "An error has occured while deleting User! " + data;
                });
            };
        }
]);

// this controller call the api method and display the record of selected employee
// in edit.html and provide an option for create and modify the employee and save the employee record
UserControllers.controller("EditUserController", ['$scope', '$filter', '$http', '$routeParams', '$location',
    function ($scope, $filter, $http, $routeParams, $location) {
        $scope.MyDate1 = $filter('date')(new Date(), 'yyyy/MM/dd');
        $scope.DatePublish = function () {
            console.log($scope.MyDate1);
        };

        function errorHandle(data,exception) {
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
                BirthCertificateID: $scope.BirthCertificateID,
                BirthDate: $scope.MyDate1,
                CurrentAddress: $scope.CurrentAddress,
                FatherName: $scope.FatherName,
                FullName: $scope.FullName,
                Id: $scope.ID,
                MotherName: $scope.MotherName,
                PermanentAddress: $scope.PermanentAddress,
                PhoneNumber: $scope.PhoneNumber,
                UserId: $scope.UserId,
                UserName: $scope.UserName
            };
            if ($scope.ID == 0) {

                $http.post('/api/User/', obj).success(function (data) {
                    location.href = "/User/Index";
                }).error(function (data,xhr) {
                    $scope.error = "An error has occured while Managing user! " + data.status+ " "+xhr;
                });
            }
            else {
                $http.put('/api/User/', obj).success(function (data) {
                    location.href = "/User/Index";
                }).error(function (data, xhr) {
                    $scope.error = "An Error has occured while Managing user! " + data.status +" "+xhr;
                });
            }
        }

        if ($routeParams.id) {
            $scope.form = {};

            $http.get('/api/User/' + $routeParams.id).success(function(data) {
                $scope.user = data;
                $scope.ID = data.Id;
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

                if ($scope.ID) {
                    $scope.title = "Edit User";
                } else {
                    $scope.title = "Create New User";
                    console.log($scope.ID);
                    //location.href = "/User/Index";
                }
            }).error(function (data, exception) {
                errorHandle(data, exception);
            });
        
        }
        else {
            $scope.title = "Create New User";
        }
    }
]);
